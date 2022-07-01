/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared.Common
*项目描述:
*类 名 称:SysMemoryCache
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/28 17:53:01
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Shared.Common
{
    public class MemoryCaching
    {
        public static MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        /// <summary>
        /// 验证缓存项是否存在
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            object cached;
            return _cache.TryGetValue(key, out cached);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="cacheKey">缓存key</param>
        /// <returns></returns>
        public static object Get(string cacheKey)
        {
            if (cacheKey == null)
                throw new ArgumentNullException(nameof(cacheKey));
            return _cache.Get(cacheKey);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="cacheKey">缓存key</param>
        /// <param name="cacheValue">缓存value</param>
        /// <param name="expiresSliding">滑动过期时长(如果在过期时间内有操作,则以当前时间点延长过期时间)</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        public static void Set(string cacheKey, object cacheValue, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (cacheKey == null)
                throw new ArgumentNullException(nameof(cacheKey));
            if (cacheValue == null)
                throw new ArgumentNullException(nameof(cacheValue));

            _cache.Set(cacheKey,cacheValue,
                new MemoryCacheEntryOptions()
                .SetSlidingExpiration(expiresSliding)
                .SetAbsoluteExpiration(expiressAbsoulte));
            //return Exists(cacheKey);
        }
    }
}
