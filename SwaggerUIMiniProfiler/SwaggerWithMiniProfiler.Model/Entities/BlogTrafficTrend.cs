/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities
*项目描述:
*类 名 称:BlogTrafficTrend
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 16:58:39
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
    /// 博客访问量趋势
    /// </summary>
    public class BlogTrafficTrend:BaseEntity<int>
    {

        /// <summary>
        /// 用户
        /// </summary>
        [SugarColumn(Length = 128, IsNullable = true, ColumnDataType = "nvarchar")]
        public string User { get; set; }

        /// <summary>
        /// 次数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
