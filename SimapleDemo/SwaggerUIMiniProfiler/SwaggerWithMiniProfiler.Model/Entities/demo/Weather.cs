using System;

namespace SwaggerWithMiniProfiler.Model.Entities
{
    /// <summary>
    /// ����ʵ��
    /// </summary>
    public class Weather
    {
        /// <summary>
        /// ʱ��
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// �¶�
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// ���ϵ�λ
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// ��Ҫ
        /// </summary>
        public string Summary { get; set; }
    }
}
