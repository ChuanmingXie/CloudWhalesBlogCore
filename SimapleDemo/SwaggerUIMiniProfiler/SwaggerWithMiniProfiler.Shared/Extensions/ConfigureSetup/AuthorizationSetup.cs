/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared.Extensions.ConfigureSetup
*项目描述:
*类 名 称:AuthorizationSetup
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/28 22:43:21
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Shared.Extensions.ConfigureSetup
{
    public static class AuthorizationSetup
    {
        public static void AddAuthorizationSetup(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Client", policy => policy.RequireClaim("Client").Build());
                options.AddPolicy("Admin", policy => policy.RequireClaim("Admin").Build());
                options.AddPolicy("SystemOrAdmin", policy => policy.RequireClaim("Admin", "System"));
                options.AddPolicy("A_S_O", policy => policy.RequireClaim("Admin", "System", "Others"));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "ColudWhales",
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ColudWhales Secret Key"))
                    };
                });

        }
    }
}
