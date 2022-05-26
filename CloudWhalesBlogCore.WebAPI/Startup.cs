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
                // �ڷǿ��������У�ʹ��HTTP�ϸ�ȫ����(or HSTS) ���ڱ���web��ȫ�Ƿǳ���Ҫ�ġ�
                // ǿ��ʵʩ HTTPS �� ASP.NET Core����� app.UseHttpsRedirection
                //app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseSwagger();

            app.UseSwaggerMiddle(
                () => GetType().GetTypeInfo().Assembly
                .GetManifestResourceStream("CloudWhalesBlogCore.WebAPI.index.html"));

            app.UseMiniProfilerMiddle();

            #region Authen
            //app.UseMiddleware<JwtTokenAuth>();//ע�����Ȩ�����Ѿ���������ʹ���±ߵĹٷ���֤������
            //��������㻹�봫User��ȫ�ֱ��������ǿ��Լ���ʹ���м��
            app.UseAuthentication();
            #endregion

            #region CORS
            //����ڶ��ַ�����ʹ�ò��ԣ���ϸ������Ϣ��ConfigureService��
            app.UseCors("LimitRequests");//�� CORS �м����ӵ� web Ӧ�ó��������, �������������


            //�����һ�ְ汾����ҪConfigureService�����÷��� services.AddCors();
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
