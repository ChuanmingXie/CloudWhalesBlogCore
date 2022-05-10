/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Shared.Common.Extensions
*项目描述:
*类 名 称:ExtensionWinformInvoke
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 9:20:55
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Windows.Forms;

namespace CloudWhalesBlogCore.Services.Extensions
{
    public static class ExtensionWinformInvoke
    {

        /// <summary>
        /// 控件有与它关联的句柄时，在创建控件的基础句柄所在线程上异步执行指定委托。
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="method">对不带参数的方法的委托。</param>
        /// <returns>IAsyncResult，无句柄时为 null </returns>
        public static IAsyncResult TryBeginInvoke(this Control control, Delegate method)
        {
            if (control.IsHandleCreated)
            {
                return control.BeginInvoke(method);
            }
            else
            {
                return null;
            }
        }
    }
}
