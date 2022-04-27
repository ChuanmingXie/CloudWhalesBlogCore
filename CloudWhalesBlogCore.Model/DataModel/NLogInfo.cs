/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Model.DataModel
*项目描述:
*类 名 称:NLogInfo
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/4/25 17:22:40
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Model.DataModel
{
    public class NLogInfo : BaseEntity
    {
        [Display(Name = "记录时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:ss:mm:fffff}", ApplyFormatInEditMode =true)]
        public DateTime Date { get; set; }

        [Display(Name ="日志源")]
        [StringLength(100)]
        public string Origin { get; set; }

        [Display(Name = "等级")]
        [StringLength(50)]
        public string Level { get; set; }

        [Display(Name = "消息")]
        [StringLength(int.MaxValue)]
        public string Message { get; set; }

        [Display(Name = "详情")]
        [StringLength(int.MaxValue)]
        public string Detail { get; set; }
    }
}
