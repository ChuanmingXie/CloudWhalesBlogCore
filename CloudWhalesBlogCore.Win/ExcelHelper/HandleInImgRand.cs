/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Win.ExcelHelper
*项目描述:
*类 名 称:HandleInImgRand
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/10 18:21:48
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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudWhalesBlogCore.Win.ExcelHelper
{
    public class HandleInImgRand : ExcelHandleSuper
    {
        public HandleInImgRand(string excelPath) : base(excelPath)
        {
        }

        public override string CreateExcel(HouseParamOutList demolitionOutList, string tableName, string savePath)
        {
            throw new NotImplementedException();
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
                            int rowIndex = 0;
                            DataTable dtCurrent = tableExcelHelper.ExcelToDataTable(item.Key);
                            List<HouseParamOut> HouseParamList = new();

                            foreach (DataRow row in dtCurrent.Rows)
                            {
                                if (rowIndex++ < 2 || row.ItemArray.Where(x => x.ToString().Contains("合计")).Any()) continue;
                                //3列和4列在表格中是公式等于2列
                                HouseParamOut demolition = new()
                                {
                                    BuildingNum = row[0].ToString(),
                                    RoomNum = row[1].ToString(),
                                    MasterRoom = Convert.ToDecimal(!string.IsNullOrEmpty(row[2].ToString()) ? row[2].ToString() : 0),
                                    SecondRoom = Convert.ToDecimal(!string.IsNullOrEmpty(row[3].ToString()) ? row[3].ToString() : 0),
                                    StudyRoom = Convert.ToDecimal(!string.IsNullOrEmpty(row[4].ToString()) ? row[4].ToString() : 0),
                                };
                                HouseParamList.Add(demolition);

                                progressbar.TryBeginInvoke(new Action(() =>
                                {
                                    progressbar.SetInfo(item.Value, $"共{dtCurrent.Rows.Count}项，已执行{rowIndex - 1}项", $"当前正在执行：{rowIndex}");
                                }));
                                Thread.Sleep(2);
                                progressbar.TryBeginInvoke(new Action(() =>
                                {
                                    progressbar.SetProgress(rowIndex, dtCurrent.Rows.Count);
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
}
}
