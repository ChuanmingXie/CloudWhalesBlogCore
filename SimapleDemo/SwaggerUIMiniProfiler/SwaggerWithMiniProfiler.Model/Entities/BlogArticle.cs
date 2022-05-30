/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities
*项目描述:
*类 名 称:BlogArticle
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 16:45:30
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
    /// 博客文章
    /// </summary>
    public class BlogArticle:BaseEntity<int>
    {
        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(Length = 128, IsNullable = true)]
        public string Submitter { get; set; }

        /// <summary>
        /// 标题blog
        /// </summary>
        [SugarColumn(Length = 256, IsNullable = true)]
        public string Title { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        [SugarColumn(Length = 256, IsNullable = true)]
        public string Category { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [SugarColumn(Length = 20000, IsNullable = true)]
        public string Content { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public int Traffic { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentNum { get; set; }

        /// <summary> 
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = 1024, IsNullable = true)]
        public string Remark { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool? IsDeleted { get; set; }
    }
}
