/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Win.WordHelper
*项目描述:
*类 名 称:WordHandleMix
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 17:41:09
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Services.Extensions;
using CloudWhalesBlogCore.Services.OfficeServices;
using CloudWhalesBlogCore.Shared.Common.ImageHelper;
using CloudWhalesBlogCore.Shared.DTO.Input;
using CloudWhalesBlogCore.Shared.DTO.Output;
using CloudWhalesBlogCore.Shared.NLogger;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Win.WordHelper
{
    public class HandleOutMixture : WordHandleSuper
    {
        public HandleOutMixture(string excelPath) : base(excelPath)
        {
        }

        public override void CreateDocument(XWPFDocument xwPFDocument, List<WordContentInput> wordContentList)
        {
            resetEvent.Reset();

            ProgressbarForm progressbar = new() { Text = "收集Excel数据窗口" };
            progressbar.SetColorfulTitle("收集数据  ", Color.DarkOrange, true);
            progressbar.SetColorfulTitle("正在执行中...", Color.Black);
            progressbar.SetInfo(null, "", "");

            cancelTokenSource.Token.Register(async () => await CancelGather());
            progressbar.AbortAction += () => cancelTokenSource.Cancel();
            progressbar.OperateAction += () =>
            {
                Task task = new(() =>
                {
                    try
                    {
                        foreach (var wordContent in wordContentList)
                        {
                            var i = 0;
                            var contentArray = wordContent.Paragraph.Split(';').Where(x => !string.IsNullOrEmpty(x));
                            //3.创建内容
                            foreach (var item in contentArray)
                            {
                                if (string.IsNullOrEmpty(item)) continue;
                                var itemlist = item.Split('-');

                                //设置对齐方式
                                CT_P contentAlign = xwPFDocument.Document.body.AddNewP();
                                contentAlign.AddNewPPr().AddNewJc().val = ST_Jc.center;
                                //创建段落
                                XWPFParagraph contentParagraph = new(contentAlign, xwPFDocument);
                                //创建run并插入内容
                                XWPFRun rowContentRunitem1 = contentParagraph.CreateRun();
                                rowContentRunitem1.FontSize = 13;
                                rowContentRunitem1.SetText(itemlist[0]);

                                if (File.Exists(itemlist[1]))
                                {
                                    //添加水印并保存
                                    using ImageAddWater imageHelper = new(itemlist[1]);
                                    var waterImagePath = imageHelper.AddWatermark(itemlist[0], itemlist[1]);
                                    //waterImage.Save(item);

                                    var imgWidth = (int)(500.0 * 9525);
                                    var imgHeight = (int)(400.0 * 9525);
                                    //创建数据流
                                    FileStream contentStream = new(waterImagePath, FileMode.Open, FileAccess.Read);
                                    rowContentRunitem1.AddPicture(contentStream, (int)PictureType.JPEG, Path.GetFileName(itemlist[1]), imgWidth, imgHeight);
                                    contentStream.Close();
                                }
                                progressbar.TryBeginInvoke(new Action(() =>
                                {
                                    progressbar.SetInfo(null, $"共{contentArray.Count()}项，已执行{i++}项", $"当前正在执行：{i}");
                                }));
                                Thread.Sleep(2);
                                progressbar.TryBeginInvoke(new Action(() =>
                                {
                                    progressbar.SetProgress(i, contentArray.Count());
                                }));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        NLogHelper._.Fatal(ex.Message, ex);
                    }
                }, cancelTokenSource.Token);
                task.Start();
                task.Wait();
            };
            var result = progressbar.ShowDialog();

        }
        private async Task CancelGather()
        {
            await Task.Run(() =>
            {
                resetEvent.WaitOne(1000 * 5);
            });
        }

        public override WordContentInput FillContent(HouseParamOutList item, string randomPath)
        {
            var contentParagraph = string.Empty;
            DirectoryInfo randDirectory = new(randomPath);
            var randRange = randDirectory.GetFiles().Length;//获取文件列表
            Random random = new();
            foreach (var house in item.HouseParams)
            {
                int numCount = 0;
                NeedPhoto(house.MasterRoom, ref numCount);
                NeedPhoto(house.SecondRoom, ref numCount);
                NeedPhoto(house.SecondRoom2, ref numCount);
                NeedPhoto(house.StudyRoom, ref numCount);

                house.StatusPhotos.RemoveAll(x => string.IsNullOrEmpty(x));
                var orginPhotoCount = house.StatusPhotos.Count;
                for (int i = 0; i < numCount - orginPhotoCount; i++)
                {
                    house.StatusPhotos.Insert(0, Path.Combine(randomPath, "图片" + random.Next(1, randRange) + ".png"));
                }
                var houseLink = house.BuildingNum + "_" + house.RoomNum;
                var roomPhoto = new List<Tuple<string, string>>();
                foreach (var photo in house.StatusPhotos)
                {
                    if (house.MasterRoom > 0 && roomPhoto.FindIndex(x => x.Item1.Contains("主")) < 0)
                    {
                        roomPhoto.Add(new Tuple<string, string>(houseLink + "主", photo));
                        continue;
                    }
                    if (house.SecondRoom > 0 && roomPhoto.FindIndex(x => x.Item1.Contains("次")) < 0)
                    {
                        roomPhoto.Add(new Tuple<string, string>(houseLink + "次", photo));
                        continue;
                    }
                    if (house.SecondRoom2 > 0 && roomPhoto.FindIndex(x => x.Item1.Contains("次2")) < 0)
                    {
                        roomPhoto.Add(new Tuple<string, string>(houseLink + "次2", photo));
                        continue;
                    }
                    if (house.StudyRoom > 0 && roomPhoto.FindIndex(x => x.Item1.Contains("书")) < 0)
                    {
                        roomPhoto.Add(new Tuple<string, string>(houseLink + "书", photo));
                        continue;
                    }
                }

                foreach (var rp in roomPhoto)
                {
                    contentParagraph += ";" + rp.Item1 + "-" + rp.Item2 + ";";
                }
            }
            NLogHelper._.Info(contentParagraph);
            WordContentInput wordContentInput = new()
            {
                Title = item.Title,
                Paragraph = contentParagraph
            };
            return wordContentInput;
        }


        private static void NeedPhoto(decimal? roomArea, ref int numCount)
        {
            if (roomArea > 0) numCount++;
        }

    }
}
