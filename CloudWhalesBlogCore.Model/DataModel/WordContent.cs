/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Model.DataModel
*项目描述:
*类 名 称:WordFileContent
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/4/24 9:51:47
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Model.DataModel
{
    public class WordContent:BaseEntity
    {
        [Display(Name = "页眉")]
        [StringLength(512, MinimumLength = 1)]
        public string Headers { get; set; }

        [Display(Name = "题目")]
        [StringLength(512, MinimumLength = 1)]
        public string Title { get; set; }

        [Display(Name = "内容")]
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string Paragraph { get; set; }

        [Display(Name = "页脚")]
        [StringLength(512, MinimumLength = 1)]
        public string Footers { get; set; }
    }
}
