using Mir.Core.LegendEngine;
using Mir.Core.utils;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mir2.Script
{
    public partial class FrmRightTip : Form
    {
        private FrmMain frmMain;
        public FrmRightTip()
        {
            InitializeComponent();
        }
        public FrmRightTip(string text, FrmMain fm)
        {
            InitializeComponent();

            this.frmMain = fm;

            this.StartPosition = FormStartPosition.Manual;
            this.TopMost = true; // 可选，保持在顶层
            
            string val = BlueCompletionData.CompletionDataList.Where(s => s.Text == text).Select(s => s.Description).FirstOrDefault().ToString();
            this.lbContent.Text = "\r\n" + val;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 吸附窗体
        /// </summary>
        public void Attach()
        {
            // 计算吸附位置
            Rectangle attachedBounds = frmMain.Bounds;
            Point attachPoint = new Point(
                attachedBounds.Right, // 窗体之间的水平间隔
                attachedBounds.Top + 30    // 垂直偏移量，根据需要调整
            );
            this.Width = 300;
            this.Height = frmMain.Height - 60;

            // 设置吸附窗体的位置
            this.DesktopLocation = attachPoint;
            this.Show();
        }

        private void lbContent_Resize(object sender, EventArgs e)
        {
            GetFrmType(this, 25, 0.1);
        }
        private void lbContent_Paint(object sender, PaintEventArgs e)
        {
            GetFrmType(this, 25, 0.1);
        }
        private void GetFrmType(Control sender, int p_1, double p_2)
        {
            GraphicsPath oPath = new GraphicsPath();
            oPath.AddClosedCurve(new Point[] { new Point(0, sender.Height / p_1), new Point(sender.Width / p_1, 0), new Point(sender.Width - sender.Width / p_1, 0), new Point(sender.Width, sender.Height / p_1), new Point(sender.Width, sender.Height - sender.Height / p_1), new Point(sender.Width - sender.Width / p_1, sender.Height), new Point(sender.Width / p_1, sender.Height), new Point(0, sender.Height - sender.Height / p_1) }, (float)p_2);
            sender.Region = new Region(oPath);
        }

        //圆角
        [DllImport("kernel32.dll")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
         (int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        //阴影
        private const int CS_DropSHADOW = 0x20000;
        private const int GCL_STYLE = (-26);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        private void FrmRightTip_Load(object sender, EventArgs e)
        {
            //圆角
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            //阴影
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
        }
    }
}
