/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.DTO
*项目描述:
*类 名 称:BlogArticleDTO
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/6/12 22:12:48
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;

namespace SwaggerWithMiniProfiler.Model.DTO
{
    /// <summary>
    /// 博客文章信息展示类
    /// </summary>
    public class BlogArticleDTO
    {

        /// <summary>
        /// BlogID
        /// </summary>
        public int BlogID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string BlogSubmitter { get; set; }

        /// <summary>
        /// 博客标题
        /// </summary>
        public string BlogTitle { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Digest { get; set; }

        /// <summary>
        /// 上一篇
        /// </summary>
        public string Previous { get; set; }

        /// <summary>
        /// 上一篇id
        /// </summary>
        public int PreviousID { get; set; }

        /// <summary>
        /// 下一篇
        /// </summary>
        public string Next { get; set; }

        /// <summary>
        /// 下一篇id
        /// </summary>
        public int NextID { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public string BlogCategory { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string BlogContent { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public int BlogTraffic { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int BlogCommentNum { get; set; }

        /// <summary> 
        /// 修改时间
        /// </summary>
        public DateTime BlogUpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime BlogCreateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BlogRemark { get; set; }
    }
}
