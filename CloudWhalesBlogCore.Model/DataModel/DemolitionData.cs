/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Model.DataModel
*项目描述:
*类 名 称:DemolitionData
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/4/23 12:24:44
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudWhalesBlogCore.Model.DataModel
{
    public class DemolitionData: BaseEntity
    {
        /// <summary>
        /// 大楼编号
        /// </summary>
        /// 
        [Required]
        [Display(Name = "大楼编号")]
        [StringLength(60, MinimumLength = 1)]
        public string BuildingNum { get; set; }

        /// <summary>
        /// 户号
        /// </summary>
        [Display(Name = "户号")]
        [StringLength(60, MinimumLength = 1)]
        public string RoomNum { get; set; }

        /// <summary>
        /// 主卧拆除面积
        /// </summary>
        [Display(Name = "主卧拆除面积")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? MasterRoom { get; set; }

        /// <summary>
        /// 次卧拆除面积
        /// </summary>
        [Display(Name = "主卧拆除面积")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? SecondRoom { get; set; }

        /// <summary>
        /// 书房拆除面积
        /// </summary>
        [Display(Name = "主卧拆除面积")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? StudyRoom { get; set; }

        /// <summary>
        /// 户内拆除面积小计
        /// </summary>
        [Display(Name = "主卧拆除面积")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? DemolitionArea { get; set; }

        /// <summary>
        /// 现场拆除状态图片列表(以此为主，次，书)
        /// </summary>
        [Display(Name = "拆除状态图片")]
        [StringLength(1024, MinimumLength = 1)]
        public string StatusPhotosJoin { get; set; }
        //List<string> statusPhotos = new();
        //public List<string> StatusPhotos
        //{
        //    get
        //    {
        //        return statusPhotos;
        //    }
        //    set
        //    {
        //        statusPhotos = value;
        //    }
        //}

        [Display(Name = "备注")]
        [StringLength(512, MinimumLength = 1)]
        public string ReviewerRemarks { get; set; }

    }
}
