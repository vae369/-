using Mir.Models.DTO;
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
using System.Windows.Forms;

namespace Mir2.db
{
    public partial class FrmConnection : UIForm2
    {
        public DBInfo DB = new DBInfo();
        int dbType = 0;
        public FrmConnection(int dbType)
        {
            InitializeComponent();
            this.dbType = dbType;
        }

        private void btnChoseFile_Click(object sender, EventArgs e)
        {
            string item = cbDBType.SelectedItem.ToString();
            switch (item)
            {
                case "Access": ofdMain.Filter = "Access数据库文件(*.mdb)|*.mdb"; break;
                case "SQLite": ofdMain.Filter = "SQLite数据库文件(*.db)|*.db"; break;
                case "Excel(996引擎)": ofdMain.Filter = "Excel文件(*.xlsx)|*.xlsx|老Excel文件(*.xls)|*.xls"; break;
            }
            ofdMain.Multiselect = false;
            ofdMain.Title = "请选择数据库文件";
            if (ofdMain.ShowDialog() == DialogResult.OK)
            {
                txtDBFilePath.Text = ofdMain.FileName;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDBName.Text.Trim()))
                {
                    this.ShowErrorDialog2("数据库名不能为空！");
                    return;
                }

                bool flag = false;
                string item = cbDBType.SelectedItem.ToString();
                string path = txtDBFilePath.Text;

                string fix = DateTime.Now.ToString("yyyyMMddHHmmss");
                path = Application.StartupPath + $"\\data\\{fix}";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                switch (item)
                {
                    case "DBE(不可用)": break;
                    case "Access":
                        path += $"\\{txtDBName.Text}.mdb";
                        DB.DataFilePath = path;
                        File.Copy(txtDBFilePath.Text,path);
                        flag = Helper.DBHelper.ConnectionAccess(path); 
                        break;
                    case "SQLite":
                        path += $"\\{txtDBName.Text}.db";
                        DB.DataFilePath = path;
                        File.Copy(txtDBFilePath.Text, path);
                        flag = Helper.DBHelper.ConnectionSqlite(path); 
                        break;
                    case "MySQL":
                        {
                            DB.DataBaseAddr = txtAddr.Text;
                            DB.DataBasePassWord = txtPassWord.Text;
                            DB.DataBaseUserName = txtUserName.Text;

                            flag = Helper.DBHelper.ConnectionMySql(DB.DataBaseAddr, DB.DataBaseName, DB.DataBaseUserName, DB.DataBasePassWord);
                        }
                        break;
                    case "MSSQL":
                        {
                            DB.DataBaseAddr = txtAddr.Text;
                            DB.DataBasePassWord = txtPassWord.Text;
                            DB.DataBaseUserName = txtUserName.Text;

                            flag = Helper.DBHelper.ConnectionMsSql(DB.DataBaseAddr, DB.DataBaseName, DB.DataBaseUserName, DB.DataBasePassWord);
                        }
                        break;
                    case "Excel(996引擎)":
                        path += $"\\{txtDBName.Text}.xlsx";
                        DB.DataFilePath = path;
                        File.Copy(txtDBFilePath.Text, path);
                        flag = Helper.DBHelper.ConnectionExcel(path); 
                        break;
                }
                if (flag)
                {
                    DB = new DBInfo
                    {
                        Mir_ID = "DB_" + fix,
                        DataFilePath = path,
                        DataBaseType = item,
                        DataBaseName = txtDBName.Text,
                        DataBaseUserName = txtUserName.Text,
                        DataBaseAddr = txtAddr.Text,
                        DataBasePassWord = txtPassWord.Text,
                        MirDBType = "连接",
                    };

                    path = Application.StartupPath + "\\data\\dbInfo.ini";
                    IniFile iniF = new IniFile(path, Encoding.UTF8);
                    iniF.Write(DB.Mir_ID, "Mir_ID", DB.Mir_ID);
                    iniF.Write(DB.Mir_ID, "DataFilePath", DB.DataFilePath);
                    iniF.Write(DB.Mir_ID, "DataBaseType", DB.DataBaseType);
                    iniF.Write(DB.Mir_ID, "DataBaseName", DB.DataBaseName);
                    iniF.Write(DB.Mir_ID, "DataBaseAddr", DB.DataBaseAddr);
                    iniF.Write(DB.Mir_ID, "DataBaseUserName", DB.DataBaseUserName);
                    iniF.Write(DB.Mir_ID, "DataBasePassWord", DB.DataBasePassWord);
                    iniF.Write(DB.Mir_ID, "MirDBType", DB.MirDBType);
                    this.ShowSuccessDialog2("数据库连接成功！\n复制了一份进行操作，为了不破坏原数据库，请右键连接的数据库查看位置。\n当前地址："+ DB.DataFilePath);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.ShowErrorDialog2("数据连接失败！", false, 5000);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog2(ex.Message);
            }
        }

        private void FrmConnection_Load(object sender, EventArgs e)
        {
            cbDBType.SelectedIndexChanged += cbDBType_SelectedIndexChanged;
            cbDBType.SelectedIndex = dbType;
        }



        private void cbDBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIComboBox cbDBType = (UIComboBox)sender;
            switch (cbDBType.Text)
            {
                case "DBE(不可用)":
                    lbDBFile.Visible = false;
                    txtDBFilePath.Visible = false;
                    btnChoseFile.Visible = false;

                    lbAddr.Visible = false;
                    txtAddr.Visible = false;

                    lbUserName.Visible = false;
                    txtUserName.Visible = false;

                    lbPassWord.Visible = false;
                    txtPassWord.Visible = false;
                    break;
                case "Excel(996引擎)":
                case "Access":
                case "SQLite":
                    lbDBFile.Visible = true;
                    txtDBFilePath.Visible = true;
                    btnChoseFile.Visible = true;

                    lbAddr.Visible = false;
                    txtAddr.Visible = false;

                    lbUserName.Visible = false;
                    txtUserName.Visible = false;

                    lbPassWord.Visible = false;
                    txtPassWord.Visible = false;
                    break;
                case "MySQL":
                    txtAddr.Text = "localhost";
                    txtUserName.Text = "root";
                    txtPassWord.Text = "123456";
                    break;
                case "MSSQL":
                    txtAddr.Text = ".";
                    txtUserName.Text = "sa";
                    txtPassWord.Text = "123456";
                    break;
            }
            if (cbDBType.Text == "MySQL" || cbDBType.Text == "MSSQL")
            {
                lbDBFile.Visible = false;
                txtDBFilePath.Visible = false;
                btnChoseFile.Visible = false;

                lbAddr.Visible = true;
                txtAddr.Visible = true;

                lbUserName.Visible = true;
                txtUserName.Visible = true;

                lbPassWord.Visible = true;
                txtPassWord.Visible = true;

            }
        }
    }
}
