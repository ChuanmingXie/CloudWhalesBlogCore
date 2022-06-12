/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared
*项目描述:
*类 名 称:HelperLinq
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/6/12 23:28:04
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Shared
{
    public static class HelperLinq
    {
        public static Expression<Func<T,bool>> True<T>()
        {
            return x => true;
        }

        public static Expression<Func<T,bool>> False<T>()
        {
            return x => false;
        }
    }
}
