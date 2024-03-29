﻿/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared.Extensions.ConfigureSetup
*项目描述:
*类 名 称:AddSwaggerSetup
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/28 11:36:52
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;

namespace SwaggerWithMiniProfiler.Shared.Extensions.ConfigureSetup
{
    public static class SwaggerSetup
    {
        public static void AddSwagerSetup(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            var basePath = AppContext.BaseDirectory;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SwaggerWithMiniProfiler.Api",
                    Version = "v1",
                    Description = "接口集合",
                    TermsOfService = new Uri("http://101.132.152.252"),
                    Contact = new OpenApiContact
                    {
                        Name = "SwaggerWithMiniProfiler.Api",
                        Email = "chuanmingxie@outlook.com",
                        Url = new Uri("http://101.132.152.252:8084")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "SwaggerWithMiniProfiler.Api 官方文档",
                        Url = new Uri("http://101.132.152.252:8084/.doc/")
                    }
                });
                // 添加注释服务
                var apiXmlPath = Path.Combine(basePath, "SwaggerWithMiniProfiler.Api.xml");
                var entityXmlPath = Path.Combine(basePath, "SwaggerWithMiniProfiler.Model.xml");
                c.IncludeXmlComments(apiXmlPath, true);//控制器层注释（true表示显示控制器注释）
                c.IncludeXmlComments(entityXmlPath);

                c.OperationFilter<AddHeaderOperationFilter>("Correlation", "Correlation id for request", false);
                //// 开启加权限接口说明
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                #region Jwt使用1:全部接口使用Jwt验证 Token绑定到ConfigureServices 
                // 下面两步配置 实现 swagger 上面 “锁”
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,  // 位于Header
                    Description = "请于此处直接填写token 无需 Bearer然后再加空格的形式",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                //每个动作都加显示加锁
                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference=new OpenApiReference{
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },Array.Empty<string>()
                    }
                });
                #endregion

                #region Jwt使用2:针对具备角色权限的接口使用Jwt验证 Token绑定到ConfigureServices 

                // 在header中添加token，传递到后台(排除所有加权限的接口 使用 使用token)
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                //给api添加token令牌证书 oauth2 不能更改(不能更改)
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });

                #endregion
            });

        }
    }
}
