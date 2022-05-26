using CloudWhalesBlogCore.Extensions.ExtensionConfigure;
using CloudWhalesBlogCore.Extensions.ExtensionServices;
using CloudWhalesBlogCore.Shared.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace CloudWhalesBlogCore.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Env { get; }

        private IServiceCollection _services;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new AppSettings(Configuration));

            services.AddMiniProfilerSetup();
            services.AddSwaggerSetup();

            services.AddHttpContextAccessor();
            //services.AddHttpContextSetup();

            services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true)
                    .Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);

            services.AddControllers();

            _services = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // 在非开发环境中，使用HTTP严格安全传输(or HSTS) 对于保护web安全是非常重要的。
                // 强制实施 HTTPS 在 ASP.NET Core，配合 app.UseHttpsRedirection
                //app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseSwagger();

            app.UseSwaggerMiddle(
                () => GetType().GetTypeInfo().Assembly
                .GetManifestResourceStream("CloudWhalesBlogCore.WebAPI.index.html"));

            app.UseMiniProfilerMiddle();

            #region Authen
            //app.UseMiddleware<JwtTokenAuth>();//注意此授权方法已经放弃，请使用下边的官方验证方法。
            //但是如果你还想传User的全局变量，还是可以继续使用中间件
            app.UseAuthentication();
            #endregion

            #region CORS
            //跨域第二种方法，使用策略，详细策略信息在ConfigureService中
            app.UseCors("LimitRequests");//将 CORS 中间件添加到 web 应用程序管线中, 以允许跨域请求。


            //跨域第一种版本，请要ConfigureService中配置服务 services.AddCors();
            //    app.UseCors(options => options.WithOrigins("http://localhost:8021").AllowAnyHeader()
            //.AllowAnyMethod()); 
            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
