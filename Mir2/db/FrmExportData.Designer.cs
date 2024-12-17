namespace Mir2.db
{
    partial class FrmExportData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExportData));
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.txtFilePath = new Sunny.UI.UITextBox();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.cbOpenFile = new Sunny.UI.UICheckBox();
            this.btnChoise = new Sunny.UI.UIButton();
            this.rbFormat2 = new Sunny.UI.UIRadioButton();
            this.rbField2 = new Sunny.UI.UIRadioButton();
            this.rbField1 = new Sunny.UI.UIRadioButton();
            this.rbFormat1 = new Sunny.UI.UIRadioButton();
            this.rbSplit2 = new Sunny.UI.UIRadioButton();
            this.rbSplit1 = new Sunny.UI.UIRadioButton();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.btnExport = new Sunny.UI.UIButton();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel1.Location = new System.Drawing.Point(26, 41);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(100, 23);
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "导出目录：";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFilePath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFilePath.Location = new System.Drawing.Point(113, 41);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFilePath.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Padding = new System.Windows.Forms.Padding(5);
            this.txtFilePath.ShowButton = true;
            this.txtFilePath.ShowText = false;
            this.txtFilePath.Size = new System.Drawing.Size(311, 25);
            this.txtFilePath.Symbol = 61563;
            this.txtFilePath.TabIndex = 54;
            this.txtFilePath.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtFilePath.Watermark = "目录地址";
            this.txtFilePath.ButtonClick += new System.EventHandler(this.txtFilePath_ButtonClick);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.cbOpenFile);
            this.uiGroupBox1.Controls.Add(this.btnChoise);
            this.uiGroupBox1.Controls.Add(this.txtFilePath);
            this.uiGroupBox1.Controls.Add(this.uiLabel1);
            this.uiGroupBox1.Controls.Add(this.rbFormat2);
            this.uiGroupBox1.Controls.Add(this.rbField2);
            this.uiGroupBox1.Controls.Add(this.rbField1);
            this.uiGroupBox1.Controls.Add(this.rbFormat1);
            this.uiGroupBox1.Controls.Add(this.rbSplit2);
            this.uiGroupBox1.Controls.Add(this.rbSplit1);
            this.uiGroupBox1.Controls.Add(this.uiLabel4);
            this.uiGroupBox1.Controls.Add(this.uiLabel3);
            this.uiGroupBox1.Controls.Add(this.uiLabel2);
            this.uiGroupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox1.Location = new System.Drawing.Point(30, 50);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.Size = new System.Drawing.Size(456, 258);
            this.uiGroupBox1.TabIndex = 55;
            this.uiGroupBox1.Text = "导出内容选择";
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbOpenFile
            // 
            this.cbOpenFile.Checked = true;
            this.cbOpenFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbOpenFile.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbOpenFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.cbOpenFile.Location = new System.Drawing.Point(127, 212);
            this.cbOpenFile.MinimumSize = new System.Drawing.Size(1, 1);
            this.cbOpenFile.Name = "cbOpenFile";
            this.cbOpenFile.Size = new System.Drawing.Size(150, 29);
            this.cbOpenFile.TabIndex = 3;
            this.cbOpenFile.Text = "导出后打开文件";
            // 
            // btnChoise
            // 
            this.btnChoise.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChoise.Enabled = false;
            this.btnChoise.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnChoise.Location = new System.Drawing.Point(321, 169);
            this.btnChoise.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnChoise.Name = "btnChoise";
            this.btnChoise.Radius = 20;
            this.btnChoise.Size = new System.Drawing.Size(103, 27);
            this.btnChoise.TabIndex = 2;
            this.btnChoise.Text = "选择字段";
            this.btnChoise.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnChoise.Click += new System.EventHandler(this.btnChoise_Click);
            // 
            // rbFormat2
            // 
            this.rbFormat2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbFormat2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbFormat2.GroupIndex = 2;
            this.rbFormat2.Location = new System.Drawing.Point(224, 79);
            this.rbFormat2.MinimumSize = new System.Drawing.Size(1, 1);
            this.rbFormat2.Name = "rbFormat2";
            this.rbFormat2.Size = new System.Drawing.Size(80, 29);
            this.rbFormat2.TabIndex = 1;
            this.rbFormat2.Text = "Excel";
            // 
            // rbField2
            // 
            this.rbField2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbField2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbField2.GroupIndex = 3;
            this.rbField2.Location = new System.Drawing.Point(224, 167);
            this.rbField2.MinimumSize = new System.Drawing.Size(1, 1);
            this.rbField2.Name = "rbField2";
            this.rbField2.Size = new System.Drawing.Size(62, 29);
            this.rbField2.TabIndex = 1;
            this.rbField2.Text = "选择";
            // 
            // rbField1
            // 
            this.rbField1.Checked = true;
            this.rbField1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbField1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbField1.GroupIndex = 3;
            this.rbField1.Location = new System.Drawing.Point(127, 167);
            this.rbField1.MinimumSize = new System.Drawing.Size(1, 1);
            this.rbField1.Name = "rbField1";
            this.rbField1.Size = new System.Drawing.Size(82, 29);
            this.rbField1.TabIndex = 1;
            this.rbField1.Text = "全部";
            // 
            // rbFormat1
            // 
            this.rbFormat1.Checked = true;
            this.rbFormat1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbFormat1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbFormat1.GroupIndex = 2;
            this.rbFormat1.Location = new System.Drawing.Point(127, 79);
            this.rbFormat1.MinimumSize = new System.Drawing.Size(1, 1);
            this.rbFormat1.Name = "rbFormat1";
            this.rbFormat1.Size = new System.Drawing.Size(82, 29);
            this.rbFormat1.TabIndex = 1;
            this.rbFormat1.Text = "TXT";
            // 
            // rbSplit2
            // 
            this.rbSplit2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbSplit2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbSplit2.GroupIndex = 1;
            this.rbSplit2.Location = new System.Drawing.Point(224, 123);
            this.rbSplit2.MinimumSize = new System.Drawing.Size(1, 1);
            this.rbSplit2.Name = "rbSplit2";
            this.rbSplit2.Size = new System.Drawing.Size(62, 29);
            this.rbSplit2.TabIndex = 1;
            this.rbSplit2.Text = "逗号";
            // 
            // rbSplit1
            // 
            this.rbSplit1.Checked = true;
            this.rbSplit1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbSplit1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbSplit1.GroupIndex = 1;
            this.rbSplit1.Location = new System.Drawing.Point(127, 123);
            this.rbSplit1.MinimumSize = new System.Drawing.Size(1, 1);
            this.rbSplit1.Name = "rbSplit1";
            this.rbSplit1.Size = new System.Drawing.Size(82, 29);
            this.rbSplit1.TabIndex = 1;
            this.rbSplit1.Text = "分号";
            // 
            // uiLabel4
            // 
            this.uiLabel4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel4.Location = new System.Drawing.Point(26, 168);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(100, 23);
            this.uiLabel4.TabIndex = 0;
            this.uiLabel4.Text = "导出字段：";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel3.Location = new System.Drawing.Point(26, 82);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(100, 23);
            this.uiLabel3.TabIndex = 0;
            this.uiLabel3.Text = "导出格式：";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel2.Location = new System.Drawing.Point(42, 125);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(81, 23);
            this.uiLabel2.TabIndex = 0;
            this.uiLabel2.Text = "分隔符：";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnExport
            // 
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExport.Location = new System.Drawing.Point(180, 330);
            this.btnExport.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnExport.Name = "btnExport";
            this.btnExport.Radius = 30;
            this.btnExport.Size = new System.Drawing.Size(132, 34);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "导出";
            this.btnExport.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // FrmExportData
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(510, 370);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.btnExport);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmExportData";
            this.Text = "数据导出";
            this.Load += new System.EventHandler(this.FrmExportData_Load);
            this.uiGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox txtFilePath;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UIRadioButton rbFormat2;
        private Sunny.UI.UIRadioButton rbFormat1;
        private Sunny.UI.UIRadioButton rbSplit2;
        private Sunny.UI.UIRadioButton rbSplit1;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIRadioButton rbField2;
        private Sunny.UI.UIRadioButton rbField1;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UIButton btnChoise;
        private Sunny.UI.UICheckBox cbOpenFile;
        private Sunny.UI.UIButton btnExport;
    }
}