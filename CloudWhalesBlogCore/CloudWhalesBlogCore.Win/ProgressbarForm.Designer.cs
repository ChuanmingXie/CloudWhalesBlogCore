
namespace CloudWhalesBlogCore.Win
{
    partial class ProgressbarForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TlpanelMian = new System.Windows.Forms.TableLayoutPanel();
            this.RichtxtboxTitle = new System.Windows.Forms.RichTextBox();
            this.LblCurrentInfo = new System.Windows.Forms.Label();
            this.LblTotalInfo = new System.Windows.Forms.Label();
            this.ProgressBarCustom = new CloudWhalesBlogCore.Win.CustomProgressbar();
            this.TlpanelAbort = new System.Windows.Forms.TableLayoutPanel();
            this.BtnAbort = new System.Windows.Forms.Button();
            this.TlpanelMian.SuspendLayout();
            this.TlpanelAbort.SuspendLayout();
            this.SuspendLayout();
            // 
            // TlpanelMian
            // 
            this.TlpanelMian.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TlpanelMian.ColumnCount = 1;
            this.TlpanelMian.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TlpanelMian.Controls.Add(this.RichtxtboxTitle, 0, 0);
            this.TlpanelMian.Controls.Add(this.LblCurrentInfo, 0, 3);
            this.TlpanelMian.Controls.Add(this.LblTotalInfo, 0, 1);
            this.TlpanelMian.Controls.Add(this.ProgressBarCustom, 0, 2);
            this.TlpanelMian.Location = new System.Drawing.Point(132, 31);
            this.TlpanelMian.Name = "TlpanelMian";
            this.TlpanelMian.RowCount = 4;
            this.TlpanelMian.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.TlpanelMian.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TlpanelMian.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.TlpanelMian.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.TlpanelMian.Size = new System.Drawing.Size(683, 272);
            this.TlpanelMian.TabIndex = 0;
            // 
            // RichtxtboxTitle
            // 
            this.RichtxtboxTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.RichtxtboxTitle.BackColor = System.Drawing.SystemColors.Control;
            this.RichtxtboxTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichtxtboxTitle.Location = new System.Drawing.Point(3, 3);
            this.RichtxtboxTitle.Name = "RichtxtboxTitle";
            this.RichtxtboxTitle.ReadOnly = true;
            this.RichtxtboxTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RichtxtboxTitle.Size = new System.Drawing.Size(677, 75);
            this.RichtxtboxTitle.TabIndex = 0;
            this.RichtxtboxTitle.Text = "业务1 任务批量执行中...";
            this.RichtxtboxTitle.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RichtxtboxTitle_KeyUp);
            this.RichtxtboxTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RichtxtboxTitle_MouseDown);
            this.RichtxtboxTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RichtxtboxTitle_MouseMove);
            // 
            // LblCurrentInfo
            // 
            this.LblCurrentInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblCurrentInfo.Location = new System.Drawing.Point(3, 216);
            this.LblCurrentInfo.Name = "LblCurrentInfo";
            this.LblCurrentInfo.Size = new System.Drawing.Size(677, 56);
            this.LblCurrentInfo.TabIndex = 1;
            this.LblCurrentInfo.Text = "当前正在执行：001号任务";
            this.LblCurrentInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTotalInfo
            // 
            this.LblTotalInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblTotalInfo.Location = new System.Drawing.Point(3, 81);
            this.LblTotalInfo.Name = "LblTotalInfo";
            this.LblTotalInfo.Size = new System.Drawing.Size(677, 54);
            this.LblTotalInfo.TabIndex = 1;
            this.LblTotalInfo.Text = "共5项 已执行3项";
            this.LblTotalInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProgressBarCustom
            // 
            this.ProgressBarCustom.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ProgressBarCustom.Location = new System.Drawing.Point(3, 152);
            this.ProgressBarCustom.Name = "ProgressBarCustom";
            this.ProgressBarCustom.Size = new System.Drawing.Size(677, 46);
            this.ProgressBarCustom.TabIndex = 2;
            // 
            // TlpanelAbort
            // 
            this.TlpanelAbort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TlpanelAbort.ColumnCount = 1;
            this.TlpanelAbort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TlpanelAbort.Controls.Add(this.BtnAbort, 0, 0);
            this.TlpanelAbort.Location = new System.Drawing.Point(132, 357);
            this.TlpanelAbort.Name = "TlpanelAbort";
            this.TlpanelAbort.RowCount = 1;
            this.TlpanelAbort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TlpanelAbort.Size = new System.Drawing.Size(683, 80);
            this.TlpanelAbort.TabIndex = 1;
            // 
            // BtnAbort
            // 
            this.BtnAbort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnAbort.Location = new System.Drawing.Point(266, 17);
            this.BtnAbort.Name = "BtnAbort";
            this.BtnAbort.Size = new System.Drawing.Size(150, 46);
            this.BtnAbort.TabIndex = 0;
            this.BtnAbort.Text = "中止";
            this.BtnAbort.UseVisualStyleBackColor = true;
            this.BtnAbort.Click += new System.EventHandler(this.BtnAbort_Click);
            // 
            // ProgressbarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 467);
            this.ControlBox = false;
            this.Controls.Add(this.TlpanelAbort);
            this.Controls.Add(this.TlpanelMian);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressbarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProgressbarForm";
            this.Load += new System.EventHandler(this.ProgressbarForm_Load);
            this.TlpanelMian.ResumeLayout(false);
            this.TlpanelAbort.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TlpanelMian;
        private System.Windows.Forms.RichTextBox RichtxtboxTitle;
        private System.Windows.Forms.Label LblCurrentInfo;
        private System.Windows.Forms.Label LblTotalInfo;
        private CustomProgressbar ProgressBarCustom;
        private System.Windows.Forms.TableLayoutPanel TlpanelAbort;
        private System.Windows.Forms.Button BtnAbort;
    }
}