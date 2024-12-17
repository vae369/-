namespace Mir2.Script
{
    partial class FrmCretFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCretFile));
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiTextBox1 = new Sunny.UI.UITextBox();
            this.uiButton17 = new Sunny.UI.UIButton();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.rbJson = new Sunny.UI.UIRadioButton();
            this.rbInI = new Sunny.UI.UIRadioButton();
            this.rbXml = new Sunny.UI.UIRadioButton();
            this.rbTxT = new Sunny.UI.UIRadioButton();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel1.Location = new System.Drawing.Point(28, 61);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(90, 23);
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "文件名称：";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiTextBox1
            // 
            this.uiTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTextBox1.Location = new System.Drawing.Point(128, 58);
            this.uiTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBox1.MinimumSize = new System.Drawing.Size(1, 16);
            this.uiTextBox1.Name = "uiTextBox1";
            this.uiTextBox1.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox1.ShowText = false;
            this.uiTextBox1.Size = new System.Drawing.Size(203, 29);
            this.uiTextBox1.TabIndex = 1;
            this.uiTextBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiTextBox1.Watermark = "";
            // 
            // uiButton17
            // 
            this.uiButton17.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton17.Font = new System.Drawing.Font("宋体", 12F);
            this.uiButton17.Location = new System.Drawing.Point(128, 231);
            this.uiButton17.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton17.Name = "uiButton17";
            this.uiButton17.Radius = 35;
            this.uiButton17.Size = new System.Drawing.Size(151, 35);
            this.uiButton17.StyleCustomMode = true;
            this.uiButton17.TabIndex = 73;
            this.uiButton17.Text = "保存";
            this.uiButton17.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton17.Click += new System.EventHandler(this.uiButton17_Click);
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel2.Location = new System.Drawing.Point(28, 122);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(90, 23);
            this.uiLabel2.TabIndex = 0;
            this.uiLabel2.Text = "文件类型：";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.rbJson);
            this.uiGroupBox1.Controls.Add(this.rbInI);
            this.uiGroupBox1.Controls.Add(this.rbXml);
            this.uiGroupBox1.Controls.Add(this.rbTxT);
            this.uiGroupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox1.Location = new System.Drawing.Point(128, 98);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.Size = new System.Drawing.Size(203, 110);
            this.uiGroupBox1.TabIndex = 74;
            this.uiGroupBox1.Text = "请选择文件类型";
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rbJson
            // 
            this.rbJson.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbJson.Font = new System.Drawing.Font("宋体", 12F);
            this.rbJson.GroupIndex = 2;
            this.rbJson.Location = new System.Drawing.Point(116, 65);
            this.rbJson.MinimumSize = new System.Drawing.Size(1, 1);
            this.rbJson.Name = "rbJson";
            this.rbJson.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.rbJson.Size = new System.Drawing.Size(85, 35);
            this.rbJson.TabIndex = 79;
            this.rbJson.Text = ".json";
            // 
            // rbInI
            // 
            this.rbInI.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbInI.Font = new System.Drawing.Font("宋体", 12F);
            this.rbInI.GroupIndex = 2;
            this.rbInI.Location = new System.Drawing.Point(116, 24);
            this.rbInI.MinimumSize = new System.Drawing.Size(1, 1);
            this.rbInI.Name = "rbInI";
            this.rbInI.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.rbInI.Size = new System.Drawing.Size(85, 35);
            this.rbInI.TabIndex = 79;
            this.rbInI.Text = ".ini";
            // 
            // rbXml
            // 
            this.rbXml.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbXml.Font = new System.Drawing.Font("宋体", 12F);
            this.rbXml.GroupIndex = 2;
            this.rbXml.Location = new System.Drawing.Point(18, 65);
            this.rbXml.MinimumSize = new System.Drawing.Size(1, 1);
            this.rbXml.Name = "rbXml";
            this.rbXml.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.rbXml.Size = new System.Drawing.Size(71, 35);
            this.rbXml.TabIndex = 78;
            this.rbXml.Text = ".xml";
            // 
            // rbTxT
            // 
            this.rbTxT.Checked = true;
            this.rbTxT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbTxT.Font = new System.Drawing.Font("宋体", 12F);
            this.rbTxT.GroupIndex = 2;
            this.rbTxT.Location = new System.Drawing.Point(18, 24);
            this.rbTxT.MinimumSize = new System.Drawing.Size(1, 1);
            this.rbTxT.Name = "rbTxT";
            this.rbTxT.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.rbTxT.Size = new System.Drawing.Size(71, 35);
            this.rbTxT.TabIndex = 77;
            this.rbTxT.Text = ".txt";
            // 
            // FrmCretFile
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(383, 284);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.uiButton17);
            this.Controls.Add(this.uiTextBox1);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.uiLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCretFile";
            this.Text = "添加文件";
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 400, 237);
            this.uiGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox uiTextBox1;
        private Sunny.UI.UIButton uiButton17;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UIRadioButton rbJson;
        private Sunny.UI.UIRadioButton rbInI;
        private Sunny.UI.UIRadioButton rbXml;
        private Sunny.UI.UIRadioButton rbTxT;
    }
}