/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Shared.Common.Extensions
*项目描述:
*类 名 称:ExtensionRichTextBox
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 8:24:48
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CloudWhalesBlogCore.Services.Extensions
{
    public static class ExtensionRichTextBox
    {

        /// <summary>
        /// 附加彩色文本
        /// https://www.cnblogs.com/bobositlife/p/csharp-winform-change-richtextbox-font-color-using-static-extension-method.html
        /// </summary>
        /// <param name="rtBox">RichTextBox对象</param>
        /// <param name="text">文本</param>
        /// <param name="color">颜色</param>
        /// <param name="addNewLine">是否在文本后附加换行</param>
        /// <param name="font">字体</param>
        public static void AppendTextColorful(this RichTextBox rtBox, string text, Color color, bool addNewLine = true, Font font = null)
        {
            if (addNewLine)
            {
                text += Environment.NewLine;
            }

            rtBox.SelectionStart = rtBox.TextLength;
            rtBox.SelectionLength = 0;
            rtBox.SelectionColor = color;
            if (font != null) rtBox.SelectionFont = font;

            rtBox.AppendText(text);
            rtBox.SelectionColor = rtBox.ForeColor;
            if (font != null) rtBox.SelectionFont = rtBox.Font;
        }
    }
}
