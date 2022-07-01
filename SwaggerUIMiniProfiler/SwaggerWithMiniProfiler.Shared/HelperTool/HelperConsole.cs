/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Shared.HelperTool
*项目描述:
*类 名 称:HelperConsole
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/6/6 8:49:10
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

namespace SwaggerWithMiniProfiler.Shared.HelperTool
{
    public static class HelperConsole
    {
        public static void WriteColorLine(string str, ConsoleColor color)
        {

            ConsoleColor currentForeColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ForegroundColor = currentForeColor;
        }

        /// <summary>
        /// 打印错误信息
        /// </summary>
        /// <param name="str">待打印的字符串</param>
        /// <param name="color">想要打印的颜色</param>
        public static void WriteLine(this string str,ConsoleColor color = ConsoleColor.Red)
        {
            WriteColorLine(str, color);
        }

        /// <summary>
        /// 打印警告信息
        /// </summary>
        /// <param name="str">打印的字符串</param>
        /// <param name="color">想要打印的颜色</param>

        public static void WriteWarningLine(this string str,ConsoleColor color = ConsoleColor.Yellow)
        {
            WriteColorLine(str, color);
        }

        /// <summary>
        /// 打印正常信息
        /// </summary>
        /// <param name="str">打印的字符串</param>
        /// <param name="color">想要打印的颜色</param>
        public static void WriteInfoLine(this string str,ConsoleColor color = ConsoleColor.White)
        {
            WriteColorLine(str, color);
        }

        /// <summary>
        /// 打印成功的信息
        /// </summary>
        /// <param name="str">打印的字符串</param>
        /// <param name="color">想要打印的颜色</param>
        public static void WriteSuccessLine(this string str,ConsoleColor color = ConsoleColor.Green)
        {
            WriteColorLine(str, color);
        }
    }
}
