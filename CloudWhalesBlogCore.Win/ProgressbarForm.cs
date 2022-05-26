using CloudWhalesBlogCore.Shared.NLogger;
using CloudWhalesBlogCore.Win.Extensions;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudWhalesBlogCore.Win
{
    public partial class ProgressbarForm : Form
    {
        /// <summary>
        /// 执行操作事件
        /// </summary>
        public event Action OperateAction;

        /// <summary>
        /// 终止操作事件
        /// </summary>

        public event Action AbortAction;

        public ProgressbarForm()
        {
            InitializeComponent();
        }

        private void ProgressbarForm_Load(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                OperateAction?.Invoke();
                DialogResult = DialogResult.Abort;
            });
        }

        private void BtnAbort_Click(object sender, EventArgs e)
        {
            AbortAction?.Invoke();
            DialogResult = DialogResult.Abort;
        }

        public void SetInfo(string refTitleContent=null,string totalMessage=null,string cueerntMessage = null)
        {
            if (refTitleContent != null) RichtxtboxTitle.Text = refTitleContent;
            if (totalMessage != null) LblTotalInfo.Text = totalMessage;
            if (cueerntMessage != null) LblCurrentInfo.Text = cueerntMessage;
        }

        /// <summary>
        /// 设置彩色标题内容
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="color">颜色</param>
        /// <param name="isNew">是否先清空</param>
        public void SetColorfulTitle(string text, Color color, bool isNew = false)
        {
            if (isNew) RichtxtboxTitle.Clear();
            RichtxtboxTitle.AppendTextColorful(text, color, false);
            RichtxtboxTitle.SelectionAlignment = HorizontalAlignment.Center;
        }


        public void SetProgress(double currentValue,double totalValue)
        {
            try
            {
                ProgressBarCustom.Value = (int)(currentValue / totalValue * 100);
            }
            catch (Exception ex)
            {
                NLogHelper._.Error(ex.Message,ex);
            }
        }

        #region 隐藏 RichTextBox 光标

        /// <summary>
        /// 隐藏光标
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32", EntryPoint = "HideCaret")]
        private static extern bool HideCaret(IntPtr hWnd);

        private void RichtxtboxTitle_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(((RichTextBox)sender).Handle);
        }

        private void RichtxtboxTitle_MouseMove(object sender, MouseEventArgs e)
        {
            HideCaret(((RichTextBox)sender).Handle);
        }

        private void RichtxtboxTitle_KeyUp(object sender, KeyEventArgs e)
        {
            HideCaret(((RichTextBox)sender).Handle);
        }

        #endregion
    }
}
