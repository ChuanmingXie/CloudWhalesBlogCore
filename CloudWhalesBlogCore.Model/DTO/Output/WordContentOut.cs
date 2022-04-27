/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Model.DTO.Output
*项目描述:
*类 名 称:WordContent
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/4/24 10:09:25
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Model.DTO.Output
{
    public class WordContentOut
    {
        /// <summary>
        /// 页眉
        /// </summary>
        public string Headers { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 段落内容
        /// </summary>

        public string Paragraph { get; set; }

        /// <summary>
        /// 页脚
        /// </summary>

        public string Footers { get; set; }
    }
}
