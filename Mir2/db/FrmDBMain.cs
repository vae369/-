using Microsoft.VisualBasic.FileIO;
using Mir.Core.controller;
using Mir.Core.file;
using Mir.Core.utils;
using Mir.IService;
using Mir.Models;
using Mir.Models.DTO;
using Mir.ORM.SqlSugar;
using Mir.Server;
using Mir2.Helper;
using Mir2.Properties;
using Spire.Xls;
using Spire.Xls.Core;
using SqlSugar;
using Sunny.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Mir2.db
{
    public partial class FrmDBMain : UIPage
    {

        List<string> std = new List<string>();

        List<DBInfo> listDb = new List<DBInfo>();
        DBInfo dBInfo = null;
        DataTable MonsterDT = new DataTable();
        DataTable StdItemsDT = new DataTable();
        DataTable MagicDT = new DataTable();

        IMonsterService MonsterService = null;
        IStdItemsService StdItemsService = null;
        IMagicService MagicService = null;


        Dictionary<string, string> SqlColmun = new Dictionary<string, string>();
        List<string> delDGVList = new List<string>();
        public FrmDBMain(DBInfo dBInfo)
        {
            InitializeComponent();
            this.dBInfo = dBInfo;
        }

        private void FrmDBMain_Load(object sender, EventArgs e)
        {
            BindTreeViewData();
            BindCheckBoxEvent();

            toolStripButton9.Image = FontImageHelper.CreateImage(57485, 22, ChineseColors.黑色系.黎);
            toolStripButton10.Image = FontImageHelper.CreateImage(361498, 22, ChineseColors.黑色系.黎);
            toolStripButton12.Image = FontImageHelper.CreateImage(557357, 22, ChineseColors.黑色系.黎);
            toolStripButton13.Image = FontImageHelper.CreateImage(557360, 22, ChineseColors.黑色系.黎);
        }

        void TabControlWithAutoSizedTabs_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 获取当前TabPage
            TabPage page = tbContent.TabPages[e.Index];
            // 计算绘制文字的大小
            SizeF textSize = e.Graphics.MeasureString(page.Text, this.Font);
            // 绘制背景
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.Bounds);
            // 绘制边框
            e.Graphics.DrawRectangle(new Pen(Color.Black), e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
            // 绘制文字
            e.Graphics.DrawString(page.Text, this.Font, new SolidBrush(this.ForeColor), new PointF(e.Bounds.X + (e.Bounds.Width - textSize.Width) / 2, e.Bounds.Y + (e.Bounds.Height - textSize.Height) / 2));
            // 重绘当前TabPage
            e.Graphics.Dispose();
        }
        private void BindCheckBoxEvent()
        {
            UICheckBox ucb = null;
            foreach (Control ct in uiPanel12.Controls)
            {
                ucb = ct as UICheckBox;
                if (ucb.Tag != null)
                    ucb.CheckedChanged += uiCheckBox_CheckedChanged;
            }
            foreach (Control ct in uiPanel13.Controls)
            {
                ucb = ct as UICheckBox;
                if (ucb.Tag != null)
                    ucb.CheckedChanged += uiCheckBox_CheckedChanged;
            }
            foreach (Control ct in uiPanel14.Controls)
            {
                ucb = ct as UICheckBox;
                if (ucb.Tag != null)
                    ucb.CheckedChanged += uiCheckBox_CheckedChanged;
            }

        }


        private void uiCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UICheckBox ucb = sender as UICheckBox;

            if (ucb.Checked)
            {
                std.Add(ucb.Tag.ToString());
            }
            else
            {
                string ucbstr = ucb.Tag.ToString();
                if (std.Contains(ucbstr))
                {
                    std.Remove(ucbstr);
                }
            }
            if (StdItemsService != null)
            {
                string where = "and Stdmode in ({0})";
                where = string.Format(where, string.Join(",", std));
                if (std.Count == 0) where = "";
                StdItemsDT = StdItemsService.GetStdItemsByTable(where);

                switch (CurrentPage.Text)
                {
                    case "StdItems":
                        UIDataGridView dgv = CurrentPage.Controls[0] as UIDataGridView;
                        dgv.DataSource = new DataTable();
                        CreateStdItemsDGV(dgv);

                        dgv.DataSource = StdItemsDT;
                        break;
                }
            }


        }

        private void BindTreeViewData()
        {
            listDb.Clear();
            if (!string.IsNullOrEmpty(dBInfo.DataBaseName))
                listDb.Add(dBInfo);
            string path = Application.StartupPath + "\\data\\dbInfo.ini";
            IniFile iniF = new IniFile(path, Encoding.UTF8);

            foreach (string item in iniF.Sections)
            {
                NameValueCollection sectValues = iniF.GetSectionValues(item);
                DBInfo DB = new DBInfo
                {
                    Mir_ID = sectValues["Mir_ID"],
                    DataFilePath = sectValues["DataFilePath"],
                    DataBaseType = sectValues["DataBaseType"],
                    DataBaseName = sectValues["DataBaseName"],
                    DataBaseUserName = sectValues["DataBaseUserName"],
                    DataBaseAddr = sectValues["DataBaseAddr"],
                    DataBasePassWord = sectValues["DataBasePassWord"],
                    MirDBType = sectValues["MirDBType"]
                };
                listDb.Add(DB);
            }

            tvDataBase.MouseDown += tbDb_MouseDown;
            tvDataBase.NodeMouseDoubleClick += tvDataBase_NodeMouseDoubleClick;
            tvDataBase.AfterLabelEdit += tvDataBase_AfterLabelEdit;

            tvDataBase.Nodes.Clear();
            TreeNode node = tvDataBase.Nodes.Add("db", "数据库", 0);
            node.SetProperty("isConnectionDB", false);
            //AddSub(node, listDb);
        }

        private void tvDataBase_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                bool isFLag = false;
                DBInfo db = JsonHelper.JsonDeserialize<DBInfo>(e.Node.Tag.ToString());
                listDb.ForEach(s =>
                {
                    if (s.DataBaseName == e.Label)
                    {
                        isFLag = true;
                        return;
                    }
                });
                if (isFLag)
                {
                    this.ShowErrorDialog2("修改失败，数据库已存在！");

                }
                else
                {
                    db.DataBaseName = e.Label;
                    e.Node.Tag = JsonHelper.JsonSerializer<DBInfo>(db);
                }

                tvDataBase.LabelEdit = false;
                e.Node.EndEdit(false);
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog2(ex.Message);
            }

        }

        private void tbDb_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // 获取点击点的位置
                Point clickPoint = new Point(e.X, e.Y);
                TreeNode selectedNode = tvDataBase.GetNodeAt(clickPoint);

                // 如果点击在节点上，选中该节点
                if (selectedNode != null)
                {
                    // 选中节点
                    tvDataBase.SelectedNode = selectedNode;

                    CreateTreeRightControl(tvDataBase, clickPoint);
                }
            }
        }

        private void AddSub(TreeNode node, List<DBInfo> listDb)
        {
            if (listDb == null)
            {
                lvContent.Items.Clear();
                DBInfo dbInfo = JsonHelper.JsonDeserialize<DBInfo>(node.Tag.ToString());
                TreeNode subNode = node.Nodes.Add($"{dbInfo.DataBaseAddr}", "Magic", 4);
                subNode.Tag = JsonHelper.JsonSerializer<DBInfo>(dbInfo);
                subNode = node.Nodes.Add($"{dbInfo.DataBaseAddr}", "Monster", 4);
                subNode.Tag = JsonHelper.JsonSerializer<DBInfo>(dbInfo);
                subNode = node.Nodes.Add($"{dbInfo.DataBaseAddr}", "StdItems", 4);
                subNode.Tag = JsonHelper.JsonSerializer<DBInfo>(dbInfo);

                string idx = "";
                string magId = "";
                if (MagicDT.Columns.Contains("magid"))
                {
                    string strmagid = MagicDT.Compute("Max(magid)", "true").ToString();
                    int.TryParse(strmagid, out int max);//最大值
                    max++;
                    magId = max + "";
                }
                if (StdItemsDT.Columns.Contains("idx"))
                {
                    string strIdx = StdItemsDT.Compute("Max(idx)", "true").ToString();
                    int.TryParse(strIdx, out int max);//最大值
                    max++;
                    idx = max + "";
                }
                ListViewItem lvi = new ListViewItem() { Text = "Magic", ImageIndex = 5, };
                lvi.SubItems.Add(MagicDT.Rows.Count.ToString());
                lvi.SubItems.Add(magId);
                lvi.SubItems.Add("系统");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add($"{dbInfo.DataBaseName}_{dbInfo.DataBaseType}");

                lvContent.Items.Add(lvi);

                lvi = new ListViewItem() { Text = "Monster", ImageIndex = 5, };
                lvi.SubItems.Add(MonsterDT.Rows.Count.ToString());
                lvi.SubItems.Add(MonsterDT.Rows.Count.ToString());
                lvi.SubItems.Add("系统");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add($"{dbInfo.DataBaseName}_{dbInfo.DataBaseType}");

                lvContent.Items.Add(lvi);

                lvi = new ListViewItem() { Text = "StdItems", ImageIndex = 5, };
                lvi.SubItems.Add(StdItemsDT.Rows.Count.ToString());
                lvi.SubItems.Add(idx);
                lvi.SubItems.Add("系统");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add($"{dbInfo.DataBaseName}_{dbInfo.DataBaseType}");

                lvContent.Items.Add(lvi);
                return;
            }
            foreach (DBInfo db in listDb)
            {
                //foreach (TreeNode nd in node.Nodes)
                //{
                //    if(nd.Text==)
                //}
                TreeNode subNode = node.Nodes.Add($"{db.DataBaseName}_{db.DataBaseType}", $"{db.DataBaseName}_{db.DataBaseType}({db.MirDBType})", 2);
                subNode.Tag = JsonHelper.JsonSerializer<DBInfo>(db);
                subNode.SetProperty("isOpenDB", false);
            }
        }


        private void CreateNodesByDB(bool isConnectionDB)
        {
            ToolStripMenuItem tsmConnection = new ToolStripMenuItem("打开连接", FontImageHelper.CreateImage(560116, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmConnection.Name = "tsmConnection";
            cms.Items.Add(tsmConnection);
            ToolStripMenuItem tsmCutConnection = new ToolStripMenuItem("断开连接", FontImageHelper.CreateImage(558618, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmCutConnection.Name = "tsmCutConnection";
            cms.Items.Add(tsmCutConnection);
            cms.Items.Add(new ToolStripSeparator());   // 添加分割行
            ToolStripMenuItem tsmCreateDB = new ToolStripMenuItem("新建数据库", FontImageHelper.CreateImage(61888, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmCreateDB.Name = "tsmCreateDB";
            cms.Items.Add(tsmCreateDB);
            ToolStripMenuItem tsmNewSelect = new ToolStripMenuItem("T-SQL(高级功能)", FontImageHelper.CreateImage(560059, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmNewSelect.Name = "tsmNewSelect";
            cms.Items.Add(tsmNewSelect);

            if (!isConnectionDB)
            {
                tsmCutConnection.Enabled = false;
                tsmCreateDB.Enabled = false;
                tsmNewSelect.Enabled = false;
            }
            else
            {
                tsmConnection.Enabled = false;
            }
        }

        private void CreateNodesByDBName(bool isOpenDB)
        {
            ToolStripMenuItem tsmOpenDB = new ToolStripMenuItem("打开", FontImageHelper.CreateImage(561902, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmOpenDB.Name = "tsmOpenDB";
            cms.Items.Add(tsmOpenDB);
            ToolStripMenuItem tsmCloseDB = new ToolStripMenuItem("关闭", FontImageHelper.CreateImage(561903, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmCloseDB.Name = "tsmCloseDB";
            cms.Items.Add(tsmCloseDB);
            ToolStripMenuItem tsmOpenFoldor = new ToolStripMenuItem("打开所在文件夹", FontImageHelper.CreateImage(261564, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmOpenFoldor.Name = "tsmOpenFoldor";
            cms.Items.Add(tsmOpenFoldor);

            cms.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem tsmCreateTable = new ToolStripMenuItem("新建表", FontImageHelper.CreateImage(57381, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmCreateTable.Name = "tsmCreateTable";
            cms.Items.Add(tsmCreateTable);
            ToolStripMenuItem tsmPloy = new ToolStripMenuItem("策略", FontImageHelper.CreateImage(162052, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmPloy.Name = "tsmPloy";
            ToolStripMenuItem tsmBakOf = new ToolStripMenuItem("备份", FontImageHelper.CreateImage(563727, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmBakOf.Name = "tsmBakOf";
            ToolStripMenuItem tsmRestore = new ToolStripMenuItem("还原(预留)", FontImageHelper.CreateImage(559689, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmRestore.Name = "tsmRestore";
            tsmPloy.DropDownItems.AddRange(new ToolStripItem[] {
            tsmBakOf,
            tsmRestore,
            });
            cms.Items.Add(tsmPloy);

            ToolStripMenuItem tsmDataConvert = new ToolStripMenuItem("数据转换", FontImageHelper.CreateImage(358483, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmDataConvert.Name = "tsmDataConvert";
            ToolStripMenuItem tsmDataConvertAccess = new ToolStripMenuItem("转Access", FontImageHelper.CreateImage(163690, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmDataConvertAccess.Name = "tsmDataConvertAccess";
            ToolStripMenuItem tsmDataConvertSQLite = new ToolStripMenuItem("转SQLite", FontImageHelper.CreateImage(558894, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmDataConvertSQLite.Name = "tsmDataConvertSQLite";
            ToolStripMenuItem tsmDataConvertMySql = new ToolStripMenuItem("转MySql(预留)", FontImageHelper.CreateImage(560373, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmDataConvertMySql.Name = "tsmDataConvertMySql";
            ToolStripMenuItem tsmDataConvertMsSql = new ToolStripMenuItem("转SQL Server(预留)", FontImageHelper.CreateImage(163541, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmDataConvertMsSql.Name = "tsmDataConvertMsSql";
            ToolStripMenuItem tsmDataConvertExcel = new ToolStripMenuItem("转Excel(996)(预留)", FontImageHelper.CreateImage(61891, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmDataConvertExcel.Name = "tsmDataConvertExcel";
            tsmDataConvert.DropDownItems.AddRange(new ToolStripItem[] {
            tsmDataConvertAccess,
            tsmDataConvertSQLite,
            tsmDataConvertMySql,
            tsmDataConvertMsSql,
            tsmDataConvertExcel,
            });
            cms.Items.Add(tsmDataConvert);

            cms.Items.Add(new ToolStripSeparator());
            ToolStripMenuItem tsmDeleteDB = new ToolStripMenuItem("分离", FontImageHelper.CreateImage(559691, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmDeleteDB.Name = "tsmDeleteDB";
            cms.Items.Add(tsmDeleteDB);

            if (isOpenDB)
            {
                tsmOpenDB.Enabled = false;
            }
            else
            {
                tsmCloseDB.Enabled = false;
                tsmCreateTable.Enabled = false;
            }

            TreeNode node = tvDataBase.SelectedNode;
            DBInfo db = JsonHelper.JsonDeserialize<DBInfo>(node.Tag.ToString());
            if (db.MirDBType == "默认")
            {
                tsmDeleteDB.Enabled = false;
                tsmDataConvert.Enabled = false;
            }
        }

        private void CreateNodesByTable()
        {
            ToolStripMenuItem tsmOpenTable = new ToolStripMenuItem("打开表", FontImageHelper.CreateImage(561251, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmOpenTable.Name = "tsmOpenTable";
            cms.Items.Add(tsmOpenTable);
            ToolStripMenuItem tsmDesignTable = new ToolStripMenuItem("设计表", FontImageHelper.CreateImage(61861, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmDesignTable.Name = "tsmDesignTable";
            cms.Items.Add(tsmDesignTable);
            ToolStripMenuItem tsmNewSelect_T = new ToolStripMenuItem("T-SQL查询(高级功能)", null, MirTree_RightClicked);
            tsmNewSelect_T.Name = "tsmNewSelect_T";
            cms.Items.Add(tsmNewSelect_T);

            ToolStripMenuItem tsmCreateLanguage = new ToolStripMenuItem("T-SQL(如果不会直接打开这个)", null, MirTree_RightClicked);
            tsmCreateLanguage.Name = "tsmCreateLanguage";
            ToolStripMenuItem tsmInsert = new ToolStripMenuItem("新增语句", null, MirTree_RightClicked);
            tsmInsert.Name = "tsmInsert";
            ToolStripMenuItem tsmUpdate = new ToolStripMenuItem("修改语句", null, MirTree_RightClicked);
            tsmUpdate.Name = "tsmUpdate";
            ToolStripMenuItem tsmDelete = new ToolStripMenuItem("删除语句", null, MirTree_RightClicked);
            tsmDelete.Name = "tsmDelete";
            ToolStripMenuItem tsmSelect = new ToolStripMenuItem("查询所有语句", null, MirTree_RightClicked);
            tsmSelect.Name = "tsmSelect";
            ToolStripMenuItem tsmSelectTop10 = new ToolStripMenuItem("查询前10语句", null, MirTree_RightClicked);
            tsmSelectTop10.Name = "tsmSelectTop10";
            ToolStripMenuItem tsmSelectSort = new ToolStripMenuItem("查询所有升序/降序语句", null, MirTree_RightClicked);
            tsmSelectSort.Name = "tsmSelectSort";
            ToolStripMenuItem tsmSelectWhere = new ToolStripMenuItem("查询条件where语句", null, MirTree_RightClicked);
            tsmSelectWhere.Name = "tsmSelectWhere";
            ToolStripMenuItem tsmSelectGroup = new ToolStripMenuItem("分组查询语句", null, MirTree_RightClicked);
            tsmSelectGroup.Name = "tsmSelectGroup";
            tsmCreateLanguage.DropDownItems.AddRange(new ToolStripItem[] {
            tsmInsert,
            tsmUpdate,
            tsmDelete,
            tsmSelect,
            tsmSelectWhere,
            tsmSelectSort,
            tsmSelectTop10,
            tsmSelectGroup,
            });
            cms.Items.Add(tsmCreateLanguage);
            cms.Items.Add(new ToolStripSeparator());   // 添加分割行

            ToolStripMenuItem tsmImport = new ToolStripMenuItem("导入", FontImageHelper.CreateImage(362831, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmImport.Name = "tsmImport";
            cms.Items.Add(tsmImport);
            ToolStripMenuItem tsmExport = new ToolStripMenuItem("导出", FontImageHelper.CreateImage(362830, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmExport.Name = "tsmExport";
            cms.Items.Add(tsmExport);
            cms.Items.Add(new ToolStripSeparator());   // 添加分割行



            ToolStripMenuItem tsmDelTable = new ToolStripMenuItem("删除表(预留)", FontImageHelper.CreateImage(57437, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmDelTable.Name = "tsmDelTable";
            cms.Items.Add(tsmDelTable);

            TreeNode node = tvDataBase.SelectedNode;
            DBInfo db = JsonHelper.JsonDeserialize<DBInfo>(node.Tag.ToString());
            if (db.MirDBType == "默认")
            {
                tsmDelTable.Enabled = false;
            }
        }


        /// <summary>
        /// 创建右键菜单内容
        /// </summary>
        private void CreateTreeRightControl(UITreeView MirTree, Point clickPoint)
        {
            cms.Items.Clear();

            switch (MirTree.SelectedNode.Text.ToLower())
            {
                case "数据库":
                    bool isConnectionDB = (bool)MirTree.SelectedNode.GetProperty("isConnectionDB");

                    CreateNodesByDB(isConnectionDB);
                    break;//数据库右键按钮功能
                case "magic":
                case "monster":
                case "stditems": CreateNodesByTable(); break;//创建表右键按钮功能
                default:
                    bool isOpenDB = (bool)MirTree.SelectedNode.GetProperty("isOpenDB");

                    CreateNodesByDBName(isOpenDB);
                    break;//某个库右键按钮功能
            }

            cms.Items.Add(new ToolStripSeparator());   // 添加分割行
            ToolStripMenuItem tsmRefresh = new ToolStripMenuItem("刷新", FontImageHelper.CreateImage(61473, 35, ChineseColors.黑色系.黑色), MirTree_RightClicked);
            tsmRefresh.Name = "tsmRefresh";
            cms.Items.Add(tsmRefresh);

            cms.Show(MirTree, clickPoint);
        }


        private void MirTree_RightClicked(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(";说明");
                sql.AppendLine(";当前这只是一个SQL语句的例子，分号开头是注释，不会执行，字符串必打'单引号，否则报错");
                sql.AppendLine(";直接点运行，如果有错误请自行更改，只能运行1条语句，多条语句请选中其中1条运行");
                sql.AppendLine(";只是为了学习基本的语法，深入学习SQL请自己百度");
                sql.AppendLine("");
                TreeNode node = tvDataBase.SelectedNode;

                ToolStripMenuItem item = sender as ToolStripMenuItem;
                switch (item.Name)
                {
                    case "tsmConnection": GetConnection(); break;
                    case "tsmCutConnection": CutConnection(); break;
                    case "tsmOpenFoldor": OpenFoldor(); break;
                    case "tsmCreateDB": CreateDB(); break;
                    case "tsmNewSelect": NewSelect(0); break;

                    case "tsmOpenDB": OpenDB(); break;
                    case "tsmCloseDB": CloseDB(); break;
                    case "tsmCreateTable": CreateTable(); break;
                    case "tsmBakOf": DBBakOf(); break;
                    case "tsmRestore": this.ShowInfoNotifier("功能预留，等以后有需求就加。"); break;

                    case "tsmDataConvertAccess": DataConvert("Access"); break;
                    case "tsmDataConvertSQLite": DataConvert("SQLite"); break;
                    case "tsmDataConvertMySql": this.ShowInfoNotifier("功能预留，等以后有需求就加。"); break;
                    case "tsmDataConvertMsSql": this.ShowInfoNotifier("功能预留，等以后有需求就加"); break;
                    case "tsmDataConvertExcel":
                        this.ShowInfoNotifier("功能预留，等以后有需求就加");
                        //DataConvert("Excel"); 
                        break;
                    case "tsmDeleteDB": DeleteDB(); break;
                    case "tsmOpenTable": OpenTable(); break;
                    case "tsmDesignTable": DesignTable(); break;
                    case "tsmNewSelect_T": NewSelect(node.Parent.Index); break;
                    case "tsmInsert":
                        switch (node.Text.ToLower())
                        {
                            case "magic":
                                sql.AppendLine("insert into magic(magid,magname,effecttype) values (1,'火球术',1)");
                                break;
                            case "monster":
                                sql.AppendLine("insert into monster(name,race,raceimg) values ('鸡',81,19)");
                                break;
                            case "stditems":
                                sql.AppendLine("insert into stditems(idx,name,stdmode) values (1,'屠龙',5)");
                                break;
                        }
                        NewSelect(node.Parent.Index, sql.ToString());
                        break;
                    case "tsmUpdate":
                        switch (node.Text.ToLower())
                        {
                            case "magic":
                                sql.AppendLine("update magic set magname='超级火球术',power=20 where magname='火球术'");
                                sql.AppendLine("");
                                sql.AppendLine(";修改火球术为超级火球术，并且伤害值下限改为20");
                                break;
                            case "monster":
                                sql.AppendLine("update monster set name='梅花鹿',exp=200,hp=2000 where name='鹿'");
                                sql.AppendLine("");
                                sql.AppendLine(";修改鹿为梅花鹿，并且经验值改为200，红改为2000");
                                break;
                            case "stditems":
                                sql.AppendLine("update stditems set name='屠龙★',dc=8,dc2=55,needlevel=45 where name='屠龙'");
                                sql.AppendLine("");
                                sql.AppendLine(";修改屠龙为屠龙★，攻击下限改为8，上限改为55，红改为2000，等级改为45");
                                break;
                        }
                        NewSelect(node.Parent.Index, sql.ToString());
                        break;
                    case "tsmDelete":
                        switch (node.Text.ToLower())
                        {
                            case "magic":
                                sql.AppendLine("delete from magic where magname='超级火球术'");
                                sql.AppendLine("");
                                sql.AppendLine(";删除超级火球术技能");
                                break;
                            case "monster":
                                sql.AppendLine("delete from monster where name='梅花鹿'");
                                sql.AppendLine("");
                                sql.AppendLine(";删除怪物梅花鹿");
                                break;
                            case "stditems":
                                sql.AppendLine("delete from stditems where name='屠龙★'");
                                sql.AppendLine("");
                                sql.AppendLine(";删除物品屠龙★");
                                break;
                        }
                        NewSelect(node.Parent.Index, sql.ToString());
                        break;
                    case "tsmSelect":
                        switch (node.Text.ToLower())
                        {
                            case "magic":
                                sql.AppendLine("select * from magic");
                                break;
                            case "monster":
                                sql.AppendLine("select * from monster");
                                break;
                            case "stditems":
                                sql.AppendLine("select * from stditems");
                                break;
                        }
                        NewSelect(node.Parent.Index, sql.ToString());
                        break;
                    case "tsmSelectSort":
                        switch (node.Text.ToLower())
                        {
                            case "magic":
                                sql.AppendLine("select * from magic order by magid asc");
                                sql.AppendLine("select * from magic order by magid desc");
                                break;
                            case "monster":
                                sql.AppendLine("select * from monster order by name asc");
                                sql.AppendLine("select * from monster order by name desc");
                                break;
                            case "stditems":
                                sql.AppendLine("select * from stditems order by name asc");
                                sql.AppendLine("select * from stditems order by name desc");
                                break;
                        }
                        NewSelect(node.Parent.Index, sql.ToString());
                        break;
                    case "tsmSelectWhere":
                        switch (node.Text.ToLower())
                        {
                            case "magic":
                                sql.AppendLine("select * from magic where magid=10");
                                sql.AppendLine("select * from magic where magname='火球术'");
                                sql.AppendLine("select * from magic where job=0");
                                break;
                            case "monster":
                                sql.AppendLine("select * from monster where name='祖玛教主'");
                                sql.AppendLine("select * from monster where hp>=500 and hp<=1000");
                                sql.AppendLine("select * from monster where undead=1");
                                break;
                            case "stditems":
                                sql.AppendLine("select * from stditems where name='逍遥扇'");
                                sql.AppendLine("select * from stditems where ac>10");
                                sql.AppendLine("select * from stditems where price>100 and price<300");
                                break;
                        }
                        NewSelect(node.Parent.Index, sql.ToString());
                        break;
                    case "tsmSelectTop10":
                        DBInfo db = JsonHelper.JsonDeserialize<DBInfo>(node.Tag.ToString());
                        switch (node.Text.ToLower())
                        {
                            case "magic":
                                switch (db.DataBaseType.ToLower())
                                {
                                    case "access": sql.AppendLine("select top 10 * from magic"); break;
                                    case "sqlite": sql.AppendLine("select * from magic limit 10"); break;
                                    default: break;
                                }
                                sql.AppendLine("");
                                sql.AppendLine(";提示：Access数据库是top sqlite是limit");

                                break;
                            case "monster":
                                switch (db.DataBaseType.ToLower())
                                {
                                    case "access": sql.AppendLine("select top 10 * from monster"); break;
                                    case "sqlite": sql.AppendLine("select * from monster limit 10"); break;
                                    default: break;
                                }
                                sql.AppendLine("");
                                sql.AppendLine(";提示：Access数据库是top sqlite是limit");
                                break;
                            case "stditems":
                                switch (db.DataBaseType.ToLower())
                                {
                                    case "access": sql.AppendLine("select top 10 * from stditems"); break;
                                    case "sqlite": sql.AppendLine("select * from stditems limit 10"); break;
                                    default: break;
                                }
                                break;
                        }
                        NewSelect(node.Parent.Index, sql.ToString());
                        break;
                    case "tsmSelectGroup":
                        switch (node.Text.ToLower())
                        {
                            case "magic":
                                sql.AppendLine("select job,\r\n       count(1) as '数量',\r\ncase   \r\n    when job=0 then '战士'\r\n    when job=1 then '法师'\r\n    else '道士' end as '所属职业'\r\nfrom magic group by job");
                                sql.AppendLine("");
                                sql.AppendLine(";将技能按职业分组，查看条数");
                                break;
                            case "monster":
                                sql.AppendLine("select undead,\r\n       count(1) as '数量',\r\ncase  undead when 0 then '否' else '是' end as '是否属不死系' \r\nfrom monster group by undead");
                                sql.AppendLine("");
                                sql.AppendLine(";将怪物按不死系分组，查看条数");
                                break;
                            case "stditems":
                                sql.AppendLine("select stdmode,\r\n       count(1) as '数量'\r\nfrom stditems group by stdmode \r\nhaving count(1) >50");
                                sql.AppendLine("");
                                sql.AppendLine(";将物品按类型分组，筛选组内数量大于50条的数据");
                                break;
                        }
                        NewSelect(node.Parent.Index, sql.ToString());
                        break;
                    case "tsmImport": Import(); break;
                    case "tsmExport": Export(); break;
                    case "tsmDelTable": DelTable(); break;

                    case "tsmRefresh": BindTreeViewData(); lvContent.Items.Clear(); break;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorTip(ex.Message);
            }

        }

        private void Import()
        {
            TreeNode node = tvDataBase.SelectedNode;
            FrmImportData frmImportData = new FrmImportData(node.Text, dBInfo);
            frmImportData.Render();
            frmImportData.ShowDialog();
        }

        private void DelTable()
        {
            TreeNode node = tvDataBase.SelectedNode;

            if (this.ShowAskDialog2("删除表", "删除表操作无法恢复，请谨慎操作。\n确定删除 " + node.Text + " 表么？"))
            {
                //MonsterService.DropTable(node.Text);
                //tvDataBase.Nodes.Remove(node);
                this.ShowSuccessTip("模拟数据库表 " + node.Text + " 删除成功！");
                uiPanel7.Text = "模拟数据库表 " + node.Text + " 删除成功！";
            }
        }

        private void Export()
        {
            try
            {
                TreeNode node = tvDataBase.SelectedNode;
                DataTable dt = null;
                switch (node.Text)
                {
                    case "Magic": dt = MagicService.GetMagicByTable(); break;
                    case "Monster": dt = MonsterService.GetMonsterByTable(); break;
                    case "StdItems": dt = StdItemsService.GetStdItemsByTable(""); break;
                }
                dt.TableName = node.Text;
                FrmExportData frmExportData = new FrmExportData(dt);
                frmExportData.Render();
                frmExportData.ShowDialog();
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog2(ex.Message);
            }
        }

        private void DesignTable()
        {
            TabPage page = new TabPage();
            try
            {
                TreeNode node = tvDataBase.SelectedNode;
                DBInfo db = JsonHelper.JsonDeserialize<DBInfo>(node.Tag.ToString());

                page.ImageIndex = 6;
                page.BackColor = Color.White;
                page.Text = "设计-" + node.Text;
                tbContent.Controls.Add(page);
                tbContent.SelectedTab = page;

                FrmTabPageDesign frmTabPageDesign = new FrmTabPageDesign(db, node.Text);
                frmTabPageDesign.Render();
                frmTabPageDesign.TopLevel = false;
                frmTabPageDesign.Width = page.Width;
                frmTabPageDesign.Height = page.Height;

                page.Controls.Add(frmTabPageDesign);

                frmTabPageDesign.Show();
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog2(ex.Message);
                if (ex.Message.Contains("另一个"))
                    tbContent.Controls.Remove(page);
            }
        }

        private void OpenTable()
        {
            TreeNode node = tvDataBase.SelectedNode;
            ShowTablePage(node);
        }

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        private void DeleteDB()
        {
            TreeNode node = tvDataBase.SelectedNode;
            DBInfo db = JsonHelper.JsonDeserialize<DBInfo>(node.Tag.ToString());
            if (this.ShowAskDialog2("分离数据库", "只会删除当前软件中的数据，不会删除数据库。\n确定分离 [" + db.DataBaseName + "] 数据库么？"))
            {
                 
                string path = Application.StartupPath + "\\data\\dbInfo.ini";
                IniFile iniF = new IniFile(path, Encoding.UTF8);
                var keys = iniF.GetKeys(db.Mir_ID);
                foreach (var key in keys)
                {
                    iniF.DeleteKey(db.Mir_ID, key);
                }
                WritePrivateProfileString(db.Mir_ID, null, null, path);
                BindTreeViewData();
                this.ShowSuccessTip("数据库 [" + db.DataBaseName + "] 分离成功！");
                uiPanel7.Text = "数据库 [" + db.DataBaseName + "] 分离成功！";
            }
        }

        private void OpenFoldor()
        {
            TreeNode node = tvDataBase.SelectedNode;
            DBInfo db = JsonHelper.JsonDeserialize<DBInfo>(node.Tag.ToString());
            string path = Path.GetDirectoryName(db.DataFilePath);

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = path,
                FileName = "explorer.exe"
            };

            Process.Start(startInfo);
        }

        private void DataConvert(string dbType)
        {
            try
            {
                TreeNode node = tvDataBase.SelectedNode;
                if (node == null || node.Text == "数据库")
                {
                    this.ShowErrorDialog2("请选择要转换的数据库！");
                    return;
                }
                DBInfo db = JsonHelper.JsonDeserialize<DBInfo>(node.Tag.ToString());
                if (db.DataBaseType == dbType) throw new Exception("不能操作同类型的数据库。");

                if (this.ShowAskDialog2("转换数据库", "转换后会仍保留原文件。\n确定转换成 [" + dbType + "] 数据库么？"))
                {
                    switch (dbType)
                    {
                        case "Access": DBHelper.DataConvertAccess(db); break;
                        case "SQLite": DBHelper.DataConvertSQLite(db); break;
                        case "Excel": DBHelper.DataConvertExcel(db); break;
                        default: return;
                    }

                    string path = Application.StartupPath + "\\data\\dbInfo.ini";
                    IniFile iniF = new IniFile(path, Encoding.UTF8);
                    iniF.Write(db.Mir_ID, "DataBaseType", db.DataBaseType);
                    iniF.Write(db.Mir_ID, "DataFilePath", db.DataFilePath);
                    if (this.ShowAskDialog2("转换数据库", "转换成功！\n是否打开文件夹查看？"))
                    {
                        OpenFoldor();
                    }

                    BindTreeViewData();
                    uiPanel7.Text = "转换数据库 [" + db.DataBaseName + "] 成功！";
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog2(ex.Message);
            }
        }

        private void DBBakOf()
        {
            try
            {
                TreeNode node = tvDataBase.SelectedNode;
                if (node == null || node.Text == "数据库")
                {
                    this.ShowErrorDialog2("请选择要备份的数据库！");
                    return;
                }
                DBInfo db = JsonHelper.JsonDeserialize<DBInfo>(node.Tag.ToString());
                string path = DBHelper.DBBakOf(db);
                string fileName = Path.GetFileName(path);
                path = Path.GetDirectoryName(path);
                if (this.ShowAskDialog2("备份成功", "数据库名 [" + fileName + "]\n是否打开文件夹查看？"))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        Arguments = path,
                        FileName = "explorer.exe"
                    };

                    Process.Start(startInfo);

                    uiPanel7.Text = "数据库 [" + db.DataBaseName + "] 备份成功！";
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog2(ex.Message);
            }

        }

        private void CreateTable()
        {
            TabPage page = new TabPage();
            page.ImageIndex = 6;
            page.BackColor = Color.White;
            page.Text = "创建-表";
            tbContent.Controls.Add(page);
            tbContent.SelectedTab = page;

            FrmTabPageCreate frmTabPageCreate = new FrmTabPageCreate();
            frmTabPageCreate.Render();
            frmTabPageCreate.TopLevel = false;
            frmTabPageCreate.Width = page.Width;
            frmTabPageCreate.Height = page.Height;

            page.Controls.Add(frmTabPageCreate);

            frmTabPageCreate.Show();

        }

        private void CloseDB()
        {
            TreeNode node = tvDataBase.SelectedNode;
            node.ImageIndex = 2;
            node.SetProperty("isOpenDB", false);

            lvContent.Items.Clear();
            node.Nodes.Clear();

            DBInfo db = JsonHelper.JsonDeserialize<DBInfo>(node.Tag.ToString());
            for (int i = tbContent.TabPages.Count - 1; i > 0; i--)
            {
                TabPage page = tbContent.TabPages[i];
                if (page.Text != "主页" && !page.Text.Contains("SQL-查询"))
                {
                    if (page.Text.StartsWith("创建"))
                    {
                        tbContent.TabPages.RemoveAt(i);
                        continue;
                    }
                    DBInfo di = JsonHelper.JsonDeserialize<DBInfo>(page.Tag.ToString());

                    //tbContent.TabPages.RemoveAt(i);
                    if (db.DataBaseName == di.DataBaseName)
                    {
                        tbContent.TabPages.RemoveAt(i);
                    }
                }
            }
        }

        private void OpenDB()
        {
            TreeNode node = tvDataBase.SelectedNode;
            if (!(bool)node.GetProperty("isOpenDB"))
                ShowTable(node);
        }

        int p_num = 1;
        private void NewSelect(int t = 0, string sql = "")
        {
            TabPage page = new TabPage();
            page.ImageIndex = 6;
            page.BackColor = Color.White;
            page.Text = "SQL-查询" + p_num++;

            UIPanel panel = new UIPanel();
            panel.Style = GameStyle.BindStyle();
            panel.Height = 35;
            panel.Dock = DockStyle.Top;
            panel.RadiusSides = UICornerRadiusSides.None;
            panel.RectSides = ToolStripStatusLabelBorderSides.Bottom | ToolStripStatusLabelBorderSides.Top;
            page.Controls.Add(panel);

            UIComboBox tscb = new UIComboBox();
            tscb.Location = new Point(5, 3);
            tscb.Style = GameStyle.BindStyle();
            tscb.Size = new Size(250, 30);
            listDb.ForEach(s =>
            {
                tscb.Items.Add(s.DataBaseName + "_" + s.DataBaseType);
            });
            tscb.SelectedIndexChanged += tscb_SelectedIndexChanged;
            tscb.SelectedIndex = t;
            panel.Controls.Add(tscb);


            UISymbolButton tsb1 = new UISymbolButton();
            tsb1.Style = GameStyle.BindStyle();
            tsb1.Symbol = 61515;
            tsb1.Click += tsb_Click;
            tsb1.Location = new Point(258, 3);
            tsb1.Size = new Size(100, 30);
            tsb1.Text = "运行";
            panel.Controls.Add(tsb1);


            UISplitContainer usc = new UISplitContainer();
            usc.Style = GameStyle.BindStyle();
            UIRichTextBox uIRichTextBox1 = new UIRichTextBox();
            uIRichTextBox1.Style = GameStyle.BindStyle();
            if (!string.IsNullOrEmpty(sql))
                uIRichTextBox1.Text = sql;
            uIRichTextBox1.Dock = DockStyle.Fill;
            uIRichTextBox1.FillColor = Color.White;
            uIRichTextBox1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            uIRichTextBox1.Location = new Point(0, 0);
            uIRichTextBox1.Margin = new Padding(4, 5, 4, 5);
            uIRichTextBox1.MinimumSize = new Size(1, 1);
            uIRichTextBox1.Name = "uiRichTextBox1";
            uIRichTextBox1.Padding = new Padding(3);
            uIRichTextBox1.ScrollBarStyleInherited = false;
            uIRichTextBox1.ShowText = false;
            uIRichTextBox1.Size = new Size(919, 306);
            uIRichTextBox1.TextAlignment = ContentAlignment.MiddleCenter;
            uIRichTextBox1.RadiusSides = UICornerRadiusSides.None;
            usc.Panel1.Padding = new Padding(1, 36, 1, 1);
            usc.Panel1.Controls.Add(uIRichTextBox1);
            usc.BarColor = Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            usc.CollapsePanel = UISplitContainer.UICollapsePanel.Panel2;
            tbContent.Controls.Add(page);
            tbContent.SelectedTab = page;
            page.Controls.Add(usc);

            usc.Size = new Size(919, 536);
            usc.SplitterDistance = 300;
            usc.SplitterWidth = 10;
            usc.Cursor = Cursors.Default;
            usc.Dock = DockStyle.Fill;
            usc.Location = new Point(0, 35);
            usc.MinimumSize = new Size(20, 20);
            usc.Name = "uiSplitContainer1";
            usc.Orientation = Orientation.Horizontal;


            UITabControl uITabControl = new UITabControl();
            uITabControl.Style = GameStyle.BindStyle();
            TabPage tp1 = new TabPage();
            TabPage tp2 = new TabPage();

            uITabControl.Controls.Add(tp1);
            uITabControl.Controls.Add(tp2);

            uITabControl.TabBackColor = Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            uITabControl.TabUnSelectedForeColor = Color.Black;

            uITabControl.Dock = DockStyle.Fill;
            uITabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            uITabControl.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            uITabControl.ItemSize = new Size(80, 30);
            uITabControl.Location = new Point(0, 0);
            uITabControl.MainPage = "";
            uITabControl.Name = "uiTabControl1";
            uITabControl.SelectedIndex = 0;
            uITabControl.Size = new Size(919, 219);
            uITabControl.SizeMode = TabSizeMode.Fixed;
            uITabControl.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));

            UIRichTextBox uIRichTextBox2 = new UIRichTextBox();
            uIRichTextBox2.Style = GameStyle.BindStyle();
            uIRichTextBox2.Dock = DockStyle.Fill;
            uIRichTextBox2.FillColor = Color.White;
            uIRichTextBox2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            uIRichTextBox2.Location = new Point(0, 0);
            uIRichTextBox2.Margin = new Padding(4, 5, 4, 5);
            uIRichTextBox2.MinimumSize = new Size(1, 1);
            uIRichTextBox2.Name = "uiRichTextBox2";
            uIRichTextBox2.Padding = new Padding(2);
            uIRichTextBox2.ReadOnly = true;
            uIRichTextBox2.ScrollBarStyleInherited = false;
            uIRichTextBox2.ShowText = false;
            uIRichTextBox2.Size = new Size(919, 189);
            uIRichTextBox2.TextAlignment = ContentAlignment.MiddleCenter;
            uIRichTextBox2.RadiusSides = UICornerRadiusSides.None;

            ListView listView1 = new ListView();
            listView1.Dock = DockStyle.Fill;
            listView1.GridLines = true;
            listView1.HideSelection = false;
            listView1.Location = new Point(0, 0);
            listView1.Name = "listView1";
            listView1.Size = new Size(919, 189);
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            tp1.Controls.Add(uIRichTextBox2);
            tp1.Location = new Point(0, 30);
            tp1.Name = "tabPage1";
            tp1.Size = new Size(919, 189);
            tp1.Text = "信息";
            tp1.UseVisualStyleBackColor = true;
            tp1.Padding = new Padding(1, 1, 1, 0);

            tp2.Controls.Add(listView1);
            tp2.Location = new Point(0, 30);
            tp2.Name = "tabPage2";
            tp2.Size = new Size(919, 189);
            tp2.Text = "结果";
            tp2.UseVisualStyleBackColor = true;
            tp2.Padding = new Padding(1, 1, 1, 0);

            usc.Panel2.Controls.Add(uITabControl);
            uiPanel7.Text = "已创建查询界面。";
        }
        DBInfo selectedDBinfo = null;
        private void tscb_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIComboBox cb = (UIComboBox)sender;
            selectedDBinfo = listDb[cb.SelectedIndex];
            if (selectedDBinfo.DataBaseType == "Excel(996引擎)")
            {
                this.ShowInfoTip("Excel类型数据库只能select where order by语句查询，无法进行增删改操作。", 5000, true);
            }
        }

        private void tsb_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            TabPage page = tbContent.SelectedTab;
            string sql = "";
            try
            {
                if (selectedDBinfo == null)
                {
                    this.ShowErrorTip("请在下拉框中选择要查询的数据库！");
                    return;
                }

                UIRichTextBox rtb = page.Controls.Find("uiRichTextBox1", true)[0] as UIRichTextBox;
                sql = rtb.SelectedText.Trim().Replace("\r\n", "").Replace("\n", "");
                if (string.IsNullOrEmpty(sql))
                {
                    string[] arrSQL = rtb.Text.Split('\n');
                    foreach (var item in arrSQL)
                    {
                        if (!item.StartsWith(";") && !string.IsNullOrEmpty(item))
                            sql += item;
                    }

                }


                DataTable dt = null;

                switch (selectedDBinfo.DataBaseType)
                {
                    case "Access":
                        dt = DBHelper.GetDataBySQL(selectedDBinfo, sql);
                        break;
                    case "SQLite":
                        dt = DBHelper.GetDataBySQL(selectedDBinfo, sql);
                        break;
                    case "MySQL":
                        dt = DBHelper.GetDataBySQL(selectedDBinfo, sql);
                        break;
                    case "MSSQL":
                        dt = DBHelper.GetDataBySQL(selectedDBinfo, sql);
                        break;
                    case "Excel(996引擎)":
                        dt = DBHelper.GetExcelDataBySQL(selectedDBinfo, sql);
                        break;
                }

                ListView listView1 = page.Controls.Find("listView1", true)[0] as ListView;
                listView1.Clear();
                ColumnHeader ch = new ColumnHeader();
                int dcIndex = 0;
                foreach (DataColumn dc in dt.Columns)
                {
                    listView1.Columns.Add(dc.ColumnName, 120, HorizontalAlignment.Center);
                    dcIndex++;
                }
                listView1.BeginUpdate();

                int c = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = dt.Rows[i][c].ToString();

                    c++;
                    for (int j = c; j < dcIndex; j++)
                    {
                        lvi.SubItems.Add(dt.Rows[i][j].ToString());
                    }
                    listView1.Items.Add(lvi);
                    c = 0;
                }
                listView1.EndUpdate();


                UITabControl uitc = page.Controls.Find("uiTabControl1", true)[0] as UITabControl;
                uitc.SelectedIndex = 1;
                UIRichTextBox richText = uitc.Controls.Find("uiRichTextBox2", true)[0] as UIRichTextBox;

                sw.Stop();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(sql);
                sb.AppendLine("执行成功！");
                sb.AppendLine($"执行时间：{sw.ElapsedMilliseconds}ms");
                richText.Text = sb.ToString();
                if (sql.Length > 60)
                    uiPanel7.Text = $"{sql.Substring(0, 60)}... (共条{dt.Rows.Count}记录)";
                else
                    uiPanel7.Text = $"{sql} (共条{dt.Rows.Count}记录)";
                uiPanel5.Text = $"执行时间：{sw.ElapsedMilliseconds}ms";
            }
            catch (Exception ex)
            {
                UITabControl uitc = page.Controls.Find("uiTabControl1", true)[0] as UITabControl;
                uitc.SelectedIndex = 0;
                UIRichTextBox richText = uitc.Controls.Find("uiRichTextBox2", true)[0] as UIRichTextBox;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(sql);
                sb.AppendLine("执行错误！");
                sb.AppendLine(ex.Message);
                richText.Text = sb.ToString();
                uiPanel7.Text = "执行错误！";
            }
        }


        private void OpenTable(bool isShowNode, TreeNode node = null)
        {
            TabPage page = new TabPage();
            page.ImageIndex = 6;
            page.BackColor = Color.White;

            if (isShowNode)
            {
                page.Text = lvContent.SelectedItems[0].Text;
                page.Name = lvContent.SelectedItems[0].Text;
                string key = lvContent.SelectedItems[0].SubItems[6].Text;
                node = SelectedTreeNodeByListView(tvDataBase.Nodes, key, page.Text);
                tvDataBase.SelectedNode = node;
                page.Tag = node.Tag;
            }
            else
            {
                page.Text = node.Text;
                page.Name = node.Name;
                page.Tag = node.Tag;
            }

            //bool flag = true;
            //foreach (TabPage tp in tbContent.TabPages)
            //{
            //    if (tp.Text == page.Text)
            //    {
            //        flag = false;
            //        page = tp;
            //    }
            //}

            //if (flag)
            //{
            //    tbContent.Controls.Add(page);
            //    tbContent.SelectedTab = page;
            //}
            //else
            //{
            //    tbContent.SelectedTab = page;
            //    return;
            //}
            tbContent.Controls.Add(page);
            tbContent.SelectedTab = page;

            #region 主体panel
            UIPanel panelMain = new UIPanel();
            panelMain.Dock = DockStyle.Fill;
            panelMain.RectSides = ToolStripStatusLabelBorderSides.Top;
            panelMain.RadiusSides = UICornerRadiusSides.None;
            CreateDGV(page);
            #endregion

            #region 底部panel
            UIPanel panelBotton = new UIPanel();
            panelBotton.Dock = DockStyle.Bottom;
            panelBotton.RectSides = ToolStripStatusLabelBorderSides.Top;
            panelBotton.RadiusSides = UICornerRadiusSides.None;
            panelBotton.Height = 35;
            panelBotton.Name = "panelBotton";
            page.Controls.Add(panelMain);
            page.Controls.Add(panelBotton);
            #endregion

            #region 底部中间的panel
            int x = 10, y = 0;
            UISymbolButton uiSymbolButton1 = new UISymbolButton();
            uiSymbolButton1.Style = GameStyle.BindStyle();
            uiSymbolButton1.Cursor = Cursors.Hand;
            uiSymbolButton1.Font = new Font("宋体", 12F);
            uiSymbolButton1.MinimumSize = new Size(1, 1);
            uiSymbolButton1.Name = "uiSymbolButton1";
            uiSymbolButton1.RadiusSides = (((UICornerRadiusSides.LeftTop | UICornerRadiusSides.LeftBottom)));
            uiSymbolButton1.Size = new Size(40, 30);
            uiSymbolButton1.Symbol = 61513;
            uiSymbolButton1.TipsText = "首行";
            uiSymbolButton1.Click += uiSymbolButton1_ClickEvent;

            uiSymbolButton1.Location = new Point(40 * y + x, 4);

            uiToolTip1.SetToolTip(uiSymbolButton1, "首行");
            uiSymbolButton1.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            panelBotton.Controls.Add(uiSymbolButton1);

            UISymbolButton uiSymbolButton2 = new UISymbolButton();
            uiSymbolButton2.Style = GameStyle.BindStyle();
            uiSymbolButton2.Cursor = Cursors.Hand;
            uiSymbolButton2.Font = new Font("宋体", 12F);
            uiSymbolButton2.MinimumSize = new Size(1, 1);
            uiSymbolButton2.Name = "uiSymbolButton2";
            uiSymbolButton2.RadiusSides = UICornerRadiusSides.None;
            uiSymbolButton2.Size = new Size(40, 30);
            uiSymbolButton2.Symbol = 61514;
            uiSymbolButton2.TipsText = "上一行";
            uiSymbolButton2.Click += uiSymbolButton2_ClickEvent;
            y++;
            uiSymbolButton2.Location = new Point(40 * y + x, 4);

            uiToolTip1.SetToolTip(uiSymbolButton2, "上一行");
            uiSymbolButton2.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            panelBotton.Controls.Add(uiSymbolButton2);

            UISymbolButton uiSymbolButton3 = new UISymbolButton();
            uiSymbolButton3.Style = GameStyle.BindStyle();
            uiSymbolButton3.Cursor = Cursors.Hand;
            uiSymbolButton3.Font = new Font("宋体", 12F);
            uiSymbolButton3.MinimumSize = new Size(1, 1);
            uiSymbolButton3.Name = "uiSymbolButton3";
            uiSymbolButton3.RadiusSides = UICornerRadiusSides.None;
            uiSymbolButton3.Size = new Size(40, 30);
            uiSymbolButton3.Symbol = 61518;
            uiSymbolButton3.TipsText = "下一行";
            uiSymbolButton3.Click += uiSymbolButton3_ClickEvent;
            y++;
            uiSymbolButton3.Location = new Point(40 * y + x, 4);

            uiToolTip1.SetToolTip(uiSymbolButton3, "下一行");
            uiSymbolButton3.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            panelBotton.Controls.Add(uiSymbolButton3);

            UISymbolButton uiSymbolButton4 = new UISymbolButton();
            uiSymbolButton4.Style = GameStyle.BindStyle();
            uiSymbolButton4.Cursor = Cursors.Hand;
            uiSymbolButton4.Font = new Font("宋体", 12F);
            uiSymbolButton4.MinimumSize = new Size(1, 1);
            uiSymbolButton4.Name = "uiSymbolButton4";
            uiSymbolButton4.RadiusSides = UICornerRadiusSides.None;
            uiSymbolButton4.Size = new Size(40, 30);
            uiSymbolButton4.Symbol = 61520;
            uiSymbolButton4.TipsText = "尾行";
            uiSymbolButton4.Click += uiSymbolButton4_ClickEvent;
            y++;
            uiSymbolButton4.Location = new Point(40 * y + x, 4);

            uiToolTip1.SetToolTip(uiSymbolButton4, "尾行");
            uiSymbolButton4.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            panelBotton.Controls.Add(uiSymbolButton4);

            UISymbolButton uiSymbolButton5 = new UISymbolButton();
            uiSymbolButton5.Style = GameStyle.BindStyle();
            uiSymbolButton5.Cursor = Cursors.Hand;
            uiSymbolButton5.Font = new Font("宋体", 12F);
            uiSymbolButton5.MinimumSize = new Size(1, 1);
            uiSymbolButton5.Name = "uiSymbolButton5";
            uiSymbolButton5.RadiusSides = UICornerRadiusSides.None;
            uiSymbolButton5.Size = new Size(40, 30);
            uiSymbolButton5.Symbol = 361543;
            uiSymbolButton5.TipsText = "增加";
            uiSymbolButton5.Click += uiSymbolButton5_ClickEvent;
            y++;
            uiSymbolButton5.Location = new Point(40 * y + x, 4);


            uiToolTip1.SetToolTip(uiSymbolButton5, "增加");
            uiSymbolButton5.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            panelBotton.Controls.Add(uiSymbolButton5);

            UISymbolButton uiSymbolButton6 = new UISymbolButton();
            uiSymbolButton6.Style = GameStyle.BindStyle();
            uiSymbolButton6.Cursor = Cursors.Hand;
            uiSymbolButton6.Font = new Font("宋体", 12F);
            uiSymbolButton6.MinimumSize = new Size(1, 1);
            uiSymbolButton6.Name = "uiSymbolButton6";
            uiSymbolButton6.RadiusSides = UICornerRadiusSides.None;
            uiSymbolButton6.Size = new Size(40, 30);
            uiSymbolButton6.Symbol = 361544;
            uiSymbolButton6.TipsText = "删除";
            uiSymbolButton6.Click += uiSymbolButton6_ClickEvent;
            y++;
            uiSymbolButton6.Location = new Point(40 * y + x, 4);

            uiToolTip1.SetToolTip(uiSymbolButton6, "删除");
            uiSymbolButton6.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            panelBotton.Controls.Add(uiSymbolButton6);

            UISymbolButton uiSymbolButton7 = new UISymbolButton();
            uiSymbolButton7.Style = GameStyle.BindStyle();
            uiSymbolButton7.Cursor = Cursors.Hand;
            uiSymbolButton7.Font = new Font("宋体", 12F);
            uiSymbolButton7.MinimumSize = new Size(1, 1);
            uiSymbolButton7.Name = "uiSymbolButton7";
            uiSymbolButton7.RadiusSides = UICornerRadiusSides.None;
            uiSymbolButton7.Size = new Size(40, 30);
            uiSymbolButton7.Symbol = 61452;
            uiSymbolButton7.TipsText = "保存";
            uiSymbolButton7.Enabled = false;
            uiSymbolButton7.Click += uiSymbolButton7_ClickEvent;
            y++;
            uiSymbolButton7.Location = new Point(40 * y + x, 4);

            uiToolTip1.SetToolTip(uiSymbolButton7, "保存");
            uiSymbolButton7.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, (byte)134);
            panelBotton.Controls.Add(uiSymbolButton7);

            UISymbolButton uiSymbolButton8 = new UISymbolButton();
            uiSymbolButton8.Style = GameStyle.BindStyle();
            uiSymbolButton8.Cursor = Cursors.Hand;
            uiSymbolButton8.Font = new Font("宋体", 12F);
            uiSymbolButton8.MinimumSize = new Size(1, 1);
            uiSymbolButton8.Name = "uiSymbolButton8";
            uiSymbolButton8.RadiusSides = UICornerRadiusSides.None;
            uiSymbolButton8.Size = new Size(40, 30);
            uiSymbolButton8.Symbol = 61453;
            uiSymbolButton8.TipsText = "取消";
            uiSymbolButton8.Enabled = false;
            uiSymbolButton8.Click += uiSymbolButton8_ClickEvent;
            y++;
            uiSymbolButton8.Location = new Point(40 * y + x, 4);

            uiToolTip1.SetToolTip(uiSymbolButton8, "取消");
            uiSymbolButton8.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            panelBotton.Controls.Add(uiSymbolButton8);

            UISymbolButton uiSymbolButton9 = new UISymbolButton();
            uiSymbolButton9.Style = GameStyle.BindStyle();
            uiSymbolButton9.Cursor = Cursors.Hand;
            uiSymbolButton9.Font = new Font("宋体", 12F);
            uiSymbolButton9.MinimumSize = new Size(1, 1);
            uiSymbolButton9.Name = "uiSymbolButton9";
            uiSymbolButton9.TipsText = "刷新";
            uiSymbolButton9.Click += uiSymbolButton9_ClickEvent;
            y++;
            uiSymbolButton9.Location = new Point(40 * y + x, 4);

            uiToolTip1.SetToolTip(uiSymbolButton9, "刷新");
            uiSymbolButton9.RadiusSides = UICornerRadiusSides.RightTop | UICornerRadiusSides.RightBottom;
            uiSymbolButton9.Size = new Size(40, 30);
            uiSymbolButton9.Symbol = 61470;
            uiSymbolButton9.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            panelBotton.Controls.Add(uiSymbolButton9);


            UITextBox textBox = new UITextBox();
            textBox.AutoSize = false;
            textBox.Cursor = Cursors.IBeam;
            textBox.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            textBox.ShowText = false;
            textBox.TextAlignment = ContentAlignment.MiddleLeft;
            textBox.Location = new Point(390, 4);
            textBox.Watermark = "请输入名称内容进行搜索";
            textBox.Size = new Size(350, 30);
            textBox.Style = GameStyle.BindStyle();
            panelBotton.Controls.Add(textBox);

            UISymbolButton sysBtn = new UISymbolButton();
            sysBtn.Cursor = Cursors.Hand;
            sysBtn.Font = new Font("宋体", 12F);
            sysBtn.Location = new Point(750, 3);
            sysBtn.MinimumSize = new Size(1, 1);
            sysBtn.Size = new Size(80, 30);
            sysBtn.Style = UIStyle.Inherited;
            sysBtn.Symbol = 561487;
            sysBtn.StyleCustomMode = true;
            sysBtn.Text = "查询";
            sysBtn.Style = GameStyle.BindStyle();
            sysBtn.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            sysBtn.Click += sysBtn_ClickEvent;
            panelBotton.Controls.Add(sysBtn);

            #endregion
        }

        private void CreateDB()
        {
            try
            {
                FrmCreateDB frmCreateDB = new FrmCreateDB(listDb, 0);
                DialogResult dia = frmCreateDB.ShowDialog();
                if (dia == DialogResult.OK)
                {
                    TreeNode node = tvDataBase.Nodes[0];

                    TreeNode subNode = node.Nodes.Add($"{frmCreateDB.DB.DataBaseName}_{frmCreateDB.DB.DataBaseType}", $"{frmCreateDB.DB.DataBaseName}({frmCreateDB.DB.DataBaseType})({frmCreateDB.DB.MirDBType})", 2);
                    subNode.Tag = JsonHelper.JsonSerializer<DBInfo>(frmCreateDB.DB);
                    subNode.SetProperty("isOpenDB", false);
                    this.ShowSuccessTip("创建成功！");
                    uiPanel7.Text = $"数据库 {frmCreateDB.DB.DataBaseName} 创建成功！";
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorTip(ex.Message);
            }
        }

        private void CutConnection()
        {
            TreeNode node = tvDataBase.SelectedNode;
            node.ImageIndex = 0;
            node.SetProperty("isConnectionDB", false);

            lvContent.Items.Clear();
            tvDataBase.Nodes[0].Nodes.Clear();

            //for (int i = tvDataBase.Nodes[0].Nodes.Count - 1; i >= 0; i--)
            //{
            //    tvDataBase.Nodes[0].Nodes.RemoveAt(i);
            //}

            for (int i = tbContent.TabPages.Count - 1; i > 0; i--)
            {
                if (tbContent.TabPages[i].Text != "主页" && !tbContent.TabPages[i].Text.Contains("SQL-查询"))
                    tbContent.TabPages.RemoveAt(i);
            }

            uiPanel7.Text = "数据库 [" + node.Text + "] 已断开连接！";
        }

        private void GetConnection()
        {
            TreeNode node = tvDataBase.SelectedNode;
            if (!(bool)node.GetProperty("isConnectionDB"))
                ShowAllDataBase(node);
        }

        private void tvDataBase_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Clicks == 2 && e.Button == MouseButtons.Left)
            {
                TreeNode node = e.Node;

                switch (node.Text.ToLower())
                {
                    case "数据库":
                        if (!(bool)node.GetProperty("isConnectionDB"))
                            ShowAllDataBase(node);
                        break;
                    case "magic":
                    case "monster":
                    case "stditems": ShowTablePage(node); break;
                    default:
                        if (!(bool)node.GetProperty("isOpenDB"))
                            ShowTable(node);
                        break;
                }
            }

        }
        private void ShowTablePage(TreeNode node)
        {
            lvContent.Items.Clear();
            DBInfo dbInfo = JsonHelper.JsonDeserialize<DBInfo>(node.Tag.ToString());
            BindData(dbInfo);
            node.ImageIndex = 5;

            //双击创建tabpage并且创建表内容
            OpenTable(false, node);
        }


        private void ShowTable(TreeNode node)
        {
            lvContent.Items.Clear();
            DBInfo dbInfo = JsonHelper.JsonDeserialize<DBInfo>(node.Tag.ToString());
            BindData(dbInfo);
            node.ImageIndex = 3;
            node.SetProperty("isOpenDB", true);

            AddSub(node, null);
            uiPanel7.Text = "打开数据库 [" + dbInfo.DataBaseName + "] 成功！";
        }

        private void ShowAllDataBase(TreeNode node)
        {
            node.ImageIndex = 1;
            node.SetProperty("isConnectionDB", true);

            AddSub(node, listDb);
            node.ExpandAll();
            uiPanel7.Text = "数据库 [" + node.Text + "] 连接成功！";
        }

        private void BindData(DBInfo db)
        {
            string ConnectionString = "";
            try
            {
                switch (db.DataBaseType)
                {
                    case "Access":
                        ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + db.DataFilePath + ";Persist Security Info=False;";
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.Access);
                        StdItemsService = new StdItemsServer(SqlSugar.DbType.Access);
                        MagicService = new MagicServer(SqlSugar.DbType.Access);
                        break;
                    case "SQLite":
                        ConnectionString = "data source=" + db.DataFilePath;
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.Sqlite);
                        StdItemsService = new StdItemsServer(SqlSugar.DbType.Sqlite);
                        MagicService = new MagicServer(SqlSugar.DbType.Sqlite);
                        break;
                    case "MySQL":
                        {
                            string addr = db.DataBaseAddr;
                            string dbname = db.DataBaseName;
                            string username = db.DataBaseUserName;
                            string password = db.DataBasePassWord;
                            ConnectionString = $"server={addr};Database={dbname};Uid={username};Pwd={password}";
                            PubConstant.ConnectionString = ConnectionString;
                            MonsterService = new MonsterServer(SqlSugar.DbType.MySql);
                            StdItemsService = new StdItemsServer(SqlSugar.DbType.MySql);
                            MagicService = new MagicServer(SqlSugar.DbType.MySql);
                        }

                        break;
                    case "MSSQL":
                        {
                            string addr = db.DataBaseAddr;
                            string dbname = db.DataBaseName;
                            string username = db.DataBaseUserName;
                            string password = db.DataBasePassWord;
                            ConnectionString = $"server={addr};Database={dbname};Uid={username};Pwd={password}";
                            PubConstant.ConnectionString = ConnectionString;
                            MonsterService = new MonsterServer(SqlSugar.DbType.SqlServer);
                            StdItemsService = new StdItemsServer(SqlSugar.DbType.SqlServer);
                            MagicService = new MagicServer(SqlSugar.DbType.SqlServer);
                        }
                        break;
                    case "Excel(996引擎)":
                        Workbook wk = new Workbook();
                        wk.LoadFromFile(db.DataFilePath);
                        MagicDT = wk.Worksheets["Magic"].ExportDataTable();
                        MonsterDT = wk.Worksheets["Monster"].ExportDataTable();
                        StdItemsDT = wk.Worksheets["StdItems"].ExportDataTable();

                        string where = "and Stdmode in ({0})";
                        where = string.Format(where, string.Join(",", std));
                        if (std.Count > 0)
                            StdItemsDT.DefaultView.RowFilter = string.Format("Stdmode in ({0})", std);
                        break;
                }

                if (db.DataBaseType != "Excel(996引擎)")
                {
                    MonsterDT = MonsterService.GetMonsterByTable();
                    string where = "and Stdmode in ({0})";
                    where = string.Format(where, string.Join(",", std));
                    if (std.Count == 0) where = "";
                    StdItemsDT = StdItemsService.GetStdItemsByTable(where);
                    MagicDT = MagicService.GetMagicByTable();
                }


                string idx = "";
                string magId = "";
                if (MagicDT.Columns.Contains("magid"))
                {
                    string strmagid = MagicDT.Compute("Max(magid)", "true").ToString();
                    int.TryParse(strmagid, out int max);//最大值
                    max++;
                    magId = max + "";
                }
                if (StdItemsDT.Columns.Contains("idx"))
                {
                    string strIdx = StdItemsDT.Compute("Max(idx)", "true").ToString();
                    int.TryParse(strIdx, out int max);//最大值
                    max++;
                    idx = max + "";
                }


                ListViewItem lvi = new ListViewItem() { Text = "Magic", ImageIndex = 5, };
                lvi.SubItems.Add(MagicDT.Rows.Count.ToString());
                lvi.SubItems.Add(magId);
                lvi.SubItems.Add("系统");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add($"{db.DataBaseName}_{db.DataBaseType}");

                lvContent.Items.Add(lvi);

                lvi = new ListViewItem() { Text = "Monster", ImageIndex = 5, };
                lvi.SubItems.Add(MonsterDT.Rows.Count.ToString());
                lvi.SubItems.Add(MonsterDT.Rows.Count.ToString());
                lvi.SubItems.Add("系统");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add($"{db.DataBaseName}_{db.DataBaseType}");

                lvContent.Items.Add(lvi);

                lvi = new ListViewItem() { Text = "StdItems", ImageIndex = 5, };
                lvi.SubItems.Add(StdItemsDT.Rows.Count.ToString());
                lvi.SubItems.Add(idx);
                lvi.SubItems.Add("系统");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add($"{db.DataBaseName}_{db.DataBaseType}");

                lvContent.Items.Add(lvi);

                lvContent.View = View.Details;
                uiPanel7.Text = $"数据库[{db.DataBaseName}] 表数据读取成功！";
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog2(ex.Message);
            }
        }



        private void 大图标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvContent.View = View.LargeIcon;
        }

        private void 小图标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvContent.View = View.SmallIcon;
        }

        private void 列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvContent.View = View.List;
        }

        private void 详细列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvContent.View = View.Details;
        }

        private void 磁铁ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvContent.View = View.Tile;
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            uiSplitContainer3.Panel2Collapsed = !uiSplitContainer3.Panel2Collapsed;
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            uiSplitContainer1.Panel1Collapsed = !uiSplitContainer1.Panel1Collapsed;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            lvContent.View = View.List;
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            lvContent.View = View.Details;
        }

        private void lvContent_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //双击创建tabpage并且创建表内容
                OpenTable(true);
            }
        }


        private void sysBtn_ClickEvent(object sender, EventArgs e)
        {
            string text = CurrentPage.Controls[2].Controls[9].Text;


            UIDataGridView dgv = CurrentPage.Controls[0] as UIDataGridView;

            switch (CurrentPage.Text)
            {
                case "Magic":
                    MagicDT.DefaultView.RowFilter = $"MagName like '%{text}%'";
                    dgv.DataSource = MagicDT.DefaultView.ToTable();
                    break;
                case "Monster":
                    MonsterDT.DefaultView.RowFilter = $"Name like '%{text}%'";
                    dgv.DataSource = MonsterDT.DefaultView.ToTable();
                    break;
                case "StdItems":
                    StdItemsDT.DefaultView.RowFilter = $"Name like '%{text}%'";
                    dgv.DataSource = StdItemsDT.DefaultView.ToTable();
                    break;
                default: break;
            }

        }

        private void uiSymbolButton9_ClickEvent(object sender, EventArgs e)
        {
            DBInfo dbInfo = JsonHelper.JsonDeserialize<DBInfo>(CurrentPage.Tag.ToString());
            UIDataGridView dgv = CurrentPage.Controls[0] as UIDataGridView;
            dgv.DataSource = new DataTable();
            CreateMagicDGV(dgv);
            switch (CurrentPage.Text)
            {
                case "Magic":
                    if (dbInfo.DataBaseType != "Excel(996引擎)")
                    {
                        MagicDT = MagicService.GetMagicByTable();
                    }
                    dgv.DataSource = MagicDT;
                    break;
                case "Monster":
                    if (dbInfo.DataBaseType != "Excel(996引擎)")
                    {
                        MonsterDT = MonsterService.GetMonsterByTable();
                    }
                    dgv.DataSource = MonsterDT;
                    break;
                case "StdItems":
                    if (dbInfo.DataBaseType != "Excel(996引擎)")
                    {
                        StdItemsDT = StdItemsService.GetStdItemsByTable("");
                    }
                    dgv.DataSource = StdItemsDT;
                    break;
                default: break;
            }

            UISymbolButton sbtn1 = CurrentPage.Controls[2].Controls[7] as UISymbolButton;
            sbtn1.Enabled = false;
            UISymbolButton sbtn2 = CurrentPage.Controls[2].Controls[6] as UISymbolButton;
            sbtn2.Enabled = false;
        }

        private void uiSymbolButton8_ClickEvent(object sender, EventArgs e)
        {
            if (SqlColmun.Count > 0)
            {
                SqlColmun["SaveKey"] = SqlColmun["oldKey"];
                SqlColmun["SaveValue"] = SqlColmun["oldValue"];

                try
                {
                    DBInfo dbInfo = JsonHelper.JsonDeserialize<DBInfo>(CurrentPage.Tag.ToString());
                    switch (CurrentPage.Text)
                    {
                        case "Magic":
                            if (dbInfo.DataBaseType != "Excel(996引擎)")
                            {
                                MagicService.SaveSingle(CurrentPage.Text, SqlColmun);
                            }
                            break;
                        case "Monster":
                            if (dbInfo.DataBaseType != "Excel(996引擎)")
                            {
                                MonsterService.SaveSingle(CurrentPage.Text, SqlColmun);
                            }
                            break;
                        case "StdItems":
                            if (dbInfo.DataBaseType != "Excel(996引擎)")
                            {
                                StdItemsService.SaveSingle(CurrentPage.Text, SqlColmun);
                            }
                            break;
                        default: break;
                    }

                    UISymbolButton sbtn1 = CurrentPage.Controls[2].Controls[7] as UISymbolButton;
                    sbtn1.Enabled = false;

                    UIDataGridView dgv = CurrentPage.Controls[0] as UIDataGridView;
                    int rowIndex = Convert.ToInt32(SqlColmun["oldRowIndex"]);
                    //如果为空表示撤销添加的记录，所以要删除数据后删除UIDataGridView控件数据
                    if (string.IsNullOrEmpty(SqlColmun["oldValue"]))
                    {
                        dgv.Rows.RemoveAt(rowIndex);
                    }
                    else
                    {
                        DataTable dt = dgv.DataSource as DataTable;
                        int columnIndex = dgv.CurrentCell.ColumnIndex;
                        dt.Rows[rowIndex][columnIndex] = SqlColmun["oldValue"];
                    }


                }
                catch (Exception ex)
                {
                    this.ShowErrorTip(ex.Message);
                }
            }
        }

        private void uiSymbolButton7_ClickEvent(object sender, EventArgs e)
        {
            SaveByStorageable();
        }
        private void SaveByStorageable()
        {
            try
            {
                DBInfo dbInfo = JsonHelper.JsonDeserialize<DBInfo>(CurrentPage.Tag.ToString());

                UIDataGridView dgv = CurrentPage.Controls[0] as UIDataGridView;
                List<int> selectedIndexes = new List<int>();
                if (dgv.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgv.SelectedRows)
                    {
                        selectedIndexes.Add(row.Index);
                    }
                }

                DataTable dt = dgv.DataSource as DataTable;
                DataTable ndt = dt.Copy();
                for (int i = ndt.Rows.Count - 1; i >= 0; i--)
                {
                    if (!selectedIndexes.Contains(i))
                    {
                        ndt.Rows.RemoveAt(i);
                    }
                }

                switch (CurrentPage.Text)
                {
                    case "Magic":
                        if (dbInfo.DataBaseType == "Excel(996引擎)")
                        {
                            Workbook wk = new Workbook();
                            wk.LoadFromFile(dbInfo.DataFilePath);
                            Worksheet worksheet = wk.Worksheets["Magic"];
                            worksheet.InsertDataTable(MagicDT, true, 1, 1);
                            wk.Save();
                        }
                        else
                        {
                            MagicService.SaveByStorageable(ndt);
                        }
                        break;
                    case "Monster":
                        if (dbInfo.DataBaseType == "Excel(996引擎)")
                        {
                            Workbook wk = new Workbook();
                            wk.LoadFromFile(dbInfo.DataFilePath);
                            Worksheet worksheet = wk.Worksheets["Monster"];
                            worksheet.InsertDataTable(MonsterDT, true, 1, 1);
                            wk.Save();
                        }
                        else
                        {
                            MonsterService.SaveByStorageable(ndt);
                        }
                        break;
                    case "StdItems":
                        if (dbInfo.DataBaseType == "Excel(996引擎)")
                        {
                            Workbook wk = new Workbook();
                            wk.LoadFromFile(dbInfo.DataFilePath);
                            Worksheet worksheet = wk.Worksheets["StdItems"];
                            worksheet.InsertDataTable(StdItemsDT, true, 1, 1);
                            wk.Save();
                        }
                        else
                        {
                            StdItemsService.SaveByStorageable(ndt);
                        }

                        break;
                    default: break;
                }
                ndt.AcceptChanges();
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog2(ex.Message);
            }
        }
        private void uiSymbolButton6_ClickEvent(object sender, EventArgs e)
        {
            UIDataGridView dgv = CurrentPage.Controls[0] as UIDataGridView;

            // 获取选定的行的索引
            if (dgv.SelectedRows.Count > 0)
            {
                string delId = "";
                List<int> selectedIndexes = new List<int>();
                foreach (DataGridViewRow row in dgv.SelectedRows)
                {
                    delId = dgv.Rows[row.Index].Cells[0].Value.ToString();
                    if (!delDGVList.Contains(delId))
                        delDGVList.Add(delId);
                    selectedIndexes.Add(row.Index);
                }

                if (this.ShowAskDialog2("提示", "选择多行可删除多条数据。\n确定删除 " + selectedIndexes.Count + " 条数据吗?", true))
                {
                    // 根据索引删除选定的行
                    selectedIndexes = selectedIndexes.OrderByDescending(s => s).ToList();
                    foreach (int index in selectedIndexes)
                    {
                        dgv.Rows.RemoveAt(index);
                    }

                    DBInfo dbInfo = JsonHelper.JsonDeserialize<DBInfo>(CurrentPage.Tag.ToString());

                    switch (CurrentPage.Text)
                    {
                        case "Magic":
                            if (dbInfo.DataBaseType != "Excel(996引擎)")
                            {
                                MagicService.Delete(delDGVList);
                            }
                            break;
                        case "Monster":
                            if (dbInfo.DataBaseType != "Excel(996引擎)")
                            {
                                MonsterService.Delete(delDGVList);
                            }

                            break;
                        case "StdItems":
                            if (dbInfo.DataBaseType != "Excel(996引擎)")
                            {
                                StdItemsService.Delete(delDGVList);
                            }

                            break;
                        default: break;
                    }

                }
            }
            else
            {
                this.ShowErrorTip("未选中任何数据，删除失败！");
            }
        }

        private void uiSymbolButton5_ClickEvent(object sender, EventArgs e)
        {
            UIDataGridView dgv = CurrentPage.Controls[0] as UIDataGridView;
            DataTable dtProLine = dgv.DataSource as DataTable;

            DataRow dr = dtProLine.NewRow();
            if (dr.Table.Columns.Contains("idx"))
            {
                string strIdx = dtProLine.Compute("Max(idx)", "true").ToString();
                int.TryParse(strIdx, out int max);//最大值
                max++;
                dr["idx"] = max;

            }
            if (dr.Table.Columns.Contains("magid"))
            {
                string strmagid = dtProLine.Compute("Max(magid)", "true").ToString();
                int.TryParse(strmagid, out int max);//最大值
                max++;
                dr["magid"] = max;
            }
            //插入到指定索引的行的前面
            dtProLine.Rows.Add(dr);//数据源添加空行
            int lastRowIndex = dgv.Rows.Count - 1;
            dgv.FirstDisplayedScrollingRowIndex = lastRowIndex;

            UISymbolButton sbtn = CurrentPage.Controls[2].Controls[6] as UISymbolButton;
            sbtn.Enabled = true;
        }

        private void uiSymbolButton3_ClickEvent(object sender, EventArgs e)
        {
            UIDataGridView dgv = CurrentPage.Controls[0] as UIDataGridView;
            int rowIndex = 0;
            if (dgv.SelectedRows.Count > 0)
            {
                rowIndex = dgv.SelectedRows[0].Index;
            }
            rowIndex++;
            if (rowIndex >= dgv.Rows.Count)
            {
                rowIndex = 0;
                dgv.FirstDisplayedScrollingRowIndex = rowIndex;
            }
            dgv.ClearSelection();
            dgv.Rows[rowIndex].Selected = true;
            BinddDGVData(dgv, dgv.Rows[rowIndex]);
        }

        private void uiSymbolButton2_ClickEvent(object sender, EventArgs e)
        {
            UIDataGridView dgv = CurrentPage.Controls[0] as UIDataGridView;
            int rowIndex = 0;
            if (dgv.SelectedRows.Count > 0)
            {
                rowIndex = dgv.SelectedRows[0].Index;
            }
            rowIndex--;
            if (rowIndex < 0)
            {
                rowIndex = dgv.Rows.Count - 1;
                dgv.FirstDisplayedScrollingRowIndex = rowIndex;
            }
            dgv.ClearSelection();
            dgv.Rows[rowIndex].Selected = true;
            BinddDGVData(dgv, dgv.Rows[rowIndex]);
        }

        private void uiSymbolButton4_ClickEvent(object sender, EventArgs e)
        {
            UIDataGridView dgv = CurrentPage.Controls[0] as UIDataGridView;

            int lastRowIndex = dgv.Rows.Count - 1;
            DataGridViewRow row = dgv.Rows[lastRowIndex];
            row.Selected = true;
            dgv.FirstDisplayedScrollingRowIndex = lastRowIndex;
            BinddDGVData(dgv, row);
        }

        private void uiSymbolButton1_ClickEvent(object sender, EventArgs e)
        {
            UIDataGridView dgv = CurrentPage.Controls[0] as UIDataGridView;
            var row = dgv.Rows[0];
            row.Selected = true;
            dgv.FirstDisplayedScrollingRowIndex = 0;
            BinddDGVData(dgv, row);
        }

        private TreeNode SelectedTreeNodeByListView(TreeNodeCollection nodes, string key, string sunNodeKey)
        {
            TreeNode tn = null;
            foreach (TreeNode node in nodes)
            {
                if (node.Name != key)
                {
                    tn = SelectedTreeNodeByListView(node.Nodes, key, sunNodeKey);
                }
                else
                {
                    foreach (TreeNode subNode in node.Nodes)
                    {
                        if (subNode.Text.ToLower() == sunNodeKey.ToLower())
                        {
                            subNode.Checked = true;
                            subNode.ImageIndex = 5;
                            tn = subNode;
                            return tn;
                        }
                    }

                }
            }
            return tn;
        }

        /// <summary>
        /// 创建数据表格内容
        /// </summary>
        private void CreateDGV(TabPage page)
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            UIDataGridView dgv = new UIDataGridView();
            
            dgv.CellClick += dgv_cellClick;
            dgv.CellValueChanged += dgv_CellValueChanged;
            dgv.EditingControlShowing += dgv_EditingControlShowing;
            dgv.CellLeave += dgv_CellLeave;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgv.BackgroundColor = Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgv.ColumnHeadersHeight = 38;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgv.DefaultCellStyle = dataGridViewCellStyle3;
            dgv.Dock = DockStyle.Fill;
            dgv.EnableHeadersVisualStyles = false;
            dgv.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            dgv.GridColor = Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(173)))), ((int)(((byte)(255)))));
            dgv.Location = new Point(0, 35);

            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgv.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionForeColor = Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dgv.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dgv.RowTemplate.Height = 23;
            dgv.SelectedIndex = -1;

            switch (page.Text)
            {
                case "Magic":
                    CreateMagicDGV(dgv);
                    break;
                case "Monster":
                    CreateMonsterDGV(dgv);
                    break;
                case "StdItems":
                    CreateStdItemsDGV(dgv);
                    break;
                default: break;
            }

            dgv.Style = GameStyle.BindStyle();

            page.Controls.Add(dgv);
        }


        private void dgv_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (CurrentPage.Controls.Count > 2)
            {
                UISymbolButton sbtn1 = CurrentPage.Controls[2].Controls[7] as UISymbolButton;
                sbtn1.Enabled = false;
                UISymbolButton sbtn2 = CurrentPage.Controls[2].Controls[6] as UISymbolButton;
                sbtn2.Enabled = false;
            }
        }

        private void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            SqlColmun.Clear();
            UIDataGridView ddgv = (UIDataGridView)sender;
            DataTable dt = ddgv.DataSource as DataTable;
            int index = ddgv.SelectedRows[0].Index;
            string saveKey = dt.Rows[index][0].ToString();
            switch (CurrentPage.Text)
            {
                case "Magic":
                    SqlColmun.Add("PrimaryKey", "MagID");
                    SqlColmun.Add("PrimaryValue", saveKey);
                    break;
                case "Monster":
                    SqlColmun.Add("PrimaryKey", "Name");
                    SqlColmun.Add("PrimaryValue", "'" + saveKey + "'");
                    break;
                case "StdItems":
                    SqlColmun.Add("PrimaryKey", "Idx");
                    SqlColmun.Add("PrimaryValue", saveKey);
                    break;
                default: break;
            }

            // 获取当前编辑的单元格位置
            int rowIndex = ddgv.CurrentCell.RowIndex;
            int columnIndex = ddgv.CurrentCell.ColumnIndex;

            // 获取列名
            string columnName = ddgv.Columns[columnIndex].Name;

            SqlColmun.Add("oldKey", columnName);
            SqlColmun.Add("oldValue", e.Control.Text);
            SqlColmun.Add("oldRowIndex", rowIndex.ToString());
            UISymbolButton sbtn2 = CurrentPage.Controls[2].Controls[6] as UISymbolButton;
            sbtn2.Enabled = true;
            UISymbolButton sbtn1 = CurrentPage.Controls[2].Controls[7] as UISymbolButton;
            sbtn1.Enabled = false;
        }


        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1) return;
                DBInfo dbInfo = JsonHelper.JsonDeserialize<DBInfo>(CurrentPage.Tag.ToString());

                UIDataGridView dgv = (UIDataGridView)sender;
                DataTable dt = dgv.DataSource as DataTable;

                DataTable ndt = dt.Copy();
                for (int i = ndt.Rows.Count - 1; i >= 0; i--)
                {
                    if (i != e.RowIndex)
                    {
                        ndt.Rows.RemoveAt(i);
                    }
                }
                ndt.AcceptChanges();

                DataGridViewCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string cellstr = cell.FormattedValue.ToString();

                SqlColmun.Add("SaveKey", dgv.Columns[e.ColumnIndex].DataPropertyName);
                switch (dgv.Columns[e.ColumnIndex].DataPropertyName.ToLower())
                {
                    case "magname":
                    case "name": SqlColmun.Add("SaveValue", "'" + cellstr + "'"); break;
                    default: SqlColmun.Add("SaveValue", cellstr); break;
                }

                switch (CurrentPage.Text)
                {
                    case "Magic":
                        if (dbInfo.DataBaseType == "Excel(996引擎)")
                        {
                            Workbook wk = new Workbook();
                            wk.LoadFromFile(dbInfo.DataFilePath);
                            Worksheet worksheet = wk.Worksheets["Magic"];
                            worksheet.InsertDataTable(MagicDT, true, 1, 1);
                            wk.Save();
                        }
                        else
                        {
                            MagicService.SaveSingle(CurrentPage.Text, SqlColmun);
                        }
                        break;
                    case "Monster":
                        if (dbInfo.DataBaseType == "Excel(996引擎)")
                        {
                            Workbook wk = new Workbook();
                            wk.LoadFromFile(dbInfo.DataFilePath);
                            Worksheet worksheet = wk.Worksheets["Monster"];
                            worksheet.InsertDataTable(MonsterDT, true, 1, 1);
                            wk.Save();
                        }
                        else
                        {
                            MonsterService.SaveSingle(CurrentPage.Text, SqlColmun);
                        }

                        break;
                    case "StdItems":
                        if (dbInfo.DataBaseType == "Excel(996引擎)")
                        {
                            Workbook wk = new Workbook();
                            wk.LoadFromFile(dbInfo.DataFilePath);
                            Worksheet worksheet = wk.Worksheets["StdItems"];
                            worksheet.InsertDataTable(StdItemsDT, true, 1, 1);
                            wk.Save();
                        }
                        else
                        {
                            StdItemsService.SaveSingle(CurrentPage.Text, SqlColmun);
                        }
                        break;
                    default: break;
                }

                UISymbolButton sbtn2 = CurrentPage.Controls[2].Controls[6] as UISymbolButton;
                sbtn2.Enabled = false;
                UISymbolButton sbtn1 = CurrentPage.Controls[2].Controls[7] as UISymbolButton;
                sbtn1.Enabled = true;
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog2(ex.Message);
            }

        }

        private void dgv_cellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            UIDataGridView dgv = (UIDataGridView)sender;
            var row = dgv.Rows[e.RowIndex];

            BinddDGVData(dgv, row);
        }
        private void BinddDGVData(UIDataGridView dgv, DataGridViewRow row)
        {
            string columnName = "";
            string dataPropertyName = "";
            lbContent.Items.Clear();
            if (dgv.Name == "dgv_Magic")
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dataPropertyName = dgv.Columns[cell.ColumnIndex].DataPropertyName.ToLower();

                    if (int.TryParse(cell.Value.ToString(), out int n))
                    {
                        if (n <= 0) continue;
                    }
                    switch (dataPropertyName)
                    {
                        case "magid": columnName = $"技能序号:{cell.Value}"; break;
                        case "magname": columnName = $"技能名称:{cell.Value}"; break;
                        case "effecttype": columnName = $"动作效果:{cell.Value}"; break;
                        case "effect": columnName = $"技能效果:{cell.Value}"; break;
                        case "spell": columnName = $"技能消耗:{cell.Value}"; break;
                        case "power": columnName = $"伤害下限:{cell.Value}"; break;
                        case "maxpower": columnName = $"伤害上限:{cell.Value}"; break;
                        case "defspell": columnName = $"升级魔法值:{cell.Value}"; break;
                        case "defpower": columnName = $"升级伤害下限:{cell.Value}"; break;
                        case "defmaxpower": columnName = $"升级伤害上限:{cell.Value}"; break;
                        case "job": columnName = $"职    业:{cell.Value}"; break;
                        case "needl1": columnName = $"1级等级:{cell.Value}"; break;
                        case "l1train": columnName = $"1级熟练度:{cell.Value}"; break;
                        case "needl2": columnName = $"2级等级:{cell.Value}"; break;
                        case "l2train": columnName = $"2级熟练度:{cell.Value}"; break;
                        case "needl3": columnName = $"3级等级:{cell.Value}"; break;
                        case "l3train": columnName = $"3级熟练度:{cell.Value}"; break;
                        case "needl4": columnName = $"4级等级:{cell.Value}"; break;
                        case "l4train": columnName = $"4级熟练度:{cell.Value}"; break;
                        case "needl5": columnName = $"5级等级:{cell.Value}"; break;
                        case "l5train": columnName = $"5级熟练度:{cell.Value}"; break;
                        case "needl6": columnName = $"6级等级:{cell.Value}"; break;
                        case "l6train": columnName = $"6级熟练度:{cell.Value}"; break;
                        case "needl7": columnName = $"7级等级:{cell.Value}"; break;
                        case "l7train": columnName = $"7级熟练度:{cell.Value}"; break;
                        case "needl8": columnName = $"8级等级:{cell.Value}"; break;
                        case "l8train": columnName = $"8级熟练度:{cell.Value}"; break;
                        case "needl9": columnName = $"9级等级:{cell.Value}"; break;
                        case "l9train": columnName = $"9级熟练度:{cell.Value}"; break;
                        case "needl10": columnName = $"10级等级:{cell.Value}"; break;
                        case "l10train": columnName = $"10级熟练度:{cell.Value}"; break;
                        case "needl11": columnName = $"11级等级:{cell.Value}"; break;
                        case "l11train": columnName = $"11级熟练度:{cell.Value}"; break;
                        case "needl12": columnName = $"12级等级:{cell.Value}"; break;
                        case "l12train": columnName = $"12级熟练度:{cell.Value}"; break;
                        case "needl13": columnName = $"13级等级:{cell.Value}"; break;
                        case "l13train": columnName = $"怪物名称:{cell.Value}"; break;
                        case "needl14": columnName = $"14级等级:{cell.Value}"; break;
                        case "l14train": columnName = $"14级熟练度:{cell.Value}"; break;
                        case "needl15": columnName = $"15级等级:{cell.Value}"; break;
                        case "l15train": columnName = $"15级熟练度:{cell.Value}"; break;
                        case "maxtrainlv": columnName = $"可修炼等级:{cell.Value}"; break;
                        case "delay": columnName = $"延    迟:{cell.Value}"; break;
                        case "descr": columnName = $"备    注:{cell.Value}"; break;
                        default: columnName = $"未    知:{cell.Value}"; break;
                    }
                    lbContent.Items.Add(columnName);
                }
            }
            else if (dgv.Name == "dgv_Monster")
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dataPropertyName = dgv.Columns[cell.ColumnIndex].DataPropertyName.ToLower();

                    if (int.TryParse(cell.Value.ToString(), out int n))
                    {
                        if (n == 0) continue;
                    }
                    switch (dataPropertyName)
                    {
                        case "name": columnName = $"怪物名称:{cell.Value}"; break;
                        case "race": columnName = $"攻击类型:{cell.Value}"; break;
                        case "raceimg": columnName = $"攻击图像:{cell.Value}"; break;
                        case "appr": columnName = $"怪物外观:{cell.Value}"; break;
                        case "lvl": columnName = $"怪物等级:{cell.Value}"; break;
                        case "undead": columnName = $"不 死 系:{cell.Value}"; break;
                        case "cooleye": columnName = $"反 隐 身:{cell.Value}"; break;
                        case "exp": columnName = $"经 验 值:{cell.Value}"; break;
                        case "hp": columnName = $"生 命 值:{cell.Value}"; break;
                        case "maxhp": columnName = $"大生命值:{cell.Value}"; break;
                        case "mp": columnName = $"魔 法 值:{cell.Value}"; break;
                        case "ac": columnName = $"防    御:{cell.Value}"; break;
                        case "mac": columnName = $"魔    御:{cell.Value}"; break;
                        case "dc": columnName = $"攻击下限:{cell.Value}"; break;
                        case "dcmax": columnName = $"攻击上限:{cell.Value}"; break;
                        case "mc": columnName = $"魔    法:{cell.Value}"; break;
                        case "sc": columnName = $"道    术:{cell.Value}"; break;
                        case "speed": columnName = $"速    度:{cell.Value}"; break;
                        case "hit": columnName = $"命 中 率:{cell.Value}"; break;
                        case "walk_spd": columnName = $"移动速度:{cell.Value}"; break;
                        case "walkstep": columnName = $"行走步伐:{cell.Value}"; break;
                        case "walkwait": columnName = $"行走等待:{cell.Value}"; break;
                        case "attack_spd": columnName = $"攻击速度:{cell.Value}"; break;
                        case "exploreitem": columnName = $"是否支持探索:{cell.Value}"; break;
                        case "inlevel": columnName = $"怪物内功等级:{cell.Value}"; break;
                        case "ipexp": columnName = $"内功经验值:{cell.Value}"; break;
                        default: columnName = $"未    知:{cell.Value}"; break;
                    }
                    lbContent.Items.Add(columnName);
                }
            }
            else
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!dgv.Columns.ContainsIndex(cell.ColumnIndex)) continue;

                    dataPropertyName = dgv.Columns[cell.ColumnIndex].DataPropertyName.ToLower();

                    if (int.TryParse(cell.Value.ToString(), out int n))
                    {
                        if (n <= 0) continue;
                    }
                    switch (dataPropertyName)
                    {
                        //物品字段加载
                        case "idx": columnName = $"序    号: {cell.Value}"; break;
                        case "name": columnName = $"物品名称: {cell.Value}"; break;
                        case "stdmode":
                            if (dgv.Columns.Contains("anicount"))
                                columnName = $"物品分类: {GetTypeName(cell.Value, row.Cells[5].Value)}";
                            break;
                        case "shape": columnName = $"装饰外观: {cell.Value}"; break;
                        case "weight": columnName = $"物品重量: {cell.Value}"; break;
                        case "anicount":
                            columnName = $"隐藏属性: {cell.Value}";
                            if (cell.Value.ToString() != "0")
                            {
                                columnName = $"触发脚本: [QFunction] {cell.Value}";
                            }
                            break;
                        case "source": columnName = $"物品神圣: {cell.Value}"; break;
                        case "reserved": columnName = $"特殊属性: {cell.Value}"; break;
                        case "looks": columnName = $"物品内观: {cell.Value}"; break;
                        case "duramax": columnName = $"物品持久: {cell.Value}"; break;
                        //case "ac": columnName = $"防御下限: {cell.Value}"; break;
                        case "ac": columnName = ""; break;
                        case "ac2": columnName = $"防    御: {MerField(row.Cells["ac"].Value.ToString(), cell.Value.ToString())}"; break;
                        //case "mac": columnName = $"魔御下限: {cell.Value}"; break;
                        case "mac": columnName = ""; break;
                        case "mac2": columnName = $"魔    御: {MerField(row.Cells["mac"].Value.ToString(), cell.Value.ToString())}"; break;
                        //case "dc": columnName = $"攻击下限: {cell.Value}"; break;
                        case "dc": columnName = ""; break;
                        case "dc2": columnName = $"攻    击: {MerField(row.Cells["dc"].Value.ToString(), cell.Value.ToString())}"; break;
                        //case "mc": columnName = $"魔法下限: {cell.Value}"; break;
                        case "mc": columnName = ""; break;
                        case "mc2": columnName = $"魔    法: {MerField(row.Cells["mc"].Value.ToString(), cell.Value.ToString())}"; break;
                        //case "sc": columnName = $"道术下限: {cell.Value}"; break;
                        case "sc": columnName = ""; break;
                        case "sc2": columnName = $"道    术: {MerField(row.Cells["sc"].Value.ToString(), cell.Value.ToString())}"; break;
                        case "need": columnName = $"限制种类: {cell.Value}"; break;
                        case "needlevel": columnName = $"特殊条件: 需要等级 {cell.Value} 级"; break;
                        case "price": columnName = $"物品价格: {cell.Value}金币"; break;
                        case "stock": columnName = $"库 存 量: {cell.Value}个"; break;
                        case "color": columnName = $"物品颜色: {cell.Value}"; break;
                        case "overlap": columnName = $"物品重叠: {cell.Value}"; break;
                        case "hp": columnName = $"血量提升: {cell.Value}"; break;
                        case "mp": columnName = $"蓝量提升: {cell.Value}"; break;
                        case "job": columnName = $"职    业: {cell.Value}"; break;
                        case "light": columnName = $"发    光: {cell.Value}"; break;
                        case "horse": columnName = $"马    牌: {cell.Value}"; break;
                        case "insurancecurrency": columnName = $"投保类型: {cell.Value}"; break;
                        case "insurancegold": columnName = $"投保金额: {cell.Value}"; break;
                        case "expand1": columnName = $"神佑物品1: {cell.Value}"; break;
                        case "expand2": columnName = $"神佑物品2: {cell.Value}"; break;
                        case "expand3": columnName = $"神佑物品3: {cell.Value}"; break;
                        case "expand4": columnName = $"神佑物品4: {cell.Value}"; break;
                        case "expand5": columnName = $"神佑物品5: {cell.Value}"; break;
                        case "expand6": columnName = $"神佑物品6: {cell.Value}"; break;
                        case "expand7": columnName = $"神佑物品7: {cell.Value}"; break;
                        case "expand8": columnName = $"神佑物品8: {cell.Value}"; break;
                        case "expand9": columnName = $"神佑物品9: {cell.Value}"; break;
                        case "expand10": columnName = $"神佑物品10: {cell.Value}"; break;
                        case "expand11": columnName = $"神佑物品11: {cell.Value}"; break;
                        case "expand12": columnName = $"神佑物品12: {cell.Value}"; break;
                        case "value1":
                        case "element": columnName = $"暴击几率: {cell.Value}%"; break;
                        case "value2":
                        case "element1": columnName = $"攻击伤害: {cell.Value}%"; break;
                        case "value3":
                        case "element2": columnName = $"伤害吸收: {cell.Value}%"; break;
                        case "value4":
                        case "element3": columnName = $"魔法防御: {cell.Value}%"; break;
                        case "value5":
                        case "element4": columnName = $"忽视防御: {cell.Value}%"; break;
                        case "value6":
                        case "element5": columnName = $"伤害反弹: {cell.Value}%"; break;
                        case "value7":
                        case "element6": columnName = $"人物暴率: {cell.Value}%"; break;
                        case "value8":
                        case "element7": columnName = $"体力增加: {cell.Value}%"; break;
                        case "value9":
                        case "element8": columnName = $"魔力增加: {cell.Value}%"; break;
                        case "value10":
                        case "element9": columnName = $"怒气恢复: {cell.Value}%"; break;
                        case "value11":
                        case "element10": columnName = $"合击伤害: {cell.Value}%"; break;
                        case "value12": columnName = $"防止暴击: {cell.Value}%"; break;
                        case "element11": columnName = $"怪物暴率: {cell.Value}%"; break;
                        case "element12": columnName = $"防暴几率: {cell.Value}%"; break;
                        case "value13":
                        case "element13": columnName = $"防止麻痹: {cell.Value}%"; break;
                        case "element14": columnName = $"防止护身: {cell.Value}%"; break;
                        case "value14":
                        case "element15": columnName = $"防止复活: {cell.Value}%"; break;
                        case "value15":
                        case "element16": columnName = $"防止全毒: {cell.Value}%"; break;
                        case "value18":
                        case "value19":
                        case "value20": columnName = $"内部调用,为0: {cell.Value}%"; break;
                        case "element17": columnName = $"防止诱惑: {cell.Value}%"; break;
                        case "element18": columnName = $"防止火墙: {cell.Value}%"; break;
                        case "value16":
                        case "element19": columnName = $"防止冰冻: {cell.Value}%"; break;
                        case "value21": columnName = $"致命一击: {cell.Value}%"; break;
                        case "value17":
                        case "element20": columnName = $"防止蛛网: {cell.Value}%"; break;
                        case "value22": columnName = $"会心一击: {cell.Value}%"; break;
                        case "element21": columnName = $"致命一击几率: {cell.Value}%"; break;
                        case "value23": columnName = $"卓越一击: {cell.Value}%"; break;
                        case "element22": columnName = $"致命一击伤害增加: {cell.Value}%"; break;
                        case "value24": columnName = $"无视一击: {cell.Value}%"; break;
                        case "value25":
                        case "element23": columnName = $"致命一击防御: {cell.Value}%"; break;
                        case "element24": columnName = $"暴击抗性: {cell.Value}%"; break;
                        case "value26": columnName = $"会心一击防御: {cell.Value}%"; break;
                        case "element25": columnName = $"攻击伤害抗性: {cell.Value}%"; break;
                        case "value27": columnName = $"卓越一击防御: {cell.Value}%"; break;
                        case "element26": columnName = $"杀怪经验倍率: {cell.Value}%"; break;
                        case "value28": columnName = $"无视一击防御: {cell.Value}%"; break;
                        default: columnName = $"未    知: {cell.Value}"; break;
                    }
                    if (!string.IsNullOrEmpty(columnName))
                        lbContent.Items.Add(columnName);
                }
            }
        }
        private string MerField(string value1, string value2)
        {
            return value1 + "-" + value2;
        }
        private string GetTypeName(object val, object anicountValue)
        {
            if (!int.TryParse(val.ToString(), out int t)) return "";
            if (!int.TryParse(anicountValue.ToString(), out int anicount)) return "";

            string r = "";
            switch (t)
            {
                case 100: r = "生肖(鼠)"; break;
                case 101: r = "生肖(牛)"; break;
                case 102: r = "生肖(虎)"; break;
                case 103: r = "生肖(兔)"; break;
                case 104: r = "生肖(龙)"; break;
                case 105: r = "生肖(蛇)"; break;
                case 106: r = "生肖(马)"; break;
                case 107: r = "生肖(羊)"; break;
                case 108: r = "生肖(猴)"; break;
                case 109: r = "生肖(鸡)"; break;
                case 110: r = "生肖(狗)"; break;
                case 111: r = "生肖(猪)"; break;
                case 5: r = "单手武器"; break;
                case 6: r = "双手武器"; break;
                case 15: r = "头盔"; break;
                case 16: r = "斗笠"; break;
                case 10: r = "男衣服"; break;
                case 11: r = "女衣服"; break;
                case 30: r = "照明物，勋章"; break;
                case 20:
                case 21:
                case 19: r = "项链"; break;
                case 24:
                case 26: r = "手镯"; break;
                case 23:
                case 22: r = "戒指"; break;
                case 64: r = "腰带"; break;
                case 62: r = "靴子"; break;
                case 7:
                case 46:
                case 63: r = "宝石"; break;
                case 28: r = "马牌"; break;
                case 12:
                case 48: r = "盾牌"; break;
                case 65: r = "军鼓"; break;
                case 25: r = "毒符"; break;
                case 29: r = "翅膀"; break;
                case 70: r = "称号"; break;
                case 53: r = "气血石"; break;
                case 90: r = "灵玉"; break;
                case 31: r = "双击触发物品"; break;
                case 68: r = "单手时装武器"; break;
                case 69: r = "双手时装武器"; break;
                case 66: r = "男时装衣服"; break;
                case 67: r = "女时装衣服"; break;
                case 78: r = "时装头盔"; break;
                case 83: r = "时装勋章"; break;
                case 75:
                case 76:
                case 77: r = "时装项链"; break;
                case 79: r = "时装[左手镯]"; break;
                case 80: r = "时装[右手镯]"; break;
                case 81: r = "时装[左戒指]"; break;
                case 82: r = "时装[右戒指]"; break;
                case 84:
                case 85: r = "时装腰带"; break;
                case 86:
                case 87: r = "时装靴子"; break;
                case 88:
                case 89: r = "时装宝石"; break;
                case 2:
                    if (anicount == 0) r = "修复装备";
                    else r = "自定义计次物品";
                    break;
                case 41: r = "制作物品"; break;
                case 42: r = "制作原料"; break;
                default: break;
            }
            return r;
        }
        private void CreateStdItemsDGV(UIDataGridView dgv)
        {
            DataGridViewTextBoxColumn dgvColumn;
            DataGridViewCellStyle dgvcs = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };

            foreach (DataColumn item in StdItemsDT.Columns)
            {
                dgvColumn = new DataGridViewTextBoxColumn();
                dgvColumn.Name = item.ColumnName;
                dgvColumn.Width = 100;
                dgvColumn.DataPropertyName = item.ColumnName;
                dgvColumn.DefaultCellStyle = dgvcs;

                switch (item.ColumnName.ToLower())
                {
                    case "idx":
                        dgvColumn.HeaderText = $"序号\n({dgvColumn.Name})";
                        dgvColumn.Width = 80;
                        break;
                    case "name": dgvColumn.HeaderText = $"物品名称\n({dgvColumn.Name})"; dgvColumn.Width = 300; break;
                    case "stdmode": dgvColumn.HeaderText = $"物品分类\n({dgvColumn.Name})"; break;
                    case "shape": dgvColumn.HeaderText = $"装饰外观\n({dgvColumn.Name})"; break;
                    case "weight": dgvColumn.HeaderText = $"物品重量\n({dgvColumn.Name})"; break;
                    case "anicount": dgvColumn.HeaderText = $"隐藏属性\n({dgvColumn.Name})"; break;
                    case "source": dgvColumn.HeaderText = $"物品神圣\n({dgvColumn.Name})"; break;
                    case "reserved": dgvColumn.HeaderText = $"特殊属性\n({dgvColumn.Name})"; break;
                    case "looks": dgvColumn.HeaderText = $"物品内观\n({dgvColumn.Name})"; break;
                    case "duramax": dgvColumn.HeaderText = $"物品持久\n({dgvColumn.Name})"; break;
                    case "ac": dgvColumn.HeaderText = $"防御下限\n({dgvColumn.Name})"; break;
                    case "ac2": dgvColumn.HeaderText = $"防御上限\n({dgvColumn.Name})"; break;
                    case "mac": dgvColumn.HeaderText = $"魔御下限\n({dgvColumn.Name})"; break;
                    case "mac2": dgvColumn.HeaderText = $"魔御上限\n({dgvColumn.Name})"; break;
                    case "dc": dgvColumn.HeaderText = $"攻击下限\n({dgvColumn.Name})"; break;
                    case "dc2": dgvColumn.HeaderText = $"攻击上限\n({dgvColumn.Name})"; break;
                    case "mc": dgvColumn.HeaderText = $"魔法下限\n({dgvColumn.Name})"; break;
                    case "mc2": dgvColumn.HeaderText = $"魔法上限\n({dgvColumn.Name})"; break;
                    case "sc": dgvColumn.HeaderText = $"道术下限\n({dgvColumn.Name})"; break;
                    case "sc2": dgvColumn.HeaderText = $"道术上限\n({dgvColumn.Name})"; break;
                    case "need": dgvColumn.HeaderText = $"限制种类\n({dgvColumn.Name})"; break;
                    case "needlevel": dgvColumn.HeaderText = $"需要等级\n({dgvColumn.Name})"; break;
                    case "price": dgvColumn.HeaderText = $"物品价格\n({dgvColumn.Name})"; break;
                    case "stock": dgvColumn.HeaderText = $"库存量\n({dgvColumn.Name})"; break;
                    case "color": dgvColumn.HeaderText = $"物品颜色\n({dgvColumn.Name})"; break;
                    case "overlap": dgvColumn.HeaderText = $"物品重叠\n({dgvColumn.Name})"; break;
                    case "hp": dgvColumn.HeaderText = $"血量提升\n({dgvColumn.Name})"; break;
                    case "mp": dgvColumn.HeaderText = $"蓝量提升\n({dgvColumn.Name})"; break;
                    case "job": dgvColumn.HeaderText = $"职业\n({dgvColumn.Name})"; break;
                    case "light": dgvColumn.HeaderText = $"发光\n({dgvColumn.Name})"; break;
                    case "horse": dgvColumn.HeaderText = $"马牌\n({dgvColumn.Name})"; break;
                    case "insurancecurrency": dgvColumn.HeaderText = $"投保类型\n({dgvColumn.Name})"; dgvColumn.Width = 130; break;
                    case "insurancegold": dgvColumn.HeaderText = $"投保金额\n({dgvColumn.Name})"; dgvColumn.Width = 140; break;

                    case "expand1": dgvColumn.HeaderText = $"神佑物品1\n({dgvColumn.Name})"; break;
                    case "expand2": dgvColumn.HeaderText = $"神佑物品2\n({dgvColumn.Name})"; break;
                    case "expand3": dgvColumn.HeaderText = $"神佑物品3\n({dgvColumn.Name})"; break;
                    case "expand4": dgvColumn.HeaderText = $"神佑物品4\n({dgvColumn.Name})"; break;
                    case "expand5": dgvColumn.HeaderText = $"神佑物品5\n({dgvColumn.Name})"; break;
                    case "expand6": dgvColumn.HeaderText = $"神佑物品6\n({dgvColumn.Name})"; break;
                    case "expand7": dgvColumn.HeaderText = $"神佑物品7\n({dgvColumn.Name})"; break;
                    case "expand8": dgvColumn.HeaderText = $"神佑物品8\n({dgvColumn.Name})"; break;
                    case "expand9": dgvColumn.HeaderText = $"神佑物品9\n({dgvColumn.Name})"; break;
                    case "expand10": dgvColumn.HeaderText = $"神佑物品10\n({dgvColumn.Name})"; break;
                    case "expand11": dgvColumn.HeaderText = $"神佑物品11\n({dgvColumn.Name})"; break;
                    case "expand12": dgvColumn.HeaderText = $"神佑物品12\n({dgvColumn.Name})"; break;

                    case "value1":
                    case "element": dgvColumn.HeaderText = $"暴击几率\n({dgvColumn.Name})"; break;
                    case "value2":
                    case "element1": dgvColumn.HeaderText = $"攻击伤害\n({dgvColumn.Name})"; break;
                    case "value3":
                    case "element2": dgvColumn.HeaderText = $"伤害吸收\n({dgvColumn.Name})"; break;
                    case "value4":
                    case "element3": dgvColumn.HeaderText = $"魔法防御\n({dgvColumn.Name})"; break;
                    case "value5":
                    case "element4": dgvColumn.HeaderText = $"忽视防御\n({dgvColumn.Name})"; break;
                    case "value6":
                    case "element5": dgvColumn.HeaderText = $"伤害反弹\n({dgvColumn.Name})"; break;
                    case "value7":
                    case "element6": dgvColumn.HeaderText = $"人物暴率\n({dgvColumn.Name})"; break;
                    case "value8":
                    case "element7": dgvColumn.HeaderText = $"体力增加\n({dgvColumn.Name})"; break;
                    case "value9":
                    case "element8": dgvColumn.HeaderText = $"魔力增加\n({dgvColumn.Name})"; break;
                    case "value10":
                    case "element9": dgvColumn.HeaderText = $"怒气恢复\n({dgvColumn.Name})"; break;
                    case "value11":
                    case "element10": dgvColumn.HeaderText = $"合击伤害\n({dgvColumn.Name})"; break;
                    case "value12": dgvColumn.HeaderText = $"防止暴击\n({dgvColumn.Name})"; break;
                    case "element11": dgvColumn.HeaderText = $"怪物暴率\n({dgvColumn.Name})"; break;
                    case "element12": dgvColumn.HeaderText = $"防暴几率\n({dgvColumn.Name})"; break;
                    case "value13":
                    case "element13": dgvColumn.HeaderText = $"防止麻痹\n({dgvColumn.Name})"; break;
                    case "element14": dgvColumn.HeaderText = $"防止护身\n({dgvColumn.Name})"; break;
                    case "value14":
                    case "element15": dgvColumn.HeaderText = $"防止复活\n({dgvColumn.Name})"; break;
                    case "value15":
                    case "element16": dgvColumn.HeaderText = $"防止全毒\n({dgvColumn.Name})"; break;
                    case "value18":
                    case "value19":
                    case "value20": dgvColumn.HeaderText = $"内部调用,为0\n({dgvColumn.Name})"; dgvColumn.Width = 120; break;
                    case "element17": dgvColumn.HeaderText = $"防止诱惑\n({dgvColumn.Name})"; break;
                    case "element18": dgvColumn.HeaderText = $"防止火墙\n({dgvColumn.Name})"; break;
                    case "value16":
                    case "element19": dgvColumn.HeaderText = $"防止冰冻\n({dgvColumn.Name})"; break;
                    case "value21": dgvColumn.HeaderText = $"致命一击\n({dgvColumn.Name})"; break;
                    case "value17":
                    case "element20": dgvColumn.HeaderText = $"防止蛛网\n({dgvColumn.Name})"; break;
                    case "value22": dgvColumn.HeaderText = $"会心一击\n({dgvColumn.Name})"; break;
                    case "element21": dgvColumn.HeaderText = $"致命一击几率\n({dgvColumn.Name})"; dgvColumn.Width = 120; break;
                    case "value23": dgvColumn.HeaderText = $"卓越一击\n({dgvColumn.Name})"; break;
                    case "element22": dgvColumn.HeaderText = $"致命一击伤害增加\n({dgvColumn.Name})"; dgvColumn.Width = 120; break;
                    case "value24": dgvColumn.HeaderText = $"无视一击\n({dgvColumn.Name})"; break;
                    case "value25":
                    case "element23": dgvColumn.HeaderText = $"致命一击防御\n({dgvColumn.Name})"; dgvColumn.Width = 120; break;
                    case "element24": dgvColumn.HeaderText = $"暴击抗性\n({dgvColumn.Name})"; break;
                    case "value26": dgvColumn.HeaderText = $"会心一击防御\n({dgvColumn.Name})"; dgvColumn.Width = 120; break;
                    case "element25": dgvColumn.HeaderText = $"攻击伤害抗性\n({dgvColumn.Name})"; dgvColumn.Width = 120; break;
                    case "value27": dgvColumn.HeaderText = $"卓越一击防御\n({dgvColumn.Name})"; dgvColumn.Width = 120; break;
                    case "element26": dgvColumn.HeaderText = $"杀怪经验倍率\n({dgvColumn.Name})"; dgvColumn.Width = 120; break;
                    case "value28": dgvColumn.HeaderText = $"无视一击防御\n({dgvColumn.Name})"; dgvColumn.Width = 120; break;
                    default: dgvColumn.HeaderText = $"未知\n({dgvColumn.Name})"; break;
                }
                dgv.Columns.Add(dgvColumn);
            }

            dgv.DataSource = StdItemsDT;
            dgv.Name = "dgv_StdItems";
        }

        private void CreateMonsterDGV(UIDataGridView dgv)
        {
            DataGridViewTextBoxColumn dgvColumn;
            DataGridViewCellStyle dgvcs = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };

            foreach (DataColumn item in MonsterDT.Columns)
            {
                dgvColumn = new DataGridViewTextBoxColumn();
                dgvColumn.Name = item.ColumnName;
                dgvColumn.Width = 150;
                dgvColumn.DataPropertyName = item.ColumnName;
                dgvColumn.DefaultCellStyle = dgvcs;

                switch (item.ColumnName.ToLower())
                {
                    case "name":
                        dgvColumn.HeaderText = $"怪物名称\n({dgvColumn.Name})";
                        dgvColumn.Width = 300;
                        break;
                    case "race": dgvColumn.HeaderText = $"攻击类型\n({dgvColumn.Name})"; break;
                    case "raceimg": dgvColumn.HeaderText = $"攻击图像\n({dgvColumn.Name})"; break;
                    case "appr": dgvColumn.HeaderText = $"怪物外观\n({dgvColumn.Name})"; break;
                    case "lvl": dgvColumn.HeaderText = $"怪物等级\n({dgvColumn.Name})"; break;
                    case "undead": dgvColumn.HeaderText = $"不死系\n({dgvColumn.Name})"; break;
                    case "cooleye": dgvColumn.HeaderText = $"反隐身\n({dgvColumn.Name})"; break;
                    case "exp": dgvColumn.HeaderText = $"经验值\n({dgvColumn.Name})"; break;
                    case "hp": dgvColumn.HeaderText = $"生命值\n({dgvColumn.Name})"; break;
                    case "maxhp":
                        dgvColumn.HeaderText = $"大生命值\n({dgvColumn.Name})";
                        dgvColumn.ReadOnly = true;
                        dgvColumn.ToolTipText = "只能看，不能改，改hp字段成功后刷新";
                        break;
                    case "mp": dgvColumn.HeaderText = $"魔法值\n({dgvColumn.Name})"; break;
                    case "ac": dgvColumn.HeaderText = $"防御\n({dgvColumn.Name})"; break;
                    case "mac": dgvColumn.HeaderText = $"魔御\n({dgvColumn.Name})"; break;
                    case "dc": dgvColumn.HeaderText = $"攻击下限\n({dgvColumn.Name})"; break;
                    case "dcmax": dgvColumn.HeaderText = $"攻击上限\n({dgvColumn.Name})"; break;
                    case "mc": dgvColumn.HeaderText = $"魔法\n({dgvColumn.Name})"; break;
                    case "sc": dgvColumn.HeaderText = $"道术\n({dgvColumn.Name})"; break;
                    case "speed": dgvColumn.HeaderText = $"速度\n({dgvColumn.Name})"; break;
                    case "hit": dgvColumn.HeaderText = $"命中率\n({dgvColumn.Name})"; break;
                    case "walk_spd": dgvColumn.HeaderText = $"移动速度\n({dgvColumn.Name})"; break;
                    case "walkstep": dgvColumn.HeaderText = $"行走步伐\n({dgvColumn.Name})"; break;
                    case "walkwait": dgvColumn.HeaderText = $"行走等待\n({dgvColumn.Name})"; break;
                    case "attack_spd": dgvColumn.HeaderText = $"攻击速度\n({dgvColumn.Name})"; break;
                    case "exploreitem": dgvColumn.HeaderText = $"是否支持探索\n({dgvColumn.Name})"; break;
                    case "inlevel": dgvColumn.HeaderText = $"怪物内功等级\n({dgvColumn.Name})"; break;
                    case "ipexp": dgvColumn.HeaderText = $"内功经验值\n({dgvColumn.Name})"; break;

                    default: dgvColumn.HeaderText = $"未知\n({dgvColumn.Name})"; break;
                }
                dgv.Columns.Add(dgvColumn);
            }

            dgv.DataSource = MonsterDT;
            dgv.Name = "dgv_Monster";
        }

        private void CreateMagicDGV(UIDataGridView dgv)
        {
            DataGridViewTextBoxColumn dgvColumn;
            DataGridViewCellStyle dgvcs = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };
            foreach (DataColumn item in MagicDT.Columns)
            {
                dgvColumn = new DataGridViewTextBoxColumn();
                dgvColumn.Name = item.ColumnName;
                dgvColumn.Width = 150;
                dgvColumn.DataPropertyName = item.ColumnName;
                dgvColumn.DefaultCellStyle = dgvcs;

                switch (item.ColumnName.ToLower())
                {
                    case "magid":
                        dgvColumn.HeaderText = $"技能序号\n({dgvColumn.Name})";
                        dgvColumn.Width = 100;
                        break;
                    case "magname": dgvColumn.HeaderText = $"技能名称\n({dgvColumn.Name})"; break;
                    case "effecttype": dgvColumn.HeaderText = $"动作效果\n({dgvColumn.Name})"; break;
                    case "effect": dgvColumn.HeaderText = $"技能效果\n({dgvColumn.Name})"; break;
                    case "spell": dgvColumn.HeaderText = $"技能消耗\n({dgvColumn.Name})"; break;
                    case "power": dgvColumn.HeaderText = $"伤害下限\n({dgvColumn.Name})"; break;
                    case "maxpower": dgvColumn.HeaderText = $"伤害上限\n({dgvColumn.Name})"; break;
                    case "defspell": dgvColumn.HeaderText = $"升级魔法值\n({dgvColumn.Name})"; break;
                    case "defpower": dgvColumn.HeaderText = $"升级伤害下限\n({dgvColumn.Name})"; break;
                    case "defmaxpower": dgvColumn.HeaderText = $"升级伤害上限\n({dgvColumn.Name})"; break;
                    case "job": dgvColumn.HeaderText = $"职业\n({dgvColumn.Name})"; break;
                    case "needl1": dgvColumn.HeaderText = $"1级等级\n({dgvColumn.Name})"; break;
                    case "l1train": dgvColumn.HeaderText = $"1级熟练度\n({dgvColumn.Name})"; break;
                    case "needl2": dgvColumn.HeaderText = $"2级等级\n({dgvColumn.Name})"; break;
                    case "l2train": dgvColumn.HeaderText = $"2级熟练度\n({dgvColumn.Name})"; break;
                    case "needl3": dgvColumn.HeaderText = $"3级等级\n({dgvColumn.Name})"; break;
                    case "l3train": dgvColumn.HeaderText = $"3级熟练度\n({dgvColumn.Name})"; break;
                    case "needl4": dgvColumn.HeaderText = $"4级等级\n({dgvColumn.Name})"; break;
                    case "l4train": dgvColumn.HeaderText = $"4级熟练度\n({dgvColumn.Name})"; break;
                    case "needl5": dgvColumn.HeaderText = $"5级等级\n({dgvColumn.Name})"; break;
                    case "l5train": dgvColumn.HeaderText = $"5级熟练度\n({dgvColumn.Name})"; break;
                    case "needl6": dgvColumn.HeaderText = $"6级等级\n({dgvColumn.Name})"; break;
                    case "l6train": dgvColumn.HeaderText = $"6级熟练度\n({dgvColumn.Name})"; break;
                    case "needl7": dgvColumn.HeaderText = $"7级等级\n({dgvColumn.Name})"; break;
                    case "l7train": dgvColumn.HeaderText = $"7级熟练度\n({dgvColumn.Name})"; break;
                    case "needl8": dgvColumn.HeaderText = $"8级等级\n({dgvColumn.Name})"; break;
                    case "l8train": dgvColumn.HeaderText = $"8级熟练度\n({dgvColumn.Name})"; break;
                    case "needl9": dgvColumn.HeaderText = $"9级等级\n({dgvColumn.Name})"; break;
                    case "l9train": dgvColumn.HeaderText = $"9级熟练度\n({dgvColumn.Name})"; break;
                    case "needl10": dgvColumn.HeaderText = $"10级等级\n({dgvColumn.Name})"; break;
                    case "l10train": dgvColumn.HeaderText = $"10级熟练度\n({dgvColumn.Name})"; break;
                    case "needl11": dgvColumn.HeaderText = $"11级等级\n({dgvColumn.Name})"; break;
                    case "l11train": dgvColumn.HeaderText = $"11级熟练度\n({dgvColumn.Name})"; break;
                    case "needl12": dgvColumn.HeaderText = $"12级等级\n({dgvColumn.Name})"; break;
                    case "l12train": dgvColumn.HeaderText = $"12级熟练度\n({dgvColumn.Name})"; break;
                    case "needl13": dgvColumn.HeaderText = $"13级等级\n({dgvColumn.Name})"; break;
                    case "l13train": dgvColumn.HeaderText = $"13级熟练度\n({dgvColumn.Name})"; break;
                    case "needl14": dgvColumn.HeaderText = $"14级等级\n({dgvColumn.Name})"; break;
                    case "l14train": dgvColumn.HeaderText = $"14级熟练度\n({dgvColumn.Name})"; break;
                    case "needl15": dgvColumn.HeaderText = $"15级等级\n({dgvColumn.Name})"; break;
                    case "l15train": dgvColumn.HeaderText = $"15级熟练度\n({dgvColumn.Name})"; break;
                    case "maxtrainlv": dgvColumn.HeaderText = $"可修炼等级\n({dgvColumn.Name})"; break;
                    case "delay": dgvColumn.HeaderText = $"延迟\n({dgvColumn.Name})"; break;
                    case "descr": dgvColumn.HeaderText = $"备注\n({dgvColumn.Name})"; break;
                    case "m_id": dgvColumn.Visible = false; break;
                    default: dgvColumn.HeaderText = $"未知\n({dgvColumn.Name})"; break;
                }
                dgv.Columns.Add(dgvColumn);
            }

            dgv.DataSource = MagicDT;
            dgv.Name = "dgv_Magic";
        }

        TabPage CurrentPage = null;
        private void tbContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            UITabControl tab = sender as UITabControl;
            CurrentPage = tab.SelectedTab;
            if (CurrentPage.Text == "主页") return;
        }

        private void 转MySqlToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.ShowInfoNotifier("功能预留，等以后有需求就加。");
        }

        private void 转SQLServerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.ShowInfoNotifier("功能预留，等以后有需求就加。");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            TreeNode node = tvDataBase.SelectedNode;
            if (node == null)
            {
                this.ShowErrorTip("请选择要操作的数据库！");
                return;
            }
            int level = node.Level;
            switch (level)
            {
                case 0: NewSelect(0); break;
                case 1:
                    NewSelect(node.Index);
                    break;
                case 2:
                    NewSelect(node.Parent.Index);
                    break;
            }

        }

        private void 转Exce996ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowInfoNotifier("功能预留，等以后有需求就加");
        }

        private void accessToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataConvert("Access");
        }

        private void 转SQLiteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataConvert("SQLite");
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            DBBakOf();
        }

        private void accessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionDB(1);
        }

        private void sQLiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionDB(2);
        }

        private void mySqlToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConnectionDB(3);
        }

        private void sQLServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionDB(4);
        }

        private void excel996ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionDB(5);
        }

        private void ConnectionDB(int type)
        {
            FrmConnection frmConnection = new FrmConnection(type);
            DialogResult dir = frmConnection.ShowDialog();
            if (dir == DialogResult.OK)
            {
                var node = tvDataBase.Nodes[0];
                bool isConnectionDB = (bool)node.GetProperty("isConnectionDB");
                listDb.Add(frmConnection.DB);
                if (isConnectionDB)
                {
                    TreeNode subNode = node.Nodes.Add($"{frmConnection.DB.DataBaseName}_{frmConnection.DB.DataBaseType}", $"{frmConnection.DB.DataBaseName}_{frmConnection.DB.DataBaseType}({frmConnection.DB.MirDBType})", 2);
                    subNode.Tag = JsonHelper.JsonSerializer<DBInfo>(frmConnection.DB);
                    subNode.SetProperty("isOpenDB", false);
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

        }
    }
}
