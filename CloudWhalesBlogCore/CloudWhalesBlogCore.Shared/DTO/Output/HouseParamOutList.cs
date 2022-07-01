/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Model.DTO.Output
*项目描述:
*类 名 称:DemolitionDataOutList
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/4/23 12:34:44
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System.Collections.Generic;

namespace CloudWhalesBlogCore.Shared.DTO.Output
{
    public class HouseParamOutList
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 表格数据项
        /// </summary>
        public List<HouseParamOut> HouseParams { get; set; }

        /// <summary>
        /// 拆除总面积
        /// </summary>
        public decimal? AreaAll { get; set; }
    }
}
