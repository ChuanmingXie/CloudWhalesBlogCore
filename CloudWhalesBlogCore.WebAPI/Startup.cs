using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace CloudWhalesBlogCore.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
                //...
            });
            services.AddControllers();
            services.AddMvc(options =>
            {
                options.ReturnHttpNotAcceptable = true;
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            });
            /*services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "CloudWhales.API", 
                    Version = "v1" 
                });
            });*/
            var basePath = AppContext.BaseDirectory;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CloudWhales.API",
                    Version = "v1",
                    Description = "API �����ĵ�",
                    TermsOfService = new Uri("http://101.132.152.252:8084"),
                    Contact = new OpenApiContact
                    {
                        Name = "CloudWhales.Core",
                        Email = "chuanmingxie@outlook.com",
                        Url = new Uri("http://101.132.152.252:8084")
                    }
                });
                try
                {
                    var xmlPath = Path.Combine(basePath, "CloudWhales.API.xml");
                    c.IncludeXmlComments(xmlPath,true);
                    var xmlModelPath = Path.Combine(basePath, "CloudWhales.Model.xml");//�������Model���xml�ļ���
                    c.IncludeXmlComments(xmlModelPath);
                }
                catch (Exception)
                {
                    throw;
                }
            });
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
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint(
                "/swagger/v1/swagger.json",
                "CloudWhales.API v1"
            ));

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
