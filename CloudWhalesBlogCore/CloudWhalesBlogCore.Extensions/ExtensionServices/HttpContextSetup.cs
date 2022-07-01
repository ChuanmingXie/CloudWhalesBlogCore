/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Extensions.ExtensionServices
*项目描述:
*类 名 称:HttpContextSetup
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/26 16:23:15
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Shared.HttpContextUser;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CloudWhalesBlogCore.Extensions.ExtensionServices
{
    public static class HttpContextSetup
    {
        public static void AddHttpContextSetup(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
