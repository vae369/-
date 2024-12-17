
using Mir.IService;
using Mir.Models;
using Mir.Models.DTO;
using Mir.ORM.SqlSugar;
using Mir.Server;
using Spire.Xls;
using SqlSugar;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mir2.db
{
    public partial class FrmImportData : UIForm2
    {
        string filePath = "";
        string tableName = "";
        DBInfo dBInfo = null;
        public FrmImportData(string tableName, DBInfo dBInfo)
        {
            InitializeComponent();
            this.tableName = tableName;
            this.dBInfo = dBInfo;
            this.Text = tableName + " 表导入";
        }

        private void txtFilePath_ButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath; // 设置初始目录
            ofd.Filter = "Excel文件 (*.xls)|*.xls|所有文件 (*.*)|*.*"; // 设置文件过滤器
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK) // 显示对话框
            {
                // 获取选中文件的路径
                filePath = ofd.FileName;
                txtFilePath.Text = filePath;
            }
        }
         
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {


                DataTable dt = null;
                Workbook wk = new Workbook();
                wk.LoadFromFile(filePath);
                foreach (var item in wk.Worksheets)
                {
                    if (item.Name.ToLower() == tableName.ToLower())
                    {
                        dt = wk.Worksheets[tableName].ExportDataTable();
                    }
                }
                DataTable distinctTable = dt.AsEnumerable().Distinct(DataRowComparer.Default).CopyToDataTable();

                // 生成sql语句
                List<string> listSql = new List<string>();
                StringBuilder sql = new StringBuilder();
                Dictionary<string, string> colDic = new Dictionary<string, string>();
                foreach (DataColumn col in dt.Columns)
                {
                    colDic.Add(col.ColumnName, col.DataType.Name);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    sql.Clear();
                    sql.Append("insert into " + tableName + " (");
                    sql.Append(string.Join(",", colDic.Select(k => k.Key).ToList()));
                    sql.Append(") values (");
                    foreach (var dic in colDic)
                    {
                        bool flag = int.TryParse(dr[dic.Key].ToString(), out int res);
                        if (!flag)
                        {
                            sql.Append($"'{dr[dic.Key].ToString()}',");
                        }
                        else
                        {
                            sql.Append($"{res},");
                        }
                    }
                    sql.Remove(sql.Length - 1, 1);
                    sql.Append(")");
                    listSql.Add(sql.ToString());
                }
                if (listSql.Count > 0)
                {
                    IMonsterService monsterService = null;
                    string ConnectionString = "";
                    switch (dBInfo.DataBaseType)
                    {
                        case "Access":
                            ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dBInfo.DataFilePath + ";Persist Security Info=False;";
                            PubConstant.ConnectionString = ConnectionString;
                            monsterService = new MonsterServer(SqlSugar.DbType.Access);
                            break;
                        case "SQLite":
                            ConnectionString = "data source=" + dBInfo.DataFilePath;
                            PubConstant.ConnectionString = ConnectionString;
                            monsterService = new MonsterServer(SqlSugar.DbType.Sqlite);
                            break;
                        case "MySQL":
                            {

                            }
                            break;
                        case "MSSQL":
                            {

                            }
                            break;
                        case "Excel(996引擎)":

                            break;
                    }

                    monsterService.ExecuteSQL(listSql);

                    this.ShowSuccessDialog2("导入成功！\n当前只支持Access，Sqlite数据库导入。");
                }                
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog2(ex.Message);
            }
        }
    }
}
