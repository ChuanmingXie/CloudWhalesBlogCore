/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities
*项目描述:
*类 名 称:BaseEntity
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 16:04:26
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SqlSugar;
using System;

namespace SwaggerWithMiniProfiler.Model.Entities.BaseKey
{
    public class BaseEntity<Tkey> where Tkey:IEquatable<Tkey>
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public Tkey Id { get; set; }
    }


    public class BaseEntiryOld
    {

        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public int Id { get; set; }
    }
}
