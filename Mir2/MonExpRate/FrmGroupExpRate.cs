using Mir.Models.DTO;
using Mir.Models;
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
using System.IO;
using Mir.Core.file;
using Mir.ORM.SqlSugar;
using System.Windows.Forms.Integration;
using Mir2.Script;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Shapes;
using System.Windows.Documents;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Mir2.MonExpRate
{
    public partial class FrmGroupExpRate : UIPage
    {
        DBInfo dBInfo = null;
        List<Monster> listMons = null;
        List<StdItems> listItems = null;
        string itemPath = "";
        string monsPath = "";
        string mirPath = "";
        List<string> treeFileList = new List<string>();

        public FrmGroupExpRate(DataTable Monster, DataTable StdItems, DBInfo dBInfo)
        {
            InitializeComponent();
            listMons = Monster.AsEnumerable().Select(s => new Mir.Models.Monster { Name = s.Field<string>("Name") }).ToList();
            listItems = StdItems.AsEnumerable().Select(s => new Mir.Models.StdItems { Name = s.Field<string>("Name") }).ToList();
            this.dBInfo = dBInfo;
        }

        private void FrmGroupExpRate_Load(object sender, EventArgs e)
        {
            string path = dBInfo.MirPath + "\\Mir200\\Envir\\咕咕鸡配置\\默认分组";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            BindTree(path);

            string itemPath = dBInfo.MirPath + "\\Mir200\\Envir\\咕咕鸡配置\\Group\\Items.txt";
            List<string> list = FileHelper.ReadTxtReturnList(itemPath).Where(m => !string.IsNullOrEmpty(m)).ToList();
            if (list != null)
                list.ForEach(s => listItems.Insert(0, new StdItems { Name = "[分组]" + s }));

            lbItems.DataSource = listItems;
            lbItems.DisplayMember = "Name";
            lbItems.ValueMember = "Name";

            uiRichTextBox1.Text = "\r\n\t介绍：\r\n\r\n\t\t1.文本框内直接输入要查询的内容可以直接查询。\r\n\r\n\t\t2.双击左侧列表可以编辑分组内容。\r\n\r\n\t\t3.双击右侧列表可添加分组内装备爆率。\r\n\r\n\t\t4.一键生成爆率文件只有当前分组，点击按钮可以生成所有的分组爆率文件。\r\n\r\n\t\t5.一级分组中添加的装备爆率为过滤装备，也就是说这些装备不生成，爆率必须一致。如:1/1 木剑\t1/10 木剑 这个是不匹配的\r\n\r\n\t\t6.右侧列表可以选择多项，右键可以添加多个。";
            Tab.ShowToolTips = true;
        }

        /// <summary>
        /// 绑定树形控件内容
        /// </summary>
        /// <param name="path"></param>
        private void BindTree(string path)
        {
            tvGroup.ImageList = treeFileImage;
            tvGroup.MouseDown += tvGroup_MouseDown;

            tvGroup.Nodes.Clear();

            AddDirectoriesAndFiles(tvGroup, path);

            tvGroup.ItemHeight = 26;
        }

        public void AddDirectoriesAndFiles(UITreeView treeView, string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            string monsPath = $"{path}\\Mons.txt";
            List<string> list = FileHelper.ReadTxtReturnList(monsPath);
            TreeNode node = treeView.Nodes.Add(path, $"默认分组({list.Count})", 0);

            AddSubDirectoriesAndFiles(node, new DirectoryInfo(path));
            node.Expand();

        }
        private void AddSubDirectoriesAndFiles(TreeNode node, DirectoryInfo directory)
        {
            string monsPath = "";
            List<string> list = new List<string>();
            foreach (DirectoryInfo subDir in directory.GetDirectories())
            {
                monsPath = $"{subDir.FullName}\\Mons.txt";
                list = FileHelper.ReadTxtReturnList(monsPath).Where(m => !string.IsNullOrEmpty(m)).ToList();
                TreeNode subNode = node.Nodes.Add(subDir.FullName, $"{subDir.Name}({list.Count})", 1);

                AddSubDirectoriesAndFiles(subNode, subDir);
            }

            if (directory.Name == "默认分组")
            {
                string path = dBInfo.MirPath + "\\Mir200\\Envir\\咕咕鸡配置\\自定义分组";
                var newdir = new DirectoryInfo(path);
                if (!Directory.Exists(newdir.FullName)) Directory.CreateDirectory(newdir.FullName);
                foreach (DirectoryInfo subDir in newdir.GetDirectories())
                {
                    monsPath = $"{subDir.FullName}\\Mons.txt";
                    list = FileHelper.ReadTxtReturnList(monsPath).Where(m => !string.IsNullOrEmpty(m)).ToList();

                    TreeNode subNode = tvGroup.Nodes.Add(subDir.FullName, $"{subDir.Name}({list.Count})", 0);
                    AddSubDirectoriesAndFiles(subNode, subDir);
                }
            }

        }


        private void SaveDocument(TabPage tabPage)
        {
            string filePath = tabPage.Name;
            string content = "";
            ElementHost elementHost = (ElementHost)tabPage.Controls[0];
            UserControl1 userControl = (UserControl1)elementHost.Child;
            if (userControl != null)
            {
                content = userControl.TextEditor.Text;
            }

            if (!File.Exists(filePath))
            {
                if (this.ShowAskDialog2("提示", "当前怪物爆率文件未创建,确定创建吗？"))
                {
                    FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                    // 关闭文件流，这将导致文件被写入磁盘
                    fileStream.Close();
                }
                else
                {
                    return;
                }
            }
            string ptext = tabPage.Text;
            if (ptext.Contains("*"))
                tabPage.Text = ptext.Substring(0, ptext.Length - 1);

            FileHelper.WriteAllText(filePath, content);
            plMsg.Text = Tab.SelectedTab.Name + " [已保存]";
            this.ShowSuccessTip("保存成功！");
        }
        private void tvGroup_MouseDown(object sender, MouseEventArgs e)
        {

            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        // 获取点击点的位置
                        Point clickPoint = new Point(e.X, e.Y);
                        TreeNode selectedNode = tvGroup.GetNodeAt(clickPoint);

                        // 如果点击在节点上，选中该节点
                        if (selectedNode != null)
                        {
                            // 选中节点
                            tvGroup.SelectedNode = selectedNode;

                            CreateTreeRightControl(tvGroup, clickPoint);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 创建右键菜单内容
        /// </summary>
        private void CreateTreeRightControl(UITreeView MirTree, Point clickPoint)
        {
            cmsMain.Items.Clear();
            ToolStripMenuItem tsmCreateGroup = new ToolStripMenuItem("新建分组", null, tsm_Clicked);
            tsmCreateGroup.Name = "tsmCreateGroup";
            tsmCreateGroup.ShortcutKeyDisplayString = "Ctrl+N";
            tsmCreateGroup.ShortcutKeys = Keys.Control | Keys.N;

            ToolStripMenuItem tsmCreateSubgroup = new ToolStripMenuItem("新建子分组", null, tsm_Clicked);
            tsmCreateSubgroup.Name = "tsmCreateSubgroup";
            tsmCreateSubgroup.ShortcutKeyDisplayString = "Ctrl+G";
            tsmCreateSubgroup.ShortcutKeys = Keys.Control | Keys.G;

            ToolStripMenuItem tsmRenameGroupName = new ToolStripMenuItem("重命名当前分组", null, tsm_Clicked);
            tsmRenameGroupName.Name = "tsmRenameGroupName";
            tsmRenameGroupName.ShortcutKeyDisplayString = "Ctrl+R";
            tsmRenameGroupName.ShortcutKeys = Keys.Control | Keys.R;

            ToolStripMenuItem tsmDelGroup = new ToolStripMenuItem("删除当前分组", null, tsm_Clicked);
            tsmDelGroup.Name = "tsmDelGroup";
            tsmDelGroup.ShortcutKeyDisplayString = "Ctrl+D";
            tsmDelGroup.ShortcutKeys = Keys.Control | Keys.D;

            ToolStripMenuItem tsmRelatedMons = new ToolStripMenuItem("关联怪物", null, tsm_Clicked);
            tsmRelatedMons.Name = "tsmRelatedMons";
            tsmRelatedMons.ShortcutKeyDisplayString = "Ctrl+L";
            tsmRelatedMons.ShortcutKeys = Keys.Control | Keys.L;

            ToolStripMenuItem tsmRelatedFilterMons = new ToolStripMenuItem("关联过滤怪物", null, tsm_Clicked);
            tsmRelatedFilterMons.Name = "tsmRelatedFilterMons";
            tsmRelatedFilterMons.ShortcutKeyDisplayString = "Ctrl+F";
            tsmRelatedFilterMons.ShortcutKeys = Keys.Control | Keys.F;

            ToolStripMenuItem tsmMonMagnificat = new ToolStripMenuItem("批量调整分组中的怪物倍数", null, tsm_Clicked);
            tsmMonMagnificat.Name = "tsmMonMagnificat";
            tsmMonMagnificat.ShortcutKeyDisplayString = "Ctrl+Z";
            tsmMonMagnificat.ShortcutKeys = Keys.Control | Keys.Z;

            ToolStripMenuItem tsmAdjustMons = new ToolStripMenuItem("仅生成选中子分组爆率内容", null, tsm_Clicked);
            tsmAdjustMons.Name = "tsmAdjustMons";
            tsmAdjustMons.ShortcutKeyDisplayString = "Ctrl+S";
            tsmAdjustMons.ShortcutKeys = Keys.Control | Keys.S;

            ToolStripMenuItem tsmReleaseCurrGroup = new ToolStripMenuItem("一键生成当前分组爆率", null, tsm_Clicked);
            tsmReleaseCurrGroup.Name = "tsmReleaseCurrGroup";
            tsmReleaseCurrGroup.ShortcutKeyDisplayString = "Ctrl+A";
            tsmReleaseCurrGroup.ShortcutKeys = Keys.Control | Keys.A;

            tsmAdjustMons.Enabled = false;
            if (tvGroup.SelectedNode.Text.StartsWith("默认分组"))
            {
                tsmRenameGroupName.Enabled = false;
                tsmRelatedMons.Enabled = false;
                tsmDelGroup.Enabled = false;
            }
            else if (tvGroup.SelectedNode.Parent != null)
            {
                tsmRelatedFilterMons.Enabled = false;
                tsmReleaseCurrGroup.Enabled = false;

            }
            else
            {
                tsmRelatedMons.Enabled = false;
            }
            cmsMain.Items.Add(tsmCreateGroup);
            cmsMain.Items.Add(tsmCreateSubgroup);
            cmsMain.Items.Add(new ToolStripSeparator());
            cmsMain.Items.Add(tsmRenameGroupName);
            cmsMain.Items.Add(tsmDelGroup);
            cmsMain.Items.Add(new ToolStripSeparator());
            cmsMain.Items.Add(tsmRelatedMons);
            cmsMain.Items.Add(tsmRelatedFilterMons);
            cmsMain.Items.Add(new ToolStripSeparator());
            cmsMain.Items.Add(tsmMonMagnificat);
            cmsMain.Items.Add(tsmAdjustMons);
            cmsMain.Items.Add(tsmReleaseCurrGroup);

            cmsMain.Show(MirTree, clickPoint);
        }

        private void tsm_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            switch (item.Name)
            {
                case "tsmCreateGroup": CreateGroup(); break;
                case "tsmCreateSubgroup": CreateSubgroup(); break;
                case "tsmRenameGroupName": RenameGroupName(); break;
                case "tsmDelGroup": DelGroup(); break;
                case "tsmRelatedMons": RelatedMons(); break;
                case "tsmRelatedFilterMons": RelatedFilterMons(); break;
                case "tsmMonMagnificat": MonMagnificat(); break;
                //case "tsmAdjustMons": OpenFolder(); break;
                case "tsmReleaseCurrGroup": ReleaseCurrGroup(); break;
            }
        }
        private void MonMagnificat()
        {
            string path = tvGroup.SelectedNode.Name.Substring(0, tvGroup.SelectedNode.Name.LastIndexOf('\\')) + "\\Groups.txt";
            string groupName = tvGroup.SelectedNode.Text.Substring(0, tvGroup.SelectedNode.Text.IndexOf('('));


            FrmCretGroup frmCretGroup = new FrmCretGroup("MonMagnificat", path, groupName);
            DialogResult dr = frmCretGroup.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //List<string> list = FileHelper.ReadTxtReturnList(frmCretGroup.ZDYGroupPath + "\\Mons.txt");

                //TreeNode tn = tvGroup.SelectedNode;
                //tn.Text = $"{frmCretGroup.GroupName}({list.Count})";
                //tn.Name = frmCretGroup.ZDYGroupPath;
                //tn.ImageIndex = 1;
                //if (tvGroup.SelectedNode.Parent == null)
                //    tn.ImageIndex = 0;
                this.ShowSuccessTip("操作成功！");
            }
        }


        private void ReleaseCurrGroup()
        {
            string groupName = tvGroup.SelectedNode.Text.Substring(0, tvGroup.SelectedNode.Text.IndexOf('('));
            if (this.ShowAskDialog2("提示", "正准备生成当前分组 [" + groupName + "] 爆率内容！\r\n点击确定会自动备份爆率文件。\r\n确定生成爆率内容吗？"))
            {
                string path = tvGroup.SelectedNode.Name;
                ReleaseGroup(path, tvGroup.SelectedNode);
                this.ShowSuccessTip("生成成功！");
            }
        }
        List<Monster> filterMons = new List<Monster>();
        List<StdItems> filterItems = new List<StdItems>();
        string filterGroupPath = "";
        private void ReleaseGroup(string path, TreeNode node)
        {
            #region 添加过滤怪物和物品
            if (node != null)
            {
                //添加顶级分组的过滤怪物
                var list = FileHelper.ReadTxtReturnList(path + "\\Mons.txt");
                list.ForEach(s =>
                {
                    if (!string.IsNullOrEmpty(s))
                        filterMons.Add(new Monster { Name = s, });
                });

                //添加顶级分组的过滤物品 
                list = FileHelper.ReadTxtReturnList(path + "\\Items.txt");
                list.ForEach(s => { filterItems.Add(new StdItems { Name = s, }); });

                string groupName = node.Text.Substring(0, node.Text.IndexOf('('));

                filterGroupPath = dBInfo.MirPath + "\\Mir200\\Envir\\MonItems\\" + groupName;
                if (!Directory.Exists(filterGroupPath)) Directory.CreateDirectory(filterGroupPath);
            }
            else
            {
                string directoryName = dBInfo.MirPath + "\\Mir200\\Envir\\咕咕鸡配置\\";
                directoryName = path.Replace(directoryName, "").Replace("自定义分组\\", "");//如果是自定义分组，就去掉最外层自定义分组文件夹名
                string groupNameStart = $";============== [分组]{directoryName} 开始 ==============";
                string groupNameEnd = $";============== [分组]{directoryName} 结束 ==============";

                //获取所有物品
                string newPath = path + "\\Items.txt";
                var listRessItems = FileHelper.ReadTxtReturnList(newPath);
                //获取所有怪物
                newPath = path + "\\Mons.txt";
                var listRessMons = FileHelper.ReadTxtReturnList(newPath);

                newPath = $"{dBInfo.MirPath}\\Mir200\\Envir\\MonItems\\{directoryName}";
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                //所有读取所有怪物所在txt，如果不存在则创建
                listRessMons.ForEach(s =>
                {
                    if (string.IsNullOrEmpty(s)) return;
                    newPath = $"{dBInfo.MirPath}\\Mir200\\Envir\\MonItems\\{directoryName}\\{s}.txt";
                    FileHelper.DeleteFile(newPath);

                    List<string> list = new List<string>();
                    int offes = 0;
                    listRessItems.ForEach(t =>
                    {
                        if (string.IsNullOrEmpty(t)) return;
                        if (filterItems.Where(st => st.Name == t).Any())
                        {
                            list.Insert(offes++, ";" + t);//如果添加的物品在过滤物品中，就注释掉当前物品,如: ;圣战戒指
                        }
                        else
                        {
                            list.Insert(offes++, t);
                        }
                    });

                    list.Insert(0, $";======= 日期:{DateTime.Now.ToString()} =======");
                    list.Insert(0, groupNameStart);
                    list.Insert(list.Count, groupNameEnd);

                    FileHelper.WriteAllText(newPath, string.Join("\r\n", list));

                    //如果当前怪物 在过滤怪物中就删除
                    if (filterMons.Where(t => t.Name == s).Any())
                    {
                        FileHelper.DeleteFile(newPath);
                    }

                });
            }
            #endregion

            string[] arrGroupPath = Directory.GetDirectories(path);
            foreach (string groupPath in arrGroupPath)
            {
                ReleaseGroup(groupPath, null);
            }
        }


        private void RelatedFilterMons()
        {
            string groupName = tvGroup.SelectedNode.Text.Substring(0, tvGroup.SelectedNode.Text.IndexOf('('));
            string path = tvGroup.SelectedNode.Name;
            FrmGroupFilterJoinMons frmGroupJoinMons = new FrmGroupFilterJoinMons(listMons, path, dBInfo.MirPath, groupName);
            frmGroupJoinMons.ShowDialog();
            path = $"{path}\\Mons.txt";
            List<string> list = FileHelper.ReadTxtReturnList(path);
            tvGroup.SelectedNode.Text = groupName + $"({list.Count})";
        }

        private void RelatedMons()
        {
            string groupName = tvGroup.SelectedNode.Text.Substring(0, tvGroup.SelectedNode.Text.IndexOf('('));
            string path = tvGroup.SelectedNode.Name;
            FrmGroupJoinMons frmGroupJoinMons = new FrmGroupJoinMons(listMons, path, dBInfo.MirPath, groupName);
            frmGroupJoinMons.ShowDialog();
            path = $"{path}\\Mons.txt";
            List<string> list = FileHelper.ReadTxtReturnList(path).Where(m => !string.IsNullOrEmpty(m)).ToList();
            tvGroup.SelectedNode.Text = groupName + $"({list.Count})";
        }

        private void DelGroup()
        {
            string groupName = tvGroup.SelectedNode.Text.Substring(0, tvGroup.SelectedNode.Text.IndexOf('('));
            if (this.ShowAskDialog2("提示", "确定删除分组 [" + groupName + "] ?\r\n子分组也将同时一起被删除！"))
            {
                int index = tvGroup.SelectedNode.Index;
                if (tvGroup.SelectedNode.Parent == null)
                {
                    if (index > 0) index--;
                }
                string path = tvGroup.SelectedNode.Name.Substring(0, tvGroup.SelectedNode.Name.LastIndexOf('\\')) + "\\Groups.txt";
                List<string> list = FileHelper.ReadTxtReturnList(path);
                list.RemoveAt(index);
                FileHelper.WriteAllText(path, string.Join("\r\n", list));

                Directory.Delete(tvGroup.SelectedNode.Name, true);
                tvGroup.SelectedNode.Remove();
                this.ShowSuccessTip("删除成功！");
            }
        }

        private void RenameGroupName()
        {
            string path = tvGroup.SelectedNode.Name.Substring(0, tvGroup.SelectedNode.Name.LastIndexOf('\\')) + "\\Groups.txt";
            string groupName = tvGroup.SelectedNode.Text.Substring(0, tvGroup.SelectedNode.Text.IndexOf('('));
            int index = tvGroup.SelectedNode.Index;
            if (tvGroup.SelectedNode.Parent == null)
            {
                if (index > 0) index--;
            }

            FrmCretGroup frmCretGroup = new FrmCretGroup("RenameGroupName", path, groupName, index);
            DialogResult dr = frmCretGroup.ShowDialog();
            if (dr == DialogResult.OK)
            {
                List<string> list = FileHelper.ReadTxtReturnList(frmCretGroup.ZDYGroupPath + "\\Mons.txt");

                TreeNode tn = tvGroup.SelectedNode;
                tn.Text = $"{frmCretGroup.GroupName}({list.Count})";
                tn.Name = frmCretGroup.ZDYGroupPath;
                tn.ImageIndex = 1;
                if (tvGroup.SelectedNode.Parent == null)
                    tn.ImageIndex = 0;
            }
        }

        private void CreateSubgroup()
        {
            string path = tvGroup.SelectedNode.Name;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string groupName = tvGroup.SelectedNode.Text.Substring(0, tvGroup.SelectedNode.Text.IndexOf('('));
            FrmCretGroup frmCretGroup = new FrmCretGroup("CreateSubgroup", path, groupName);
            DialogResult dr = frmCretGroup.ShowDialog();
            if (dr == DialogResult.OK)
            {
                TreeNode tn = new TreeNode();
                tn.Text = frmCretGroup.GroupName + "(0)";
                tn.ImageIndex = 1;
                tn.Name = frmCretGroup.ZDYGroupPath;
                tvGroup.SelectedNode.Nodes.Add(tn);
            }
        }

        private void CreateGroup()
        {
            string path = dBInfo.MirPath + "\\Mir200\\Envir\\咕咕鸡配置\\自定义分组";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            FrmCretGroup frmCretGroup = new FrmCretGroup("CreateGroup", path);
            DialogResult dr = frmCretGroup.ShowDialog();
            if (dr == DialogResult.OK)
            {
                TreeNode tn = new TreeNode();
                tn.Text = frmCretGroup.GroupName + "(0)";
                tn.ImageIndex = 0;
                tn.Name = frmCretGroup.ZDYGroupPath;
                tvGroup.Nodes.Add(tn);
            }
        }

        private void txtItems_TextChanged(object sender, EventArgs e)
        {
            var query = listItems.Where(s => s.Name.Contains(txtItems.Text.Trim())).ToList();

            lbItems.DataSource = query;
            lbItems.DisplayMember = "Name";
            lbItems.ValueMember = "Name";
        }

        int caretLine = 0;
        private void lbItems_DoubleClick(object sender, EventArgs e)
        {
            if (Tab == null) return;
            if (Tab.SelectedTab.Text == "主页") return;
            ElementHost elementHost = (ElementHost)Tab.SelectedTab.Controls[0];
            UserControl1 userControl = (UserControl1)elementHost.Child;
            var caret = userControl.TextEditor.TextArea.Caret;
            caretLine = caret.Line;

            string text = userControl.TextEditor.Text.Replace("\r\n", "ら");
            List<string> list = text.Split('ら').ToList();

            string itemPath = "";
            string itemText = lbItems.ActiveControl.Text;

            List<string> listGroupItems = new List<string>();
            if (itemText.StartsWith("[分组]"))
            {
                itemText = itemText.Replace("[分组]", "");
                itemPath = dBInfo.MirPath + $"\\Mir200\\Envir\\咕咕鸡配置\\Group\\Items\\{itemText}.txt";
                listGroupItems = FileHelper.ReadTxtReturnList(itemPath);
            }

            AddItems(listGroupItems, list, userControl, itemText);
        }
        private void AddItems(List<string> listGroupItems, List<string> list, UserControl1 userControl, string itemText)
        {
            string text = "";

            if (rbOld.Checked)
            {
                if (listGroupItems.Count > 0)
                {
                    listGroupItems.ForEach(s =>
                    {
                        text = $"1/{tud.Value}\t{s}";
                        list.Insert(caretLine, text);
                    });
                }
                else
                {
                    if (lbItems.SelectedItems.Count > 1)
                    {
                        foreach (StdItems item in lbItems.SelectedItems)
                        {
                            text = $"1/{tud.Value}\t{item.Name}";
                            list.Insert(caretLine++, text);
                        }
                    }
                    else
                    {
                        text = $"1/{tud.Value}\t{itemText}";
                        list.Insert(caretLine++, text);
                    }
                }
            }
            else
            {
                text = $"#CHILD 1/{tud.Value} RANDOM";
                list.Insert(caretLine++, text);
                text = "(";
                list.Insert(caretLine++, text);
                if (listGroupItems.Count > 0)
                {
                    listGroupItems.ForEach(s =>
                    {
                        text = $"\t1/{tud.Value}\t{s}";
                        list.Insert(caretLine++, text);
                    });
                }
                else
                {
                    if (lbItems.SelectedItems.Count > 1)
                    {
                        foreach (StdItems item in lbItems.SelectedItems)
                        {
                            text = $"1/{tud.Value}\t{item.Name}";
                            list.Insert(caretLine++, text);
                        }
                    }
                    else
                    {
                        text = $"1/{tud.Value}\t{itemText}";
                        list.Insert(caretLine++, text);
                    }
                }

                text = ")";
                list.Insert(caretLine, text);
            }

            text = string.Join("\r\n", list);
            userControl.TextEditor.Text = text;
            userControl.TextEditor.CaretOffset = ++caretLine;

            plMsg.Text = Tab.SelectedTab.Name + " [未保存]";
            text = Tab.SelectedTab.Text;
            if (!text.Contains("*"))
            {
                Tab.SelectedTab.Text = text + "*";
            }
        }

        private void tvGroup_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks >= 2)
            {
                string groupName = GetGroupName(tvGroup.SelectedNode).TrimEnd('>');
                string path = tvGroup.SelectedNode.Name + "\\Items.txt";
                BindTabPageData(groupName, path);
            }
        }
        private string GetGroupName(TreeNode node)
        {
            string groupName = "";
            if (node.Parent != null)
            {
                groupName = GetGroupName(node.Parent);
            }
            groupName += node.Text.Substring(0, node.Text.IndexOf('(')) + ">";
            return groupName;
        }
        private void BindTabPageData(string tbText, string path)
        {
            string tipText = tbText;
            if (tbText.Length > 6)
            {
                if (tbText.IndexOf('>') > 0)
                    tbText = "..." + tbText.Split('>')[1];
                else
                    tbText = tbText.Substring(0, 6) + "...";
            }
            //添加文档
            TabPage tp = new TabPage();
            tp.ToolTipText = tipText;
            tp.Text = tbText;
            tp.Name = path;
            tp.BackColor = Color.White;

            bool isExti = true;
            foreach (TabPage item in Tab.TabPages)
            {
                if (item.Name == tp.Name) isExti = false;
            }
            if (!isExti) return;

            UIPanel panel = new UIPanel();
            //p2.Resize += UIPanal_Resize;
            panel.AutoSizeMode = AutoSizeMode.GrowOnly;
            panel.Size = new System.Drawing.Size(Tab.Width, Tab.Height);
            panel.Name = "panel";
            panel.Dock = DockStyle.Fill;

            ElementHost elHost = new ElementHost();
            elHost.Dock = DockStyle.Fill;
            elHost.ContextMenuStrip = cmsTab;

            UserControl1 uc = new UserControl1(dBInfo.MirType);
            uc.TextEditor.Text = FileHelper.ReadTxt(path).Trim();
            uc.TextEditor.FontSize = 18;
            uc.TextSetting();//设置文本文件格式
            uc.KeyDown += userControl_KeyDown;
            uc.TextEditor.TextArea.Caret.PositionChanged += CaretOnPositionChanged;

            elHost.Child = uc;
            tp.Controls.Add(elHost);
            caretLine = 0;
            plMsg.Text = path;

            Tab.TabPages.Add(tp);
            Tab.SelectedTab = tp;

        }

        private void CaretOnPositionChanged(object sender, EventArgs eventArgs)
        {
            TabPage tabPage = Tab.SelectedTab;
            ElementHost elementHost = (ElementHost)tabPage.Controls[0];
            UserControl1 userControl = (UserControl1)elementHost.Child;
            plColRow.Text = $"行 {userControl.TextEditor.TextArea.Caret.Line} 列 {userControl.TextEditor.TextArea.Caret.Column}";
        }

        private void userControl_KeyDown(object sender, EventArgs e)
        {
            System.Windows.Input.KeyEventArgs kea = e as System.Windows.Input.KeyEventArgs;
            if (kea.KeyboardDevice.Modifiers == System.Windows.Input.ModifierKeys.Control && kea.Key == System.Windows.Input.Key.S)
            {
                SaveDocument(Tab.SelectedTab);
            }
            else
            {
                //Tab.SelectedTab.Size = new Size(20, 30);
                //操作某个tabpage
                plMsg.Text = Tab.SelectedTab.Name + " [未保存]";
                string text = Tab.SelectedTab.Text;
                if (!text.Contains("*"))
                {
                    Tab.SelectedTab.Text = text + " *";
                }
            }
        }

        private void lbItems_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        // 获取点击点的位置
                        Point clickPoint = new Point(e.X, e.Y);

                        CreateTreeRightControl(lbItems, e.Location);
                    }
                    break;
            }
        }


        /// <summary>
        /// 创建右键菜单内容
        /// </summary>
        private void CreateTreeRightControl(UIListBox items, Point clickPoint)
        {
            cmsMain.Items.Clear();
            ToolStripMenuItem addItems = new ToolStripMenuItem("添加选中的物品", null, tsmAddItems_Clicked);
            addItems.Name = "addItems";
            addItems.ShortcutKeyDisplayString = "Ctrl+A";
            addItems.ShortcutKeys = Keys.Control | Keys.A;

            cmsMain.Items.Add(addItems);

            cmsMain.Show(items, clickPoint);
        }

        private void tsmAddItems_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            switch (item.Name)
            {
                case "addItems": RightAddItems(); break;
            }
        }

        private void RightAddItems()
        {
            if (Tab == null) return;
            if (Tab.SelectedTab.Text == "主页") return;
            //if (Tab.SelectedTab.Controls.Count == 1) return;
            ElementHost elementHost = (ElementHost)Tab.SelectedTab.Controls[0];
            UserControl1 userControl = (UserControl1)elementHost.Child;
            var caret = userControl.TextEditor.TextArea.Caret;
            caretLine = caret.Line;

            string text = userControl.TextEditor.Text.Replace("\r\n", "ら");
            List<string> list = text.Split('ら').ToList();

            string itemPath = "";
            string itemText = "";
            if (lbItems.ActiveControl != null) itemText = lbItems.ActiveControl.Text;
            List<string> listGroupItems = new List<string>();

            if (lbItems.SelectedItems.Count > 1)
            {
                foreach (StdItems item in lbItems.SelectedItems)
                {
                    if (!item.Name.StartsWith("[分组]"))
                    {
                        listGroupItems.Add(item.Name);
                    }
                    else
                    {
                        itemText = itemText.Replace("[分组]", "");
                        itemPath = dBInfo.MirPath + $"\\Mir200\\Envir\\咕咕鸡配置\\Group\\Items\\{itemText}.txt";
                        listGroupItems.AddRange(FileHelper.ReadTxtReturnList(itemPath));
                    }
                }
            }
            else
            {
                if (itemText.StartsWith("[分组]"))
                {
                    itemText = itemText.Replace("[分组]", "");
                    itemPath = dBInfo.MirPath + $"\\Mir200\\Envir\\咕咕鸡配置\\Group\\Items\\{itemText}.txt";
                    listGroupItems = FileHelper.ReadTxtReturnList(itemPath);
                }
            }

            AddItems(listGroupItems, list, userControl, itemText);
        }

        private void btnRalease_Click(object sender, EventArgs e)
        {
            if (this.ShowAskDialog2("提示", "正准备生成所有分组爆率内容。\r\n点击确定会自动备份爆率文件。\r\n确定生成爆率内容吗？"))
            {
                foreach (TreeNode item in tvGroup.Nodes)
                {
                    string path = item.Name;
                    ReleaseGroup(path, tvGroup.SelectedNode);
                }
                this.ShowSuccessTip("生成成功！");
            }
        }

        private void Tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tab = Tab.SelectedTab;
            plMsg.Text = tab.Name;
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocument(Tab.SelectedTab);            
        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile(Tab.SelectedTab.Name);
        }
        private void OpenFile(string path)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = path,
                FileName = "explorer.exe"
            };

            Process.Start(startInfo);
        }

        private void 打开所在文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(Tab.SelectedTab.Name);
            OpenFile(path);
        }

        private void 格式化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormatText();
        }

        private void FormatText()
        {
            TabPage tabPage = Tab.SelectedTab;
            if (!tabPage.Text.Contains("*"))
            {
                Tab.SelectedTab.Text = tabPage.Text + "*";
            }
            ElementHost elementHost = tabPage.Controls[0] as ElementHost;
            UserControl1 userControl = elementHost.Child as UserControl1;

            if (userControl != null)
            {
                string fomatStr = userControl.TextEditor.Text;
                fomatStr = fomatStr.Replace("\r\n", "や");
                string[] strArray = fomatStr.Split(new char[] { 'や' });
                int spaceLenght = 0;
                // 计算需要多少个空格
                for (int i = 0; i < strArray.Length; i++)
                {
                    string str = strArray[i];
                    if (str.StartsWith("#CHILD")
                        || str.StartsWith("(")
                        || str.StartsWith(")")
                        || string.IsNullOrEmpty(str)
                        || str.StartsWith(";"))
                    {
                        strArray[i] = str;
                        continue;
                    }
                    fomatStr = Regex.Replace(str.Trim(), @"\s+", " ");//将所有空格改为1个空格
                    strArray[i] = fomatStr;
                    if (spaceLenght < fomatStr.Split(' ')[0].Length)
                        spaceLenght = fomatStr.Split(' ')[0].Length;
                }

                // 添加空格
                for (int i = 0; i < strArray.Length; i++)
                {
                    string[] nAr = strArray[i].Split(' ');
                    string originalString = nAr[0];
                    if (originalString.StartsWith("#CHILD")
                        || originalString.StartsWith("(")
                        || originalString.StartsWith(")")
                        || string.IsNullOrEmpty(originalString)
                        || originalString.StartsWith(";"))
                    {
                        //strArray[i] = originalString;
                        continue;
                    }
                    int n = spaceLenght - originalString.Length;
                    for (int j = 0; j < n; j++)
                    {
                        originalString = originalString.Insert(originalString.Length, " ");
                    }
                    nAr[0] = originalString;
                    strArray[i] = string.Join("\t\t", nAr);//默认4个空格
                }

                userControl.TextEditor.Text = string.Join("\r\n", strArray);
            }
        }

        private void 关闭所有ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TabPage tabPage in Tab.TabPages)
            {
                if (tabPage.Text != "主页")
                    Tab.TabPages.Remove(tabPage);
            }
        }

        private void 除此之外全部关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage page = Tab.SelectedTab;
            foreach (TabPage tabPage in Tab.TabPages)
            {
                if (tabPage.Text != "主页" && tabPage.Text != page.Text)
                    Tab.TabPages.Remove(tabPage);
            }
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage page = Tab.SelectedTab;
            Tab.TabPages.Remove(page);
        }
    }
}
