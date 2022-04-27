/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Win
*项目描述:
*类 名 称:PhotoImageHelper
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/4/27 9:28:25
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Win
{
    public class PhotoImageHelper : DisposableClass
    {
        private bool _disposed;
        private Image image;

        public PhotoImageHelper(string imagePath)
        {
            this.image = Image.FromFile(imagePath);
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //TODO:回收托管的资源
                }
                //TODO:回收非托管的资源
                _disposed = true;
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 添加水印
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string AddWatermark(string text, string orignPath)
        {
            Bitmap bitmap = new(image, image.Width, image.Height);
            Graphics g = Graphics.FromImage(bitmap);

            float fontSize = 13.0f; //字体大小
            float textWidth = text.Length * fontSize; //文本的长度

            //下面定义一个矩形区域，以后在这个矩形里画上白底黑字

            float rectWidth = text.Length * (fontSize + 18);
            float rectHeight = fontSize + 38;
            //定位在左中
            float rectY = bitmap.Height / 2;
            float rectX = 0;
            //定位到右上角
            /*float rectY = 0;
            float rectX = 0;*/
            //定位在右下角
            /*float rectY = bitmap.Height - rectHeight;
            float rectX = bitmap.Width - rectWidth;*/

            //声明矩形域
            RectangleF textArea = new(rectX, rectY, rectWidth, rectHeight);

            Font font = new("微软雅黑", fontSize,FontStyle.Bold); //定义字体
            Brush whiteBrush = new SolidBrush(Color.White); //白笔刷，画文字用
            Brush blackBrush = new SolidBrush(Color.Transparent); //黑笔刷，画背景用

            g.FillRectangle(blackBrush, rectX, rectY, rectWidth, rectHeight);

            g.DrawString(text, font, whiteBrush, textArea);
            MemoryStream ms = new MemoryStream();
            //保存为Jpg类型
            switch (Path.GetExtension(orignPath))
            {
                case ".jpeg": bitmap.Save(ms, ImageFormat.Jpeg);break;
                case ".png": bitmap.Save(ms, ImageFormat.Png);break;
            }            

            Image watermarkImg = Image.FromStream(ms);

            var waterPath = Path.Combine(Path.GetDirectoryName(orignPath), "waterpic");
            var waterName = Path.GetFileName(orignPath);
            if (!Directory.Exists(waterPath))
                Directory.CreateDirectory(waterPath);
            var waterImagePath = Path.Combine(waterPath, waterName);
            watermarkImg.Save(waterImagePath);

            g.Dispose();
            bitmap.Dispose();

            return waterImagePath;
            //return watermarkImg;
        }
    }
}
