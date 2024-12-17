
using Mir.IService;
using Mir.Models;
using Mir.Models.DTO;
using Mir.ORM.SqlSugar;
using Mir.Server;
using Mir2.Helper;
using Spire.Xls;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Mir2.db
{
    public partial class FrmTabPageDesign : UIPage
    {
        bool isInsertColumn = false;
        int lastColumn = 0;
        DBInfo DB = null;
        string tableName = "";
        IMonsterService MonsterService = null;

        UIComboBox cmb_Temp = new UIComboBox();

        tb_MirTable mirTable = null;

        // 创建一个新的Workbook
        Workbook workbook = new Workbook();
        Worksheet workSheet = null;
        public FrmTabPageDesign(DBInfo db, string tableName)
        {
            InitializeComponent();
            this.DB = db;
            this.tableName = tableName;
        }

        private void btnAddField_Click(object sender, EventArgs e)
        {
            AddField();
        }
        private void AddField()
        {
            tb_MirColumn col = new tb_MirColumn
            {
                DataType = "int",
                ColumnLength = 0,
                IsPrimarykey = false
            };
            mirTable.Columns.Add(col);

            dgv.DataSource = new List<tb_MirColumn>();
            dgv.DataSource = mirTable.Columns;
            dgv.Refresh();
            dgv.FirstDisplayedScrollingRowIndex = dgv.Rows.Count - 1;
        }

        private void btnInsertField_Click(object sender, EventArgs e)
        {
            InsertField();
        }

        private void InsertField()
        {
            int r_index = dgv.SelectedIndex;
            if (r_index == -1) return;

            tb_MirColumn col = new tb_MirColumn
            {
                DataType = "int",
                ColumnLength = 0,
                IsPrimarykey = false
            };
            mirTable.Columns.Insert(r_index, col);
            dgv.DataSource = new List<tb_MirColumn>();
            dgv.DataSource = mirTable.Columns;
            dgv.Refresh();
            dgv.SelectedIndex = r_index;

            isInsertColumn = true;
        }

        private void btnDelField_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteField();
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog2(ex.Message);
            }
            
        }
        private void DeleteField()
        {
            int r_index = dgv.SelectedIndex;
            if (r_index < 0) return;

            string ColumnName = "";
            if (dgv.Rows[r_index].Cells["ColumnName"].Value != null)
                ColumnName = dgv.Rows[r_index].Cells["ColumnName"].Value.ToString();
            if (this.ShowAskDialog2("确认删除", $"删除字段 {ColumnName}?"))
            {
                var mirCol = mirTable.Columns[r_index];
                mirTable.Columns.RemoveAt(r_index);
                dgv.DataSource = new List<tb_MirColumn>();
                dgv.DataSource = mirTable.Columns;
                dgv.Refresh();
                                
                MonsterService.SqlExcuteColumn(mirTable.TableName, mirCol, "delete");

                if (DB.DataBaseType == "Excel(996引擎)")
                {
                    string s = workSheet.Range[1, r_index + 1].Text;
                    workSheet.DeleteColumn(r_index + 1);
                }
            }
        }

        private void btnGoTop_Click(object sender, EventArgs e)
        {
            MoveUpRow();
        }
        private void MoveUpRow()
        {
            // 选择的行号  
            int selectedRowIndex = GetSelectedRowIndex(this.dgv);
            if (selectedRowIndex >= 1)
            {
                // 拷贝选中的行
                var col = mirTable.Columns[selectedRowIndex];
                // 删除选中的行
                mirTable.Columns.Remove(col);
                // 将拷贝的行，插入到选中的下一行位置
                int nextIndex = selectedRowIndex - 1;
                mirTable.Columns.Insert(nextIndex, col);

                dgv.DataSource = new List<tb_MirColumn>();
                dgv.DataSource = mirTable.Columns;
                dgv.Refresh();
                foreach (DataGridViewRow item in dgv.Rows)
                {
                    item.Selected = false;
                }
                dgv.Rows[nextIndex].Selected = true;

                if (DB.DataBaseType == "Excel(996引擎)")
                {
                    string temp1 = workSheet.Range[1, selectedRowIndex + 1].Text;
                    string temp2 = workSheet.Range[1, selectedRowIndex].Text;
                    workSheet.Range[1, selectedRowIndex + 1].Text = temp2;
                    workSheet.Range[1, selectedRowIndex].Text = temp1;


                    //int column1 = 2; // 第2列，从1开始计数
                    //int column2 = 3; // 第3列

                    //// 遍历行
                    //for (int i = 1; i <= workSheet.Rows.Length; i++)
                    //{
                    //    // 交换H列和G列的数据
                    //    CellRange hRange = workSheet.Range[i, column1];
                    //    CellRange gRange = workSheet.Range[i, column2];

                    //    string temp = hRange.Value;
                    //    hRange.Value = gRange.Value;
                    //    gRange.Value = temp;
                    //}
                }
            }
        }

        private void btnGoDown_Click(object sender, EventArgs e)
        {
            MoveDownRow();
        }
        private void MoveDownRow()
        {
            int selectedRowIndex = GetSelectedRowIndex(this.dgv);
            if (selectedRowIndex < dgv.Rows.Count - 1)
            {
                // 拷贝选中的行
                var col = mirTable.Columns[selectedRowIndex];
                // 删除选中的行
                mirTable.Columns.Remove(col);
                // 将拷贝的行，插入到选中的下一行位置
                mirTable.Columns.Insert(selectedRowIndex + 1, col);

                dgv.DataSource = new List<tb_MirColumn>();
                dgv.DataSource = mirTable.Columns;
                dgv.Refresh();
                foreach (DataGridViewRow item in dgv.Rows)
                {
                    item.Selected = false;
                }
                dgv.Rows[selectedRowIndex + 1].Selected = true;

                if (DB.DataBaseType == "Excel(996引擎)")
                {
                    int colIndex = selectedRowIndex + 1;
                    string temp1 = workSheet.Range[1, colIndex].Text;
                    string temp2 = workSheet.Range[1, colIndex + 1].Text;
                    workSheet.Range[1, colIndex].Text = temp2;
                    workSheet.Range[1, colIndex + 1].Text = temp1;
                }
            }
        }
        // 获取DataGridView中选择的行索引号  
        private int GetSelectedRowIndex(DataGridView ddgv)
        {
            if (ddgv.Rows.Count == 0)
            {
                return 0;
            }
            foreach (DataGridViewRow row in ddgv.Rows)
            {
                foreach (DataGridViewCell item in row.Cells)
                {
                    if (item.Selected)
                    {
                        return item.RowIndex;
                    }
                }
            }
            return 0;
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control)
            {
                ToCopy();
            }

            if (e.KeyCode == Keys.V && e.Control)
            {
                ToPaste();
            }
        }

        private void ToCopy()
        {
            string data = "";
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                foreach (DataGridViewCell cell in dgr.Cells)
                {
                    if (cell.Selected)
                    {
                        data += cell.Value.ToString() + "\t";
                    }
                }
                data += "\r";
            }
            Clipboard.SetText(data);
            this.ShowSuccessTip("复制成功！");
        }

        private void ToPaste()
        {
            string data = Clipboard.GetText();
            string[] rows = data.Split(new string[] { "\r" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string row in rows)
            {
                string[] columns = row.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                if (columns.Length == 1)
                {
                    dgv.SelectedCells[0].Value = columns[0];
                }
                else
                {
                    for (int i = 0; i < dgv.SelectedCells.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(columns[i]))
                            dgv.SelectedCells[i].Value = columns[i];
                    }
                }
            }
        }



        private void dgv_CellClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cms.Items.Clear();
                ToolStripMenuItem tsmToCopy = new ToolStripMenuItem("复制", FontImageHelper.CreateImage(61637, 35, ChineseColors.黑色系.黑色), dgvCell_RightClicked, Keys.Control | Keys.C);
                tsmToCopy.Name = "tsmToCopy";
                cms.Items.Add(tsmToCopy);
                ToolStripMenuItem tsmToPaste = new ToolStripMenuItem("粘贴", FontImageHelper.CreateImage(361674, 35, ChineseColors.黑色系.黑色), dgvCell_RightClicked, Keys.Control | Keys.V);
                tsmToPaste.Name = "tsmToPaste";
                cms.Items.Add(tsmToPaste);

                cms.Items.Add(new ToolStripSeparator());

                ToolStripMenuItem tsmAddFields = new ToolStripMenuItem("添加字段", FontImageHelper.CreateImage(559771, 35, ChineseColors.黑色系.黑色), dgvCell_RightClicked);
                tsmAddFields.Name = "tsmAddFields";
                cms.Items.Add(tsmAddFields);
                ToolStripMenuItem tsmInsertFields = new ToolStripMenuItem("插入字段", FontImageHelper.CreateImage(557390, 35, ChineseColors.黑色系.黑色), dgvCell_RightClicked);
                tsmInsertFields.Name = "tsmInsertFields";
                cms.Items.Add(tsmInsertFields);
                ToolStripMenuItem tsmDelFields = new ToolStripMenuItem("删除字段", FontImageHelper.CreateImage(358605, 35, ChineseColors.黑色系.黑色), dgvCell_RightClicked);
                tsmDelFields.Name = "tsmDelFields";
                tsmDelFields.ForeColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
                cms.Items.Add(tsmDelFields);

                cms.Items.Add(new ToolStripSeparator());
                ToolStripMenuItem tsmMoveUp = new ToolStripMenuItem("上移", FontImageHelper.CreateImage(558054, 35, ChineseColors.黑色系.黑色), dgvCell_RightClicked);
                tsmMoveUp.Name = "tsmMoveUp";
                cms.Items.Add(tsmMoveUp);
                ToolStripMenuItem tsmMoveDown = new ToolStripMenuItem("下移", FontImageHelper.CreateImage(558052, 35, ChineseColors.黑色系.黑色), dgvCell_RightClicked);
                tsmMoveDown.Name = "tsmMoveDown";
                cms.Items.Add(tsmMoveDown);
                ToolStripMenuItem tsmRefresh = new ToolStripMenuItem("刷新", FontImageHelper.CreateImage(558052, 35, ChineseColors.黑色系.黑色), dgvCell_RightClicked);
                tsmRefresh.Name = "tsmRefresh";
                cms.Items.Add(tsmRefresh);
                cms.Show(dgv, e.Location);

            }
        }

        private void dgvCell_RightClicked(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            switch (item.Name)
            {
                case "tsmToCopy": ToCopy(); break;
                case "tsmToPaste": ToPaste(); break;
                case "tsmAddFields": AddField(); break;
                case "tsmInsertFields": InsertField(); break;
                case "tsmDelFields": DeleteField(); break;
                case "tsmMoveUp": MoveUpRow(); break;
                case "tsmMoveDown": MoveDownRow(); break;
                case "tsmRefresh": BindData(); break;
            }
        }



        private void FrmTabPageDesign_Load(object sender, EventArgs e)
        {
            btnGoTop.Image = FontImageHelper.CreateImage(558054, 30, ChineseColors.黑色系.黝黑);
            btnGoTop.ImageHover = FontImageHelper.CreateImage(558054, 30, ChineseColors.水色系.水蓝);
            btnGoTop.ImagePress = FontImageHelper.CreateImage(558054, 30, ChineseColors.黑色系.黝黑);

            btnGoDown.Image = FontImageHelper.CreateImage(558052, 30, ChineseColors.黑色系.黝黑);
            btnGoDown.ImageHover = FontImageHelper.CreateImage(558052, 30, ChineseColors.水色系.水蓝);
            btnGoDown.ImagePress = FontImageHelper.CreateImage(558052, 30, ChineseColors.黑色系.黝黑);

            dgv.MouseDown += dgv_CellClick;
            dgv.CellValueChanged += dgv_CellValueChanged;
            dgv.EditingControlShowing += dgv_EditingControlShowing;
            InitDGVTextBoxColumn(DataGridViewContentAlignment.MiddleCenter, "ColumnName", "名称", false, true, 200, 0);
            InitDGVComBoxColumn(DataGridViewContentAlignment.MiddleCenter, "DataType", "类型", false, true, 120, 1);
            InitDGVTextBoxColumn(DataGridViewContentAlignment.MiddleCenter, "ColumnLength", "长度", false, true, 120, 2);
            InitDGVCheckBoxColumn(DataGridViewContentAlignment.MiddleCenter, "IsPrimarykey", "是否主键", true, true, 100, 3);
            InitDGVTextBoxColumn(DataGridViewContentAlignment.MiddleLeft, "ColumnDescription", "说明", false, true, 260, 4);
            InitDGVTextBoxColumn(DataGridViewContentAlignment.MiddleCenter, "SortCode", "排序字段", false, false, 260, 5);
            InitDGVTextBoxColumn(DataGridViewContentAlignment.MiddleCenter, "ColumnData", "数据内容", false, false, 260, 6);

            BindData();

        }
        string oldKey = "";
        private void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            oldKey = "";
            UIDataGridView ddgv = (UIDataGridView)sender;
            List<tb_MirColumn> list = (List<tb_MirColumn>)ddgv.DataSource;

            // 获取当前编辑的单元格位置
            int rowIndex = ddgv.CurrentCell.RowIndex;
            int columnIndex = ddgv.CurrentCell.ColumnIndex;
            string saveKey = list[rowIndex].ColumnName;

            // 获取列名
            string columnName = ddgv.Columns[columnIndex].Name;
            if (columnName == "ColumnName")
            {
                oldKey = saveKey;
            }
        }

        private void InitDGVTextBoxColumn(DataGridViewContentAlignment dvca, string columnName, string headText, bool readOnly, bool visiable, int width, int displayIndex)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = columnName;
            column.DataPropertyName = columnName;
            column.Width = width;
            column.HeaderText = headText;
            column.HeaderCell.Style.Alignment = dvca;
            column.ReadOnly = readOnly;
            column.Visible = visiable;
            column.DisplayIndex = displayIndex;
            dgv.Columns.Add(column);
        }

        private void InitDGVCheckBoxColumn(DataGridViewContentAlignment dvca, string columnName, string headText, bool readOnly, bool visiable, int width, int displayIndex)
        {
            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
            column.Name = columnName;
            column.DataPropertyName = columnName;
            column.Width = width;
            column.HeaderText = headText;
            column.HeaderCell.Style.Alignment = dvca;
            column.ReadOnly = readOnly;
            column.Visible = visiable;
            column.DisplayIndex = displayIndex;
            dgv.Columns.Add(column);
        }
        private DataTable GetDataTypeByDataTable()
        {
            DataTable dtDataType = new DataTable();
            dtDataType.Columns.Add("DataValue");
            dtDataType.Columns.Add("DataName");
            DataRow drSex;
            drSex = dtDataType.NewRow();
            drSex[0] = "int";
            drSex[1] = "数字型";
            dtDataType.Rows.Add(drSex);
            drSex = dtDataType.NewRow();
            drSex[0] = "long";
            drSex[1] = "大数字型";
            dtDataType.Rows.Add(drSex);
            drSex = dtDataType.NewRow();
            drSex[0] = "string";
            drSex[1] = "文本型";
            dtDataType.Rows.Add(drSex);
            drSex = dtDataType.NewRow();
            drSex[0] = "byte";
            drSex[1] = "字节型";
            dtDataType.Rows.Add(drSex);
            return dtDataType;
        }
        private void InitDGVComBoxColumn(DataGridViewContentAlignment dvca, string columnName, string headText, bool readOnly, bool visiable, int width, int displayIndex)
        {
            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            column.Name = columnName;
            column.DataPropertyName = columnName;
            column.DataSource = GetDataTypeByDataTable();
            column.ValueMember = "DataValue";
            column.DisplayMember = "DataName";
            column.Width = width;
            column.HeaderText = headText;
            column.HeaderCell.Style.Alignment = dvca;
            column.ReadOnly = readOnly;
            column.Visible = visiable;
            column.DisplayIndex = displayIndex;
            dgv.Columns.Add(column);
        }



        private void BindData()
        {
            try
            {
                string ConnectionString = "";
                switch (DB.DataBaseType)
                {
                    case "Access":
                        ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DB.DataFilePath + ";Persist Security Info=False;";
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.Access);
                        break;
                    case "SQLite":
                        ConnectionString = "data source=" + DB.DataFilePath;
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.Sqlite);
                        break;
                    case "MySQL":
                        {
                            string addr = DB.DataBaseAddr;
                            string dbname = DB.DataBaseName;
                            string username = DB.DataBaseUserName;
                            string password = DB.DataBasePassWord;
                            ConnectionString = $"server={addr};Database={dbname};Uid={username};Pwd={password}";
                            PubConstant.ConnectionString = ConnectionString;
                            MonsterService = new MonsterServer(SqlSugar.DbType.MySql);
                        }
                        break;
                    case "MSSQL":
                        {
                            string addr = DB.DataBaseAddr;
                            string dbname = DB.DataBaseName;
                            string username = DB.DataBaseUserName;
                            string password = DB.DataBasePassWord;
                            ConnectionString = $"server={addr};Database={dbname};Uid={username};Pwd={password}";
                            PubConstant.ConnectionString = ConnectionString;
                            MonsterService = new MonsterServer(SqlSugar.DbType.SqlServer);
                        }
                        break;
                    case "Excel(996引擎)":
                        mirTable = new tb_MirTable();
                        tb_MirColumn mirCol = null;
                        mirTable.Columns = new List<tb_MirColumn>();

                        FileStream fs = new FileStream(DB.DataFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                        workbook.LoadFromStream(fs);
                        int byteLength = (int)fs.Length;
                        byte[] fileBytes = new byte[byteLength];
                        fs.Read(fileBytes, 0, byteLength);
                        fs.Close();

                        workSheet = workbook.Worksheets[tableName];
                        DataTable dt = workSheet.ExportDataTable();
                        mirTable.MirDataTable = dt;
                        foreach (DataColumn item in dt.Columns)
                        {
                            mirCol = new tb_MirColumn()
                            {
                                ColumnName = item.ColumnName,
                                IsPrimarykey = false,
                                ColumnLength = 0,
                                ColumnDescription = "",
                                DataType = item.DataType.Name.ToLower()
                            };
                            mirTable.Columns.Add(mirCol);
                        }
                        break;
                }

                if (DB.DataBaseType != "Excel(996引擎)")
                {
                    var list = MonsterService.GetMirTable();
                    mirTable = list.Where(s => s.TableName == tableName).FirstOrDefault();

                }
                lastColumn = mirTable.Columns.Count;
                dgv.DataSource = mirTable.Columns;
            }
            catch (Exception ex)
            {
                //this.ShowErrorTip(ex.Message);
                throw ex;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                switch (DB.DataBaseType)
                {
                    case "Excel(996引擎)": workbook.SaveToFile(DB.DataFilePath, ExcelVersion.Version2010); break;
                        //case "Access": MonsterService.SqlExcuteColumn(mirTable); break;
                }
                this.ShowSuccessTip("保存成功!");
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog2(ex.Message);
            }


        }
        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // 获取修改后的值
                DataGridView dgv = sender as DataGridView;
                string newValue = dgv[e.ColumnIndex, e.RowIndex].Value.ToString();
                if (DB.DataBaseType == "Excel(996引擎)")
                {
                    if (e.RowIndex >= lastColumn)
                    {//添加和修改
                        workSheet.Range[1, e.RowIndex + 1].Text = newValue;
                    }
                    else
                    {
                        if (!isInsertColumn)//修改原来的
                            workSheet.Range[1, e.RowIndex + 1].Text = newValue;
                        else
                        {//插入值
                            workSheet.InsertColumn(e.RowIndex + 1, 1, InsertOptionsType.FormatAsAfter);
                            workSheet.Range[1, e.RowIndex + 1].Text = newValue;
                            isInsertColumn = false;//插入完改回状态
                        }
                    }

                    workSheet.Range.AutoFitColumns();
                }
                else
                {
                    var mirCol = mirTable.Columns[e.RowIndex];
                    //添加和修改
                    if (e.RowIndex >= lastColumn)// 点击添加按钮时，行索引大于等于原数据库就添加
                    {
                        MonsterService.SqlExcuteColumn(mirTable.TableName, mirCol, "insert");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(oldKey))// 改当前字段名称为空时，则表示改字段长度，说明，数据类型等
                        {
                            if (isInsertColumn) // 如果点插入按钮时，则为新增的数据，rowIndex行索引是比原数据库少的
                                MonsterService.SqlExcuteColumn(mirTable.TableName, mirCol, "insert");
                            else
                                MonsterService.SqlExcuteColumn(mirTable.TableName, mirCol, "update");
                        }
                        else// 改当前字段名称不为空时，则表示改当前字段名称
                        {
                            mirCol.ColumnDescription = oldKey;// 改名操作 吧老字段存说明里，
                            MonsterService.SqlExcuteColumn(mirTable.TableName, mirCol, "rename");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog2(ex.Message);
            }
        }
    }
}
