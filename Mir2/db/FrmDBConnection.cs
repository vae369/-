using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mir.Models.DTO;
using Sunny.UI;

namespace Mir2.db
{
    public partial class FrmDBConnection : UIForm2
    {
        DBInfo db = new DBInfo();

        public FrmDBConnection()
        {
            InitializeComponent();
        }
        //protected override bool CheckData()
        //{
        //    return CheckEmpty(txtMirPath, "服务端目录不能为空！")
        //           && CheckEmpty(txtDBName, "数据库名不能为空");
        //    && CheckRange(txtDBFilePath, 18, 60, "输入年龄范围18~60")
        //    && CheckEmpty(txtDBFilePath, "请选择部门")
        //    && CheckEmpty(edtDate, "请选择生日");
        //}

        private void FrmDBConnection_Load(object sender, EventArgs e)
        {
            cbDBType.SelectedIndexChanged += cbDBType_SelectedIndexChanged;
            BindDBData();
        }
        private void BindDBData()
        {
            db.MirPath = Mir.Core.utils.ConfigHelper.GetAppConfig("MirPath");
            db.DataBaseType = Mir.Core.utils.ConfigHelper.GetAppConfig("DataBaseType");
            db.DataBaseAddr = Mir.Core.utils.ConfigHelper.GetAppConfig("DataBaseAddr");
            db.DataBaseName = Mir.Core.utils.ConfigHelper.GetAppConfig("DataBaseName");
            db.DataBaseUserName = Mir.Core.utils.ConfigHelper.GetAppConfig("DataBaseUserName");
            db.DataBasePassWord = Mir.Core.utils.ConfigHelper.GetAppConfig("DataBasePassWord");
            db.DataFilePath = Mir.Core.utils.ConfigHelper.GetAppConfig("DataFilePath");

            switch (db.DataBaseType)
            {
                case "DBE(不可用)": cbDBType.SelectedIndex = 0; break;
                case "Access": cbDBType.SelectedIndex = 1; break;
                case "SQLite": cbDBType.SelectedIndex = 2; break;
                case "MySQL": cbDBType.SelectedIndex = 3; break;
                case "MSSQL": cbDBType.SelectedIndex = 4; break;
                case "Excel(996引擎)": cbDBType.SelectedIndex = 5; break;
                default: cbDBType.SelectedIndex = 0; break;
            }
            txtDBFilePath.Text = db.DataFilePath;
            txtAddr.Text = db.DataBaseAddr;
            txtDBName.Text = string.IsNullOrEmpty(db.DataBaseName) ? "HeroDB" : db.DataBaseName;
            txtPassWord.Text = db.DataBasePassWord;
            txtUserName.Text = db.DataBaseUserName;
            txtMirPath.Text = db.MirPath;
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

                lbAddr.Location = new Point(16, 180);
                txtAddr.Location = new Point(129, 180);

                lbUserName.Location = new Point(16, 222);
                txtUserName.Location = new Point(129, 220);

                lbPassWord.Location = new Point(371, 222);
                txtPassWord.Location = new Point(434, 220);
            }
        }
        private void uiSymbolButton13_Click(object sender, EventArgs e)
        {
            string dir = "";
            if (DirEx.SelectDirEx("扩展打开文件夹", ref dir))
            {
                txtMirPath.Text = dir.Substring(0, dir.LastIndexOf('\\'));
                UIMessageTip.ShowOk(dir);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMirPath.Text.Trim()))
                {
                    this.ShowErrorDialog2("服务端目录不能为空！");
                    return;
                }
                if (string.IsNullOrEmpty(txtDBName.Text.Trim()))
                {
                    this.ShowErrorDialog2("数据库名不能为空！");
                    return;
                }

                bool flag = false;
                string item = cbDBType.SelectedItem.ToString();
                string path = txtDBFilePath.Text;

                switch (item)
                {
                    case "DBE(不可用)": break;
                    case "Access": flag = Helper.DBHelper.ConnectionAccess(path); break;
                    case "SQLite": flag = Helper.DBHelper.ConnectionSqlite(path); break;
                    case "MySQL":
                        {
                            db.DataBaseAddr = txtAddr.Text;
                            db.DataBasePassWord = txtPassWord.Text;
                            db.DataBaseUserName = txtUserName.Text;

                            flag = Helper.DBHelper.ConnectionMySql(db.DataBaseAddr, db.DataBaseName, db.DataBaseUserName, db.DataBasePassWord);
                        }
                        break;
                    case "MSSQL":
                        {
                            db.DataBaseAddr = txtAddr.Text;
                            db.DataBasePassWord = txtPassWord.Text;
                            db.DataBaseUserName = txtUserName.Text;

                            flag = Helper.DBHelper.ConnectionMsSql(db.DataBaseAddr, db.DataBaseName, db.DataBaseUserName, db.DataBasePassWord);
                        }
                        break;
                    case "Excel(996引擎)": flag = Helper.DBHelper.ConnectionExcel(path); break;
                }
                if (flag)
                {
                    db.MirPath = txtMirPath.Text;
                    db.DataBaseName = txtDBName.Text;
                    db.DataFilePath = txtDBFilePath.Text;
                    Mir.Core.utils.ConfigHelper.UpdateAppConfig("MirPath", db.MirPath);
                    Mir.Core.utils.ConfigHelper.UpdateAppConfig("DataBaseType", item);
                    Mir.Core.utils.ConfigHelper.UpdateAppConfig("DataBaseAddr", db.DataBaseAddr);
                    Mir.Core.utils.ConfigHelper.UpdateAppConfig("DataBaseName", db.DataBaseName);
                    Mir.Core.utils.ConfigHelper.UpdateAppConfig("DataBaseUserName", db.DataBaseUserName);
                    Mir.Core.utils.ConfigHelper.UpdateAppConfig("DataBasePassWord", db.DataBasePassWord);
                    Mir.Core.utils.ConfigHelper.UpdateAppConfig("DataFilePath", db.DataFilePath);

                    this.ShowSuccessNotifier("数据连接成功！", false, 5000);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.ShowErrorNotifier("数据连接失败！", false, 5000);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog2(ex.Message);
            }
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
    }
}
