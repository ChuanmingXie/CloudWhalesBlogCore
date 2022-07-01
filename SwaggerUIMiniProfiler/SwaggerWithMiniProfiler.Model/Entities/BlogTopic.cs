﻿/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities
*项目描述:
*类 名 称:BlogTopic
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 16:47:24
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;
using SwaggerWithMiniProfiler.Model.Entities.BaseKey;
using System;
using System.Collections.Generic;

namespace SwaggerWithMiniProfiler.Model.Entities
{
    /// <summary>
    /// 博客话题
    /// </summary>
    public class BlogTopic : BaseEntity<int>
    {
        public BlogTopic()
        {
            BlogTopicList = new List<BlogTopicDetial>();
            Updatetime = DateTime.Now;
        }
        /// <summary>
        /// Logo
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string Logo { get; set; }

        /// <summary>
        /// 话题名称
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string Name { get; set; }

        /// <summary>
        /// 话题细节
        /// </summary>
        [SugarColumn(Length = 400, IsNullable = true)]
        public string Detail { get; set; }

        /// <summary>
        /// 话题作者
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string Author { get; set; }

        /// <summary>
        /// 子话题细节
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string SectendDetail { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; } = false;

        /// <summary>
        /// 读者
        /// </summary>
        public int Read { get; set; }

        /// <summary>
        /// 评论
        /// </summary>
        public int Commend { get; set; }

        /// <summary>
        /// 点赞
        /// </summary>
        public int Good { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Createtime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Updatetime { get; set; }

        /// <summary>
        /// 导航属性-博客话题
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public virtual ICollection<BlogTopicDetial> BlogTopicList { get; set; }
    }
}
