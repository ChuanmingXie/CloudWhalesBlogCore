/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities
*项目描述:
*类 名 称:ScheduleTask
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/6/6 9:49:30
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;
using SwaggerWithMiniProfiler.Model.DTO;
using System;
using System.Collections.Generic;

namespace SwaggerWithMiniProfiler.Model.Entities
{
    /// <summary>
    /// 计划任务表
    /// </summary>
    public class ScheduleTask:BaseKey.BaseEntity<int>
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string Name { get; set; }
        /// <summary>
        /// 任务分组
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string JobGroup { get; set; }
        /// <summary>
        /// 任务运行时间表达式
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string Cron { get; set; }
        /// <summary>
        /// 任务所在DLL对应的程序集名称
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string AssemblyName { get; set; }
        /// <summary>
        /// 任务所在类
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string ClassName { get; set; }
        /// <summary>
        /// 任务描述
        /// </summary>
        [SugarColumn(Length = 1000, IsNullable = true)]
        public string Remark { get; set; }
        /// <summary>
        /// 执行次数
        /// </summary>
        public int RunTimes { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 触发器类型（0、simple 1、cron）
        /// </summary>
        public int TriggerType { get; set; }
        /// <summary>
        /// 执行间隔时间, 秒为单位
        /// </summary>
        public int IntervalSecond { get; set; }
        /// <summary>
        /// 循环执行次数
        /// </summary>
        public int CycleRunTimes { get; set; }
        /// <summary>
        /// 是否启动
        /// </summary>
        public bool IsStart { get; set; } = false;
        /// <summary>
        /// 执行传参
        /// </summary>
        public string JobParams { get; set; }


        [SugarColumn(IsNullable = true)]
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 任务内存中的状态
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<ScheduleTaskDTO> Triggers { get; set; }
    }
}
