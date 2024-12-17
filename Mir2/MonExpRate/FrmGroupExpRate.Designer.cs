namespace Mir2.MonExpRate
{
    partial class FrmGroupExpRate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGroupExpRate));
            this.uiToolTip1 = new Sunny.UI.UIToolTip(this.components);
            this.plMsg = new Sunny.UI.UIPanel();
            this.plColRow = new Sunny.UI.UIPanel();
            this.uiPanel6 = new Sunny.UI.UIPanel();
            this.uiPanel3 = new Sunny.UI.UIPanel();
            this.Tab = new Sunny.UI.UITabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.uiRichTextBox1 = new Sunny.UI.UIRichTextBox();
            this.cmsMain = new Sunny.UI.UIContextMenuStrip();
            this.rbNew = new Sunny.UI.UIRadioButton();
            this.rbOld = new Sunny.UI.UIRadioButton();
            this.txtItems = new Sunny.UI.UITextBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.tud = new Sunny.UI.UIIntegerUpDown();
            this.uiPanel4 = new Sunny.UI.UIPanel();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.lbItems = new Sunny.UI.UIListBox();
            this.uiPanel5 = new Sunny.UI.UIPanel();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.tvGroup = new Sunny.UI.UITreeView();
            this.uiPanel8 = new Sunny.UI.UIPanel();
            this.btnRalease = new Sunny.UI.UIButton();
            this.uiPanel7 = new Sunny.UI.UIPanel();
            this.treeFileImage = new System.Windows.Forms.ImageList(this.components);
            this.cmsTab = new Sunny.UI.UIContextMenuStrip();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开所在文件夹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.格式化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.关闭所有ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.除此之外全部关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uiPanel6.SuspendLayout();
            this.uiPanel3.SuspendLayout();
            this.Tab.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.uiPanel4.SuspendLayout();
            this.uiGroupBox1.SuspendLayout();
            this.uiPanel5.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            this.uiPanel1.SuspendLayout();
            this.uiPanel8.SuspendLayout();
            this.cmsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiToolTip1
            // 
            this.uiToolTip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.uiToolTip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.uiToolTip1.OwnerDraw = true;
            // 
            // plMsg
            // 
            this.plMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMsg.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plMsg.Location = new System.Drawing.Point(135, 0);
            this.plMsg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.plMsg.MinimumSize = new System.Drawing.Size(1, 1);
            this.plMsg.Name = "plMsg";
            this.plMsg.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.plMsg.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
            this.plMsg.Size = new System.Drawing.Size(453, 30);
            this.plMsg.TabIndex = 1;
            this.plMsg.Text = null;
            this.plMsg.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // plColRow
            // 
            this.plColRow.Dock = System.Windows.Forms.DockStyle.Left;
            this.plColRow.Font = new System.Drawing.Font("宋体", 12F);
            this.plColRow.Location = new System.Drawing.Point(0, 0);
            this.plColRow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.plColRow.MinimumSize = new System.Drawing.Size(1, 1);
            this.plColRow.Name = "plColRow";
            this.plColRow.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.plColRow.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.plColRow.Size = new System.Drawing.Size(135, 30);
            this.plColRow.TabIndex = 0;
            this.plColRow.Text = "行 0 列 0";
            this.plColRow.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiPanel6
            // 
            this.uiPanel6.Controls.Add(this.plMsg);
            this.uiPanel6.Controls.Add(this.plColRow);
            this.uiPanel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiPanel6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel6.Location = new System.Drawing.Point(0, 551);
            this.uiPanel6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel6.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel6.Name = "uiPanel6";
            this.uiPanel6.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel6.Size = new System.Drawing.Size(588, 30);
            this.uiPanel6.TabIndex = 24;
            this.uiPanel6.Text = null;
            this.uiPanel6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiPanel3
            // 
            this.uiPanel3.Controls.Add(this.Tab);
            this.uiPanel3.Controls.Add(this.uiPanel6);
            this.uiPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel3.Location = new System.Drawing.Point(187, 0);
            this.uiPanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel3.Name = "uiPanel3";
            this.uiPanel3.Radius = 1;
            this.uiPanel3.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel3.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
            this.uiPanel3.Size = new System.Drawing.Size(588, 581);
            this.uiPanel3.TabIndex = 5;
            this.uiPanel3.Text = null;
            this.uiPanel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Tab
            // 
            this.Tab.Controls.Add(this.tabPage2);
            this.Tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tab.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.Tab.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Tab.ItemSize = new System.Drawing.Size(150, 33);
            this.Tab.Location = new System.Drawing.Point(0, 0);
            this.Tab.MainPage = "主页";
            this.Tab.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.Tab.Name = "Tab";
            this.Tab.SelectedIndex = 0;
            this.Tab.ShowCloseButton = true;
            this.Tab.Size = new System.Drawing.Size(588, 551);
            this.Tab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.Tab.TabIndex = 26;
            this.Tab.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Tab.SelectedIndexChanged += new System.EventHandler(this.Tab_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tabPage2.Controls.Add(this.uiRichTextBox1);
            this.tabPage2.Location = new System.Drawing.Point(0, 33);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(588, 518);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "主页";
            // 
            // uiRichTextBox1
            // 
            this.uiRichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiRichTextBox1.FillColor = System.Drawing.Color.Silver;
            this.uiRichTextBox1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiRichTextBox1.ForeColor = System.Drawing.Color.SaddleBrown;
            this.uiRichTextBox1.Location = new System.Drawing.Point(0, 0);
            this.uiRichTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiRichTextBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRichTextBox1.Name = "uiRichTextBox1";
            this.uiRichTextBox1.Padding = new System.Windows.Forms.Padding(2);
            this.uiRichTextBox1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiRichTextBox1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiRichTextBox1.ScrollBarStyleInherited = false;
            this.uiRichTextBox1.ShowText = false;
            this.uiRichTextBox1.Size = new System.Drawing.Size(588, 518);
            this.uiRichTextBox1.TabIndex = 1;
            this.uiRichTextBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmsMain
            // 
            this.cmsMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.cmsMain.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cmsMain.Name = "cmsMain";
            this.cmsMain.Size = new System.Drawing.Size(61, 4);
            // 
            // rbNew
            // 
            this.rbNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbNew.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbNew.Location = new System.Drawing.Point(104, 91);
            this.rbNew.MinimumSize = new System.Drawing.Size(1, 1);
            this.rbNew.Name = "rbNew";
            this.rbNew.Size = new System.Drawing.Size(80, 29);
            this.rbNew.TabIndex = 10;
            this.rbNew.Text = "新爆率";
            // 
            // rbOld
            // 
            this.rbOld.Checked = true;
            this.rbOld.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbOld.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbOld.Location = new System.Drawing.Point(12, 91);
            this.rbOld.MinimumSize = new System.Drawing.Size(1, 1);
            this.rbOld.Name = "rbOld";
            this.rbOld.Size = new System.Drawing.Size(77, 29);
            this.rbOld.TabIndex = 10;
            this.rbOld.Text = "旧爆率";
            // 
            // txtItems
            // 
            this.txtItems.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtItems.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtItems.Location = new System.Drawing.Point(12, 25);
            this.txtItems.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtItems.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtItems.Name = "txtItems";
            this.txtItems.Padding = new System.Windows.Forms.Padding(5);
            this.txtItems.Radius = 1;
            this.txtItems.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.txtItems.ShowText = false;
            this.txtItems.Size = new System.Drawing.Size(172, 29);
            this.txtItems.TabIndex = 1;
            this.txtItems.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtItems.Watermark = "";
            this.txtItems.TextChanged += new System.EventHandler(this.txtItems_TextChanged);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel1.Location = new System.Drawing.Point(10, 65);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(56, 23);
            this.uiLabel1.TabIndex = 9;
            this.uiLabel1.Text = "默认爆率";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tud
            // 
            this.tud.Font = new System.Drawing.Font("宋体", 12F);
            this.tud.Location = new System.Drawing.Point(63, 58);
            this.tud.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tud.Maximum = 99999999;
            this.tud.Minimum = 1;
            this.tud.MinimumSize = new System.Drawing.Size(100, 0);
            this.tud.Name = "tud";
            this.tud.ShowText = false;
            this.tud.Size = new System.Drawing.Size(121, 35);
            this.tud.TabIndex = 8;
            this.tud.Text = null;
            this.tud.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.tud.Value = 1;
            // 
            // uiPanel4
            // 
            this.uiPanel4.Controls.Add(this.uiGroupBox1);
            this.uiPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiPanel4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel4.Location = new System.Drawing.Point(0, 0);
            this.uiPanel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel4.Name = "uiPanel4";
            this.uiPanel4.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel4.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.uiPanel4.Size = new System.Drawing.Size(200, 136);
            this.uiPanel4.TabIndex = 4;
            this.uiPanel4.Text = null;
            this.uiPanel4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.txtItems);
            this.uiGroupBox1.Controls.Add(this.tud);
            this.uiGroupBox1.Controls.Add(this.uiLabel1);
            this.uiGroupBox1.Controls.Add(this.rbNew);
            this.uiGroupBox1.Controls.Add(this.rbOld);
            this.uiGroupBox1.Font = new System.Drawing.Font("Segoe Script", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Location = new System.Drawing.Point(4, 3);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.Radius = 1;
            this.uiGroupBox1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiGroupBox1.Size = new System.Drawing.Size(192, 123);
            this.uiGroupBox1.TabIndex = 2;
            this.uiGroupBox1.Text = "普通设置";
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiGroupBox1.TitleTop = 10;
            // 
            // lbItems
            // 
            this.lbItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbItems.Font = new System.Drawing.Font("Segoe Print", 9F);
            this.lbItems.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.lbItems.ItemSelectForeColor = System.Drawing.Color.White;
            this.lbItems.Location = new System.Drawing.Point(0, 0);
            this.lbItems.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbItems.MinimumSize = new System.Drawing.Size(1, 1);
            this.lbItems.Name = "lbItems";
            this.lbItems.Padding = new System.Windows.Forms.Padding(2);
            this.lbItems.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.lbItems.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.lbItems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbItems.ShowText = false;
            this.lbItems.Size = new System.Drawing.Size(200, 445);
            this.lbItems.TabIndex = 3;
            this.lbItems.Text = "uiListBox2";
            this.lbItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbItems_MouseDown);
            this.lbItems.DoubleClick += new System.EventHandler(this.lbItems_DoubleClick);
            // 
            // uiPanel5
            // 
            this.uiPanel5.Controls.Add(this.lbItems);
            this.uiPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel5.Location = new System.Drawing.Point(0, 136);
            this.uiPanel5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel5.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel5.Name = "uiPanel5";
            this.uiPanel5.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel5.Size = new System.Drawing.Size(200, 445);
            this.uiPanel5.TabIndex = 5;
            this.uiPanel5.Text = null;
            this.uiPanel5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.uiPanel5);
            this.uiPanel2.Controls.Add(this.uiPanel4);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.uiPanel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel2.Location = new System.Drawing.Point(775, 0);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.Radius = 1;
            this.uiPanel2.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel2.Size = new System.Drawing.Size(200, 581);
            this.uiPanel2.TabIndex = 4;
            this.uiPanel2.Text = null;
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.tvGroup);
            this.uiPanel1.Controls.Add(this.uiPanel8);
            this.uiPanel1.Controls.Add(this.uiPanel7);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiPanel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel1.Location = new System.Drawing.Point(0, 0);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Radius = 1;
            this.uiPanel1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel1.Size = new System.Drawing.Size(187, 581);
            this.uiPanel1.TabIndex = 3;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tvGroup
            // 
            this.tvGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvGroup.FillColor = System.Drawing.Color.White;
            this.tvGroup.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvGroup.Location = new System.Drawing.Point(0, 33);
            this.tvGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tvGroup.MinimumSize = new System.Drawing.Size(1, 1);
            this.tvGroup.Name = "tvGroup";
            this.tvGroup.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.tvGroup.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.tvGroup.ScrollBarStyleInherited = false;
            this.tvGroup.ShowText = false;
            this.tvGroup.Size = new System.Drawing.Size(187, 518);
            this.tvGroup.TabIndex = 3;
            this.tvGroup.Text = "uiTreeView1";
            this.tvGroup.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.tvGroup.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tvGroup_MouseDoubleClick);
            // 
            // uiPanel8
            // 
            this.uiPanel8.Controls.Add(this.btnRalease);
            this.uiPanel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiPanel8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel8.Location = new System.Drawing.Point(0, 551);
            this.uiPanel8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel8.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel8.Name = "uiPanel8";
            this.uiPanel8.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel8.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.uiPanel8.Size = new System.Drawing.Size(187, 30);
            this.uiPanel8.TabIndex = 3;
            this.uiPanel8.Text = null;
            this.uiPanel8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRalease
            // 
            this.btnRalease.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRalease.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRalease.Location = new System.Drawing.Point(6, 3);
            this.btnRalease.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnRalease.Name = "btnRalease";
            this.btnRalease.Radius = 20;
            this.btnRalease.Size = new System.Drawing.Size(175, 24);
            this.btnRalease.TabIndex = 3;
            this.btnRalease.Text = "一键生成爆率文件";
            this.btnRalease.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRalease.Click += new System.EventHandler(this.btnRalease_Click);
            // 
            // uiPanel7
            // 
            this.uiPanel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiPanel7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel7.Location = new System.Drawing.Point(0, 0);
            this.uiPanel7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel7.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel7.Name = "uiPanel7";
            this.uiPanel7.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel7.Size = new System.Drawing.Size(187, 33);
            this.uiPanel7.TabIndex = 3;
            this.uiPanel7.Text = "使用右键进行分组操作";
            this.uiPanel7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // treeFileImage
            // 
            this.treeFileImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeFileImage.ImageStream")));
            this.treeFileImage.TransparentColor = System.Drawing.Color.Transparent;
            this.treeFileImage.Images.SetKeyName(0, "01.png");
            this.treeFileImage.Images.SetKeyName(1, "06.png");
            // 
            // cmsTab
            // 
            this.cmsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.cmsTab.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cmsTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存ToolStripMenuItem,
            this.打开文件ToolStripMenuItem,
            this.打开所在文件夹ToolStripMenuItem,
            this.格式化ToolStripMenuItem,
            this.toolStripSeparator1,
            this.关闭所有ToolStripMenuItem,
            this.除此之外全部关闭ToolStripMenuItem,
            this.关闭ToolStripMenuItem});
            this.cmsTab.Name = "cmsMain";
            this.cmsTab.Size = new System.Drawing.Size(207, 200);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // 打开文件ToolStripMenuItem
            // 
            this.打开文件ToolStripMenuItem.Name = "打开文件ToolStripMenuItem";
            this.打开文件ToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.打开文件ToolStripMenuItem.Text = "打开文件";
            this.打开文件ToolStripMenuItem.Click += new System.EventHandler(this.打开文件ToolStripMenuItem_Click);
            // 
            // 打开所在文件夹ToolStripMenuItem
            // 
            this.打开所在文件夹ToolStripMenuItem.Name = "打开所在文件夹ToolStripMenuItem";
            this.打开所在文件夹ToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.打开所在文件夹ToolStripMenuItem.Text = "打开所在文件夹";
            this.打开所在文件夹ToolStripMenuItem.Click += new System.EventHandler(this.打开所在文件夹ToolStripMenuItem_Click);
            // 
            // 格式化ToolStripMenuItem
            // 
            this.格式化ToolStripMenuItem.Name = "格式化ToolStripMenuItem";
            this.格式化ToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.格式化ToolStripMenuItem.Text = "格式化";
            this.格式化ToolStripMenuItem.Click += new System.EventHandler(this.格式化ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(203, 6);
            // 
            // 关闭所有ToolStripMenuItem
            // 
            this.关闭所有ToolStripMenuItem.Name = "关闭所有ToolStripMenuItem";
            this.关闭所有ToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.关闭所有ToolStripMenuItem.Text = "关闭所有";
            this.关闭所有ToolStripMenuItem.Click += new System.EventHandler(this.关闭所有ToolStripMenuItem_Click);
            // 
            // 除此之外全部关闭ToolStripMenuItem
            // 
            this.除此之外全部关闭ToolStripMenuItem.Name = "除此之外全部关闭ToolStripMenuItem";
            this.除此之外全部关闭ToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.除此之外全部关闭ToolStripMenuItem.Text = "除此之外全部关闭";
            this.除此之外全部关闭ToolStripMenuItem.Click += new System.EventHandler(this.除此之外全部关闭ToolStripMenuItem_Click);
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.关闭ToolStripMenuItem.Text = "关闭";
            this.关闭ToolStripMenuItem.Click += new System.EventHandler(this.关闭ToolStripMenuItem_Click);
            // 
            // FrmGroupExpRate
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(975, 581);
            this.Controls.Add(this.uiPanel3);
            this.Controls.Add(this.uiPanel2);
            this.Controls.Add(this.uiPanel1);
            this.Name = "FrmGroupExpRate";
            this.Text = "分组爆率";
            this.Load += new System.EventHandler(this.FrmGroupExpRate_Load);
            this.uiPanel6.ResumeLayout(false);
            this.uiPanel3.ResumeLayout(false);
            this.Tab.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.uiPanel4.ResumeLayout(false);
            this.uiGroupBox1.ResumeLayout(false);
            this.uiPanel5.ResumeLayout(false);
            this.uiPanel2.ResumeLayout(false);
            this.uiPanel1.ResumeLayout(false);
            this.uiPanel8.ResumeLayout(false);
            this.cmsTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UIToolTip uiToolTip1;
        private Sunny.UI.UIPanel plMsg;
        private Sunny.UI.UIPanel plColRow;
        private Sunny.UI.UIPanel uiPanel6;
        private Sunny.UI.UIPanel uiPanel3;
        private Sunny.UI.UIContextMenuStrip cmsMain;
        private Sunny.UI.UIRadioButton rbNew;
        private Sunny.UI.UIRadioButton rbOld;
        private Sunny.UI.UITextBox txtItems;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIIntegerUpDown tud;
        private Sunny.UI.UIPanel uiPanel4;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UIListBox lbItems;
        private Sunny.UI.UIPanel uiPanel5;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UIPanel uiPanel7;
        private Sunny.UI.UITreeView tvGroup;
        private Sunny.UI.UIPanel uiPanel8;
        private Sunny.UI.UIButton btnRalease;
        private System.Windows.Forms.ImageList treeFileImage;
        private Sunny.UI.UITabControl Tab;
        private System.Windows.Forms.TabPage tabPage2;
        private Sunny.UI.UIRichTextBox uiRichTextBox1;
        private Sunny.UI.UIContextMenuStrip cmsTab;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开所在文件夹ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 格式化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 关闭所有ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 除此之外全部关闭ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
    }
}