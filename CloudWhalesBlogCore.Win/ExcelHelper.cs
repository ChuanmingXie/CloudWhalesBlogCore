/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Win
*项目描述:
*类 名 称:ExcelHelper
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/4/24 7:51:34
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Model.DTO.Output;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Win
{
    public class ExcelHelper:DisposableClass
    {
        private string excelPath; //文件名

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


        public ExcelHelper(string excelPath)
        {
            this.excelPath = excelPath;
        }

        /// <summary>
        /// 将Excel表格中的图片保存到指定位置
        /// </summary>
        /// <param name="excelpath"></param>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public static List<string> SaveExcelPhotos(string excelpath, string savePath)
        {
            savePath += "\\pic";
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);
            LoggerHelper._.Info($"导出目录{savePath}创建完成");
            List<string> photoPathList = new();
            if (excelpath.IndexOf(".xlsx") <= 0)
            {
                string[] files = Directory.GetFiles(excelpath);
                foreach (string file in files)
                    SaveImage(file, savePath, ref photoPathList);
            }
            else
            {
                SaveImage(excelpath, savePath, ref photoPathList);
            }
            return photoPathList;

        }

        /// <summary>
        /// 执行保存过程
        /// </summary>
        /// <param name="excelpath">sheets路径</param>
        /// <param name="savePath">保存到的位置</param>
        /// <param name="photoPathList">位置列表</param>
        /// <returns></returns>
        private static bool SaveImage(string excelpath, string savePath, ref List<string> photoPathList)
        {
            try
            {
                using FileStream excelStream = File.OpenRead(excelpath);
                XSSFWorkbook sheets = new(excelStream);
                IList pthots = sheets.GetAllPictures();
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
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 拆分sheet的数据表另存
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="savePath"></param>
        /// <param name="sheetsList"></param>
        /// <returns></returns>
        public List<string> GetSaveAsExcel(string savePath)
        {
            savePath += "\\excel";
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);
            LoggerHelper._.Info($"导出目录{savePath}创建完成");
            List<string> sheetPathList = new();
            if (SaveAsSheet(excelPath, savePath, ref sheetPathList))
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
        private static bool SaveAsSheet(string excelPath, string savePath, ref List<string> sheetPathList)
        {
            try
            {
                FileStream excelStram = new(excelPath, FileMode.Open, FileAccess.Read);
                IWorkbook workbook = new XSSFWorkbook(excelStram);
                int count = workbook.NumberOfSheets; //获取所有SheetName
                for (int i = 0; i < count; i++)
                {
                    var tempNewSheet = Path.Combine(savePath, workbook.GetSheetName(i) + ".xlsx");
                    if (File.Exists(tempNewSheet))
                        File.Delete(tempNewSheet);

                    XSSFWorkbook workbookMerged = new XSSFWorkbook();
                    ((XSSFSheet)workbook.GetSheetAt(i)).CopyTo(workbookMerged, workbook.GetSheetName(i), true, true);

                    using (FileStream fs = new FileStream(tempNewSheet, FileMode.Append, FileAccess.Write))
                        workbookMerged.Write(fs);
                    sheetPathList.Add(tempNewSheet);
                }
            }
            catch (Exception ex)
            {
                LoggerHelper._.Error(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 合并所有单张excel
        /// </summary>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public string MergeExcel(string savePath)
        {
            string[] files = Directory.GetFiles(excelPath);
            XSSFWorkbook workbookMerged = new XSSFWorkbook();

            foreach (string file in files)
            {
                XSSFWorkbook workbook;
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    workbook = new XSSFWorkbook(fs);
                    for (int i = 0; i < workbook.NumberOfSheets; i++)
                    {
                        ((XSSFSheet)workbook.GetSheetAt(i)).CopyTo(workbookMerged, workbook.GetSheetName(i), true, true);
                    }
                }
            }
            var newExcelPath = Path.Combine(savePath, "Merge.xlsx");
            using (FileStream fs = new FileStream(newExcelPath, FileMode.Append, FileAccess.Write))
            {
                workbookMerged.Write(fs);
            }
            return newExcelPath;
        }


        /// <summary>
        /// 收集整理数据
        /// </summary>
        /// <returns></returns>
        public List<DemolitionDataOutList> GatherData()
        {
            try
            {
                ExcelXmlHelper excelXmlHelper = new(excelPath);
                var photoList = excelXmlHelper.XMLPhotoList();

                List<DemolitionDataOutList> dataAllList = new();
                using DataTableExcelHelper tableExcelHelper = new(excelPath);
                var sheetDic = tableExcelHelper.ReturnSheetList();
                var dtCurrent = new DataTable();
                foreach (var item in sheetDic)
                {
                    if (item.Key == 16) continue;
                    List<DemolitionDataOut> demolitionList = new();
                    DemolitionDataOutList dataListItem;
                    dtCurrent = tableExcelHelper.ExcelToDataTable(item.Key);
                    int rowIndex = 1;
                    foreach (DataRow row in dtCurrent.Rows)
                    {
                        if (rowIndex++ == 1 || row[4].ToString().Equals("合计")|| row[5].ToString().Equals("合计")) continue;
                        if (row.ItemArray.Length!=10)
                        {
                            DemolitionDataOut demolition = new()
                            {
                                BuildingNum = row[0].ToString(),
                                RoomNum = row[1].ToString(),
                                MasterRoom = Convert.ToDecimal(!string.IsNullOrEmpty(row[2].ToString()) ? row[2].ToString() : 0),
                                SecondRoom = Convert.ToDecimal(!string.IsNullOrEmpty(row[3].ToString()) ? row[3].ToString() : 0),
                                SecondRoom2 = 0,
                                StudyRoom = Convert.ToDecimal(!string.IsNullOrEmpty(row[4].ToString()) ? row[4].ToString() : 0),
                                StatusPhotos = new()
                                {
                                    FindImage(row[7].ToString(), photoList),
                                    item.Key == 0 ? FindImage(rowIndex - 1,photoList) : FindImage(row[6].ToString(), photoList),
                                }
                            };
                            demolition.DemolitionArea = demolition.MasterRoom + demolition.SecondRoom + demolition.StudyRoom;
                            demolitionList.Add(demolition);
                        }
                        else
                        {
                            DemolitionDataOut demolition = new()
                            {
                                BuildingNum = row[0].ToString(),
                                RoomNum = row[1].ToString(),
                                MasterRoom = Convert.ToDecimal(!string.IsNullOrEmpty(row[2].ToString()) ? row[2].ToString() : 0),
                                SecondRoom = Convert.ToDecimal(!string.IsNullOrEmpty(row[3].ToString()) ? row[3].ToString() : 0),
                                SecondRoom2 = Convert.ToDecimal(!string.IsNullOrEmpty(row[4].ToString()) ? row[4].ToString() : 0),
                                StudyRoom = Convert.ToDecimal(!string.IsNullOrEmpty(row[5].ToString()) ? row[5].ToString() : 0),
                                StatusPhotos = new()
                                {
                                    FindImage(row[8].ToString(), photoList),
                                    item.Key == 0 ? FindImage(rowIndex - 1, photoList) : FindImage(row[7].ToString(), photoList),
                                }
                            };
                            demolition.DemolitionArea = demolition.MasterRoom + demolition.SecondRoom + demolition.SecondRoom2 + demolition.StudyRoom;
                            demolitionList.Add(demolition);
                        }
                    }
                    dataListItem = new()
                    {
                        Title = dtCurrent.TableName,
                        AreaAll = demolitionList.Sum(s => s.MasterRoom + s.SecondRoom + s.StudyRoom),
                        DemolitionDatas = demolitionList
                    };
                    dataAllList.Add(dataListItem);
                }
                return dataAllList;
            }
            catch (Exception ex)
            {
                LoggerHelper._.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 根据Id查找
        /// </summary>
        /// <param name="orgionPicId"></param>
        /// <returns></returns>
        private string FindImage(string orgionPicId, List<Tuple<int, int, string, string, string>> photoList)
        {
            if (string.IsNullOrEmpty(orgionPicId)) return string.Empty;
            Regex regPId = new("(?<=\").*?(?=\")", RegexOptions.None);
            var regPicId = regPId.Match(orgionPicId);
            var photo = photoList.Find(x => x.Item3 == regPicId.Value);
            return photo.Item5;
            //return ";" + photo.Item5 + ";" + photo.Item4;
        }

        /// <summary>
        /// 根据表格行列查找
        /// </summary>
        /// <param name="picRow"></param>
        /// <returns></returns>
        private string FindImage(int picRow,List<Tuple<int, int, string, string, string>> photoList)
        {
            if (picRow == 0) return string.Empty;

            var photo = photoList.Find(x => x.Item1 == picRow);
            return photo.Item5;
            //return ";" + photo.Item5 + ";" + photo.Item4;
        }

        /// <summary>
        /// 获取Excel中的数据
        /// </summary>
        /// <param name="excelPath"></param>
        /// <returns></returns>
        public static List<DemolitionDataOutList> GatherData(string excelPath)
        {
            List<DemolitionDataOutList> data = new();
            using FileStream fs = new(excelPath, FileMode.Open, FileAccess.Read);
            XSSFWorkbook excelWorkbook = new(fs);
            for (int i = 0; i < excelWorkbook.NumberOfSheets - 1; i++)
            {
                var title = string.Empty;
                DemolitionDataOutList dataItem;
                List<DemolitionDataOut> demolitionDatas = new();
                ISheet sheet = excelWorkbook.GetSheetAt(i);
                title = sheet.GetRow(0).GetCell(0).ToString();
                for (int j = 2; j <= sheet.LastRowNum - 1; j++)
                {
                    IRow row = sheet.GetRow(j);
                    if (row == null)
                    {
                        continue;
                    }
                    DemolitionDataOut demolition = new()
                    {
                        BuildingNum = row.GetCell(0).ToString(),
                        RoomNum = row.GetCell(1).ToString(),
                        MasterRoom = Convert.ToDecimal(!string.IsNullOrEmpty(row.GetCell(2).ToString()) ? row.GetCell(2).ToString() : 0),
                        SecondRoom = Convert.ToDecimal(!string.IsNullOrEmpty(row.GetCell(3).ToString()) ? row.GetCell(3).ToString() : 0),
                        StudyRoom = Convert.ToDecimal(!string.IsNullOrEmpty(row.GetCell(4).ToString()) ? row.GetCell(4).ToString() : 0)
                    };
                    if (sheet.SheetName == "1#号楼")
                        demolition.StatusPhotos.Add("this cell have pic");
                    else
                        demolition.StatusPhotos.Add(row.GetCell(6).ToString());
                    demolition.StatusPhotos.Add(row.GetCell(7).ToString());
                    demolitionDatas.Add(demolition);
                }
                var areaAll = demolitionDatas.Sum(s => s.MasterRoom) + demolitionDatas.Sum(s => s.SecondRoom) + demolitionDatas.Sum(s => s.StudyRoom);
                dataItem = new()
                {
                    Title = title,
                    AreaAll = areaAll,
                    DemolitionDatas = demolitionDatas
                };
                data.Add(dataItem);
            }
            return data;
        }

        /// <summary>
        /// 创建表格
        /// </summary>
        /// <returns></returns>
        public static string ModelToExecl(DemolitionDataOutList demolitionOutList, string tableName, string savePath)
        {
            //创建excel工作簿与单元格
            IWorkbook excelWork = new HSSFWorkbook();

            #region 创建标题

            //创建第一行的第一个单元格里标题数据
            ISheet sheetTable = excelWork.CreateSheet(tableName);
            var titleRow = sheetTable.CreateRow(0);
            titleRow.CreateCell(0);
            titleRow.HeightInPoints = 30;
            titleRow.Cells[0].SetCellValue(demolitionOutList.Title);
            //设置sheet标题样式
            ICellStyle titleStyle = excelWork.CreateCellStyle();
            titleStyle.Alignment = HorizontalAlignment.Center;
            titleStyle.VerticalAlignment = VerticalAlignment.Center;
            titleStyle.WrapText = true;
            //设置sheet标题字体
            IFont titleFont = excelWork.CreateFont();
            titleFont.FontHeightInPoints = 12;
            titleFont.IsBold = true;
            titleStyle.SetFont(titleFont);
            titleRow.GetCell(0).CellStyle = titleStyle;
            //合并单元格的方法:参数为起始行、终止行、起始列、终止列
            int dataColumnCount = typeof(DemolitionDataOut).GetProperties().Length;
            sheetTable.AddMergedRegion(new CellRangeAddress(0, 0, 0, dataColumnCount));

            #endregion

            #region 创建表头

            //创建第二行表头的数据
            IRow headerRow = sheetTable.CreateRow(1);
            var columnName = typeof(DemolitionDataOut).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            for (int i = 0; i < dataColumnCount; i++)
            {
                headerRow.HeightInPoints = 25;
                headerRow.CreateCell(i).SetCellValue(columnName[i].Name);
                //样式
                ICellStyle headerStyle = excelWork.CreateCellStyle();
                sheetTable.AutoSizeColumn(i);
                headerStyle.Alignment = HorizontalAlignment.Center;
                headerStyle.VerticalAlignment = VerticalAlignment.Center;
                headerStyle.WrapText = true;
                //字体
                IFont headerFont = excelWork.CreateFont();
                headerFont.FontHeightInPoints = 12;
                headerFont.IsBold = true;
                headerStyle.SetFont(headerFont);
                headerRow.GetCell(i).CellStyle = headerStyle;
            }

            #endregion

            #region 创建内容

            int rowIndex = 2;
            ModelToTableHelper<DemolitionDataOut> convertModel = new();
            DataTable dt = convertModel.FillDataTable(demolitionOutList.DemolitionDatas);
            foreach (DataRow row in dt.Rows)
            {
                int columnIndex = 0;
                IRow contentRow = sheetTable.CreateRow(rowIndex);
                foreach (DataColumn column in dt.Columns)
                {
                    ICell datacell = contentRow.CreateCell(columnIndex);
                    IDataFormat format = excelWork.CreateDataFormat();
                    ICellStyle cellStyle = excelWork.CreateCellStyle();
                    //水平和垂直对齐
                    cellStyle.Alignment = HorizontalAlignment.Center;
                    cellStyle.VerticalAlignment = VerticalAlignment.Center;
                    cellStyle.DataFormat = format.GetFormat("text");
                    contentRow.GetCell(columnIndex).CellStyle = cellStyle;
                    datacell.SetCellValue(row[column.ColumnName].ToString());
                    sheetTable.AutoSizeColumn(columnIndex);
                    columnIndex++;
                }
            }
            //int dataRowCount = demolitionOutList.DemolitionDatas.Count + 2;
            int dataRowCount = dt.Rows.Count + 2;
            var summaryline = sheetTable.CreateRow(dataRowCount);
            summaryline.CreateCell(0);
            summaryline.Cells[0].SetCellValue("拆除合计:" + demolitionOutList.AreaAll + "㎡");


            #endregion

            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);
            using FileStream excelStream = File.OpenWrite(savePath);
            excelWork.Write(excelStream);

            return "";
        }

    }
}
