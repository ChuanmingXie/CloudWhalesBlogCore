/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Win
*项目描述:
*类 名 称:WordHelper
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/4/24 7:52:01
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Model.DTO.Input;
using CloudWhalesBlogCore.Model.DTO.Output;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace CloudWhalesBlogCore.Win
{
    public class WordHelper : DisposableClass
    {
        private string ExcelPath;

        private bool _disposed; //表示是否已经被回收

        protected override void Dispose(bool disposing)
        {
            if (!_disposed) //如果还没有被回收
            {
                if (disposing) //如果需要回收一些托管资源
                {
                    //TODO:回收托管资源，调用IDisposable的Dispose()方法就可以
                }
                //TODO：回收非托管资源，把之设置为null，等待CLR调用析构函数的时候回收
                _disposed = true;

            }
            base.Dispose(disposing);//再调用父类的垃圾回收逻辑
        }

        public WordHelper(string excelPath)
        {
            ExcelPath = excelPath;
        }

        /// <summary>
        /// 创建定制文档
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public bool CreateWordDocument(string savePath, string randomPath, List<DemolitionDataOutList> dataList)
        {
            try
            {
                savePath += "\\doc";
                if (!Directory.Exists(savePath))
                    Directory.CreateDirectory(savePath);
                var documentName = Path.GetFileNameWithoutExtension(ExcelPath);
                var documentFile = Path.Combine(savePath, documentName + ".docx");

                //1. 初始化文档大小(宽高对调显示成横向)
                XWPFDocument xwPFDocument = new();
                CT_SectPr sizeDocuemnt = new();
                sizeDocuemnt.pgSz.w = (ulong)11906;
                sizeDocuemnt.pgSz.h = (ulong)16838;
                xwPFDocument.Document.body.sectPr = sizeDocuemnt;

                //2. 创建标题
                XWPFParagraph titleParagraph = xwPFDocument.CreateParagraph();
                titleParagraph.Alignment = ParagraphAlignment.LEFT;
                XWPFRun rowTitleRun = titleParagraph.CreateRun();
                rowTitleRun.FontFamily = "黑体";
                rowTitleRun.FontSize = 14;
                rowTitleRun.IsBold = true;
                rowTitleRun.SetText(documentName);
                CT_P titleAlign = xwPFDocument.Document.body.GetPArray(0);
                titleAlign.AddNewPPr().AddNewJc().val = ST_Jc.center;

                //3. 创建内容
                List<WordContentInput> wordContentList = new();
                foreach (var demolition in dataList)
                {
                    wordContentList.Add(GatherContent(demolition, randomPath));
                }
                CreateDocument(xwPFDocument, wordContentList);

                //4. 保存内容
                using FileStream sw = new(documentFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                xwPFDocument.Write(sw);
                return true;
            }
            catch (Exception ex)
            {
                LoggerHelper._.Error(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 整理传递过来的数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private static WordContentInput GatherContent(DemolitionDataOutList item, string randomPath)
        {
            var contentParagraph = string.Empty;
            foreach (var house in item.DemolitionDatas)
            {
                int numCount = 0;
                NeedPhoto(house.MasterRoom, ref numCount);
                NeedPhoto(house.SecondRoom, ref numCount);
                NeedPhoto(house.SecondRoom2, ref numCount);
                NeedPhoto(house.StudyRoom, ref numCount);

                house.StatusPhotos.RemoveAll(x => string.IsNullOrEmpty(x));
                var orginPhotoCount = house.StatusPhotos.Count;
                DirectoryInfo randDirectory = new DirectoryInfo(randomPath);
                var randRange = randDirectory.GetFiles().Length;//获取文件列表
                Random random = new();
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
                //var photoInfo = house.BuildingNum + "-" + house.RoomNum + ";";
                //contentParagraph += string.Join(';', house.StatusPhotos);
                LoggerHelper._.Info(contentParagraph);
            }
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

        private static void RemoveNull<T>(List<T> list)
        {
            // 找出第一个空元素 O(n)
            int count = list.Count;
            for (int i = 0; i < count; i++)
                if (list[i] == null || list[i].ToString() == "")
                {
                    // 记录当前位置
                    int newCount = i++;

                    // 对每个非空元素，复制至当前位置 O(n)
                    for (; i < count; i++)
                        if (list[i] != null)
                            list[newCount++] = list[i];

                    // 移除多余的元素 O(n)
                    list.RemoveRange(newCount, count - newCount);
                    break;
                }
        }


        /// <summary>
        /// 执行Word创建过程
        /// </summary>
        /// <param name="wordContent"></param>
        /// <param name="savePath"></param>
        private static void CreateDocument(XWPFDocument xwPFDocument, List<WordContentInput> wordContentList)
        {
            foreach (var wordContent in wordContentList)
            {
                var contentArray = wordContent.Paragraph.Split(';');
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
                        using PhotoImageHelper imageHelper = new(itemlist[1]);
                        var waterImagePath = imageHelper.AddWatermark(itemlist[0], itemlist[1]);
                        //waterImage.Save(item);

                        var imgWidth = (int)(500.0 * 9525);
                        var imgHeight = (int)(400.0 * 9525);
                        //创建数据流
                        FileStream contentStream = new(waterImagePath, FileMode.Open, FileAccess.Read);
                        rowContentRunitem1.AddPicture(contentStream, (int)PictureType.JPEG, Path.GetFileName(itemlist[1]), imgWidth, imgHeight);
                        contentStream.Close();
                    }
                }

            }
        }


        /// <summary>
        /// 创建定制文档
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="savePath"></param>
        /// <param name="photoPathList"></param>
        /// <param name="demolitionOutList"></param>
        /// <returns></returns>
        [Obsolete("已经弃用的方法")]
        public static string CreateWordDocument(string excelPath, string savePath, List<string> photoPathList, List<DemolitionDataOutList> demolitionOutList)
        {
            try
            {
                savePath += "\\doc";
                if (!Directory.Exists(savePath))
                    Directory.CreateDirectory(savePath);
                var documentName = Path.GetFileNameWithoutExtension(excelPath);
                var documentFile = Path.Combine(savePath, documentName + ".docx");
                //if (File.Exists(documentFile))
                //    File.Delete(documentFile);
                //File.Create(documentFile);

                //1. 初始化文档大小(宽高对调显示成横向)
                XWPFDocument xwPFDocument = new();
                CT_SectPr sizeDocuemnt = new();
                sizeDocuemnt.pgSz.w = (ulong)11906;
                sizeDocuemnt.pgSz.h = (ulong)16838;
                xwPFDocument.Document.body.sectPr = sizeDocuemnt;


                //2. 创建标题
                XWPFParagraph titleParagraph = xwPFDocument.CreateParagraph();
                titleParagraph.Alignment = ParagraphAlignment.LEFT;
                XWPFRun rowTitleRun = titleParagraph.CreateRun();
                rowTitleRun.FontFamily = "黑体";
                rowTitleRun.FontSize = 14;
                rowTitleRun.IsBold = true;
                rowTitleRun.SetText(documentName);
                CT_P titleAlign = xwPFDocument.Document.body.GetPArray(0);
                titleAlign.AddNewPPr().AddNewJc().val = ST_Jc.center;

                List<WordContentInput> wordContentList = new();
                //3. 创建内容

                int index = 0;
                foreach (var item in demolitionOutList)
                {
                    wordContentList.Add(GatherContent(ref index, photoPathList, item));
                }

                CreateDocument(xwPFDocument, wordContentList);
                //4. 保存内容
                using FileStream sw = new(documentFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                xwPFDocument.Write(sw);
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 整理传递过来的文档内容
        /// </summary>
        /// <param name="photoList"></param>
        /// <param name="demolitionList"></param>
        /// <returns></returns>
        [Obsolete("已经弃用的方法")]
        private static WordContentInput GatherContent(ref int index, List<string> photoList, DemolitionDataOutList demolitionList)
        {
            var contentParagraph = string.Empty;
            foreach (var house in demolitionList.DemolitionDatas)
            {
                var photos = string.Empty;
                foreach (var imgItem in house.StatusPhotos)
                {
                    if (!string.IsNullOrEmpty(imgItem))
                    {
                        if (index > 427)
                        {
                            continue;
                        }
                        photos += photoList[index++] + ";";
                    }
                }
                var photoInfo = house.BuildingNum + "-" + house.RoomNum + ";";
                contentParagraph += photos + photoInfo;
            }
            WordContentInput wordContentInput = new()
            {
                Title = demolitionList.Title,
                Paragraph = contentParagraph
            };

            return wordContentInput;
        }

    }
}
