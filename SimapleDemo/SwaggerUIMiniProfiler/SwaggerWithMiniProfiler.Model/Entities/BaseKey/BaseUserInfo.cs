/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.Entities.BaseKey
*项目描述:
*类 名 称:BaseUserInfo
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/30 16:21:14
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
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class BaseUserInfo<Tkey> where Tkey:IEquatable<Tkey>
    {
        /// <summary>
        /// 泛型主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public Tkey UId { get; set; }

        [SugarColumn(IsIgnore =true)]
        public List<Tkey> RIds { get; set; }
    }
}
