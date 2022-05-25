/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Extensions.ExtensionServices
*项目描述:
*类 名 称:SwaggerSetup
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/25 23:51:36
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Extensions.ExtensionServices
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentException(nameof(services));
        }
    }
}
