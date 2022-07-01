/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Services.Extensions
*项目描述:
*类 名 称:ExtensionServices
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 12:28:05
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using Autofac;

namespace CloudWhalesBlogCore.Services.Extensions
{
    public static class ExtensionServices
    {
        public static ContainerBuilder AddViewCenter<TCenter, ICenter>(this ContainerBuilder services)
        {
            services.RegisterType<TCenter>().Named(typeof(TCenter).Name, typeof(ICenter));
            return services;
        }

        public static ContainerBuilder AddRepository<TRepository, IRepository>(this ContainerBuilder services)
        where TRepository : class
        {
            services.RegisterType<TRepository>().As<IRepository>();
            return services;
        }

        public static ContainerBuilder AddViewModel<TRepository, IRepository>(this ContainerBuilder services)
        where TRepository : class
        {
            services.RegisterType<TRepository>().As<IRepository>();
            return services;
        }
    }
}
