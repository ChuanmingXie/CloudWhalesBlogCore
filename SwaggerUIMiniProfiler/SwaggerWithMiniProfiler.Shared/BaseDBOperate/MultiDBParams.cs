/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared
*项目描述:
*类 名 称:DataBaseType
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/31 18:38:02
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Shared.BaseDBOperate
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum BaseDBType
    {
        MySQL = 0,
        SQLServer = 1,
        SQLite = 2,
        Oracle = 3,
        PostgreSQL = 4,
        DM = 5,
        Kdbndp = 6
    }

    /// <summary>
    /// 多数据库参数
    /// </summary>
    public class MultiDBParams
    {
        /// <summary>
        /// 数据库启用开关
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 数据库连接ID
        /// </summary>
        public string ConnectionID { get; set; }

        /// <summary>
        /// 从库执行级别
        /// </summary>
        public int HitRate { get; set; }

        /// <summary>
        /// 数据库连接串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public BaseDBType DbType { get; set; }
    }
}
