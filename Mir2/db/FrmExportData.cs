using Mir.Core.file;
using Mir.Models;
using Spire.Xls;
using Spire.Xls.Core;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;

namespace Mir2.db
{
    public partial class FrmExportData : UIForm2
    {
        DataTable data = null;
        List<string> fields = new List<string>();
        public FrmExportData(DataTable data)
        {
            InitializeComponent();
            this.Text = data.TableName + " 表导出";
            this.data = data;
        }

        private void txtFilePath_ButtonClick(object sender, EventArgs e)
        {
            string dir = "";
            if (DirEx.SelectDirEx("扩展打开文件夹", ref dir))
            {
                string txtMirPath = dir.Substring(0, dir.LastIndexOf('\\'));
                txtFilePath.Text = txtMirPath;
            }
        }

        private void FrmExportData_Load(object sender, EventArgs e)
        {
            rbFormat1.CheckedChanged += new System.EventHandler(rbFormat1_CheckedChanged);
            rbFormat2.CheckedChanged += new System.EventHandler(rbFormat2_CheckedChanged);
            rbField2.CheckedChanged += new System.EventHandler(rbField2_CheckedChanged);
        }

        private void rbField2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbField2.Checked)
            {
                btnChoise.Enabled = true;
            }
        }

        private void rbFormat2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFormat2.Checked)
            {
                rbSplit1.Checked = false;
                rbSplit1.Enabled = false;
                rbSplit2.Enabled = false;
            }
        }

        private void rbFormat1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFormat1.Checked)
            {
                rbSplit1.Checked = true;
                rbSplit1.Enabled = true;
                rbSplit2.Enabled = true;
            }
        }

        private void btnChoise_Click(object sender, EventArgs e)
        {
            FrmField frmField = new FrmField(data, fields);
            frmField.Render();
            DialogResult dir = frmField.ShowDialog();
            if (dir == DialogResult.OK)
            {
                fields = frmField.fields;
                btnChoise.Text = $"选择字段({fields.Count})";
            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilePath.Text.Trim()))
            {
                UIMessageBox.ShowInfo("导出目录不能为空！");
                return;
            }
            string path = txtFilePath.Text.Trim() + "\\" + data.TableName;
            if (rbFormat1.Checked)
            {
                path += ".txt";

                FileHelper.DeleteFile(path);

                if (rbSplit1.Checked)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        string colStr = "";

                        if (rbField2.Checked)
                        {
                            foreach (string f in fields)
                            {
                                if (string.IsNullOrEmpty(row[f].ToString()))
                                    colStr += "0,";
                                else
                                    colStr += row[f].ToString() + ",";
                            }
                        }
                        else
                        {
                            foreach (DataColumn col in data.Columns)
                            {
                                if (string.IsNullOrEmpty(row[col.ColumnName].ToString()))
                                    colStr += "0,";
                                else
                                    colStr += row[col.ColumnName].ToString() + ",";
                            }
                        }

                        FileHelper.WriteLine(path, colStr.TrimEnd(','));
                    }
                }
                else
                {
                    foreach (DataRow row in data.Rows)
                    {
                        string colStr = "";

                        if (rbField2.Checked)
                        {
                            foreach (string f in fields)
                            {
                                if (string.IsNullOrEmpty(row[f].ToString()))
                                    colStr += "0;";
                                else
                                    colStr += row[f].ToString() + ";";
                            }
                        }
                        else
                        {
                            foreach (DataColumn col in data.Columns)
                            {
                                if (string.IsNullOrEmpty(row[col.ColumnName].ToString()))
                                    colStr += "0;";
                                else
                                    colStr += row[col.ColumnName].ToString() + ";";
                            }
                        }

                        FileHelper.WriteLine(path, colStr.TrimEnd(';'));
                    }
                }
            }
            else
            {
                path += ".xls";

                Workbook workBook = new Workbook();
                //创建空sheet
                workBook.CreateEmptySheets(1);
                Worksheet sheet = workBook.Worksheets[0];
                sheet.Name = data.TableName;
                if (!rbField2.Checked)
                {
                    sheet.InsertDataTable(data, true, 1, 1);
                }
                else
                {
                    DataTable newDt = data.Copy();
                    for (int i = newDt.Columns.Count - 1; i >= 0; i--)
                    {
                        if (!fields.Contains(newDt.Columns[i].ColumnName))
                        {
                            newDt.Columns.Remove(newDt.Columns[i]);
                        }
                    }

                    sheet.InsertDataTable(newDt, true, 1, 1);
                }

                workBook.SaveToFile(path);
            }
            
            UIMessageBox.ShowSuccess("导出成功！");

            if (cbOpenFile.Checked)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = path,
                    FileName = "explorer.exe"
                };
                Process.Start(startInfo);
            }
        }
    }
}
