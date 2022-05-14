/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Win.WordHelper
*项目描述:
*类 名 称:HandleOutImgRand
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/10 18:20:49
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
    public class HandleOutImgRand : WordHandleSuper
    {
        public HandleOutImgRand(string excelPath) : base(excelPath)
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
                            var index = 0;
                            var contentArray = wordContent.Paragraph.Split(';').Where(x => !string.IsNullOrEmpty(x));
                            //3.创建内容
                            foreach (var item in contentArray)
                            {
                                if (string.IsNullOrEmpty(item)) continue;
                                var itemlist = item.Split('#');

                                //设置对齐方式
                                CT_P contentAlign = xwPFDocument.Document.body.AddNewP();
                                contentAlign.AddNewPPr().AddNewJc().val = ST_Jc.center;
                                //创建段落
                                XWPFParagraph contentParagraph = new(contentAlign, xwPFDocument);
                                //创建run并插入内容
                                XWPFRun rowContentRunitem1 = contentParagraph.CreateRun();
                                rowContentRunitem1.FontSize = 13;
                                rowContentRunitem1.SetText(itemlist[0]);
                                rowContentRunitem1.AddBreak(BreakType.TEXTWRAPPING);

                                for (int i = 1; i < itemlist.Length; i++)
                                {
                                    if (File.Exists(itemlist[i]))
                                    {
                                        //添加水印并保存
                                        using ImageAddWater imageHelper = new(itemlist[i]);
                                        var waterImagePath = imageHelper.AddWatermark(itemlist[0], itemlist[i]);
                                        //waterImagePath = itemlist[i];
                                        //创建数据流
                                        FileStream contentStream = new(waterImagePath, FileMode.Open, FileAccess.Read);
                                        var imgWidth = (int)(500.0 * 9525);
                                        var imgHeight = (int)(400.0 * 9525);
                                        rowContentRunitem1.AddPicture(contentStream, (int)PictureType.JPEG, Path.GetFileName(itemlist[i]), imgWidth, imgHeight);
                                        contentStream.Close();
                                    }
                                }
                                progressbar.TryBeginInvoke(new Action(() =>
                                {
                                    progressbar.SetInfo(null, $"共{contentArray.Count()}项，已执行{index++}项", $"当前正在执行：{index}");
                                }));
                                Thread.Sleep(2);
                                progressbar.TryBeginInvoke(new Action(() =>
                                {
                                    progressbar.SetProgress(index, contentArray.Count());
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
            WordContentInput wordContentInput = null;
            try
            {
                var contentParagraph = string.Empty;
                var roomPhoto = new List<Tuple<string, string>>();
                List<FileInfo> selectedPic = new();
                DirectoryInfo randDirectory = new(randomPath);
                var randRange = randDirectory.GetFiles().ToList();


                Random random = new();

                foreach (var house in item.HouseParams)
                {
                    if (string.IsNullOrEmpty(house.RoomNum)) continue;
                    var houseLink = house.BuildingNum + "-" + house.RoomNum;
                    IEnumerable<FileInfo> enumerable = randRange.Where(x =>
                    {
                        if (x.Name.Length >= houseLink.Length)
                        {
                            return x.Name.Substring(0, houseLink.Length).Equals(houseLink);
                        }
                        return false;
                    });
                    if (enumerable.Any())
                        house.StatusPhotos.AddRange(enumerable.Select(x => x.FullName).ToList());
                    else
                        house.StatusPhotos.Add(Path.Combine(randomPath, "图片" + random.Next(1, randRange.Count) + ".png"));
                    var housePic = string.Join('#', house.StatusPhotos);
                    roomPhoto.Add(new Tuple<string, string>(houseLink, housePic));
                }

                foreach (var rp in roomPhoto)
                {
                    contentParagraph += rp.Item1 + "#" + rp.Item2 + ";";
                }
                NLogHelper._.Info(contentParagraph);
                wordContentInput = new()
                {
                    Title = item.Title,
                    Paragraph = contentParagraph
                };
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(ex.Message, ex);
            }
            return wordContentInput;
        }
    }
}
