/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities
*项目描述:
*类 名 称:SystemPassword
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 19:17:22
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;
using System;

namespace SwaggerWithMiniProfiler.Model.Entities
{
    /// <summary>
    /// 密码库表
    /// </summary>
    [SugarTable("SystemPasswordLib", "WMBLOG_MSSQL_2")]
    public class SystemPasswordLib
    {

        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int PLID { get; set; }

        /// <summary>
        ///获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool? IsDeleted { get; set; }

        [SugarColumn(Length = 200, IsNullable = true)]
        public string plURL { get; set; }

        [SugarColumn(Length = 100, IsNullable = true)]
        public string plPWD { get; set; }

        [SugarColumn(Length = 200, IsNullable = true)]
        public string plAccountName { get; set; }

        [SugarColumn(IsNullable = true)]
        public int? plStatus { get; set; }

        [SugarColumn(IsNullable = true)]
        public int? plErrorCount { get; set; }

        [SugarColumn(Length = 200, IsNullable = true)]
        public string plHintPwd { get; set; }

        [SugarColumn(Length = 200, IsNullable = true)]
        public string plHintquestion { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? plCreateTime { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? plUpdateTime { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? plLastErrTime { get; set; }

        [SugarColumn(Length = 200, IsNullable = true)]
        public string test { get; set; }

    }
}
