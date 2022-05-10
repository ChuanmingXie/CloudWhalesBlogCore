using CloudWhalesBlogCore.Services.Extensions;
using CloudWhalesBlogCore.Services.OfficeServices;
using CloudWhalesBlogCore.Win.ExcelHelper;
using CloudWhalesBlogCore.Win.WordHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudWhalesBlogCore.Win
{
    public partial class ClientToolsOffice : Form
    {
        public ClientToolsOffice()
        {
            InitializeComponent();
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            var openFile = new OpenFileDialog
            {
                Title = "请选择要处理的Excel文件"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                BtnOpenFile.Text = openFile.FileName;
                BtnOpenFile.ForeColor = Color.Black;
            }
        }

        private void BtnOutputDirectory_Click(object sender, EventArgs e)
        {
            var floder = new FolderBrowserDialog
            {
                Description = "请选择处理后保存位置"
            };
            if (floder.ShowDialog() == DialogResult.OK)
            {
                BtnOutputDirectory.Text = floder.SelectedPath;
                BtnOutputDirectory.ForeColor = Color.Black;
            }
        }

        private void BtnMergeDocument_Click(object sender, EventArgs e)
        {
            var floder = new FolderBrowserDialog
            {
                Description = "请选择需要合并的文件所在位置"
            };
            if (floder.ShowDialog() == DialogResult.OK)
            {
                BtnMergeDocument.Text = floder.SelectedPath;
                BtnMergeDocument.ForeColor = Color.Black;
            }
        }

        private void BtnMergeOutput_Click(object sender, EventArgs e)
        {
            var floder = new FolderBrowserDialog
            {
                Description = "请选择合并导出的位置"
            };
            if (floder.ShowDialog() == DialogResult.OK)
            {
                BtnMergeOutput.Text = floder.SelectedPath;
                BtnMergeOutput.ForeColor = Color.Black;
            }
        }

        private async void BtnExcelPhotos_Click(object sender, EventArgs e)
        {
            ExcelHandleSuper excelHelper = new HandleInOnlyImage(BtnOpenFile.Text);
            var photoItem = await Task.Run(() => excelHelper.ExcelSavePhotos(BtnOutputDirectory.Text));
            if (photoItem != null)
            {
                MessageBox.Show("导出成功!");
            }
            else
            {
                MessageBox.Show("导出失败!");
            }
        }

        private void BtnRandomPath_Click(object sender, EventArgs e)
        {
            var floder = new FolderBrowserDialog
            {
                Description = "请选择补充随机图片的位置"
            };
            if (floder.ShowDialog() == DialogResult.OK)
            {
                BtnRandomPath.Text = floder.SelectedPath;
                BtnRandomPath.ForeColor = Color.Black;
            }
        }

        private async void BtnSplitExcel_Click(object sender, EventArgs e)
        {
            ExcelHandleSuper excelHelper = new HandleInOnlyImage(BtnOpenFile.Text);
            var sheetItem = await Task.Run(() => excelHelper.ExcelSplitSheet(BtnOutputDirectory.Text));
            if (sheetItem != null)
            {
                MessageBox.Show("拆分成功!");
            }
            else
            {
                MessageBox.Show("拆分失败!");
            }
        }

        private void BtnMergeExcel_Click(object sender, EventArgs e)
        {
            ExcelHandleSuper excelHelper = new HandleInOnlyImage(BtnMergeDocument.Text);
            var mergeExcel = excelHelper.ExcelMultiMerge(BtnMergeOutput.Text);
            if (!string.IsNullOrEmpty(mergeExcel))
            {
                MessageBox.Show("合并成功!");
            }
            else
            {
                MessageBox.Show("合并失败!");
            }
        }

        private async void BtnWordPhotos_Click(object sender, EventArgs e)
        {
            DateTime startTime, endTime;
            TimeSpan time;
            startTime = DateTime.Now;

            //......任务......
            using ExcelXmlHelper excelXmlHelper = new(BtnOpenFile.Text);


            using ExcelHandleSuper excelHelper = ExcelHandleFactory.CreateExcelHandle(CbxHandleType.SelectedItem.ToString(), BtnOpenFile.Text);
            using WordHandleSuper wordHelper = WordHandleFactory.CreateWordHandle(CbxHandleType.SelectedItem.ToString(), BtnOpenFile.Text);

            var document = await Task.Run(
                () => wordHelper.CreateWordDocument(
                    BtnOutputDirectory.Text,
                    BtnRandomPath.Text,
                    excelHelper.GatherData()
                )
            );


            endTime = DateTime.Now;
            time = endTime - startTime;
            double runTime = time.TotalSeconds;

            if (document)
            {
                MessageBox.Show($"导出成功!用时:{runTime}");
            }
            else
            {
                MessageBox.Show($"导出失败!用时:{runTime}");
            }
        }


        private CancellationTokenSource _Cts; //任务取消令牌;
        private AutoResetEvent _AutoResetEvent = new AutoResetEvent(false);//参数传 false，则 WaitOne 时阻塞等待;


        private void BtnProgressbar_Click(object sender, EventArgs e)
        {

            _AutoResetEvent.Reset();
            string businessName = "业务1";

            ProgressbarForm progressWindow = new ProgressbarForm()
            {
                Text = "任务处理窗口",
            };

            progressWindow.SetColorfulTitle("业务1 ", Color.DarkOrange, true);
            progressWindow.SetColorfulTitle("正在执行中......", Color.Black);
            progressWindow.SetInfo(null, "", "");

            List<string> orders = new List<string>() { "订单1", "订单2", "订单3", "订单4", "订单5" }; //业务数据;
            List<string> leftList = orders.Select(x => x).ToList(); //剩余（未处理）数据;
            int successCount = 0; //成功数量;

            _Cts = new CancellationTokenSource();

            //注册一个将在取消此 CancellationToken 时调用的委托;

            _Cts.Token.Register(async () => await CancelRegister(orders, businessName, successCount, leftList));
            progressWindow.OperateAction += () => ProgressWindow_OperateAction(orders, businessName, successCount, leftList, progressWindow);
            progressWindow.AbortAction += () => _Cts.Cancel();

            var result = progressWindow.ShowDialog();

            int leftCount = orders.Count - successCount;
            if (result == DialogResult.OK || leftCount <= 0)
            {
                ShowInfo($"{businessName} 整体完成。");
            }
            else if (result == DialogResult.Abort)
            {
                //移到 _Cts.Token.Register 处一起判断，不然数目可能不准;
                //ShowInfo($"{businessName} 有 {leftCount} 项任务被终止，可在消息框中查看具体项。");
            }
        }

        private Task CancelRegister(List<string> orders, string businessName, int successCount, List<string> leftList)
        {

            ShowInfo("操作终止");
            return Task.Run(() =>
            {
                _AutoResetEvent.WaitOne(1000 * 5); //等待有可能还在执行的业务方法;

                if (successCount < orders.Count)
                {
                    ShowInfo($"{businessName} 有 {orders.Count - successCount} 项任务被终止，可在消息框中查看具体项。");

                    foreach (var leftName in leftList)
                    {
                        ShowInfo($"【{businessName}】的【{leftName}】执行失败，失败原因：【手动终止】。");
                    }
                }
            });



        }

        private void ProgressWindow_OperateAction(List<string> orders, string businessName, int successCount, List<string> leftList, ProgressbarForm progressWindow)
        {
            Task task = new(() =>
            {
                foreach (var order in orders)
                {
                    //判断是否被取消;
                    if (_Cts.Token.IsCancellationRequested)
                    {
                        break;
                    }

                    progressWindow.TryBeginInvoke(new Action(() =>
                    {
                        progressWindow.SetInfo(null, $"共{orders.Count}项，已执行{successCount}项", $"当前正在执行：{order}");
                    }));

                    if (BusinessMethod(order, businessName))
                    {
                        successCount++;
                        leftList.RemoveAll(x => x == order);

                        if (_Cts.Token.IsCancellationRequested)
                        {
                            _AutoResetEvent.Set(); //放行 Register 委托处的等待;
                        }
                    }

                    progressWindow.TryBeginInvoke(new Action(() =>
                    {
                        progressWindow.SetProgress(orders.IndexOf(order) + 1, orders.Count);
                    }));
                }

                //正常结束则延迟一段时间来让进度条走完;
                if (!_Cts.Token.IsCancellationRequested)
                {
                    Thread.Sleep(200);
                }
            }, _Cts.Token);

            task.Start();
            task.Wait();
        }

        private void ShowInfo(string messgae)
        {
            TxtMessage.TryBeginInvoke(new Action(() =>
            {
                TxtMessage.AppendText($"{messgae}\r\n\r\n");
            }));
        }
        private bool BusinessMethod(string order, string businessName)
        {
            string errStr = $"【{businessName}】的 {order} 任务失败，失败原因：";

            //测试
            Thread.Sleep(1000 * 2);

            try
            {
                //业务方法;

                ShowInfo($"【{businessName}】的 {order} 任务执行成功。");
                return true;
            }
            catch (Exception ex)
            {
                ShowInfo($"{errStr}{ex.Message}");
            }

            return false;
        }

    }
}
