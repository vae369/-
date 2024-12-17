using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mir.Core.controller
{
    public class CustomComboBox : ComboBox
    {
        private const int WM_PAINT = 0xF;
        [Browsable(true)]
        [Category("Appearance")]
        [Description("边框颜色")]
        public Color BorderColor { get; set; } = Color.FromArgb(204, 204, 204); // 默认边框颜色
        public CustomComboBox()
        {
            
        }

        public CustomComboBox(IContainer container)
        {
            container.Add(this);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT)
            {
                using (Graphics g = Graphics.FromHwnd(this.Handle))
                {
                    // 获取客户区的边界
                    Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                    // 绘制边框
                    g.DrawRectangle(new Pen(this.BorderColor), rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // 绘制边框颜色
            e.Graphics.DrawRectangle(new Pen(this.BorderColor), 0, 0, this.Width - 1, this.Height - 1);
        } 
    }
}
