/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Repository.SqlSugarCore
*项目描述:
*类 名 称:BaseDBConfig
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/27 10:18:42
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

namespace SwaggerWithMiniProfiler.Repository.SqlSugarCore
{
    /// <summary>
    /// 数据库基本操作
    /// </summary>
    public class BaseDBConfig
    {
        public static string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=PayAPI;Trusted_Connection=True;MultipleActiveResultSets=true";

    }
}
