/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared.DataBaseOperate
*项目描述:
*类 名 称:BaseDBConnection
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/31 18:52:23
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Shared.BaseDBOperate
{
    /// <summary>
    /// 获取数据库连接字符串
    /// </summary>
    public class BaseDBConnection
    {
        /*
         * 多数据库连接字符串的获取，默认加载appsetting.json中
         * 设置DataBaseSetting为true的第一个数据库
         * 涉及主从数据库操作
         */

        public static (List<MultiDBParams> multiDBAll, List<MultiDBParams> dbSubordinate) MultiConnectionString => MultiInitConnection();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static (List<MultiDBParams>, List<MultiDBParams>) MultiInitConnection()
        {
            List<MultiDBParams> dbConnectionList = HelperAppSettings
                .app<MultiDBParams>("DBS")
                .Where(i => i.Enabled).ToList();

            foreach (var param in dbConnectionList)
            {
                DBCustomConnection(param);
            }
            List<MultiDBParams> ListMultiDBAll = new List<MultiDBParams>();
            List<MultiDBParams> ListDBSubordinate = new List<MultiDBParams>();

            var MultiDBEnabled = HelperAppSettings.app(new string[] { "DataBaseSetting", "MultiDBEnabled" }).ObjectToBool();
            var CQRSEnabled = HelperAppSettings.app(new string[] { "DataBaseSetting", "CQRSEnabled" }).ObjectToBool();
            var MainDB = HelperAppSettings.app(new string[] { "DataBaseSetting", "MainDB" }).ObjectToString();

            //只对一个数据操作,且无读写分离
            if (!CQRSEnabled && !MultiDBEnabled)
            {
                if (dbConnectionList.Count == 1)
                    return (dbConnectionList, ListDBSubordinate);
                else
                {
                    var dbFirstItem = dbConnectionList.FirstOrDefault(d => d.ConnectionID == MainDB);
                    if (dbFirstItem == null)
                    {
                        dbFirstItem = dbConnectionList.FirstOrDefault();
                    }
                    ListMultiDBAll.Add(dbFirstItem);
                    return (ListMultiDBAll, ListDBSubordinate);
                }
            }

            //单库模式读写分离
            if (CQRSEnabled && !MultiDBEnabled)
            {
                if (dbConnectionList.Count > 1)
                {
                    ListDBSubordinate = dbConnectionList.Where(d => d.ConnectionID != MainDB).ToList();
                }
            }
            return (dbConnectionList, ListDBSubordinate);
        }

        /// <summary>
        /// 定制数据库字符串,保证安全性,优先从本地txt文件获取,
        /// 如本地不存在则从appsettings.json中获取
        /// </summary>
        /// <param name="dbPatam"></param>
        private static void DBCustomConnection(MultiDBParams dbPatam)
        {
            dbPatam.ConnectionString = dbPatam.DBType switch
            {
                BaseDBType.SQLite => $"DataSoucre=" + Path.Combine(Environment.CurrentDirectory, dbPatam.ConnectionString),
                BaseDBType.MySQL => HandlerCustomConnection(@"C:\pro-file\db.connection.mysql.txt", dbPatam.ConnectionString),
                BaseDBType.Oracle => HandlerCustomConnection(@"C:\pro-file\db.connection.oracle.txt", dbPatam.ConnectionString),
                BaseDBType.SQLServer => HandlerCustomConnection(@"C:\pro-file\db.connection.sqlserver.txt", dbPatam.ConnectionString),
                _ => dbPatam.ConnectionString,
            };
        }

        /// <summary>
        /// 处理定制的连接字符串
        /// </summary>
        /// <param name="connParams"></param>
        /// <returns></returns>
        private static string HandlerCustomConnection(params string[] connParams)
        {
            foreach (var item in connParams)
            {
                try
                {
                    if (File.Exists(item))
                    {
                        return File.ReadAllText(item).Trim();
                    }
                }
                catch (Exception) { }
            }
            return connParams[connParams.Length - 1];
        }
    }
}
