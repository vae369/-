namespace Mir2.Forum
{
    partial class FrmSepg
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbbb = new System.Windows.Forms.Label();
            this.lvtopic = new System.Windows.Forms.ListView();
            this.lbww = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lbgx = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.lbjifen = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbspace = new System.Windows.Forms.Label();
            this.lbUserID = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txthuashu = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.lbhuashu = new System.Windows.Forms.ListBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lbuser = new System.Windows.Forms.ListBox();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.btnImportUser = new System.Windows.Forms.Button();
            this.ofdMain = new System.Windows.Forms.OpenFileDialog();
            this.lbmsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbbb
            // 
            this.lbbb.AutoSize = true;
            this.lbbb.Location = new System.Drawing.Point(72, 216);
            this.lbbb.Name = "lbbb";
            this.lbbb.Size = new System.Drawing.Size(11, 12);
            this.lbbb.TabIndex = 12;
            this.lbbb.Text = "0";
            // 
            // lvtopic
            // 
            this.lvtopic.FullRowSelect = true;
            this.lvtopic.HideSelection = false;
            this.lvtopic.Location = new System.Drawing.Point(6, 17);
            this.lvtopic.Name = "lvtopic";
            this.lvtopic.Size = new System.Drawing.Size(487, 174);
            this.lvtopic.TabIndex = 0;
            this.lvtopic.UseCompatibleStateImageBehavior = false;
            this.lvtopic.View = System.Windows.Forms.View.Tile;
            // 
            // lbww
            // 
            this.lbww.AutoSize = true;
            this.lbww.Location = new System.Drawing.Point(231, 196);
            this.lbww.Name = "lbww";
            this.lbww.Size = new System.Drawing.Size(11, 12);
            this.lbww.TabIndex = 12;
            this.lbww.Text = "0";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(337, 231);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox2);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.lbbb);
            this.groupBox3.Controls.Add(this.lvtopic);
            this.groupBox3.Controls.Add(this.lbww);
            this.groupBox3.Controls.Add(this.btnStart);
            this.groupBox3.Controls.Add(this.lbgx);
            this.groupBox3.Controls.Add(this.btnStop);
            this.groupBox3.Controls.Add(this.lbjifen);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.lbspace);
            this.groupBox3.Controls.Add(this.lbUserID);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(12, 213);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(499, 258);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "帖子管理";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.ForeColor = System.Drawing.Color.Red;
            this.checkBox2.Location = new System.Drawing.Point(259, 235);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(72, 16);
            this.checkBox2.TabIndex = 13;
            this.checkBox2.Text = "发送留言";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.ForeColor = System.Drawing.Color.Red;
            this.checkBox1.Location = new System.Drawing.Point(181, 235);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "发送消息";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // lbgx
            // 
            this.lbgx.AutoSize = true;
            this.lbgx.Location = new System.Drawing.Point(148, 216);
            this.lbgx.Name = "lbgx";
            this.lbgx.Size = new System.Drawing.Size(11, 12);
            this.lbgx.TabIndex = 12;
            this.lbgx.Text = "0";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(418, 231);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lbjifen
            // 
            this.lbjifen.AutoSize = true;
            this.lbjifen.Location = new System.Drawing.Point(148, 197);
            this.lbjifen.Name = "lbjifen";
            this.lbjifen.Size = new System.Drawing.Size(11, 12);
            this.lbjifen.TabIndex = 12;
            this.lbjifen.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "已用空间：";
            // 
            // lbspace
            // 
            this.lbspace.AutoSize = true;
            this.lbspace.Location = new System.Drawing.Point(72, 197);
            this.lbspace.Name = "lbspace";
            this.lbspace.Size = new System.Drawing.Size(11, 12);
            this.lbspace.TabIndex = 12;
            this.lbspace.Text = "0";
            // 
            // lbUserID
            // 
            this.lbUserID.AutoSize = true;
            this.lbUserID.Location = new System.Drawing.Point(232, 216);
            this.lbUserID.Name = "lbUserID";
            this.lbUserID.Size = new System.Drawing.Size(0, 12);
            this.lbUserID.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 216);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "帮币：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "积分：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(188, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "用户：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(188, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "威望：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(112, 216);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "贡献：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txthuashu);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.btnImport);
            this.groupBox2.Controls.Add(this.lbhuashu);
            this.groupBox2.Location = new System.Drawing.Point(271, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(240, 203);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "话术管理";
            // 
            // txthuashu
            // 
            this.txthuashu.Location = new System.Drawing.Point(145, 54);
            this.txthuashu.Name = "txthuashu";
            this.txthuashu.Size = new System.Drawing.Size(74, 21);
            this.txthuashu.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(145, 83);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(145, 20);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "导入";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // lbhuashu
            // 
            this.lbhuashu.FormattingEnabled = true;
            this.lbhuashu.ItemHeight = 12;
            this.lbhuashu.Location = new System.Drawing.Point(8, 18);
            this.lbhuashu.Name = "lbhuashu";
            this.lbhuashu.Size = new System.Drawing.Size(120, 172);
            this.lbhuashu.TabIndex = 0;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(145, 54);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(74, 21);
            this.txtUser.TabIndex = 2;
            // 
            // lbuser
            // 
            this.lbuser.FormattingEnabled = true;
            this.lbuser.ItemHeight = 12;
            this.lbuser.Location = new System.Drawing.Point(6, 18);
            this.lbuser.Name = "lbuser";
            this.lbuser.Size = new System.Drawing.Size(120, 172);
            this.lbuser.TabIndex = 0;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(145, 83);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(75, 23);
            this.btnAddUser.TabIndex = 1;
            this.btnAddUser.Text = "添加";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // btnImportUser
            // 
            this.btnImportUser.Location = new System.Drawing.Point(145, 20);
            this.btnImportUser.Name = "btnImportUser";
            this.btnImportUser.Size = new System.Drawing.Size(75, 23);
            this.btnImportUser.TabIndex = 1;
            this.btnImportUser.Text = "导入";
            this.btnImportUser.UseVisualStyleBackColor = true;
            this.btnImportUser.Click += new System.EventHandler(this.btnImportUser_Click);
            // 
            // ofdMain
            // 
            this.ofdMain.FileName = "openFileDialog1";
            // 
            // lbmsg
            // 
            this.lbmsg.Name = "lbmsg";
            this.lbmsg.Size = new System.Drawing.Size(32, 17);
            this.lbmsg.Text = "消息";
            // 
            // tssl1
            // 
            this.tssl1.Name = "tssl1";
            this.tssl1.Size = new System.Drawing.Size(140, 17);
            this.tssl1.Text = "欢迎使用传奇帮论坛助手";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl1,
            this.lbmsg});
            this.statusStrip1.Location = new System.Drawing.Point(0, 476);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(525, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.lbuser);
            this.groupBox1.Controls.Add(this.btnAddUser);
            this.groupBox1.Controls.Add(this.btnImportUser);
            this.groupBox1.Location = new System.Drawing.Point(12, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 203);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "用户管理";
            // 
            // FrmSepg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 498);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmSepg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "传奇帮论坛助手 QQ:745223613";
            this.Load += new System.EventHandler(this.FrmSepg_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbbb;
        public System.Windows.Forms.ListView lvtopic;
        private System.Windows.Forms.Label lbww;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lbgx;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lbjifen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbspace;
        private System.Windows.Forms.Label lbUserID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txthuashu;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.ListBox lbhuashu;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.ListBox lbuser;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnImportUser;
        private System.Windows.Forms.OpenFileDialog ofdMain;
        private System.Windows.Forms.ToolStripStatusLabel lbmsg;
        private System.Windows.Forms.ToolStripStatusLabel tssl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}

