/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Shared.Common
*项目描述:
*类 名 称:Appsetings
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/25 22:53:31
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Shared.Common
{
    /// <summary>
    /// appsettings.json 操作类
    /// </summary>
    public class AppSettings
    {
        static IConfiguration Configuration { get; set; }
        
        static string ContentPath { get; set; }

        public AppSettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public AppSettings(string contentPath)
        {
            string path = "appsettings.json";
            //如果你把配置文件 是 根据环境变量来分开了，可以这样写
            //Path = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";
            Configuration = new ConfigurationBuilder()
                .SetBasePath(contentPath)
                .Add(new JsonConfigurationSource
                {
                    Path = path, Optional = false, ReloadOnChange = true 
                })
                .Build();
        }

        public static string app(params string[] sections)
        {
            try
            {
                return Configuration[string.Join(":", sections)];
            }
            catch (Exception)
            {
            }
            return "";
        }

        public static List<T> app<T>(params string[] sections)
        {
            List<T> list = new List<T>();
            Configuration.Bind(string.Join(":", sections),list);
            return list;
        }
    }
}
