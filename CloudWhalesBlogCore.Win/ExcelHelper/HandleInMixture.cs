/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Win.ExcelHelper
*项目描述:
*类 名 称:HandleMixture
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 17:51:01
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Services.Extensions;
using CloudWhalesBlogCore.Services.OfficeServices;
using CloudWhalesBlogCore.Shared.Common.DataTableHelper;
using CloudWhalesBlogCore.Shared.DTO.Output;
using CloudWhalesBlogCore.Shared.NLogger;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Win.ExcelHelper
{
    public class HandleInMixture : ExcelHandleSuper
    {
        public HandleInMixture(string excelPath) : base(excelPath)
        {
        }

        public override List<HouseParamOutList> GatherData()
        {
            resetEvent.Reset();

            ProgressbarForm progressbar = new() { Text = "读取Excel数据窗口" };
            progressbar.SetColorfulTitle("读取数据  ", Color.DarkOrange, true);
            progressbar.SetColorfulTitle("正在执行中...", Color.Black);
            progressbar.SetInfo(null, "", "");

            ExcelXmlHelper excelXmlHelper = new(excelPath);
            var photoList = excelXmlHelper.XMLPhotoList();

            List<HouseParamOutList> dataAllList = new();

            using DataTableWithExcel tableExcelHelper = new(excelPath);
            var sheetDic = tableExcelHelper.ReturnSheetList();

            cancelTokenSource.Token.Register(async () => await CancelGather());
            progressbar.AbortAction += () => cancelTokenSource.Cancel();
            progressbar.OperateAction += () =>
            {
                Task task = new(() =>
                {
                    try
                    {
                        foreach (var item in sheetDic)
                        {
                            if (item.Key == 16) continue;

                            int rowIndex = 1;
                            DataTable dtCurrent = tableExcelHelper.ExcelToDataTable(item.Key);
                            List<HouseParamOut> HouseParamList = new();

                            foreach (DataRow row in dtCurrent.Rows)
                            {
                                if (rowIndex++ == 1 || row[4].ToString().Equals("合计") || row[5].ToString().Equals("合计")) continue;
                                if (row.ItemArray.Length != 10)
                                {
                                    HouseParamOut demolition = new()
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
                                            item.Key == 0 ? FindImage(rowIndex - 1, photoList) : FindImage(row[6].ToString(), photoList),
                                        }
                                    };
                                    demolition.DemolitionArea = demolition.MasterRoom + demolition.SecondRoom + demolition.StudyRoom;
                                    HouseParamList.Add(demolition);
                                }
                                else
                                {
                                    HouseParamOut demolition = new()
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
                                    HouseParamList.Add(demolition);
                                }

                                progressbar.TryBeginInvoke(new Action(() =>
                                {
                                    progressbar.SetInfo(item.Value, $"共{dtCurrent.Rows.Count}项，已执行{rowIndex - 1}项", $"当前正在执行：{rowIndex}");
                                }));
                                Thread.Sleep(2);
                                progressbar.TryBeginInvoke(new Action(() =>
                                {
                                    progressbar.SetProgress(rowIndex - 1, dtCurrent.Rows.Count);
                                }));
                            }
                            HouseParamOutList dataListItem = new()
                            {
                                Title = dtCurrent.TableName,
                                AreaAll = HouseParamList.Sum(s => s.MasterRoom + s.SecondRoom + s.StudyRoom),
                                HouseParams = HouseParamList
                            };
                            dataAllList.Add(dataListItem);
                        }
                    }
                    catch (Exception ex)
                    {
                        NLogHelper._.Error(ex.Message, ex);
                    }
                }, cancelTokenSource.Token);
                task.Start();
                task.Wait();
            };

            var result = progressbar.ShowDialog();
            return dataAllList;
        }

        private async Task CancelGather()
        {
            await Task.Run(() =>
            {
                resetEvent.WaitOne(1000 * 5);
            });
        }


        /// <summary>
        /// 根据Id查找
        /// </summary>
        /// <param name="orgionPicId"></param>
        /// <returns></returns>
        private string FindImage(string orgionPicId, List<Tuple<int, int, string, string, string>> photoList)
        {
            if (string.IsNullOrEmpty(orgionPicId)) return string.Empty;
            Regex regPId = new("(?<=\").*?(?=\")", RegexOptions.None);  //选取双引号中的内容
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
        private string FindImage(int picRow, List<Tuple<int, int, string, string, string>> photoList)
        {
            if (picRow == 0) return string.Empty;

            var photo = photoList.Find(x => x.Item1 == picRow);
            return photo.Item5;
            //return ";" + photo.Item5 + ";" + photo.Item4;
        }


        public override string CreateExcel(HouseParamOutList houseOutList, string tableName, string savePath)
        {
            //创建excel工作簿与单元格
            IWorkbook excelWork = new HSSFWorkbook();

            #region 创建标题

            //创建第一行的第一个单元格里标题数据
            ISheet sheetTable = excelWork.CreateSheet(tableName);
            var titleRow = sheetTable.CreateRow(0);
            titleRow.CreateCell(0);
            titleRow.HeightInPoints = 30;
            titleRow.Cells[0].SetCellValue(houseOutList.Title);
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
            int dataColumnCount = typeof(HouseParamOut).GetProperties().Length;
            sheetTable.AddMergedRegion(new CellRangeAddress(0, 0, 0, dataColumnCount));

            #endregion

            #region 创建表头

            //创建第二行表头的数据
            IRow headerRow = sheetTable.CreateRow(1);
            var columnName = typeof(HouseParamOut).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
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
            ModelToDataTable<HouseParamOut> convertModel = new();
            DataTable dt = convertModel.FillDataTable(houseOutList.HouseParams);
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
            summaryline.Cells[0].SetCellValue("拆除合计:" + houseOutList.AreaAll + "㎡");


            #endregion

            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);
            using FileStream excelStream = File.OpenWrite(savePath);
            excelWork.Write(excelStream);

            return string.Empty;
        }

    }
}
