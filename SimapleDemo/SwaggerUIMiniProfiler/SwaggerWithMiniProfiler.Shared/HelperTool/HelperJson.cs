/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared.HelperTool
*项目描述:
*类 名 称:HelperJson
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/6/6 10:05:27
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Shared.HelperTool
{
    public class HelperJson
    {
        public static T ParseFormByJson<T>(string jsonStr)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonStr)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}
