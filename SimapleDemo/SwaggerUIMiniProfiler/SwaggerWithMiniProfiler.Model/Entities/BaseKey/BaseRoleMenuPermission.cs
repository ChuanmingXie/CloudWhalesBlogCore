/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities.BaseKey
*项目描述:
*类 名 称:BaseRoleMenuPermission
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 18:48:24
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Model.Entities.BaseKey
{
    public class BaseRoleMenuPermission<Tkey>:BaseEntity<Tkey> where Tkey:IEquatable<Tkey>
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
