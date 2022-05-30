/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities.BaseKey
*项目描述:
*类 名 称:BaseMenuPermission
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 18:23:30
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/

using SqlSugar;
using System;

namespace SwaggerWithMiniProfiler.Model.Entities.BaseKey
{
    public class BaseMenuPermission<Tkey>:BaseEntity<Tkey> where Tkey:IEquatable<Tkey>
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public Tkey RoleId { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public Tkey ModuleId { get; set; }
        /// <summary>
        /// api ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public Tkey PermissionId { get; set; }
    }
}
