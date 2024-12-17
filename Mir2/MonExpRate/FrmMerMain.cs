using Mir.Models.DTO;
using Mir2.Helper;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Mir2.MonExpRate
{
    public partial class FrmMerMain : UIPage
    {
        DBInfo dBInfo = null;
        DataTable Monster = new DataTable();
        DataTable StdItems = new DataTable();

        public FrmMerMain(DBInfo db)
        {
            InitializeComponent();
            dBInfo = db;
        }

        private void FrmMerMain_Load(object sender, EventArgs e)
        {
            BindData();
            BindNvaMenu();
        }

        private void BindData()
        {
            plVersion.Text = "数据库：" + dBInfo.DataBaseType;
            switch (dBInfo.DataBaseType)
            {
                case "Access":
                    Monster = DBHelper.GetDataByAccess("Monster", dBInfo.DataFilePath);
                    StdItems = DBHelper.GetDataByAccess("StdItems", dBInfo.DataFilePath);
                    break;
                case "SQLite":
                    Monster = DBHelper.GetDataBySqlite("Monster", dBInfo.DataFilePath);
                    StdItems = DBHelper.GetDataBySqlite("StdItems", dBInfo.DataFilePath);
                    break;
                case "MySQL":
                    Monster = DBHelper.GetDataByMySql(dBInfo, "Monster");
                    StdItems = DBHelper.GetDataByMySql(dBInfo, "StdItems");
                    break;
                case "MSSQL":
                    Monster = DBHelper.GetDataByMsSql(dBInfo,"Monster");
                    StdItems = DBHelper.GetDataByMsSql(dBInfo,"StdItems");
                    break;
                case "Excel(996引擎)": break;
            }
            uiPanel3.Text = "怪物数量：" + Monster.Rows.Count;
            uiPanel5.Text = "物品数量：" + StdItems.Rows.Count;
        }

        private void BindNvaMenu()
        {
            navMain.CreateNode("普通爆率", 363185, 24, 1);
            navMain.CreateNode("分组管理", 558154, 24, 2);
            navMain.CreateNode("分组爆率", 559526, 24, 3);
            navMain.CreateNode("爆率模拟", 362834, 24, 4);

            navMain.SelectedNode = navMain.Nodes[0];
            FrmGeneral fg = new FrmGeneral(Monster, StdItems, dBInfo);
            fg.Render();
            Tab.AddPage(fg);
            Tab.ShowToolTips = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            plTime.Text = DateTime.Now.DateTimeString();
        }

        private void navMain_MenuItemClick(TreeNode node, NavMenuItem item, int pageIndex)
        {
            Tab.TabPages.Clear();
            switch (node.Text)
            {
                case "普通爆率":
                    {
                        FrmGeneral fg = new FrmGeneral(Monster, StdItems, dBInfo);
                        fg.Render();

                        Tab.AddPage(fg);
                        Tab.TabIndex = pageIndex;
                    }
                    break;
                case "分组管理":
                    {
                        FrmGroup fg = new FrmGroup(Monster, StdItems, dBInfo);
                        fg.Render();
                        Tab.AddPage(fg);
                        Tab.TabIndex = pageIndex;
                    }
                    break;
                case "分组爆率":
                    {
                        FrmGroupExpRate fg = new FrmGroupExpRate(Monster, StdItems, dBInfo);
                        fg.Render();
                        Tab.AddPage(fg);
                        Tab.TabIndex = pageIndex;
                    }

                    break;
                case "爆率模拟":
                    {
                        FrmExpRate fg = new FrmExpRate(Monster, StdItems, dBInfo);
                        fg.Render();
                        Tab.AddPage(fg);
                        Tab.TabIndex = pageIndex;
                    }
                    break;
            }
        }
    }
}
