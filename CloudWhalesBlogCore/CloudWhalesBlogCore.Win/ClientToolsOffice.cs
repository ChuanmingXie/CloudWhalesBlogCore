using CloudWhalesBlogCore.Services.Extensions;
using CloudWhalesBlogCore.Services.OfficeServices;
using CloudWhalesBlogCore.Shared.Common.ImageHelper;
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

        private void BtnProgressbar_Click(object sender, EventArgs e)
        {
            //var text=ImageAddWater.WordsOCR(@"D:\Software\2.png");
            //text+=ImageAddWater.WordsOCR(@"D:\Software\16-503.jpg");
            //MessageBox.Show(text);
        }

    }
}
