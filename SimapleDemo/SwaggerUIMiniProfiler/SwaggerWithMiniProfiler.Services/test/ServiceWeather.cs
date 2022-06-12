/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Services
*项目描述:
*类 名 称:ServiceWeather
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/27 18:37:32
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using Microsoft.AspNetCore.Http;
using StackExchange.Profiling;
using SwaggerWithMiniProfiler.IServices;
using SwaggerWithMiniProfiler.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Services
{
    public class ServiceWeather : IWeather
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public IEnumerable<Weather> GetWeathers()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Weather
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
