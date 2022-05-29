using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using StackExchange.Profiling.Storage;
using SwaggerWithMiniProfiler.Shared.Extensions.ConfigureSetup;
using SwaggerWithMiniProfiler.Shared.Extensions.ConfigureUse;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;

namespace SwaggerWithMiniProfiler.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMiniProfilerSetup();
            services.AddSwagerSetup();

            services.AddAuthorizationSetup();
            services.AddMemoryCacheSetup();
            services.AddHttpContextSetup();

            services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true)
                    .Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);

            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseHttpsRedirection();


            app.UseSwaggerModdle(
                () =>GetType()
                .GetTypeInfo().Assembly
                .GetManifestResourceStream("SwaggerWithMiniProfiler.Api.index.html"));
            app.UseMiniProfilerMiddle();


            app.UseRouting();
            //app.UseMiddleware<JwtAuthMiddle>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
