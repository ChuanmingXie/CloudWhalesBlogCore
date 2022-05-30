using System;

namespace SwaggerWithMiniProfiler.Model.Entities
{
    /// <summary>
    /// 天气实体
    /// </summary>
    public class Weather
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// 华氏单位
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// 概要
        /// </summary>
        public string Summary { get; set; }
    }
}
