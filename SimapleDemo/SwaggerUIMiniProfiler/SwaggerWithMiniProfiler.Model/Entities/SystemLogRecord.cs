/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities
*项目描述:
*类 名 称:SystemOperateLog
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 16:42:31
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
    /// 系统日志
    /// </summary>
    public class SystemLogRecord:BaseEntity<int>
    {

        /// <summary>
        ///获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// 区域名
        /// </summary>
        [SugarColumn(Length = 2000, IsNullable = true)]
        public string Area { get; set; }
        /// <summary>
        /// 区域控制器名
        /// </summary>
        [SugarColumn(Length = 2000, IsNullable = true)]
        public string Controller { get; set; }
        /// <summary>
        /// Action名称
        /// </summary>
        [SugarColumn(Length = 2000, IsNullable = true)]
        public string Action { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        [SugarColumn(Length = 2000, IsNullable = true)]
        public string IPAddress { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [SugarColumn(Length = 2000, IsNullable = true)]
        public string Description { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? LogTime { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        [SugarColumn(Length = 2000, IsNullable = true)]
        public string LoginName { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        [SugarColumn(IsIgnore = true)]
        public virtual SystemUserInfo User { get; set; }
    }
}
