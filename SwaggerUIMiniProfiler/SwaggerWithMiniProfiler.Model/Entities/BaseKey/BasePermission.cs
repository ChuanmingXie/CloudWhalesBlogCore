/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities.BaseKey
*项目描述:
*类 名 称:BasePermission
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 18:35:56
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;
using System;
using System.Collections.Generic;

namespace SwaggerWithMiniProfiler.Model.Entities.BaseKey
{
    public class BasePermission<Tkey> : BaseEntity<Tkey> where Tkey : IEquatable<Tkey>
    {
        /// <summary>
        /// 上一级菜单（0表示上一级无菜单）
        /// </summary>
        public Tkey Pid { get; set; }

        /// <summary>
        /// 接口api
        /// </summary>
        public Tkey Mid { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<Tkey> PidArr { get; set; }
    }
}
