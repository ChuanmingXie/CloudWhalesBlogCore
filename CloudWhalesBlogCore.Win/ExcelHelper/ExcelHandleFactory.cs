/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Win.ExcelHelper
*项目描述:
*类 名 称:ExcelHandleFactory
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 18:36:41
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

namespace CloudWhalesBlogCore.Win.ExcelHelper
{
    public class ExcelHandleFactory
    {
        public static ExcelHandleSuper CreateExcelHandle(string type, string excelPath)
        {
            ExcelHandleSuper excelHandle = type switch
            {
                "混合导出" => new HandleInMixture(excelPath),
                "图表对应" => new HandleInOnlyImage(excelPath),
                "表格导出" => new HandleInOnlyExcel(excelPath),
                "表对应图随机" => new HandleInImgRand(excelPath),
                _ => new HandleInOnlyImage(excelPath),
            };
            return excelHandle;
        }

    }
}
