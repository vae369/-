namespace Mir2.db
{
    partial class FrmDBConnection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBConnection));
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.txtMirPath = new Sunny.UI.UITextBox();
            this.uiSymbolButton13 = new Sunny.UI.UISymbolButton();
            this.cbDBType = new Sunny.UI.UIComboBox();
            this.lbDBFile = new Sunny.UI.UILabel();
            this.txtDBFilePath = new Sunny.UI.UITextBox();
            this.lbAddr = new Sunny.UI.UILabel();
            this.txtAddr = new Sunny.UI.UITextBox();
            this.txtUserName = new Sunny.UI.UITextBox();
            this.lbPassWord = new Sunny.UI.UILabel();
            this.txtPassWord = new Sunny.UI.UITextBox();
            this.lbUserName = new Sunny.UI.UILabel();
            this.ofdMain = new System.Windows.Forms.OpenFileDialog();
            this.uiLabel8 = new Sunny.UI.UILabel();
            this.txtDBName = new Sunny.UI.UITextBox();
            this.btnOK = new Sunny.UI.UISymbolButton();
            this.btnChoseFile = new Sunny.UI.UISymbolButton();
            this.SuspendLayout();
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel1.Location = new System.Drawing.Point(52, 78);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(108, 23);
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "服务端目录：";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel2.Location = new System.Drawing.Point(52, 117);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(108, 23);
            this.uiLabel2.TabIndex = 0;
            this.uiLabel2.Text = "数据库类型：";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMirPath
            // 
            this.txtMirPath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMirPath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMirPath.Location = new System.Drawing.Point(167, 78);
            this.txtMirPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMirPath.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtMirPath.Name = "txtMirPath";
            this.txtMirPath.Padding = new System.Windows.Forms.Padding(5);
            this.txtMirPath.ShowText = false;
            this.txtMirPath.Size = new System.Drawing.Size(383, 29);
            this.txtMirPath.TabIndex = 1;
            this.txtMirPath.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMirPath.Watermark = "";
            // 
            // uiSymbolButton13
            // 
            this.uiSymbolButton13.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton13.Font = new System.Drawing.Font("宋体", 12F);
            this.uiSymbolButton13.Location = new System.Drawing.Point(557, 75);
            this.uiSymbolButton13.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton13.Name = "uiSymbolButton13";
            this.uiSymbolButton13.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.uiSymbolButton13.Radius = 29;
            this.uiSymbolButton13.Size = new System.Drawing.Size(120, 35);
            this.uiSymbolButton13.Symbol = 61717;
            this.uiSymbolButton13.TabIndex = 2;
            this.uiSymbolButton13.Text = "选择目录";
            this.uiSymbolButton13.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton13.Click += new System.EventHandler(this.uiSymbolButton13_Click);
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
            this.cbDBType.Location = new System.Drawing.Point(167, 116);
            this.cbDBType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDBType.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbDBType.Name = "cbDBType";
            this.cbDBType.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cbDBType.Size = new System.Drawing.Size(205, 29);
            this.cbDBType.SymbolSize = 24;
            this.cbDBType.TabIndex = 2;
            this.cbDBType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbDBType.Watermark = "";
            // 
            // lbDBFile
            // 
            this.lbDBFile.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDBFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lbDBFile.Location = new System.Drawing.Point(52, 195);
            this.lbDBFile.Name = "lbDBFile";
            this.lbDBFile.Size = new System.Drawing.Size(108, 23);
            this.lbDBFile.TabIndex = 0;
            this.lbDBFile.Text = "数据库文件：";
            this.lbDBFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDBFilePath
            // 
            this.txtDBFilePath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDBFilePath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDBFilePath.Location = new System.Drawing.Point(167, 192);
            this.txtDBFilePath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDBFilePath.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtDBFilePath.Name = "txtDBFilePath";
            this.txtDBFilePath.Padding = new System.Windows.Forms.Padding(5);
            this.txtDBFilePath.ShowText = false;
            this.txtDBFilePath.Size = new System.Drawing.Size(383, 29);
            this.txtDBFilePath.TabIndex = 4;
            this.txtDBFilePath.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtDBFilePath.Watermark = "";
            // 
            // lbAddr
            // 
            this.lbAddr.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAddr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lbAddr.Location = new System.Drawing.Point(54, 234);
            this.lbAddr.Name = "lbAddr";
            this.lbAddr.Size = new System.Drawing.Size(101, 23);
            this.lbAddr.TabIndex = 0;
            this.lbAddr.Text = "连接地址：";
            this.lbAddr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAddr
            // 
            this.txtAddr.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAddr.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAddr.Location = new System.Drawing.Point(167, 230);
            this.txtAddr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAddr.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Padding = new System.Windows.Forms.Padding(5);
            this.txtAddr.ShowText = false;
            this.txtAddr.Size = new System.Drawing.Size(205, 29);
            this.txtAddr.TabIndex = 5;
            this.txtAddr.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtAddr.Watermark = "";
            // 
            // txtUserName
            // 
            this.txtUserName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUserName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserName.Location = new System.Drawing.Point(167, 268);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtUserName.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Padding = new System.Windows.Forms.Padding(5);
            this.txtUserName.ShowText = false;
            this.txtUserName.Size = new System.Drawing.Size(205, 29);
            this.txtUserName.TabIndex = 6;
            this.txtUserName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtUserName.Watermark = "";
            // 
            // lbPassWord
            // 
            this.lbPassWord.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPassWord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lbPassWord.Location = new System.Drawing.Point(409, 272);
            this.lbPassWord.Name = "lbPassWord";
            this.lbPassWord.Size = new System.Drawing.Size(58, 23);
            this.lbPassWord.TabIndex = 0;
            this.lbPassWord.Text = "密码：";
            this.lbPassWord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPassWord
            // 
            this.txtPassWord.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPassWord.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPassWord.Location = new System.Drawing.Point(472, 268);
            this.txtPassWord.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPassWord.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.Padding = new System.Windows.Forms.Padding(5);
            this.txtPassWord.ShowText = false;
            this.txtPassWord.Size = new System.Drawing.Size(205, 29);
            this.txtPassWord.TabIndex = 7;
            this.txtPassWord.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtPassWord.Watermark = "";
            // 
            // lbUserName
            // 
            this.lbUserName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lbUserName.Location = new System.Drawing.Point(54, 273);
            this.lbUserName.Name = "lbUserName";
            this.lbUserName.Size = new System.Drawing.Size(101, 23);
            this.lbUserName.TabIndex = 0;
            this.lbUserName.Text = "用户名：";
            this.lbUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // uiLabel8
            // 
            this.uiLabel8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel8.Location = new System.Drawing.Point(54, 156);
            this.uiLabel8.Name = "uiLabel8";
            this.uiLabel8.Size = new System.Drawing.Size(101, 23);
            this.uiLabel8.TabIndex = 0;
            this.uiLabel8.Text = "数据库名：";
            this.uiLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDBName
            // 
            this.txtDBName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDBName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDBName.Location = new System.Drawing.Point(167, 154);
            this.txtDBName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDBName.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Padding = new System.Windows.Forms.Padding(5);
            this.txtDBName.ShowText = false;
            this.txtDBName.Size = new System.Drawing.Size(205, 29);
            this.txtDBName.TabIndex = 3;
            this.txtDBName.Text = "HeroDB";
            this.txtDBName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtDBName.Watermark = "";
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Font = new System.Drawing.Font("宋体", 12F);
            this.btnOK.Location = new System.Drawing.Point(301, 331);
            this.btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.btnOK.Radius = 29;
            this.btnOK.Size = new System.Drawing.Size(148, 35);
            this.btnOK.StyleCustomMode = true;
            this.btnOK.Symbol = 557768;
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "开始使用";
            this.btnOK.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnChoseFile
            // 
            this.btnChoseFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChoseFile.Font = new System.Drawing.Font("宋体", 12F);
            this.btnChoseFile.Image = global::Mir2.Properties.Resources.save;
            this.btnChoseFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChoseFile.Location = new System.Drawing.Point(557, 188);
            this.btnChoseFile.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnChoseFile.Name = "btnChoseFile";
            this.btnChoseFile.Padding = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.btnChoseFile.Radius = 29;
            this.btnChoseFile.Size = new System.Drawing.Size(120, 35);
            this.btnChoseFile.StyleCustomMode = true;
            this.btnChoseFile.Symbol = 61530;
            this.btnChoseFile.TabIndex = 114;
            this.btnChoseFile.Text = "选择文件";
            this.btnChoseFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnChoseFile.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnChoseFile.Click += new System.EventHandler(this.btnChoseFile_Click);
            // 
            // FrmDBConnection
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(740, 380);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnChoseFile);
            this.Controls.Add(this.cbDBType);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.txtDBName);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.txtAddr);
            this.Controls.Add(this.txtDBFilePath);
            this.Controls.Add(this.uiSymbolButton13);
            this.Controls.Add(this.txtMirPath);
            this.Controls.Add(this.lbPassWord);
            this.Controls.Add(this.uiLabel8);
            this.Controls.Add(this.lbUserName);
            this.Controls.Add(this.lbAddr);
            this.Controls.Add(this.lbDBFile);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.uiLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDBConnection";
            this.Text = "服务器连接";
            this.Load += new System.EventHandler(this.FrmDBConnection_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UITextBox txtMirPath;
        private Sunny.UI.UISymbolButton uiSymbolButton13;
        private Sunny.UI.UIComboBox cbDBType;
        private Sunny.UI.UILabel lbDBFile;
        private Sunny.UI.UITextBox txtDBFilePath;
        private Sunny.UI.UILabel lbAddr;
        private Sunny.UI.UITextBox txtAddr;
        private Sunny.UI.UITextBox txtUserName;
        private Sunny.UI.UILabel lbPassWord;
        private Sunny.UI.UITextBox txtPassWord;
        private Sunny.UI.UILabel lbUserName;
        private System.Windows.Forms.OpenFileDialog ofdMain;
        private Sunny.UI.UILabel uiLabel8;
        private Sunny.UI.UITextBox txtDBName;
        private Sunny.UI.UISymbolButton btnChoseFile;
        private Sunny.UI.UISymbolButton btnOK;
    }
}