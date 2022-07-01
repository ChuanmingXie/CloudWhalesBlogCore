/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Extensions.ExtensionServices
*项目描述:
*类 名 称:SwaggerSetup
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/25 23:51:36
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Shared.Common;
using CloudWhalesBlogCore.Shared.NLogger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using static CloudWhalesBlogCore.Extensions.ExtensionServices.CustomApiVersion;

namespace CloudWhalesBlogCore.Extensions.ExtensionServices
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentException(nameof(services));
            var basePath = AppContext.BaseDirectory;
            var ApiName = AppSettings.app(new string[] { "Startup", "ApiName" });
            services.AddSwaggerGen(c =>
            {
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new OpenApiInfo
                    {
                        Version = version,
                        Title = $"{ApiName}接口文档——{RuntimeInformation.FrameworkDescription}",
                        Description = $"{ApiName}HTTP API" + version,
                        Contact = new OpenApiContact
                        {
                            Name = ApiName,
                            Email = "chuanmingxie@outlook.com",
                            Url = new Uri("http://101.132.152.252:8084")
                        },
                        License = new OpenApiLicense { Name = ApiName + " 官方文档", Url = new Uri("http://101.132.152.252:8084/.doc/") }
                    });
                    c.OrderActionsBy(o => o.RelativePath);
                });

                try
                {
                    var xmlPath = Path.Combine(basePath, "CloudWhales.API.xml");
                    c.IncludeXmlComments(xmlPath, true);
                    //此处是同Model层的xml文件名
                    var xmlModelPath = Path.Combine(basePath, "CloudWhales.Model.xml");
                    c.IncludeXmlComments(xmlModelPath);
                }
                catch (Exception ex)
                {
                    NLogHelper._.Error("CloudWhales.API.xml和CloudWhales.Model.xml丢失,请检查拷贝.\n" + ex.Message);
                }
            });
        }
    }


    /// <summary>
    /// 自定义版本
    /// </summary>
    public class CustomApiVersion
    {
        /// <summary>
        /// Api接口版本 自定义
        /// </summary>
        public enum ApiVersions
        {
            /// <summary>
            /// V1 版本
            /// </summary>
            V1 = 1,
            /// <summary>
            /// V2 版本
            /// </summary>
            V2 = 2,
        }
    }
}
