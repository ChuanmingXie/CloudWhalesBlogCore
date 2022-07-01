/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Services.OfficeServices
*项目描述:
*类 名 称:WordHandleSuper
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 17:05:06
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Shared.Common.Base;
using CloudWhalesBlogCore.Shared.DTO.Input;
using CloudWhalesBlogCore.Shared.DTO.Output;
using CloudWhalesBlogCore.Shared.NLogger;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Services.OfficeServices
{
    public abstract class WordHandleSuper : BaseDisposable
    {
        /// <summary>
        /// 任务取消令牌
        /// </summary>
        public readonly CancellationTokenSource cancelTokenSource = new();
        /// <summary>
        /// 资源同步 初始化为阻塞等待
        /// </summary>
        public readonly AutoResetEvent resetEvent = new(false);


        private readonly string ExcelPath;

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

        public WordHandleSuper(string excelPath)
        {
            ExcelPath = excelPath;
        }
        
        /// <summary>
        /// 创建文档
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="randomPath"></param>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public bool CreateWordDocument(string savePath, string randomPath, List<HouseParamOutList> dataList)
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
                    wordContentList.Add(FillContent(demolition, randomPath));
                }
                CreateDocument(xwPFDocument, wordContentList);

                //4. 保存内容
                using FileStream sw = new(documentFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                xwPFDocument.Write(sw);
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(ex.Message,ex);
                return false;
            }
        }

        /// <summary>
        /// 抽象算法:收集填充内容
        /// </summary>
        /// <param name="item"></param>
        /// <param name="randomPath"></param>
        /// <returns></returns>
        public abstract WordContentInput FillContent(HouseParamOutList item, string randomPath);

        /// <summary>
        /// 抽象算法：生成文档内容
        /// </summary>
        /// <param name="xwPFDocument"></param>
        /// <param name="wordContentList"></param>
        public abstract void CreateDocument(XWPFDocument xwPFDocument, List<WordContentInput> wordContentList);

    }
}
