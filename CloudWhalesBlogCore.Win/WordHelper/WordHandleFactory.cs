/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Win.WordHelper
*项目描述:
*类 名 称:WordHandleFactory
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 18:37:10
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Services.OfficeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Win.WordHelper
{
    public class WordHandleFactory
    {
        public static WordHandleSuper CreateWordHandle(string type,string excelPath)
        {
            WordHandleSuper wordHandle = type switch
            {
                "混合导出" => new HandleOutMixture(excelPath),
                "图表对应" => new HandleOutOnlyImage(excelPath),
                "表格导出" => new HandleOutOnlyExcel(excelPath),
                "表对应图随机"=>new HandleOutImgRand(excelPath),
                _ => new HandleOutOnlyImage(excelPath),
            };
            return wordHandle;
        }
    }
}
