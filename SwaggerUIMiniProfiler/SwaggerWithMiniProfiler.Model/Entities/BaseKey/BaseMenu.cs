/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities.BaseKey
*项目描述:
*类 名 称:BaseMenu
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 18:21:07
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;
using System;

namespace SwaggerWithMiniProfiler.Model.Entities.BaseKey
{
    public class BaseMenu<Tkey> :BaseEntity<Tkey> where Tkey:IEquatable<Tkey>
    {
        /// <summary>
        /// 父ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public Tkey ParentId { get; set; }
    }
}
