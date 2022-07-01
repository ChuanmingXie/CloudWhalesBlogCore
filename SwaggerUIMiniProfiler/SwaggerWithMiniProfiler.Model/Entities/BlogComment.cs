/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities
*项目描述:
*类 名 称:BlogComment
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 16:47:08
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;
using SwaggerWithMiniProfiler.Model.Entities.BaseKey;
using System;

namespace SwaggerWithMiniProfiler.Model.Entities
{
    /// <summary>
    /// 博客评论
    /// </summary>
    public class BlogComment:BaseEntity<int>
    {

        /// <summary>
        /// 博客ID
        /// </summary>
        public int? BlogID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string UserName { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [SugarColumn(Length = 20, IsNullable = true)]
        public string Phone { get; set; }
        /// <summary>
        /// qq
        /// </summary>
        [SugarColumn(Length =200, IsNullable = true)]
        public string QQ { get; set; }

        /// <summary>
        /// 留言内容
        /// </summary>
        [SugarColumn(Length = 2000, IsNullable = true)]
        public string Message { get; set; }
        /// <summary>
        /// ip地址
        /// </summary>
        [SugarColumn(Length = 2000, IsNullable = true)]
        public string IP { get; set; }

        /// <summary>
        /// 是否显示在前台,0否1是
        /// </summary>
        public bool IsShow { get; set; }

        [SugarColumn(IsIgnore = true)]
        public BlogArticle Blog { get; set; }
    }
}
