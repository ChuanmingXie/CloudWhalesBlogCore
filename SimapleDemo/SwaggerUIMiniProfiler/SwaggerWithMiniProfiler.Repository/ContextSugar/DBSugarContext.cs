/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Repository.ContextSugar
*项目描述:
*类 名 称:DBSugarContext
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/6/1 23:15:06
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;
using SwaggerWithMiniProfiler.Shared.BaseDBOperate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Repository.ContextSugar
{
    public class DBSugarContext
    {
        private static MultiDBParams DBParams => GetMainDBConnection();

        private static string _connectionString = DBParams.ConnectionString;

        public static string _connectionID = DBParams.ConnectionID;
        
        private static DbType _dbType = (DbType)DBParams.DBType;

        private SqlSugarScope _db;

        public DBSugarContext(ISqlSugarClient sqlSugarClient)
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new ArgumentNullException("数据库连接字符串不能为空!");
            _db = sqlSugarClient as SqlSugarScope;
        }

        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public static DbType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }

        public SqlSugarScope DB
        {
            get { return _db; }
            private set { _db = value; }
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <returns></returns>
        private static MultiDBParams GetMainDBConnection()
        {
            var multiConnectionDB = BaseDBConnection
                .MultiConnectionString
                .multiDBAll
                .Find(x => x.ConnectionID == BaseDBType.SQLServer.ToString());

            if (BaseDBConnection.MultiConnectionString.multiDBAll.Count > 0)
            {
                if (multiConnectionDB == null)
                {
                    multiConnectionDB = BaseDBConnection.MultiConnectionString.multiDBAll[0];
                }
            }
            else
            {
                throw new Exception("请确保appsettings.json中配置连接字符串,并设置Enabled为true");
            }
            return multiConnectionDB;
        }
    
        /// <summary>
        /// 设置初始化参数
        /// </summary>
        /// <param name="strConnectionString"></param>
        /// <param name="enumDbType"></param>
        public static void InitContext(string strConnectionString,DbType enumDbType = DbType.SqlServer)
        {
            _connectionString = strConnectionString;
            _dbType = enumDbType;
        }

        /// <summary>
        /// 根据实体类生成数据库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isBackupTable">是否备份</param>
        /// <param name="listEntity"></param>
        public void CreateTableByEntity<T>(bool isBackupTable, params T[] listEntity) where T : class, new()
        {
            Type[] ListTypes = null;
            if (listEntity != null)
            {
                ListTypes = new Type[ListTypes.Length];
                for (int i = 0; i < ListTypes.Length; i++)
                {
                    T t = listEntity[i];
                    ListTypes[i] = typeof(T);
                }
            }
            CreateTableByEntity(isBackupTable, ListTypes);
        }

        /// <summary>
        /// 根据实体类生成数据库
        /// </summary>
        /// <param name="isBackupTable">是否备份</param>
        /// <param name="listEntity"></param>
        public void CreateTableByEntity(bool isBackupTable, params Type[] listEntity)
        {
            if (isBackupTable)
                _db.CodeFirst.BackupTable().InitTables(listEntity);
            else
                _db.CodeFirst.InitTables(listEntity);
        }

        /// <summary>
        /// 创建一个连接配置
        /// </summary>
        /// <param name="isAutoCloseConnection"></param>
        /// <param name="isShardSameThread"></param>
        /// <returns></returns>
        public static ConnectionConfig GetConnectionConfig(bool isAutoCloseConnection=true,bool isShardSameThread=false)
        {
            ConnectionConfig config = new ConnectionConfig()
            {
                ConnectionString = _connectionString,
                DbType = _dbType,
                IsAutoCloseConnection = isAutoCloseConnection,
                ConfigureExternalServices = new ConfigureExternalServices()
                {

                }
            };
            return config;
        }

        /// <summary>
        /// 获取数据处理对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public SimpleClient<T> GetEnrityDB<T>() where T : class, new()
        {
            return new SimpleClient<T>(_db);
        }

        /// <summary>
        /// 获取一个自定义的数据库处理对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sugarScope"></param>
        /// <returns></returns>
        public static SimpleClient<T> GetCustomEntityDB<T> (SqlSugarScope sugarScope) where T:class, new()
        {
            return new SimpleClient<T>(sugarScope);
        }

        /// <summary>
        /// 获取一个自定义的数据库处理对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <returns></returns>
        public static SimpleClient<T> GetCustomEntityDB<T>(ConnectionConfig config)where T : class, new()
        {
            SqlSugarScope sugarScope = GetCustomDB(config);
            return GetCustomEntityDB<T>(sugarScope);
        }

        /// <summary>
        /// 获取一个自定义的DB
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private static SqlSugarScope GetCustomDB(ConnectionConfig config)
        {
            return new SqlSugarScope(config);
        }
    }
}
