/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.ViewModel
*项目描述:
*类 名 称:TableModel
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/27 10:30:17
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

namespace SwaggerWithMiniProfiler.Model.ViewModel
{
    public class ModelTable<T>
    {
        /// <summary>
        /// 表格数据，支持分页
        /// </summary>
        public int Code { get; set; }
        public string Msg { get; set; }
        public int Count { get; set; }

        public List<T> Data { get; set; }

    }
}
