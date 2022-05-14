/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Shared.Common.ImageHelper
*项目描述:
*类 名 称:ImageAddWater
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 8:19:07
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Shared.Common.Base;
using CloudWhalesBlogCore.Shared.NLogger;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace CloudWhalesBlogCore.Shared.Common.ImageHelper
{
    public class ImageAddWater : BaseDisposable
    {
        private bool _disposed;
        private Image image;

        public ImageAddWater(string imagePath)
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
            var waterImagePath = string.Empty;
            try
            {
                Bitmap bitmaptemp = new(image, image.Width, image.Height);
                Bitmap bitmap = KiResizeImage(bitmaptemp, 500, 400);
                Graphics g = Graphics.FromImage(bitmap);

                //定位在左中
                float rectY = bitmap.Height / 2;
                float rectX = 0;
                //定位到右上角
                /*float rectY = 0;
                float rectX = 0;*/
                //定位在右下角
                /*float rectY = bitmap.Height - rectHeight;
                float rectX = bitmap.Width - rectWidth;*/

                //设置字体大小
                /*float fontsize = 14f; //字体大小
                float textwidth = (text.length + 1) * fontsize; //文本的长度*/
                //下面定义一个矩形区域，以后在这个矩形里画上白底黑字
                /*float rectWidth = text.Length * (fontSize + 18);
                //float rectWidth = textWidth;
                float rectHeight = (fontSize + 38);*/

                //声明矩形域
                /*RectangleF textArea = new(rectX, rectY, rectWidth, rectHeight);
                Font font = new("微软雅黑", fontSize, FontStyle.Bold); //定义字体
                Brush whiteBrush = new SolidBrush(Color.White); //白笔刷，画文字用
                Brush blackBrush = new SolidBrush(Color.Transparent); //黑笔刷，画背景用
                g.FillRectangle(blackBrush, rectX, rectY, rectWidth, rectHeight); //填充一个矩形区域
                g.DrawString(text, font, whiteBrush, textArea);*/

                Font stringFont = new("微软雅黑", bitmap.Width * 0.01f);
                SizeF stringSize = new();
                //测量用指定的 Font 绘制的指定字符串
                //stringSize = g.MeasureString("楼栋户号:"+text+"\n房间号:", stringFont);
                stringSize = g.MeasureString(text, stringFont);
                //画一个透明色的矩形框
                //g.DrawRectangle(new Pen(Color.Transparent, 1), rectX, rectY, stringSize.Width, stringSize.Height);
                g.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.Green)), rectX, rectY, stringSize.Width, stringSize.Height);
                //g.DrawString("楼栋户号:" + text + "\n房间号:", stringFont, Brushes.White, new PointF(rectX, rectY));
                g.DrawString(text, stringFont, Brushes.White, new PointF(rectX, rectY));

                using MemoryStream ms = new();
                switch (Path.GetExtension(orignPath))
                {
                    case ".jpeg":
                    case ".jpg": bitmap.Save(ms, ImageFormat.Jpeg); break;
                    case ".png": bitmap.Save(ms, ImageFormat.Png); break;
                }

                Image watermarkImg = Image.FromStream(ms);

                var waterPath = Path.Combine(Path.GetDirectoryName(orignPath), "waterpic");
                var waterName = Path.GetFileName(orignPath);
                if (!Directory.Exists(waterPath))
                    Directory.CreateDirectory(waterPath);
                waterImagePath = Path.Combine(waterPath, waterName);
                watermarkImg.Save(waterImagePath);

                g.Dispose();
                bitmap.Dispose();
            }
            catch (Exception ex)
            {
                NLogHelper._.Fatal(ex.Message, ex);
            }
            return waterImagePath;
        }

        /// <summary>
        /// 调整图片大小
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="newW"></param>
        /// <param name="newH"></param>
        /// <returns></returns>
        public static Bitmap KiResizeImage(Bitmap bmp, int newW, int newH)
        {
            try
            {
                Bitmap b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }
    }
}
