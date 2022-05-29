/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Model.ViewModel
*项目描述:
*类 名 称:MessageModel
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/27 10:45:13
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
    /// <summary>
    /// 消息传递通用类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModelMessage<T>
    {
        public bool Success { get; set; }
        public string Msg { get; set; }
        public List<T> Data { get; set; }
    }
}
