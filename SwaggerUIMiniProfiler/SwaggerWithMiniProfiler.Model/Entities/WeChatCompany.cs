/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities
*项目描述:
*类 名 称:WeChatCompany
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 18:55:58
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Model.Entities
{
    [SugarTable("WeChatCompany")]
    public partial class WeChatCompany
    {
        /// <summary>
        /// 公司ID
        /// </summary> 
        [SugarColumn(IsPrimaryKey = true, Length = 100, IsNullable = false)]
        public string CompanyID { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false)]
        public string CompanyName { get; set; }
        /// <summary>
        /// 公司IP
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false)]
        public string CompanyIP { get; set; }
        /// <summary>
        /// 公司备注
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = false)]
        public string CompanyRemark { get; set; }
        /// <summary>
        /// api地址
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = false)]
        public string CompanyAPI { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 创建者id
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? CreateId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改者id
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? ModifyId { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string ModifyBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? ModifyTime { get; set; }
    }
}
