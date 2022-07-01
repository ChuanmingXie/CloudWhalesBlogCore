using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudWhalesBlogCore.Win
{
    public partial class CustomProgressbar : ProgressBar
    {
        public CustomProgressbar()
        {
            base.SetStyle(ControlStyles.UserPaint|ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Rectangle bounds = new (0, 0, base.Width, base.Height);
            bounds.Height -= 4;
            bounds.Width = ((int)(bounds.Width * (((double)base.Value) / ((double)base.Maximum)))) - 4;
            SolidBrush brush = new (Color.DeepSkyBlue);
            pe.Graphics.FillRectangle(brush, 2, 2, bounds.Width, bounds.Height);
        }
    }
}
