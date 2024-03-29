﻿/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Shared.GlobalData
*项目描述:
*类 名 称:GlobalVariable
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/26 16:44:15
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

namespace CloudWhalesBlogCore.Shared.GlobalData
{

    public static class Permissions
    {
        public const string Name = "Permission";

        /// <summary>
        /// 测试网关授权
        /// 可以使用Blog.Core项目中的test用户
        /// 账号：test
        /// 密码：test
        /// </summary>
        public const string GWName = "GW";

        /// <summary>
        /// 当前项目是否启用IDS4权限方案
        /// true：表示启动IDS4
        /// false：表示使用JWT
        public static bool IsUseIds4 = false;
    }

    /// <summary>
    /// 路由变量前缀配置
    /// </summary>
    public static class RoutePrefix
    {
        /// <summary>
        /// 前缀名
        /// 如果不需要，尽量留空，不要修改
        /// 除非一定要在所有的 api 前统一加上特定前缀
        /// </summary>
        public const string Name = "";
    }

    /// <summary>
    /// RedisMqKey
    /// </summary>
    public static class RedisMqKey
    {
        public const string Loging = "Loging";
    }
}
