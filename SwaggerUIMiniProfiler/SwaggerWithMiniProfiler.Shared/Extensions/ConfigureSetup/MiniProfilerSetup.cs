/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared.Extensions.ConfigureSetup
*项目描述:
*类 名 称:MiniProfilerSetup
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/28 11:48:19
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Profiling.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Shared.Extensions.ConfigureSetup
{
    public static class MiniProfilerSetup
    {
        public static void AddMiniProfilerSetup(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            services.AddMiniProfiler(options =>
            {
                options.RouteBasePath = "/profiler";
                //数据缓存时间
                (options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(60);
                //sql格式化设置
                options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();
                //跟踪连接打开关闭
                options.TrackConnectionOpenClose = true;
                //界面主题颜色方案;默认浅色
                options.ColorScheme = StackExchange.Profiling.ColorScheme.Dark;
                //.net core 3.0以上：对MVC过滤器进行分析
                options.EnableMvcFilterProfiling = true;
                //对视图进行分析
                options.EnableMvcViewProfiling = true;

                //控制访问页面授权，默认所有人都能访问
                //options.ResultsAuthorize;
                //要控制分析哪些请求，默认说有请求都分析
                //options.ShouldProfile;

                //内部异常处理
                //options.OnInternalError = e => MyExceptionLogger(e);
            }).AddEntityFramework();
        }
    }
}
