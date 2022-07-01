/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities
*项目描述:
*类 名 称:WeChatPublish
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 19:00:19
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
    [SugarTable("WeChatPublish")]
    public partial class WeChatPublish
    {
        /// <summary>
        /// 推送ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsNullable = false)]
        public string id { get; set; }
        /// <summary>
        /// 来自谁
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string PushLogFrom { get; set; }

        /// <summary>
        /// 推送IP
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string PushLogIP { get; set; }

        /// <summary>
        /// 推送客户
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string PushLogCompanyID { get; set; }

        /// <summary>
        /// 推送用户
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string PushLogToUserID { get; set; }

        /// <summary>
        /// 推送模板ID
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string PushLogTemplateID { get; set; }

        /// <summary>
        /// 推送内容
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string PushLogContent { get; set; }

        /// <summary>
        /// 推送时间
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public DateTime? PushLogTime { get; set; }

        /// <summary>
        /// 推送状态(Y/N)
        /// </summary>
        [SugarColumn(Length = 1, IsNullable = false)]
        public string PushLogStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = false)]
        public string PushLogRemark { get; set; }

        /// <summary>
        /// 推送OpenID
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false)]
        public string PushLogOpenid { get; set; }

        /// <summary>
        /// 推送微信公众号
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false)]
        public string PushLogPublicAccount { get; set; }
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
