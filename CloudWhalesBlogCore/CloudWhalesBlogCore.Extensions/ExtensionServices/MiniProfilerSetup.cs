/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Extensions.ExtensionServices
*项目描述:
*类 名 称:MiniProfilerSetup
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/26 15:37:36
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Shared.Common;
using CloudWhalesBlogCore.Shared.Common.ConvertHelper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Extensions.ExtensionServices
{
    public static class MiniProfilerSetup
    {
        public static void AddMiniProfilerSetup(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if(AppSettings.app(new string[] { "Startup", "MiniProfiler", "Enabled" }).ObjectToBool())
            {
                services.AddMiniProfiler(options =>
                {
                    options.RouteBasePath = "/profiler";
                }).AddEntityFramework();

                // 3.x使用MiniProfiler，必须要注册MemoryCache服务
                // services.AddMiniProfiler(options =>
                // {
                //     options.RouteBasePath = "/profiler";
                //     //(options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(10);
                //     options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.Left;
                //     options.PopupShowTimeWithChildren = true;

                //     // 可以增加权限
                //     //options.ResultsAuthorize = request => request.HttpContext.User.IsInRole("Admin");
                //     //options.UserIdProvider = request => request.HttpContext.User.Identity.Name;
                // });
            }
        }
    }
}
