/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared.Helper
*项目描述:
*类 名 称:AppSettings
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/31 18:17:21
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

namespace SwaggerWithMiniProfiler.Shared
{
    /// <summary>
    /// 操作appsettings.json帮助类
    /// </summary>
    public class HelperAppSettings
    {
        private static IConfiguration Configuration { get; set; }

        private static string ContenetPath { get; set; }

        public HelperAppSettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public HelperAppSettings(string contentPath)
        {
            string path = "appsettings.json";
            /*配置文件根据环境变量分开的写法*/
            Configuration = new ConfigurationBuilder()
                .SetBasePath(contentPath)
                .Add(new JsonConfigurationSource
                {
                    Path = path,
                    Optional = false,
                    //直接读取目录的json文件,而不是bin文件下,所以不用换更改文件的属性为复制
                    ReloadOnChange = true
                }).Build();
        }

        /// <summary>
        /// 通过appsettings.json文件的Key获取Value
        /// </summary>
        /// <param name="sections">节点配置</param>
        /// <returns></returns>
        public static string app(params string[] sections)
        {
            try
            {
                if (sections.Any())
                {
                    return Configuration[string.Join(":", sections)];
                }
            }
            catch (Exception){}
            return string.Empty;
        }

        /// <summary>
        /// 通过appsettings.json文件的Key获取Value的泛型方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static List<T> app<T>(params string[] sections)
        {
            List<T> list = new List<T>();
            Configuration.Bind(string.Join(":", sections), list);
            return list;
        }

    }
}
