/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared.Extensions
*项目描述:
*类 名 称:MemoryCacheSetup
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/28 22:23:40
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using SwaggerWithMiniProfiler.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Shared.Extensions.ConfigureSetup
{
    public static class MemoryCacheSetup
    {
        public static void AddMemoryCacheSetup(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            //services.AddScoped<ICacheProvider, MemoryCaching>();
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });
        }
    }
}
