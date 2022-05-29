/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared.Common
*项目描述:
*类 名 称:ICacheProvider
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/28 17:55:23
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Shared.Common
{
    public interface ICacheProvider
    {
        object Get(string cacheKey);

        void Set(string cacheKey, object cacheValue, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);
    }
}
