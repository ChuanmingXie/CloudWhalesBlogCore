/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.IServices
*项目描述:
*类 名 称:IWeather
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/27 18:30:56
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using SwaggerWithMiniProfiler.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.IServices
{
    public interface IWeather
    {
        IEnumerable<Weather> GetWeathers();

    }
}
