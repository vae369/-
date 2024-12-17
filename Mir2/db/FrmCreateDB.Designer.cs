namespace Mir2.db
{
    partial class FrmCreateDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCreateDB));
            this.txtDBName = new Sunny.UI.UITextBox();
            this.cbDBType = new Sunny.UI.UIComboBox();
            this.uiLabel8 = new Sunny.UI.UILabel();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.lbAddr = new Sunny.UI.UILabel();
            this.lbUserName = new Sunny.UI.UILabel();
            this.lbPassWord = new Sunny.UI.UILabel();
            this.txtAddr = new Sunny.UI.UITextBox();
            this.txtUserName = new Sunny.UI.UITextBox();
            this.txtPassWord = new Sunny.UI.UITextBox();
            this.btnOK = new Sunny.UI.UISymbolButton();
            this.SuspendLayout();
            // 
            // txtDBName
            // 
            this.txtDBName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDBName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDBName.Location = new System.Drawing.Point(186, 98);
            this.txtDBName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDBName.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Padding = new System.Windows.Forms.Padding(5);
            this.txtDBName.ShowText = false;
            this.txtDBName.Size = new System.Drawing.Size(205, 29);
            this.txtDBName.TabIndex = 125;
            this.txtDBName.Text = "HeroDB";
            this.txtDBName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtDBName.Watermark = "";
            // 
            // cbDBType
            // 
            this.cbDBType.DataSource = null;
            this.cbDBType.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cbDBType.FillColor = System.Drawing.Color.White;
            this.cbDBType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbDBType.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cbDBType.Items.AddRange(new object[] {
            "DBE(不可用)",
            "Access",
            "SQLite",
            "MySQL",
            "MSSQL",
            "Excel(996引擎)"});
            this.cbDBType.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cbDBType.Location = new System.Drawing.Point(186, 60);
            this.cbDBType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDBType.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbDBType.Name = "cbDBType";
            this.cbDBType.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cbDBType.Size = new System.Drawing.Size(205, 29);
            this.cbDBType.SymbolSize = 24;
            this.cbDBType.TabIndex = 123;
            this.cbDBType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbDBType.Watermark = "";
            this.cbDBType.SelectedIndexChanged += new System.EventHandler(this.cbDBType_SelectedIndexChanged);
            // 
            // uiLabel8
            // 
            this.uiLabel8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel8.Location = new System.Drawing.Point(73, 100);
            this.uiLabel8.Name = "uiLabel8";
            this.uiLabel8.Size = new System.Drawing.Size(101, 23);
            this.uiLabel8.TabIndex = 119;
            this.uiLabel8.Text = "数据库名：";
            this.uiLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel2.Location = new System.Drawing.Point(71, 61);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(108, 23);
            this.uiLabel2.TabIndex = 120;
            this.uiLabel2.Text = "数据库类型：";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbAddr
            // 
            this.lbAddr.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAddr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lbAddr.Location = new System.Drawing.Point(73, 143);
            this.lbAddr.Name = "lbAddr";
            this.lbAddr.Size = new System.Drawing.Size(101, 23);
            this.lbAddr.TabIndex = 117;
            this.lbAddr.Text = "连接地址：";
            this.lbAddr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbUserName
            // 
            this.lbUserName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lbUserName.Location = new System.Drawing.Point(73, 182);
            this.lbUserName.Name = "lbUserName";
            this.lbUserName.Size = new System.Drawing.Size(101, 23);
            this.lbUserName.TabIndex = 118;
            this.lbUserName.Text = "用户名：";
            this.lbUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbPassWord
            // 
            this.lbPassWord.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPassWord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lbPassWord.Location = new System.Drawing.Point(123, 220);
            this.lbPassWord.Name = "lbPassWord";
            this.lbPassWord.Size = new System.Drawing.Size(58, 23);
            this.lbPassWord.TabIndex = 121;
            this.lbPassWord.Text = "密码：";
            this.lbPassWord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAddr
            // 
            this.txtAddr.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAddr.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAddr.Location = new System.Drawing.Point(186, 139);
            this.txtAddr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAddr.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Padding = new System.Windows.Forms.Padding(5);
            this.txtAddr.ShowText = false;
            this.txtAddr.Size = new System.Drawing.Size(205, 29);
            this.txtAddr.TabIndex = 127;
            this.txtAddr.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtAddr.Watermark = "";
            // 
            // txtUserName
            // 
            this.txtUserName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUserName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserName.Location = new System.Drawing.Point(186, 177);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtUserName.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Padding = new System.Windows.Forms.Padding(5);
            this.txtUserName.ShowText = false;
            this.txtUserName.Size = new System.Drawing.Size(205, 29);
            this.txtUserName.TabIndex = 128;
            this.txtUserName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtUserName.Watermark = "";
            // 
            // txtPassWord
            // 
            this.txtPassWord.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPassWord.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPassWord.Location = new System.Drawing.Point(186, 216);
            this.txtPassWord.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPassWord.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.Padding = new System.Windows.Forms.Padding(5);
            this.txtPassWord.ShowText = false;
            this.txtPassWord.Size = new System.Drawing.Size(205, 29);
            this.txtPassWord.TabIndex = 129;
            this.txtPassWord.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtPassWord.Watermark = "";
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Font = new System.Drawing.Font("宋体", 12F);
            this.btnOK.Location = new System.Drawing.Point(210, 264);
            this.btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.btnOK.Radius = 29;
            this.btnOK.Size = new System.Drawing.Size(148, 35);
            this.btnOK.StyleCustomMode = true;
            this.btnOK.Symbol = 558060;
            this.btnOK.TabIndex = 130;
            this.btnOK.Text = "创建";
            this.btnOK.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FrmCreateDB
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(496, 302);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtDBName);
            this.Controls.Add(this.cbDBType);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.txtAddr);
            this.Controls.Add(this.lbPassWord);
            this.Controls.Add(this.uiLabel8);
            this.Controls.Add(this.lbUserName);
            this.Controls.Add(this.lbAddr);
            this.Controls.Add(this.uiLabel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCreateDB";
            this.Text = "创建数据库";
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UITextBox txtDBName;
        private Sunny.UI.UIComboBox cbDBType;
        private Sunny.UI.UILabel uiLabel8;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel lbAddr;
        private Sunny.UI.UILabel lbUserName;
        private Sunny.UI.UILabel lbPassWord;
        private Sunny.UI.UITextBox txtAddr;
        private Sunny.UI.UITextBox txtUserName;
        private Sunny.UI.UITextBox txtPassWord;
        private Sunny.UI.UISymbolButton btnOK;
    }
}