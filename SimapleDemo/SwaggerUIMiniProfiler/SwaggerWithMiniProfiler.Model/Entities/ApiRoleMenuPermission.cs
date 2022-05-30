/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities
*项目描述:
*类 名 称:ApiRoleMenuPermission
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 18:46:52
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
    /// 按钮跟权限关联表
    /// </summary>
    public class ApiRoleMenuPermission:BaseRoleMenuPermission<int>
    {
        public ApiRoleMenuPermission()
        {

        }

        /// <summary>
        ///获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// 创建ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? CreateId { get; set; }
        
        /// <summary>
        /// 创建者
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string CreateBy { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        
        /// <summary>
        /// 修改ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? ModifyId { get; set; }
        
        /// <summary>
        /// 修改者
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string ModifyBy { get; set; }
        
        /// <summary>
        /// 修改时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? ModifyTime { get; set; } = DateTime.Now;

        // 下边三个实体参数，只是做传参作用，所以忽略下
        [SugarColumn(IsIgnore = true)]
        public SystemRoleType Role { get; set; }
        
        [SugarColumn(IsIgnore = true)]
        public ApiMenuModules Module { get; set; }
        
        [SugarColumn(IsIgnore = true)]
        public ApiPermissionModules Permission { get; set; }
    }
}
