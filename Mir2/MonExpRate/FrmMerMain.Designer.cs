namespace Mir2.MonExpRate
{
    partial class FrmMerMain
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
            this.components = new System.ComponentModel.Container();
            this.navMain = new Sunny.UI.UINavMenu();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.plVersion = new Sunny.UI.UIPanel();
            this.plTime = new Sunny.UI.UIPanel();
            this.plMsg = new Sunny.UI.UIPanel();
            this.uiPanel5 = new Sunny.UI.UIPanel();
            this.uiPanel3 = new Sunny.UI.UIPanel();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.Tab = new Sunny.UI.UITabControl();
            this.uiPanel2.SuspendLayout();
            this.plMsg.SuspendLayout();
            this.uiPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // navMain
            // 
            this.navMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.navMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navMain.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.navMain.Font = new System.Drawing.Font("宋体", 12F);
            this.navMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.navMain.FullRowSelect = true;
            this.navMain.HotTracking = true;
            this.navMain.ItemHeight = 50;
            this.navMain.Location = new System.Drawing.Point(0, 0);
            this.navMain.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.navMain.Name = "navMain";
            this.navMain.ShowLines = false;
            this.navMain.ShowPlusMinus = false;
            this.navMain.ShowRootLines = false;
            this.navMain.Size = new System.Drawing.Size(200, 570);
            this.navMain.TabIndex = 22;
            this.navMain.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.navMain.MenuItemClick += new Sunny.UI.UINavMenu.OnMenuItemClick(this.navMain_MenuItemClick);
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.navMain);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiPanel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel2.Location = new System.Drawing.Point(0, 0);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.Radius = 1;
            this.uiPanel2.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.uiPanel2.Size = new System.Drawing.Size(200, 570);
            this.uiPanel2.TabIndex = 1;
            this.uiPanel2.Text = null;
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // plVersion
            // 
            this.plVersion.Dock = System.Windows.Forms.DockStyle.Left;
            this.plVersion.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plVersion.Location = new System.Drawing.Point(0, 0);
            this.plVersion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.plVersion.MinimumSize = new System.Drawing.Size(1, 1);
            this.plVersion.Name = "plVersion";
            this.plVersion.Radius = 1;
            this.plVersion.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.plVersion.Size = new System.Drawing.Size(200, 30);
            this.plVersion.TabIndex = 0;
            this.plVersion.Text = null;
            this.plVersion.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // plTime
            // 
            this.plTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.plTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plTime.Location = new System.Drawing.Point(800, 0);
            this.plTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.plTime.MinimumSize = new System.Drawing.Size(1, 1);
            this.plTime.Name = "plTime";
            this.plTime.Radius = 1;
            this.plTime.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.plTime.Size = new System.Drawing.Size(200, 30);
            this.plTime.TabIndex = 1;
            this.plTime.Text = null;
            this.plTime.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // plMsg
            // 
            this.plMsg.Controls.Add(this.uiPanel5);
            this.plMsg.Controls.Add(this.uiPanel3);
            this.plMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMsg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plMsg.Location = new System.Drawing.Point(200, 0);
            this.plMsg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.plMsg.MinimumSize = new System.Drawing.Size(1, 1);
            this.plMsg.Name = "plMsg";
            this.plMsg.Radius = 1;
            this.plMsg.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.plMsg.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.plMsg.Size = new System.Drawing.Size(600, 30);
            this.plMsg.TabIndex = 3;
            this.plMsg.Text = null;
            this.plMsg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiPanel5
            // 
            this.uiPanel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiPanel5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel5.Location = new System.Drawing.Point(135, 0);
            this.uiPanel5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel5.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel5.Name = "uiPanel5";
            this.uiPanel5.Radius = 1;
            this.uiPanel5.Size = new System.Drawing.Size(135, 30);
            this.uiPanel5.TabIndex = 1;
            this.uiPanel5.Text = "物品数量：0";
            this.uiPanel5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiPanel3
            // 
            this.uiPanel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiPanel3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel3.Location = new System.Drawing.Point(0, 0);
            this.uiPanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel3.Name = "uiPanel3";
            this.uiPanel3.Radius = 1;
            this.uiPanel3.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel3.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.uiPanel3.Size = new System.Drawing.Size(135, 30);
            this.uiPanel3.TabIndex = 0;
            this.uiPanel3.Text = "怪物数量：0";
            this.uiPanel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.plMsg);
            this.uiPanel1.Controls.Add(this.plTime);
            this.uiPanel1.Controls.Add(this.plVersion);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiPanel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel1.Location = new System.Drawing.Point(0, 570);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(1000, 30);
            this.uiPanel1.TabIndex = 0;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Tab
            // 
            this.Tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tab.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.Tab.FillColor = System.Drawing.Color.Black;
            this.Tab.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Tab.ItemSize = new System.Drawing.Size(0, 1);
            this.Tab.Location = new System.Drawing.Point(200, 0);
            this.Tab.MainPage = "主页";
            this.Tab.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.Tab.Name = "Tab";
            this.Tab.SelectedIndex = 0;
            this.Tab.ShowCloseButton = true;
            this.Tab.Size = new System.Drawing.Size(800, 570);
            this.Tab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.Tab.TabBackColor = System.Drawing.Color.Wheat;
            this.Tab.TabIndex = 23;
            this.Tab.TabVisible = false;
            this.Tab.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // FrmMerMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.Tab);
            this.Controls.Add(this.uiPanel2);
            this.Controls.Add(this.uiPanel1);
            this.Name = "FrmMerMain";
            this.Text = "FrmMerMain";
            this.Load += new System.EventHandler(this.FrmMerMain_Load);
            this.uiPanel2.ResumeLayout(false);
            this.plMsg.ResumeLayout(false);
            this.uiPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UINavMenu navMain;
        private Sunny.UI.UIPanel uiPanel2;
        private System.Windows.Forms.Timer timer1;
        private Sunny.UI.UIPanel plVersion;
        private Sunny.UI.UIPanel plTime;
        private Sunny.UI.UIPanel plMsg;
        private Sunny.UI.UIPanel uiPanel5;
        private Sunny.UI.UIPanel uiPanel3;
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UITabControl Tab;
    }
}