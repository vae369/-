namespace Mir2.db
{
    partial class FrmTabPageCreate
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.uiPanel3 = new Sunny.UI.UIPanel();
            this.dgv = new Sunny.UI.UIDataGridView();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.btnGoDown = new Sunny.UI.UIImageButton();
            this.btnGoTop = new Sunny.UI.UIImageButton();
            this.btnDelField = new Sunny.UI.UISymbolButton();
            this.btnInsertField = new Sunny.UI.UISymbolButton();
            this.btnAddField = new Sunny.UI.UISymbolButton();
            this.btnSave = new Sunny.UI.UISymbolButton();
            this.cms = new Sunny.UI.UIContextMenuStrip();
            this.c_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.c_lenght = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiPanel1.SuspendLayout();
            this.uiPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.uiPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnGoDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGoTop)).BeginInit();
            this.SuspendLayout();
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.uiPanel3);
            this.uiPanel1.Controls.Add(this.uiPanel2);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel1.Location = new System.Drawing.Point(0, 0);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.uiPanel1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel1.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Top | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.uiPanel1.Size = new System.Drawing.Size(894, 624);
            this.uiPanel1.TabIndex = 0;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiPanel3
            // 
            this.uiPanel3.Controls.Add(this.dgv);
            this.uiPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel3.Location = new System.Drawing.Point(5, 42);
            this.uiPanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel3.Name = "uiPanel3";
            this.uiPanel3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.uiPanel3.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel3.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.uiPanel3.Size = new System.Drawing.Size(884, 577);
            this.uiPanel3.TabIndex = 1;
            this.uiPanel3.Text = null;
            this.uiPanel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.ColumnHeadersHeight = 32;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_Name,
            this.c_type,
            this.c_lenght,
            this.c_desc});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgv.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(173)))), ((int)(((byte)(255)))));
            this.dgv.Location = new System.Drawing.Point(0, 5);
            this.dgv.Name = "dgv";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgv.RowHeadersVisible = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.SelectedIndex = -1;
            this.dgv.Size = new System.Drawing.Size(884, 572);
            this.dgv.TabIndex = 7;
            this.dgv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_KeyDown);
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.btnGoDown);
            this.uiPanel2.Controls.Add(this.btnGoTop);
            this.uiPanel2.Controls.Add(this.btnDelField);
            this.uiPanel2.Controls.Add(this.btnInsertField);
            this.uiPanel2.Controls.Add(this.btnAddField);
            this.uiPanel2.Controls.Add(this.btnSave);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiPanel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel2.Location = new System.Drawing.Point(5, 5);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiPanel2.Size = new System.Drawing.Size(884, 37);
            this.uiPanel2.TabIndex = 0;
            this.uiPanel2.Text = null;
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGoDown
            // 
            this.btnGoDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoDown.Font = new System.Drawing.Font("宋体", 12F);
            this.btnGoDown.Location = new System.Drawing.Point(535, 4);
            this.btnGoDown.Name = "btnGoDown";
            this.btnGoDown.Size = new System.Drawing.Size(70, 30);
            this.btnGoDown.TabIndex = 115;
            this.btnGoDown.TabStop = false;
            this.btnGoDown.Text = "下移";
            this.btnGoDown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGoDown.Click += new System.EventHandler(this.btnGoDown_Click);
            // 
            // btnGoTop
            // 
            this.btnGoTop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoTop.Font = new System.Drawing.Font("宋体", 12F);
            this.btnGoTop.Location = new System.Drawing.Point(459, 4);
            this.btnGoTop.Name = "btnGoTop";
            this.btnGoTop.Size = new System.Drawing.Size(70, 30);
            this.btnGoTop.TabIndex = 115;
            this.btnGoTop.TabStop = false;
            this.btnGoTop.Text = "上移";
            this.btnGoTop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGoTop.Click += new System.EventHandler(this.btnGoTop_Click);
            // 
            // btnDelField
            // 
            this.btnDelField.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelField.Font = new System.Drawing.Font("宋体", 12F);
            this.btnDelField.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelField.Location = new System.Drawing.Point(338, 4);
            this.btnDelField.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnDelField.Name = "btnDelField";
            this.btnDelField.Padding = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.btnDelField.Radius = 25;
            this.btnDelField.Size = new System.Drawing.Size(113, 30);
            this.btnDelField.StyleCustomMode = true;
            this.btnDelField.Symbol = 358605;
            this.btnDelField.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDelField.SymbolHoverColor = System.Drawing.Color.IndianRed;
            this.btnDelField.TabIndex = 114;
            this.btnDelField.Text = "删除字段";
            this.btnDelField.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelField.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelField.Click += new System.EventHandler(this.btnDelField_Click);
            // 
            // btnInsertField
            // 
            this.btnInsertField.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInsertField.Font = new System.Drawing.Font("宋体", 12F);
            this.btnInsertField.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInsertField.Location = new System.Drawing.Point(217, 4);
            this.btnInsertField.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnInsertField.Name = "btnInsertField";
            this.btnInsertField.Padding = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.btnInsertField.Radius = 25;
            this.btnInsertField.Size = new System.Drawing.Size(113, 30);
            this.btnInsertField.StyleCustomMode = true;
            this.btnInsertField.Symbol = 557390;
            this.btnInsertField.TabIndex = 114;
            this.btnInsertField.Text = "插入字段";
            this.btnInsertField.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInsertField.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInsertField.Click += new System.EventHandler(this.btnInsertField_Click);
            // 
            // btnAddField
            // 
            this.btnAddField.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddField.Font = new System.Drawing.Font("宋体", 12F);
            this.btnAddField.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddField.Location = new System.Drawing.Point(96, 4);
            this.btnAddField.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnAddField.Name = "btnAddField";
            this.btnAddField.Padding = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.btnAddField.Radius = 25;
            this.btnAddField.Size = new System.Drawing.Size(113, 30);
            this.btnAddField.StyleCustomMode = true;
            this.btnAddField.Symbol = 559771;
            this.btnAddField.TabIndex = 114;
            this.btnAddField.Text = "添加字段";
            this.btnAddField.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddField.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddField.Click += new System.EventHandler(this.btnAddField_Click);
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("宋体", 12F);
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(7, 4);
            this.btnSave.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.btnSave.Radius = 25;
            this.btnSave.Size = new System.Drawing.Size(81, 30);
            this.btnSave.StyleCustomMode = true;
            this.btnSave.Symbol = 560256;
            this.btnSave.TabIndex = 114;
            this.btnSave.Text = "保存";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cms
            // 
            this.cms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.cms.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(61, 4);
            // 
            // c_Name
            // 
            this.c_Name.DataPropertyName = "c_Name";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_Name.DefaultCellStyle = dataGridViewCellStyle3;
            this.c_Name.HeaderText = "名称";
            this.c_Name.Name = "c_Name";
            this.c_Name.Width = 300;
            // 
            // c_type
            // 
            this.c_type.DataPropertyName = "c_type";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_type.DefaultCellStyle = dataGridViewCellStyle4;
            this.c_type.HeaderText = "类型";
            this.c_type.Items.AddRange(new object[] {
            "数字型",
            "大数字型",
            "文本型",
            "字节型"});
            this.c_type.Name = "c_type";
            this.c_type.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.c_type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.c_type.Width = 120;
            // 
            // c_lenght
            // 
            this.c_lenght.DataPropertyName = "c_lenght";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.c_lenght.DefaultCellStyle = dataGridViewCellStyle5;
            this.c_lenght.HeaderText = "长度";
            this.c_lenght.Name = "c_lenght";
            this.c_lenght.Width = 120;
            // 
            // c_desc
            // 
            this.c_desc.DataPropertyName = "c_desc";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.c_desc.DefaultCellStyle = dataGridViewCellStyle6;
            this.c_desc.HeaderText = "说明";
            this.c_desc.Name = "c_desc";
            this.c_desc.Width = 320;
            // 
            // FrmTabPageCreate
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(894, 624);
            this.Controls.Add(this.uiPanel1);
            this.Name = "FrmTabPageCreate";
            this.Text = "FrmTabPageCreate";
            this.Load += new System.EventHandler(this.FrmTabPageCreate_Load);
            this.uiPanel1.ResumeLayout(false);
            this.uiPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.uiPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnGoDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGoTop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UISymbolButton btnSave;
        private Sunny.UI.UISymbolButton btnAddField;
        private Sunny.UI.UISymbolButton btnDelField;
        private Sunny.UI.UISymbolButton btnInsertField;
        private Sunny.UI.UIImageButton btnGoDown;
        private Sunny.UI.UIImageButton btnGoTop;
        private Sunny.UI.UIPanel uiPanel3;
        private Sunny.UI.UIDataGridView dgv;
        private Sunny.UI.UIContextMenuStrip cms;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_Name;
        private System.Windows.Forms.DataGridViewComboBoxColumn c_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_lenght;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_desc;
    }
}