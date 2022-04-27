/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Model.DTO.Output
*项目描述:
*类 名 称:DemolitionDataOut
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/4/23 15:07:09
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System.Collections.Generic;

namespace CloudWhalesBlogCore.Model.DTO.Output
{
    public class DemolitionDataOut
    {
        /// <summary>
        /// 大楼编号
        /// </summary>
        public string BuildingNum { get; set; }

        /// <summary>
        /// 户号
        /// </summary>
        public string RoomNum { get; set; }

        /// <summary>
        /// 主卧拆除面积
        /// </summary>
        public decimal? MasterRoom { get; set; }

        /// <summary>
        /// 次卧拆除面积
        /// </summary>
        public decimal? SecondRoom { get; set; }
        public decimal? SecondRoom2 { get; set; }

        /// <summary>
        /// 书房拆除面积
        /// </summary>
        public decimal? StudyRoom { get; set; }

        /// <summary>
        /// 户内拆除面积小计
        /// </summary>
        public decimal? DemolitionArea { get; set; }

        /// <summary>
        /// 现场拆除状态图片列表(以此为主，次，书)
        /// </summary>
        List<string> statusPhotos = new();
        public List<string> StatusPhotos
        {
            get
            {
                return statusPhotos;
            }
            set
            {
                statusPhotos = value;
            }
        }
    }
}
