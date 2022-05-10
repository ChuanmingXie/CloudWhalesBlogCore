/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Services.OfficeServices
*项目描述:
*类 名 称:ExcelHandleSuper
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 16:48:31
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Shared.Common.Base;
using CloudWhalesBlogCore.Shared.DTO.Output;
using CloudWhalesBlogCore.Shared.NLogger;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;

namespace CloudWhalesBlogCore.Services.OfficeServices
{
    public abstract class ExcelHandleSuper : BaseDisposable
    {
        /// <summary>
        /// 任务取消令牌
        /// </summary>
        public readonly CancellationTokenSource cancelTokenSource = new();
        /// <summary>
        /// 资源同步 初始化为阻塞等待
        /// </summary>
        public readonly AutoResetEvent resetEvent = new(false);

        public readonly string excelPath; //文件名

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

        public ExcelHandleSuper(string excelPath)
        {
            this.excelPath = excelPath;
        }

        /// <summary>
        /// 表格数据图片
        /// </summary>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public List<string> ExcelSavePhotos(string savePath)
        {
            savePath += "\\pic";
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);
            NLogHelper._.Info($"导出目录{savePath}创建完成");
            List<string> photoPathList = new();
            SaveImage(excelPath, savePath, ref photoPathList);
            return photoPathList;
        }

        /// <summary>
        /// 保存表格图片过程
        /// </summary>
        /// <param name="excelpath"></param>
        /// <param name="savePath"></param>
        /// <param name="photoPathList"></param>
        /// <returns></returns>
        private static bool SaveImage(string excelpath, string savePath, ref List<string> photoPathList)
        {
            try
            {
                IWorkbook workbook = null;
                using FileStream excelStream = File.OpenRead(excelpath);
                if (excelpath.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(excelStream);
                else if (excelpath.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(excelStream);

                IList pthots = workbook.GetAllPictures();

                int index = 0;
                foreach (XSSFPictureData img in pthots)
                {
                    string path = string.Empty;
                    string imgExtension = img.SuggestFileExtension();
                    var secondPath = Path.Combine(savePath);
                    if (!Directory.Exists(secondPath))
                        Directory.CreateDirectory(secondPath);
                    switch (imgExtension)
                    {
                        case "jpeg":
                        case "jpg": path = Path.Combine(secondPath, string.Format("pic{0}.jpg", index++)); break;
                        case "png": path = Path.Combine(secondPath, string.Format("pic{0}.png", index++)); break;
                    }
                    Image image = Image.FromStream(new MemoryStream(img.Data));
                    image.Save(path);
                    if (!string.IsNullOrEmpty(path))
                        photoPathList.Add(path);
                }
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(ex.Message,ex);
                return false;
            }
        }

        /// <summary>
        /// 表格sheet拆分
        /// </summary>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public List<string> ExcelSplitSheet(string savePath)
        {
            savePath += "\\excel";
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);
            NLogHelper._.Info($"导出目录{savePath}创建完成");
            List<string> sheetPathList = new();
            if (SplitSheet(excelPath, savePath, ref sheetPathList))
                return sheetPathList;
            else
                return null;
        }

        /// <summary>
        /// 执行拆分表格过程
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="savePath"></param>
        /// <param name="sheetPathList"></param>
        /// <returns></returns>
        private static bool SplitSheet(string excelPath, string savePath, ref List<string> sheetPathList)
        {
            try
            {
                IWorkbook workbook = null;
                FileStream excelStream = new(excelPath, FileMode.Open, FileAccess.Read);
                if (excelPath.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(excelStream);
                else if (excelPath.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(excelStream);

                int count = workbook.NumberOfSheets; //获取所有SheetName
                for (int i = 0; i < count; i++)
                {
                    var tempNewSheet = Path.Combine(savePath, workbook.GetSheetName(i) + ".xlsx");
                    if (File.Exists(tempNewSheet))
                        File.Delete(tempNewSheet);

                    XSSFWorkbook workbookMerged = new XSSFWorkbook();
                    ((XSSFSheet)workbook.GetSheetAt(i)).CopyTo(workbookMerged, workbook.GetSheetName(i), true, true);

                    using (FileStream fs = new(tempNewSheet, FileMode.Append, FileAccess.Write))
                        workbookMerged.Write(fs);
                    sheetPathList.Add(tempNewSheet);
                }
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(ex.Message,ex);
                return false;
            }
        }

        /// <summary>
        /// 表格文件合并
        /// </summary>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public string ExcelMultiMerge(string savePath)
        {
            try
            {
                string[] files = Directory.GetFiles(excelPath);
                XSSFWorkbook workbookMerged = new();

                foreach (string file in files)
                {
                    XSSFWorkbook workbook;
                    using FileStream fs = new(file, FileMode.Open, FileAccess.Read);
                    workbook = new XSSFWorkbook(fs);

                    for (int i = 0; i < workbook.NumberOfSheets; i++)
                    {
                        ((XSSFSheet)workbook.GetSheetAt(i)).CopyTo(workbookMerged, workbook.GetSheetName(i), true, true);
                    }
                }
                var newExcelPath = Path.Combine(savePath, "Merge.xlsx");
                using (FileStream fs = new FileStream(newExcelPath, FileMode.Append, FileAccess.Write))
                {
                    workbookMerged.Write(fs);
                }
                return newExcelPath;
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(ex.Message,ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// 收集数据
        /// </summary>
        /// <returns></returns>
        public abstract List<HouseParamOutList> GatherData();

        /// <summary>
        /// 创建表格
        /// </summary>
        /// <returns></returns>
        public abstract string CreateExcel(HouseParamOutList demolitionOutList, string tableName, string savePath);
        
    }
}
