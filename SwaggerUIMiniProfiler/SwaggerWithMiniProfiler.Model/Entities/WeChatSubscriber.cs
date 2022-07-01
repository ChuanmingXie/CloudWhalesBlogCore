/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities
*项目描述:
*类 名 称:WeChatSubscriber
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 19:09:52
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

    [SugarTable("WeChatSubscriber")]
    public partial class WeChatSubscriber
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public string id { get; set; }
        /// <summary>
        /// 来自哪个公众号
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false, IndexGroupNameList = new string[] { "index" })]
        public string SubFromPublicAccount { get; set; }

        /// <summary>
        /// 绑定公司id
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false, IndexGroupNameList = new string[] { "index" })]
        public string CompanyID { get; set; }

        /// <summary>
        /// 绑定员工id
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false, IndexGroupNameList = new string[] { "index" })]
        public string SubJobID { get; set; }

        /// <summary>
        /// 绑定微信id
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false)]
        public string SubUserOpenID { get; set; }

        /// <summary>
        /// 绑定微信联合id
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string SubUserUnionID { get; set; }

        /// <summary>
        /// 绑定时间
        /// </summary>
        public DateTime SubUserRegTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? SubUserRefTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string SubUserRemark { get; set; }

        /// <summary>
        /// 是否已解绑
        /// </summary>
        public bool IsUnBind { get; set; }

        /// <summary>
        /// 上次绑定微信id
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string LastSubUserOpenID { get; set; }
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
