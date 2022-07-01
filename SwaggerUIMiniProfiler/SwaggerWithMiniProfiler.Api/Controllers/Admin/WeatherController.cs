using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Profiling;
using SwaggerWithMiniProfiler.BLL.Admin;
using SwaggerWithMiniProfiler.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SwaggerWithMiniProfiler.Api.Admin.Controllers
{
    /// <summary>
    /// 天气模块
    /// </summary>
    [Produces("application/json")]
    [Route("api/Admin/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<WeatherController> _logger;
        private readonly WeatherBLL weatherBLL = new WeatherBLL();

        public WeatherController(ILogger<WeatherController> logger, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _accessor = accessor;
        }

        /// <summary>
        /// 获取天气
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetWeatherInfo")]
        public IEnumerable<Weather> Get()
        {
            return weatherBLL.GetWeathers();
        }

        /// <summary>
        /// 获取MiniProfiler头文件
        /// </summary>
        [HttpGet]
        [Produces("text/plain")]
        [Route("GetHeaderSript")]
        public IActionResult SwaggerUIHeader()
        {
            var result = MiniProfiler.Current.RenderIncludes(_accessor.HttpContext);
            return Ok(result.Value);
        }
    }
}
