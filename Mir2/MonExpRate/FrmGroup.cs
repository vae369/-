using Mir.Core.controller;
using Mir.Core.file;
using Mir.Models.DTO;
using Mir.ORM.SqlSugar;
using Mir2.Script;
using Spire.Xls.Core;
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
using System.Windows.Forms.Integration;
using System.Windows.Media.Animation;

namespace Mir2.MonExpRate
{
    public partial class FrmGroup : UIPage
    {
        DBInfo db = null;
        DataTable Monster = null;
        DataTable StdItems = null;
        string itemPath = "";
        string monsPath = "";
        string mirPath = "";
        public FrmGroup(DataTable Monster, DataTable StdItems, DBInfo db)
        {
            InitializeComponent();
            this.db = db;
            this.Monster = Monster;
            this.StdItems = StdItems;
            itemPath = db.MirPath + "\\Mir200\\Envir\\咕咕鸡配置\\Group\\Items.txt";
            monsPath = db.MirPath + "\\Mir200\\Envir\\咕咕鸡配置\\Group\\Mons.txt";
            mirPath = db.MirPath;
        }

        private void uiButton6_Click(object sender, EventArgs e)
        {
            FrmCretGroup frmCretGroup = new FrmCretGroup("item", db.MirPath);
            DialogResult dr = frmCretGroup.ShowDialog();
            if (dr == DialogResult.OK)
            {
                BindlbItemNameGroup();
            }
        }

        private void BindlbItemNameGroup()
        {
            List<string> list = FileHelper.ReadTxtReturnList(itemPath);
            if (list == null) return;
            lbItemNameGroup.Items.Clear();
            list.ForEach(item => lbItemNameGroup.Items.Add(item));
            lbItemNameGroup.SelectedIndex = list.Count - 1;
        }

        private void FrmGroup_Load(object sender, EventArgs e)
        {
            BindlbItemNameGroup();
            lbItemNameGroup.SelectedIndex = 0;
            BindlbMonNameGroup();
            lbMonNameGroup.SelectedIndex = 0;
        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            int index = lbItemNameGroup.SelectedIndex;
            if (index < 0)
            {
                this.ShowErrorTip("未选择物品分组，不能删除！");
                return;
            }
            if (this.ShowAskDialog2("提示", "确定删除当前分组吗？\n点击确定删除当前分组并且关联的物品也会删除。"))
            {

                List<string> list = FileHelper.ReadTxtReturnList(itemPath);
                list.RemoveAt(index);
                FileHelper.WriteAllText(itemPath, string.Join("\r\n", list));

                string path = mirPath + "\\Mir200\\Envir\\咕咕鸡配置\\Group\\Items\\" + lbItemNameGroup.SelectedItem.ToString() + ".txt";
                FileHelper.DeleteFile(path);

                lbItemNameGroup.Items.Clear();
                list.ForEach(item => lbItemNameGroup.Items.Add(item));

                if (index == 0)
                    lbItemNameGroup.SelectedIndex = 0;
                else
                    lbItemNameGroup.SelectedIndex = index - 1;
                BindlbItemGroup();
                this.ShowSuccessTip("删除成功！");
            }

        }

