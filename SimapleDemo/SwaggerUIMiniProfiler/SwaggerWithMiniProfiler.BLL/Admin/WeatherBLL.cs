/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.BLL.Admin
*项目描述:
*类 名 称:WeatherBLL
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/27 11:01:02
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using Microsoft.AspNetCore.Http;
using SwaggerWithMiniProfiler.IServices;
using SwaggerWithMiniProfiler.Model.Entities;
using SwaggerWithMiniProfiler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.BLL.Admin
{
    public class WeatherBLL
    {

        private IWeather iService = new ServiceWeather();

        public IEnumerable<Weather> GetWeathers() => iService.GetWeathers();

    }
}
