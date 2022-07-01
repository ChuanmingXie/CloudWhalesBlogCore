/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Shared.Common.Aop
*项目描述:
*类 名 称:GlobalLoger
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 11:52:24
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using AspectInjector.Broker;
using CloudWhalesBlogCore.Shared.Common.DataInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Shared.Common.Aop
{
    /// <summary>
    /// 全局日志
    /// </summary>
    [Aspect(Scope.Global)]
    [Injection(typeof(GlobalLoger))]
    public class GlobalLoger : Attribute
    {
        private readonly ILog log;

        public GlobalLoger()
        {
            this.log = NetCoreProvider.Resolve<ILog>();
        }

        [Advice(Kind.Before, Targets = Target.Method)]
        public void Start([Argument(Source.Name)] string methodName, [Argument(Source.Arguments)] object[] arg)
        {
            log.Debug($"开始调用方法:{methodName},参数:{string.Join(",", arg)}");
        }

        [Advice(Kind.After, Targets = Target.Method)]
        public void End([Argument(Source.Name)] string methodName, [Argument(Source.ReturnValue)] object arg)
        {
            log.Debug($"结束调用方法:{methodName},返回值:{arg}");
        }
    }
}
