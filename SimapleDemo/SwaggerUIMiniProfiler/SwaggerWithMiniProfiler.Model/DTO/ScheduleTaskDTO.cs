/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Dto
*项目描述:
*类 名 称:ScheduleTaskDTO
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/6/6 9:52:41
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

namespace SwaggerWithMiniProfiler.Model.DTO
{
    /// <summary>
    /// 调度任务触发器实体
    /// </summary>
    public class ScheduleTaskDTO
    {

        /// <summary>
        /// 任务ID
        /// </summary>
        public string JobId { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 任务分组
        /// </summary>
        public string JobGroup { get; set; }
        /// <summary>
        /// 触发器ID
        /// </summary>
        public string TriggerId { get; set; }
        /// <summary>
        /// 触发器名称
        /// </summary>
        public string TriggerName { get; set; }
        /// <summary>
        /// 触发器分组
        /// </summary>
        public string TriggerGroup { get; set; }
        /// <summary>
        /// 触发器状态
        /// </summary>
        public string TriggerStatus { get; set; }
    }
}
