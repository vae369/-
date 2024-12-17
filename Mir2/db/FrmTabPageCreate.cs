using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Mir2.db
{
    public partial class FrmTabPageCreate : UIPage
    {
        List<string> fieldTypes = new List<string>();
        public FrmTabPageCreate()
        {
            InitializeComponent();
        }

        private void FrmTabPageCreate_Load(object sender, EventArgs e)
        {
            btnGoTop.Image = FontImageHelper.CreateImage(558054, 30, ChineseColors.黑色系.黝黑);
            btnGoTop.ImageHover = FontImageHelper.CreateImage(558054, 30, ChineseColors.水色系.水蓝);
            btnGoTop.ImagePress = FontImageHelper.CreateImage(558054, 30, ChineseColors.黑色系.黝黑);

            btnGoDown.Image = FontImageHelper.CreateImage(558052, 30, ChineseColors.黑色系.黝黑);
            btnGoDown.ImageHover = FontImageHelper.CreateImage(558052, 30, ChineseColors.水色系.水蓝);
            btnGoDown.ImagePress = FontImageHelper.CreateImage(558052, 30, ChineseColors.黑色系.黝黑);

            fieldTypes.Add("数字型");
            fieldTypes.Add("大数字型");
            fieldTypes.Add("文本型");
            fieldTypes.Add("字节型");

            dgv.MouseDown += dgv_CellClick;
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
            }
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

        private void btnAddField_Click(object sender, EventArgs e)
        {
            AddField();
        }

        private void AddField()
        {
            int r_index = dgv.Rows.Add();

            // 设置新行的单元格值
            dgv.Rows[r_index].Cells["c_Name"].Value = "";
            // 如果你想要设置ComboBox列的值，你需要先确保ComboBox列是编辑状态
            DataGridViewComboBoxCell c_type = (DataGridViewComboBoxCell)dgv.Rows[r_index].Cells["c_type"];

            // 设置ComboBox的选定项，例如选择第一个选项
            c_type.Value = "数字型";

            dgv.Rows[r_index].Cells["c_lenght"].Value = "0";
            dgv.Rows[r_index].Cells["c_desc"].Value = "";
            // 完成添加后结束编辑状态
            dgv.EndEdit();
        }

        private void btnInsertField_Click(object sender, EventArgs e)
        {
            InsertField();
        }

        private void InsertField()
        {
            int r_index = dgv.SelectedIndex;
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewTextBoxCell c_Name = new DataGridViewTextBoxCell();
            c_Name.Value = "";
            row.Cells.Add(c_Name);

            DataGridViewComboBoxCell c_type = new DataGridViewComboBoxCell();
            c_type.Items.Add("数字型");
            c_type.Items.Add("大数字型");
            c_type.Items.Add("文本型");
            c_type.Items.Add("字节型");

            c_type.Value = "数字型";
            row.Cells.Add(c_type);


            DataGridViewTextBoxCell c_lenght = new DataGridViewTextBoxCell();
            c_lenght.Value = "0";
            row.Cells.Add(c_lenght);

            DataGridViewTextBoxCell c_desc = new DataGridViewTextBoxCell();
            c_desc.Value = "";
            row.Cells.Add(c_desc);

            dgv.Rows.Insert(r_index, row);
        }

        private void btnDelField_Click(object sender, EventArgs e)
        {
            DeleteField();
        }

        private void DeleteField()
        {
            int r_index = dgv.SelectedIndex;
            if (r_index < 0) return;
            string c_name = dgv.Rows[r_index].Cells["c_Name"].Value.ToString();
            if (this.ShowAskDialog2("确认删除", $"删除字段 {c_name}?"))
                dgv.Rows.RemoveAt(r_index);
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
                DataGridViewRow newRow = dgv.Rows[selectedRowIndex];
                // 删除选中的行  
                dgv.Rows.Remove(dgv.Rows[selectedRowIndex]);

                // 将拷贝的行，插入到选中的上一行位置  
                dgv.Rows.Insert(selectedRowIndex - 1, newRow);
                dgv.ClearSelection();
                // 选中最初选中的行 
                dgv.Rows[selectedRowIndex - 1].Selected = true;
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
                DataGridViewRow newRow = dgv.Rows[selectedRowIndex];
                // 删除选中的行  
                dgv.Rows.Remove(dgv.Rows[selectedRowIndex]);
                // 将拷贝的行，插入到选中的下一行位置  
                dgv.Rows.Insert(selectedRowIndex + 1, newRow);
                dgv.ClearSelection();
                // 选中最初选中的行  
                dgv.Rows[selectedRowIndex + 1].Selected = true;

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            //FrmAddTable frmAddTable = new FrmAddTable();
            //frmAddTable.ShowDialog();
            this.ShowSuccessTip("只是测试功能，不一定用");
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
    }
}
