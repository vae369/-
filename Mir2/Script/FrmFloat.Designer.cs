namespace Mir2.Script
{
    partial class FrmFloat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFloat));
            this.plBottom = new Sunny.UI.UIPanel();
            this.plMsg = new Sunny.UI.UIPanel();
            this.plColRow = new Sunny.UI.UIPanel();
            this.plVersion = new Sunny.UI.UIPanel();
            this.lbContent = new Sunny.UI.UIListBox();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.tbContent = new Mir2.UserControl1();
            this.plBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // plBottom
            // 
            this.plBottom.Controls.Add(this.plMsg);
            this.plBottom.Controls.Add(this.plColRow);
            this.plBottom.Controls.Add(this.plVersion);
            this.plBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plBottom.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plBottom.Location = new System.Drawing.Point(0, 613);
            this.plBottom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.plBottom.MinimumSize = new System.Drawing.Size(1, 1);
            this.plBottom.Name = "plBottom";
            this.plBottom.Radius = 1;
            this.plBottom.Size = new System.Drawing.Size(919, 30);
            this.plBottom.TabIndex = 0;
            this.plBottom.Text = null;
            this.plBottom.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // plMsg
            // 
            this.plMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMsg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plMsg.Location = new System.Drawing.Point(200, 0);
            this.plMsg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.plMsg.MinimumSize = new System.Drawing.Size(1, 1);
            this.plMsg.Name = "plMsg";
            this.plMsg.Radius = 1;
            this.plMsg.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.plMsg.Size = new System.Drawing.Size(539, 30);
            this.plMsg.TabIndex = 2;
            this.plMsg.Text = null;
            this.plMsg.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // plColRow
            // 
            this.plColRow.Dock = System.Windows.Forms.DockStyle.Right;
            this.plColRow.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plColRow.Location = new System.Drawing.Point(739, 0);
            this.plColRow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.plColRow.MinimumSize = new System.Drawing.Size(1, 1);
            this.plColRow.Name = "plColRow";
            this.plColRow.Radius = 1;
            this.plColRow.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.plColRow.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Right | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.plColRow.Size = new System.Drawing.Size(180, 30);
            this.plColRow.TabIndex = 1;
            this.plColRow.Text = "行 0 列 0";
            this.plColRow.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.plVersion.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.plVersion.Size = new System.Drawing.Size(200, 30);
            this.plVersion.TabIndex = 0;
            this.plVersion.Text = null;
            this.plVersion.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbContent
            // 
            this.lbContent.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbContent.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbContent.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.lbContent.ItemSelectForeColor = System.Drawing.Color.White;
            this.lbContent.Location = new System.Drawing.Point(739, 35);
            this.lbContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbContent.MinimumSize = new System.Drawing.Size(1, 1);
            this.lbContent.Name = "lbContent";
            this.lbContent.Padding = new System.Windows.Forms.Padding(2);
            this.lbContent.Radius = 1;
            this.lbContent.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.lbContent.ShowText = false;
            this.lbContent.Size = new System.Drawing.Size(180, 578);
            this.lbContent.TabIndex = 1;
            this.lbContent.Text = "uiListBox1";
            this.lbContent.SelectedIndexChanged += new System.EventHandler(this.lbContent_SelectedIndexChanged);
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 35);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(739, 578);
            this.elementHost1.TabIndex = 3;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.tbContent;
            // 
            // FrmFloat
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(919, 643);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.lbContent);
            this.Controls.Add(this.plBottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmFloat";
            this.Text = "FrmFloat";
            this.Load += new System.EventHandler(this.FrmFloat_Load);
            this.plBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel plBottom;
        private Sunny.UI.UIPanel plVersion;
        private Sunny.UI.UIPanel plColRow;
        private Sunny.UI.UIPanel plMsg;
        private Sunny.UI.UIListBox lbContent;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private UserControl1 tbContent;
    }
}