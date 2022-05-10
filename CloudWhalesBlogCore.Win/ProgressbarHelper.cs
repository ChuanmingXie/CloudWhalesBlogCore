/*****************************************************************************
*项目名称:CloudWhalesBlogCore.Win
*项目描述:
*类 名 称:ProgressbarHelper
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/8 19:01:38
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using CloudWhalesBlogCore.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudWhalesBlogCore.Win
{
    class ProgressbarHelper
    {
        private readonly string title;

        public ProgressbarHelper(string title)
        {
            this.title = title;
        }

        public ProgressbarHelper()
        {

        }
        #region 测试任务进度条弹窗

        private CancellationTokenSource _Cts; //任务取消令牌;
        private AutoResetEvent _AutoResetEvent = new AutoResetEvent(false);//参数传 false，则 WaitOne 时阻塞等待;


        /// <summary>
        /// 测试任务进度弹窗
        /// </summary>
        private void ShowProgressDialog(object sender, EventArgs e)
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
            _Cts.Token.Register(async () =>
            {
                MessageBox.Show("操作终止");

                await Task.Run(() =>
                {
                    _AutoResetEvent.WaitOne(1000 * 5); //等待有可能还在执行的业务方法;

                    if (successCount < orders.Count)
                    {
                        MessageBox.Show($"{businessName} 有 {orders.Count - successCount} 项任务被终止，可在消息框中查看具体项。");

                        foreach (var leftName in leftList)
                        {
                            MessageBox.Show($"【{businessName}】的【{leftName}】执行失败，失败原因：【手动终止】。");
                        }
                    }
                });

            });

            progressWindow.OperateAction += () =>
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
                }, _Cts.Token);

                task.Start();
                task.Wait();
            };

            progressWindow.AbortAction += () =>
            {
                _Cts.Cancel();
            };

            var result = progressWindow.ShowDialog();
            int leftCount = orders.Count - successCount;
            if (result == DialogResult.OK || leftCount <= 0)
            {
                MessageBox.Show($"{businessName} 整体完成。");
            }
            else if (result == DialogResult.Abort)
            {
                //移到 _Cts.Token.Register 处一起判断，不然数目可能不准;
                //ShowInfo($"{businessName} 有 {leftCount} 项任务被终止，可在消息框中查看具体项。");
            }
        }

        /// <summary>
        /// 业务处理方法
        /// </summary>
        private bool BusinessMethod(string order, string businessName)
        {
            string errStr = $"【{businessName}】的 {order} 任务失败，失败原因：";

            //测试
            Thread.Sleep(1000 * 2);

            try
            {
                //业务方法;

                MessageBox.Show($"【{businessName}】的 {order} 任务执行成功。");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{errStr}{ex.Message}");
            }

            return false;
        }

        #endregion
    }
}
