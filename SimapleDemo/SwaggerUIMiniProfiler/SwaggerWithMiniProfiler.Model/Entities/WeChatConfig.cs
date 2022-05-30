/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities
*项目描述:
*类 名 称:WeChatConfig
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 19:10:52
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
    [SugarTable("WeChatConfig")]
    public class WeChatConfig
    {

        /// <summary>
        /// 微信公众号唯一标识
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, Length = 100, IsNullable = false)]
        public string publicAccount { get; set; }

        /// <summary>
        /// 微信公众号名称
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = false)]
        public string publicNick { get; set; }

        /// <summary>
        /// 微信账号
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false)]
        public string weChatAccount { get; set; }

        /// <summary>
        /// 微信名称
        /// </summary>
        [SugarColumn(Length = 200)]
        public string weChatNick { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        [SugarColumn(Length = 100)]
        public string appid { get; set; }

        /// <summary>
        /// 应用秘钥
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false)]
        public string appsecret { get; set; }

        /// <summary>
        /// 公众号推送token
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string token { get; set; }

        /// <summary>
        /// 验证秘钥(验证消息是否真实)
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false)]
        public string interactiveToken { get; set; }

        /// <summary>
        /// 微信公众号token过期时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? tokenExpiration { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string remark { get; set; }
        
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