        private void lbItemNameGroup_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    CreateItemsRightControl(e.X, e.Y);//当鼠标右键按在文档控件上显示右键菜单
                    break;
                case MouseButtons.Left:
                    BindlbItemGroup();
                    break;
            }
        }

        private void CreateItemsRightControl(int x, int y)
        {
            cmsMain.Items.Clear();
            ToolStripMenuItem tsmRename = new ToolStripMenuItem("重命名", null, tsmi_Clicked, "tsmRename");
            ToolStripMenuItem tsmMoveup = new ToolStripMenuItem("上移", null, tsmi_Clicked, "tsmMoveup");
            ToolStripMenuItem tsmDown = new ToolStripMenuItem("下移", null, tsmi_Clicked, "tsmDown");

            ToolStripSeparator separator1 = new ToolStripSeparator();

            cmsMain.Items.Add(tsmRename);
            cmsMain.Items.Add(separator1);   // 添加分割行
            cmsMain.Items.Add(tsmMoveup);
            cmsMain.Items.Add(tsmDown);

            int index = lbItemNameGroup.SelectedIndex;
            int count = lbItemNameGroup.Items.Count;

            if (index == 0)
            {
                tsmMoveup.Enabled = false;
                tsmDown.Enabled = true;
            }
            if (index == count - 1)
            {
                tsmMoveup.Enabled = true;
                tsmDown.Enabled = false;
            }


            // 获取点击点的位置
            Point clickPoint = new Point(x, y);
            cmsMain.Show(lbItemNameGroup, clickPoint);
        }

        // 右键菜单项点击事件
        private void tsmi_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            List<string> list = FileHelper.ReadTxtReturnList(itemPath);
            int index = lbItemNameGroup.SelectedIndex;
             
            string temp = "";
            switch (item.Name)
            {
                case "tsmRename":
                    FrmCretGroup frmCretGroup = new FrmCretGroup("itemRename", itemPath, list[index], index);
                    DialogResult dr = frmCretGroup.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        BindlbItemNameGroup();
                    }
                    break;
                case "tsmMoveup":
                    temp = list[index];
                    if (index - 1 < 0) return;
                    list[index--] = list[index];
                    list[index] = temp;
                    FileHelper.WriteAllText(itemPath, string.Join("\r\n", list));
                    BindlbItemNameGroup();
                    break;
                case "tsmDown":
                    temp = list[index];
                    list[index++] = list[index];
                    list[index] = temp;
                    FileHelper.WriteAllText(itemPath, string.Join("\r\n", list));
                    BindlbItemNameGroup();
                    break;
            }
            lbItemNameGroup.SelectedIndex = index;
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            if (lbItemNameGroup.SelectedItem == null)
            {
                this.ShowErrorTip("请先创建分组！");
                return;
            }
            FrmJoinItem frmJoinItem = new FrmJoinItem(StdItems, mirPath, lbItemNameGroup.SelectedItem.ToString());
            DialogResult dr = frmJoinItem.ShowDialog();
            if (dr == DialogResult.OK)
            {
                BindlbItemGroup();
            }
        }

        private void BindlbItemGroup()
        {
            lbItemGroup.Items.Clear();

            if (lbItemNameGroup.SelectedItem == null) return;
            string itemName = lbItemNameGroup.SelectedItem.ToString();
            string path = mirPath + $"\\Mir200\\Envir\\咕咕鸡配置\\Group\\Items\\{itemName}.txt";
            List<string> list = FileHelper.ReadTxtReturnList(path);

            if (list != null)
            {
                list.ForEach(item => lbItemGroup.Items.Add(item));
            }
        }

        private void lbItemNameGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindlbItemGroup();
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            FrmCretGroup frmCretGroup = new FrmCretGroup("mon", db.MirPath);
            DialogResult dr = frmCretGroup.ShowDialog();
            if (dr == DialogResult.OK)
            {
                BindlbMonNameGroup();
            }
        }

        private void BindlbMonNameGroup()
        {
            List<string> list = FileHelper.ReadTxtReturnList(monsPath);
            if (list == null) return;
            lbMonNameGroup.Items.Clear();
            list.ForEach(item => lbMonNameGroup.Items.Add(item));
            lbMonNameGroup.SelectedIndex = list.Count - 1;
        }

        private void lbMonNameGroup_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    CreateMonsRightControl(e.X, e.Y);//当鼠标右键按在文档控件上显示右键菜单
                    break;
                case MouseButtons.Left:
                    BindlbMonsGroup();
                    break;
            }
        }

        private void CreateMonsRightControl(int x, int y)
        {
            cmsMain.Items.Clear();
            ToolStripMenuItem tsmRename = new ToolStripMenuItem("重命名", null, mons_Clicked, "monsRename");
            ToolStripMenuItem tsmMoveup = new ToolStripMenuItem("上移", null, mons_Clicked, "tsmMoveup");
            ToolStripMenuItem tsmDown = new ToolStripMenuItem("下移", null, mons_Clicked, "tsmDown");

            ToolStripSeparator separator1 = new ToolStripSeparator();

            cmsMain.Items.Add(tsmRename);
            cmsMain.Items.Add(separator1);   // 添加分割行
            cmsMain.Items.Add(tsmMoveup);
            cmsMain.Items.Add(tsmDown);

            int index = lbMonNameGroup.SelectedIndex;
            int count = lbMonNameGroup.Items.Count;

            if (index == 0)
            {
                tsmMoveup.Enabled = false;
                tsmDown.Enabled = true;
            }
            if (index == count - 1)
            {
                tsmMoveup.Enabled = true;
                tsmDown.Enabled = false;
            }


            // 获取点击点的位置
            Point clickPoint = new Point(x, y);
            cmsMain.Show(lbMonNameGroup, clickPoint);
        }

        // 右键菜单项点击事件
        private void mons_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            List<string> list = FileHelper.ReadTxtReturnList(monsPath);
            int index = lbMonNameGroup.SelectedIndex; 
            string temp = "";
            switch (item.Name)
            {
                case "monsRename":
                    FrmCretGroup frmCretGroup = new FrmCretGroup("monsRename", monsPath, list[index], index);
                    DialogResult dr = frmCretGroup.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        BindlbMonNameGroup();
                    }
                    break;
                case "tsmMoveup":
                    temp = list[index];
                    if (index - 1 < 0) return;
                    list[index--] = list[index];
                    list[index] = temp;
                    FileHelper.WriteAllText(monsPath, string.Join("\r\n", list));
                    BindlbMonNameGroup();
                    break;
                case "tsmDown":
                    temp = list[index];
                    list[index++] = list[index];
                    list[index] = temp;
                    FileHelper.WriteAllText(monsPath, string.Join("\r\n", list));
                    BindlbMonNameGroup();
                    break;
            }
            lbMonNameGroup.SelectedIndex = index;
        }

        private void uiButton4_Click(object sender, EventArgs e)
        {
            int index = lbMonNameGroup.SelectedIndex;
            if (index < 0)
            {
                this.ShowErrorTip("未选择怪物分组 ，不能删除！");
                return;
            }
            if (this.ShowAskDialog2("提示", "确定删除当前分组吗？\n点击确定删除当前分组并且关联的物品也会删除。"))
            {

                List<string> list = FileHelper.ReadTxtReturnList(monsPath);
                list.RemoveAt(index);
                FileHelper.WriteAllText(monsPath, string.Join("\r\n", list));

                string path = mirPath + "\\Mir200\\Envir\\咕咕鸡配置\\Group\\Mons\\" + lbMonNameGroup.SelectedItem.ToString() + ".txt";
                FileHelper.DeleteFile(path);

                lbMonNameGroup.Items.Clear();
                list.ForEach(item => lbMonNameGroup.Items.Add(item));

                if (index == 0)
                    lbMonNameGroup.SelectedIndex = 0;
                else
                    lbMonNameGroup.SelectedIndex = index - 1;
                BindlbMonsGroup();
                this.ShowSuccessTip("删除成功！");
            }
        }

        private void uiButton5_Click(object sender, EventArgs e)
        {
            if (lbItemNameGroup.SelectedItem == null)
            {
                this.ShowErrorTip("请先创建分组！");
                return;
            }

            FrmJoinMons frmJoinItem = new FrmJoinMons(Monster, mirPath, lbMonNameGroup.SelectedItem.ToString());
            DialogResult dr = frmJoinItem.ShowDialog();
            if (dr == DialogResult.OK)
            {
                BindlbMonsGroup();
            }
        }

        private void BindlbMonsGroup()
        {
            lbMonGroup.Items.Clear();

            if (lbMonNameGroup.SelectedItem == null) return;
            string itemName = lbMonNameGroup.SelectedItem.ToString();
            string path = mirPath + $"\\Mir200\\Envir\\咕咕鸡配置\\Group\\Mons\\{itemName}.txt";
            List<string> list = FileHelper.ReadTxtReturnList(path);

            if (list != null)
            {
                list.ForEach(item => lbMonGroup.Items.Add(item));
            }


        }

        private void lbMonNameGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindlbMonsGroup();
        }
    }
}
