/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.ViewModel
*项目描述:
*类 名 称:ModelToken
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/28 16:29:43
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

namespace SwaggerWithMiniProfiler.Model.ViewModel
{
    /// <summary>
    /// 临牌实体
    /// </summary>
    public class ModelToken
    {
        public ModelToken()
        {
            this.uid = 0;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long uid { get;set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UName { get; set; }

       /// <summary>
       /// 手机
       /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string UNickName { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Sub { get; set; }
    }
}
