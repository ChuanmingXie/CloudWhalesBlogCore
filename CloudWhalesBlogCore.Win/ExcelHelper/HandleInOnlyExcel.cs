/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Win.ExcelHelper
*项目描述:
*类 名 称:HandleOnlyExcel
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/9 17:57:38
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Win.ExcelHelper
{
    public class HandleInOnlyExcel : ExcelHandleSuper
    {
        public HandleInOnlyExcel(string excelPath) : base(excelPath)
        {

        }

        /// <summary>
        /// 收集数据
        /// </summary>
        /// <returns></returns>
        public override List<HouseParamOutList> GatherData()
        {
            resetEvent.Reset();

            ProgressbarForm progressbar = new() { Text = "读取Excel数据窗口" };
            progressbar.SetColorfulTitle("读取数据  ", Color.DarkOrange, true);
            progressbar.SetColorfulTitle("正在执行中...", Color.Black);
            progressbar.SetInfo(null, "", "");

            ExcelXmlHelper excelXmlHelper = new(excelPath);
            var photoList = excelXmlHelper.XMLPhotoList();

            using DataTableWithExcel tableExcelHelper = new(excelPath);
            var sheetDic = tableExcelHelper.ReturnSheetList();

            Regex numGet = new(@"[0-9]+", RegexOptions.None);

            List<HouseParamOutList> dataAllList = new();

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
                            if (cancelTokenSource.IsCancellationRequested)
                            {
                                break;
                            }
                            DataTable dtCurrent = tableExcelHelper.ExcelToDataTable(item.Key);
                            List<HouseParamOut> HouseParamList = new();

                            for (int i = 1; i < dtCurrent.Rows.Count; i += 4)
                            {
                                if (string.IsNullOrEmpty(dtCurrent.Rows[i + 1][0].ToString())) continue;

                                //在此处困住好几个小时,未初始化所有元素
                                HouseParamOut[] houseParam = new HouseParamOut[2];

                                //左侧的列数据
                                houseParam[0] = new HouseParamOut()
                                {
                                    BuildingNum = dtCurrent.Rows[i + 1][0].ToString(),
                                    RoomNum = dtCurrent.Rows[i + 1][1].ToString(),
                                    MasterRoom = decimal.Parse(numGet.Match(dtCurrent.Rows[i][3].ToString()).Value == "" ? "0" : numGet.Match(dtCurrent.Rows[i][3].ToString()).Value),
                                    SecondRoom = decimal.Parse(numGet.Match(dtCurrent.Rows[i + 1][3].ToString()).Value == "" ? "0" : numGet.Match(dtCurrent.Rows[i + 1][3].ToString()).Value),
                                    StudyRoom = decimal.Parse(numGet.Match(dtCurrent.Rows[i + 2][3].ToString()).Value == "" ? "0" : numGet.Match(dtCurrent.Rows[i + 2][3].ToString()).Value),
                                    StatusPhotos = new()
                                    {
                                        FindImage(i, i + 5, 3, 8, photoList),
                                    }
                                };

                                //右侧的列数据
                                houseParam[1] = new HouseParamOut()
                                {
                                    BuildingNum = dtCurrent.Rows[i + 1][9].ToString(),
                                    RoomNum = dtCurrent.Rows[i + 1][10].ToString(),
                                    MasterRoom = decimal.Parse(numGet.Match(dtCurrent.Rows[i][12].ToString()).Value == "" ? "0" : numGet.Match(dtCurrent.Rows[i][12].ToString()).Value),
                                    SecondRoom = decimal.Parse(numGet.Match(dtCurrent.Rows[i + 1][12].ToString()).Value == "" ? "0" : numGet.Match(dtCurrent.Rows[i + 1][12].ToString()).Value),
                                    StudyRoom = decimal.Parse(numGet.Match(dtCurrent.Rows[i + 2][12].ToString()).Value == "" ? "0" : numGet.Match(dtCurrent.Rows[i + 2][12].ToString()).Value),
                                    StatusPhotos = new()
                                    {
                                        FindImage(i, i + 5, 12, 18, photoList),
                                    }
                                };

                                HouseParamList.AddRange(houseParam);

                                progressbar.TryBeginInvoke(new Action(() =>
                                {
                                    progressbar.SetInfo(item.Value, $"共{dtCurrent.Rows.Count}项，已执行{i - 1}项", $"当前正在执行：{i}");
                                }));
                                Thread.Sleep(2);
                                progressbar.TryBeginInvoke(new Action(() =>
                                {
                                    progressbar.SetProgress(i, dtCurrent.Rows.Count);
                                }));
                            }
                            HouseParamOutList dataListItem = new()
                            {
                                Title = dtCurrent.TableName,
                                AreaAll = HouseParamList.Sum(s => s.MasterRoom + s.SecondRoom + s.SecondRoom2 + s.StudyRoom + s.DemolitionArea),
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
        /// 根据表格行列查找
        /// </summary>
        /// <param name="piccolumns"></param>
        /// <returns></returns>
        private string FindImage(int rows1, int rows2, int columns1, int columns2, List<Tuple<int, int, string, string, string>> photoList)
        {

            var photo = photoList.FindAll(x => (x.Item1 > rows1 && x.Item1 < rows2)).FindAll(x => (x.Item2 > columns1 && x.Item2 < columns2));
            return string.Join("#", photo.Select(x => x.Item5));
        }

        public override string CreateExcel(HouseParamOutList demolitionOutList, string tableName, string savePath)
        {
            throw new NotImplementedException();
        }

    }
}
