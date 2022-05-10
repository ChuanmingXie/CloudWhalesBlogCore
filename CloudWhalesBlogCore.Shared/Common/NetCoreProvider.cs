/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Shared.Common
*项目描述:
*类 名 称:NetCoreProvider
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 11:58:10
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/


using System;
using Autofac;

namespace CloudWhalesBlogCore.Shared.Common
{
    /// <summary>
    /// 服务提供者
    /// </summary>
    public class NetCoreProvider
    {
        public static IContainer Instance { get; private set; }

        public static void RegisterServiceLocator(IContainer locator)
        {
            if (Instance == null)
                Instance = locator;
        }

        public static T Resolve<T>()
        {
            if (!Instance.IsRegistered<T>())
                new ArgumentNullException(nameof(T));

            return Instance.Resolve<T>();
        }

        public static T ResolveNamed<T>(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                new ArgumentNullException(nameof(T));

            return Instance.ResolveNamed<T>(typeName);
        }
    }
}
