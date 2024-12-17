using Mir.Models.DTO;
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
using System.Windows.Forms;

namespace Mir2.db
{
    public partial class FrmCreateDB : UIForm2
    {
        List<DBInfo> listDB;
        public DBInfo DB { get; set; }
        public FrmCreateDB(List<DBInfo> listDB, int dbType)
        {
            InitializeComponent();
            this.listDB = listDB;
            cbDBType.SelectedIndex = dbType;
        }

        private void cbDBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIComboBox cbDBType = (UIComboBox)sender;
            switch (cbDBType.Text)
            {
                case "DBE(不可用)":
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
                lbAddr.Visible = true;
                txtAddr.Visible = true;

                lbUserName.Visible = true;
                txtUserName.Visible = true;

                lbPassWord.Visible = true;
                txtPassWord.Visible = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string item = cbDBType.SelectedItem.ToString();
            if (string.IsNullOrEmpty(txtDBName.Text.Trim()))
            {
                this.ShowErrorDialog2("数据库名不能为空！");
                return;
            }
            foreach (var db in listDB)
            {
                if (db.DataBaseName + "_" + db.DataBaseType == txtDBName.Text + "_" + item)
                {
                    this.ShowErrorDialog2("数据库已存在，请更改数据库名！");
                    return;
                }
            }

            if (cbDBType.SelectedItem == null)
            {
                this.ShowErrorDialog2("请选择数据库类型！");
                return;
            }
            if (cbDBType.SelectedIndex == 0)
            {
                this.ShowErrorDialog2("DBC数据库不能用，请更换其它数据库。");
                return;
            }
            bool flag = false;

            string fix = DateTime.Now.ToString("yyyyMMddHHmmss");
            string path = Application.StartupPath + $"\\data\\{fix}";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            DB = new DBInfo
            {
                Mir_ID = "DB_" + fix,
                DataFilePath = path,
                DataBaseType = item,
                DataBaseName = txtDBName.Text,
                DataBaseUserName = txtUserName.Text,
                DataBaseAddr = txtAddr.Text,
                DataBasePassWord = txtPassWord.Text,
                MirDBType = "自建",
            };

            try
            {
                switch (item)
                {
                    case "DBE(不可用)": break;
                    case "Access":
                        path += $"\\{txtDBName.Text}.mdb";
                        DB.DataFilePath = path;
                        flag = Helper.DBHelper.CreateDBByAccess(path);
                        string tableName = "Magic";
                        string fields = "MagID int PRIMARY KEY, MagName TEXT,EffectType int,Effect int,Spell int,Power int,MaxPower int,DefSpell int,DefPower int,DefMaxPower int,Job int,NeedL1 int,L1Train int,NeedL2 int,L2Train int,NeedL3 int,L3Train int,Delay int,Descr int,NeedL4 int,L4Train int,NeedL5 int,L5Train int,NeedL6 int,L6Train int,NeedL7 int,L7Train int,NeedL8 int,L8Train int,NeedL9 int,L9Train int,NeedL10 int,L10Train int,NeedL11 int,L11Train int,NeedL12 int,L12Train int,NeedL13 int,L13Train int,NeedL14 int,L14Train int,NeedL15 int,L15Train int,MaxTrainLv int,CanUpgrade int,MaxUpgradeLv int";
                        Helper.DBHelper.CreateTableByAccess(path, tableName, fields);

                        tableName = "Monster";
                        fields = "Name TEXT,Race int,RaceImg int,Appr int,Lvl int,Undead int,CoolEye int,HP int,MP int,AC int,MAC int,DC int,DCMAX int,MC int,SC int,SPEED int,HIT int,WALK_SPD int,WALKSTEP int,WALKWAIT int,ATTACK_SPD int,Exp int,ExploreItem int,InLevel int,IPExp int,AttackState int,AttackSource int,DisableSimpleActor int,MapType int";
                        Helper.DBHelper.CreateTableByAccess(path, tableName, fields);

                        tableName = "StdItems";
                        fields = "Idx int PRIMARY KEY, Name TEXT,Stdmode int,Shape int,Weight int,Anicount int,Source int,Reserved int,Looks int,DuraMax int,Ac int,Ac2 int,Mac int,Mac2 int,Dc int,Dc2 int,Mc int,Mc2 int,Sc int,Sc2 int,Need int,NeedLevel int,Price int,Stock int,Color int,OverLap int,HP int,MP int,Light int,Horse int,Element int,Expand1 int,Expand2 int,Expand3 int,Expand4 int,Element1 int,Element2 int,Element3 int,Element4 int,Element5 int,Element6 int,Element7 int,Element8 int,Element9 int,Element10 int,Element11 int,Element12 int,Element13 int,Element14 int,Element15 int,Element16 int,Element17 int,Element18 int,Element19 int,Element20 int,Expand5 int,InsuranceCurrenc int,InsuranceGold int,Element21 int,Element22 int,Element23 int,Element24 int,Element25 int,WeaponType int";
                        Helper.DBHelper.CreateTableByAccess(path, tableName, fields);
                        break;
                    case "SQLite":
                        path += $"\\{txtDBName.Text}.db";
                        DB.DataFilePath = path;
                        flag = Helper.DBHelper.CreateDBBySQLite(path);
                        tableName = "Magic";
                        fields = "MagID int PRIMARY KEY, MagName TEXT,EffectType int,Effect int,Spell int,Power int,MaxPower int,DefSpell int,DefPower int,DefMaxPower int,Job int,NeedL1 int,L1Train int,NeedL2 int,L2Train int,NeedL3 int,L3Train int,Delay int,Descr int,NeedL4 int,L4Train int,NeedL5 int,L5Train int,NeedL6 int,L6Train int,NeedL7 int,L7Train int,NeedL8 int,L8Train int,NeedL9 int,L9Train int,NeedL10 int,L10Train int,NeedL11 int,L11Train int,NeedL12 int,L12Train int,NeedL13 int,L13Train int,NeedL14 int,L14Train int,NeedL15 int,L15Train int,MaxTrainLv int,CanUpgrade int,MaxUpgradeLv int";
                        Helper.DBHelper.CreateTableBySQLite(path, tableName, fields);

                        tableName = "Monster";
                        fields = "Name TEXT,Race int,RaceImg int,Appr int,Lvl int,Undead int,CoolEye int,HP int,MP int,AC int,MAC int,DC int,DCMAX int,MC int,SC int,SPEED int,HIT int,WALK_SPD int,WALKSTEP int,WALKWAIT int,ATTACK_SPD int,Exp int,ExploreItem int,InLevel int,IPExp int,AttackState int,AttackSource int,DisableSimpleActor int,MapType int";
                        Helper.DBHelper.CreateTableBySQLite(path, tableName, fields);

                        tableName = "StdItems";
                        fields = "Idx int PRIMARY KEY, Name TEXT,Stdmode int,Shape int,Weight int,Anicount int,Source int,Reserved int,Looks int,DuraMax int,Ac int,Ac2 int,Mac int,Mac2 int,Dc int,Dc2 int,Mc int,Mc2 int,Sc int,Sc2 int,Need int,NeedLevel int,Price int,Stock int,Color int,OverLap int,HP int,MP int,Light int,Horse int,Element int,Expand1 int,Expand2 int,Expand3 int,Expand4 int,Element1 int,Element2 int,Element3 int,Element4 int,Element5 int,Element6 int,Element7 int,Element8 int,Element9 int,Element10 int,Element11 int,Element12 int,Element13 int,Element14 int,Element15 int,Element16 int,Element17 int,Element18 int,Element19 int,Element20 int,Expand5 int,InsuranceCurrenc int,InsuranceGold int,Element21 int,Element22 int,Element23 int,Element24 int,Element25 int,WeaponType int";
                        Helper.DBHelper.CreateTableBySQLite(path, tableName, fields);
                        break;
                    case "MySQL":
                        flag = Helper.DBHelper.CreateDBByMySQL(DB);
                        break;
                    case "MSSQL":
                        flag = Helper.DBHelper.CreateDBByMSSQL(DB);
                        break;
                    case "Excel(996引擎)":
                        path += $"\\{txtDBName.Text}.xlsx";
                        DB.DataFilePath = path;
                        flag = Helper.DBHelper.CreateDBByExcel(path);
                        Workbook workbook = new Workbook();
                        workbook.LoadFromFile(path);
                        // 获取第一个工作表
                        Worksheet sheet1 = workbook.Worksheets[0];
                        sheet1.Name = "Magic";
                        // 在单元格中插入数据
                        fields = "id,MagID,MagName,EffectType,Effect,Spell,Power,MaxPower,DefSpell,DefPower,DefMaxPower,Job,NeedL1,L1Train,NeedL2,L2Train,NeedL3,L3Train,Delay,Descr,NeedL4,L4Train,NeedL5,L5Train,NeedL6,L6Train,NeedL7,L7Train,NeedL8,L8Train,NeedL9,L9Train,NeedL10,L10Train,NeedL11,L11Train,NeedL12,L12Train,NeedL13,L13Train,NeedL14,L14Train,NeedL15,L15Train,MaxTrainLv,CanUpgrade,MaxUpgradeLv";
                        var arrField = fields.Split(',');
                        for (int i = 1; i <= arrField.Length - 1; i++)
                        {
                            sheet1.Range[1, i].Text = arrField[i];
                        }

                        Worksheet sheet2 = workbook.Worksheets[1];
                        sheet2.Name = "Monster";
                        // 在单元格中插入数据
                        fields = "id,Name,Race,RaceImg,Appr,Lvl,Undead,CoolEye,HP,MP,AC,MAC,DC,DCMAX,MC,SC,SPEED,HIT,WALK_SPD,WALKSTEP,WALKWAIT,ATTACK_SPD,Exp,ExploreItem,InLevel,IPExp,AttackState,AttackSource,DisableSimpleActor,MapType";
                        arrField = fields.Split(',');
                        for (int i = 1; i <= arrField.Length - 1; i++)
                        {
                            sheet2.Range[1, i].Text = arrField[i];
                        }

                        Worksheet sheet3 = workbook.Worksheets[2];
                        sheet3.Name = "StdItems";
                        // 在单元格中插入数据
                        fields = "id,Idx,Name,Stdmode,Shape,Weight,Anicount,Source,Reserved,Looks,DuraMax,Ac,Ac2,Mac,Mac2,Dc,Dc2,Mc,Mc2,Sc,Sc2,Need,NeedLevel,Price,Stock,Color,OverLap,HP,MP,Light,Horse,Element,Expand1,Expand2,Expand3,Expand4,Element1,Element2,Element3,Element4,Element5,Element6,Element7,Element8,Element9,Element10,Element11,Element12,Element13,Element14,Element15,Element16,Element17,Element18,Element19,Element20,Expand5,InsuranceCurrenc,InsuranceGold,Element21,Element22,Element23,Element24,Element25,WeaponType";
                        arrField = fields.Split(',');
                        for (int i = 1; i <= arrField.Length - 1; i++)
                        {
                            sheet3.Range[1, i].Text = arrField[i];
                        }
                        workbook.SaveToFile(path, ExcelVersion.Version2010);
                        break;
                }

                if (!flag)
                {
                    this.ShowErrorDialog2("创建失败！");
                    return;
                }



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

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog2("异常:" + ex.Message);
            }
        }
    }
}
