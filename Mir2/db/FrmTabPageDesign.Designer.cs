namespace Mir2.db
{
    partial class FrmTabPageDesign
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.uiPanel1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
            this.uiPanel1.Size = new System.Drawing.Size(894, 624);
            this.uiPanel1.TabIndex = 1;
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
            this.dgv.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.ColumnHeadersHeight = 32;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgv.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.dgv.Location = new System.Drawing.Point(0, 5);
            this.dgv.Name = "dgv";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.SelectedIndex = -1;
            this.dgv.Size = new System.Drawing.Size(884, 572);
            this.dgv.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.dgv.TabIndex = 0;
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
            this.btnDelField.SymbolSize = 20;
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
            this.btnInsertField.SymbolSize = 20;
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
            this.btnAddField.SymbolSize = 20;
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
            this.btnSave.SymbolSize = 20;
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
            // FrmTabPageDesign
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(894, 624);
            this.Controls.Add(this.uiPanel1);
            this.Name = "FrmTabPageDesign";
            this.Text = "FrmTabPageDesign";
            this.Load += new System.EventHandler(this.FrmTabPageDesign_Load);
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
        private Sunny.UI.UIPanel uiPanel3;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UIImageButton btnGoDown;
        private Sunny.UI.UIImageButton btnGoTop;
        private Sunny.UI.UISymbolButton btnDelField;
        private Sunny.UI.UISymbolButton btnInsertField;
        private Sunny.UI.UISymbolButton btnAddField;
        private Sunny.UI.UISymbolButton btnSave;
        private Sunny.UI.UIContextMenuStrip cms;
        private Sunny.UI.UIDataGridView dgv;
    }
}