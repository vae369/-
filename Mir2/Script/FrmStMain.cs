
using Microsoft.VisualBasic.FileIO;
using Mir.Core.controller;
using Mir.Core.file;
using Mir.Core.LegendEngine;
using Mir.Models.DTO;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Mir2.Script
{
    public partial class FrmStMain : UIPage
    {
        DBInfo dbInfo = null;
        List<string> treeFileList = new List<string>();
        string fileType = "";
        List<string> fileStringList = new List<string>();
        FrmMain frmMain = null;
        public FrmStMain()
        {
            InitializeComponent();

        }
        public FrmStMain(DBInfo db, FrmMain frmMain)
        {
            InitializeComponent();
            dbInfo = db;
            uiToolTip1.SetToolTip(btnQManage, "QManage脚本\n登陆脚本，就是传奇开启，人物上线后第一个检测的脚本。");
            uiToolTip1.SetToolTip(btnQFunction, "QFunction脚本\n功能脚本，很多功能都要通过它实现。");
            uiToolTip1.SetToolTip(btnMonGen, "MonGen\n控制游戏中所有怪物刷新时间，地点等等信息。");
            uiToolTip1.SetToolTip(btnMerChant, "MerChant\n所有的NPC配置文件，记录了地点，坐标，地图等等信息。");
            uiToolTip1.SetToolTip(btnMapInfo, "MapInfo\n游戏中所有地图配置。");
            uiToolTip1.SetToolTip(lbParagraphIndex, "双击定位到对应代码段落");
            this.frmMain = frmMain;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            uiPanel3.Text = DateTime.Now.DateTimeString();
        }
        private void BindFileFix()
        {
            treeFileList.Add(".txt");
            treeFileList.Add(".ini");
            treeFileList.Add(".xml");
            treeFileList.Add(".json");
            treeFileList.Add(".TXT");
            treeFileList.Add(".INI");
            treeFileList.Add(".XML");
            treeFileList.Add(".JSON");
        }
        private void FrmStMain_Load(object sender, EventArgs e)
        {
            //获取本机版本号信息
            plVersion.Text = "当前版本：" + FileVersionInfo.GetVersionInfo(System.Windows.Forms.Application.StartupPath + "\\Mir2.exe").FileVersion;

            switch (dbInfo.MirType)
            {
                case "GOM": cbMain.SelectedIndex = 1; break;
                case "GEE\\GXX\\LF": cbMain.SelectedIndex = 3; break;
                case "Blue": cbMain.SelectedIndex = 2; break;
                case "Hero": cbMain.SelectedIndex = 4; break;
                default: cbMain.SelectedIndex = 0; break;
            }

            BindFileFix();
            BindTree(dbInfo.MirPath);
            cpSize.Value = System.Drawing.ColorTranslator.FromHtml(dbInfo.MirBackground);
            tbContent.MouseDown += tbContent_MouseDown;
            tbContent.SelectedIndexChanged += tbContent_SelectedIndexChanged;
            tbContent.DrawItem += new DrawItemEventHandler(tbContent_DrawItemEventHandler);
            tbContent.ShowToolTips = true;
            tabPage1.Padding = new Padding(10, 10, 10, 10);

            Random rnd = new Random();
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = rnd.Next(3333, 5555);
            timer.Tick += (a, b) =>
            {
                timer.Stop();

                this.ShowInfoNotifier("当前只适配了Blue和GEE/GXX/LF引擎脚本。", false, 5000);
            };

            timer.Start();

        }

        private void tbContent_DrawItemEventHandler(object sender, DrawItemEventArgs e)
        {
            // 获取TabControl对象和当前TabPage
            UITabControl tabControl = (UITabControl)sender;
            TabPage tabPage = tabControl.TabPages[e.Index];

            // 设置绘制样式
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            e.DrawBackground();

            // 计算文本宽度
            SizeF size = e.Graphics.MeasureString(tabPage.Text, e.Font);

            // 绘制文本
            e.Graphics.DrawString(tabPage.Text, e.Font, new SolidBrush(e.ForeColor), new PointF(e.Bounds.X + (e.Bounds.Width - size.Width) / 2, e.Bounds.Y + (e.Bounds.Height - size.Height) / 2));

            // 如果需要，可以绘制更多的自定义内容
            e.DrawFocusRectangle();
        }


        /// <summary>
        /// 关闭文档
        /// </summary>
        private void tbContent_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            UITabControl tabControl = sender as UITabControl;
            if (tabControl.SelectedIndex == 0)
            {
                return;
            }
            TabPage page = tbContent.SelectedTab;

            switch (e.Button)
            {
                case MouseButtons.Right:
                    CreateTabPageRightControl(e.X, e.Y);//当鼠标右键按在文档控件上显示右键菜单
                    break;
                case MouseButtons.Middle:
                    tbContent.TabPages.Remove(page);
                    lbParagraphIndex.Items.Clear();
                    break;
            }
        }
        TabPage CurrentSelectedPage = null;
        /// <summary>
        /// 创建右键菜单内容
        /// </summary>
        private void CreateTabPageRightControl(int x, int y)
        {
            cmsMain.Items.Clear();
            ToolStripMenuItem tsmClose = new ToolStripMenuItem("关闭", null, tsmi_Clicked, "tsmClose");
            ToolStripMenuItem tsmCloseAndSave = new ToolStripMenuItem("关闭并保存", null, tsmi_Clicked, "tsmCloseAndSave");
            ToolStripMenuItem tsmCloseAll = new ToolStripMenuItem("关闭所有选项卡", null, tsmi_Clicked, "tsmCloseAll");
            ToolStripMenuItem tsmCloseOther = new ToolStripMenuItem("除此之外全部关闭", null, tsmi_Clicked, "tsmCloseOther");
            ToolStripMenuItem tsmCopyPath = new ToolStripMenuItem("复制完整路径", null, tsmi_Clicked, "tsmCopyPath");

            ToolStripMenuItem tsmOpenFile = new ToolStripMenuItem("打开文件", null, tsmi_Clicked, "tsmOpenFile");
            ToolStripMenuItem tsmDirectory = new ToolStripMenuItem("打开所在文件夹", null, tsmi_Clicked, "tsmDirectory");

            ToolStripMenuItem tsmFloat = new ToolStripMenuItem("浮动", null, tsmi_Clicked, "tsmFloat");
            ToolStripMenuItem tsmFloatAll = new ToolStripMenuItem("全部浮动", null, tsmi_Clicked, "tsmFloatAll");

            ToolStripSeparator separator1 = new ToolStripSeparator();
            ToolStripSeparator separator2 = new ToolStripSeparator();

            cmsMain.Items.Add(tsmClose);
            cmsMain.Items.Add(tsmCloseAndSave);
            cmsMain.Items.Add(tsmCloseAll);
            cmsMain.Items.Add(tsmCloseOther);
            cmsMain.Items.Add(tsmCopyPath);
            cmsMain.Items.Add(separator1);   // 添加分割行

            cmsMain.Items.Add(tsmOpenFile);
            cmsMain.Items.Add(tsmDirectory);
            cmsMain.Items.Add(separator2);   // 添加分割行

            cmsMain.Items.Add(tsmFloat);
            cmsMain.Items.Add(tsmFloatAll);


            // 获取点击点的位置
            Point clickPoint = new Point(x, y);

            for (int i = 0; i < tbContent.TabPages.Count; i++)
            {
                if (tbContent.GetTabRect(i).Contains(clickPoint))
                {
                    CurrentSelectedPage = tbContent.TabPages[i];
                    break;
                }
            }
            if (CurrentSelectedPage.Text == "主页")
            {
                // 控制启用
                tsmClose.Enabled = false;
                tsmCloseAndSave.Enabled = false;
                tsmOpenFile.Enabled = false;
                tsmDirectory.Enabled = false;
                tsmCopyPath.Enabled = false;
                tsmFloat.Enabled = false;
                tsmFloatAll.Enabled = false;
            }

            cmsMain.Show(tbContent, clickPoint);
        }
        // 右键菜单项点击事件
        private void tsmi_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            switch (item.Name)
            {
                case "tsmClose":
                    tbContent.TabPages.Remove(CurrentSelectedPage);
                    lbParagraphIndex.Items.Clear();
                    break;
                case "tsmCloseAndSave":
                    SaveDocument(CurrentSelectedPage);
                    tbContent.TabPages.Remove(CurrentSelectedPage);
                    lbParagraphIndex.Items.Clear();
                    break;
                case "tsmCloseAll":
                    for (int i = tbContent.TabPages.Count - 1; i > 0; i--)
                    {
                        if (tbContent.TabPages[i].Text != "主页")
                            tbContent.TabPages.RemoveAt(i);
                    }
                    break;
                case "tsmCloseOther":
                    for (int i = tbContent.TabPages.Count - 1; i > 0; i--)
                    {
                        if (tbContent.TabPages[i].Text == "主页")
                            continue;
                        if (CurrentSelectedPage.Name == tbContent.TabPages[i].Name)
                            continue;
                        tbContent.TabPages.RemoveAt(i);
                    }
                    break;
                case "tsmCopyPath": ClipboardText.CopyToClipboard(CurrentSelectedPage.Name); break;
                case "tsmOpenFile": OpenFile(CurrentSelectedPage.Name); break;
                case "tsmDirectory":
                    string path = CurrentSelectedPage.Name.Substring(0, CurrentSelectedPage.Name.LastIndexOf('\\'));
                    OpenFile(path);
                    break;
                case "tsmFloat":
                    {
                        tbContent.TabPages.Remove(CurrentSelectedPage);
                        ElementHost elementHost = CurrentSelectedPage.Controls[1].Controls[0] as ElementHost;
                        UserControl1 userControl = elementHost.Child as UserControl1;

                        FrmFloat frmFloat = new FrmFloat(CurrentSelectedPage.Name, userControl.TextEditor.Text);
                        frmFloat.Show();
                        lbParagraphIndex.Items.Clear();
                    }

                    break;
                case "tsmFloatAll":
                    {
                        for (int i = tbContent.TabPages.Count - 1; i > 0; i--)
                        {
                            if (tbContent.TabPages[i].Text == "主页")
                                continue;

                            ElementHost elementHost = tbContent.TabPages[i].Controls[1].Controls[0] as ElementHost;
                            UserControl1 userControl = elementHost.Child as UserControl1;

                            FrmFloat frmFloat = new FrmFloat(tbContent.TabPages[i].Name, userControl.TextEditor.Text);
                            frmFloat.Show();
                            tbContent.TabPages.RemoveAt(i);
                            lbParagraphIndex.Items.Clear();
                        }
                    }
                    break;
            }
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
        private void tbContent_SelectedIndexChanged(object sender, EventArgs e)
        {

            UITabControl tab = sender as UITabControl;
            TabPage tabPage = tab.SelectedTab;
            if (tabPage.Text == "主页") return;
            plMsg.Text = $"正在操作 [{tabPage.Tag}] *";

            UIComboBox comboBox = tabPage.Controls[0].Controls[0] as UIComboBox;
            if (comboBox != null)
            {
                comboBox.Items.Clear();
                foreach (TabPage tp in tab.TabPages)
                {
                    if (tp.Text != "主页")
                        comboBox.Items.Add(tp.Name);
                }
            }
            comboBox.SelectedItem = tabPage.Name;

            //绑定右侧listbox内容
            ElementHost elementHost = tabPage.Controls[1].Controls[0] as ElementHost;

            UserControl1 userControl = elementHost.Child as UserControl1;
            if (userControl != null)
            {
                //string str = userControl.TextEditor.Text;
                fileStringList = FileHelper.ReadTxtReturnList(tabPage.Name);

                string pattern = @"^\[.*\]$";
                lbParagraphIndex.Items.Clear();
                foreach (string line in fileStringList)
                {
                    if (Regex.IsMatch(line, pattern))
                    {
                        lbParagraphIndex.Items.Add(line);
                    }
                }
            }
        }
        /// <summary>
        /// 绑定树形控件内容
        /// </summary>
        /// <param name="MirPath"></param>
        private void BindTree(string MirPath)
        {
            MirServerTree.ImageList = treeFileImage;
            MirServerTree.MouseDown += MirServerTree_MouseDown;
            MirServerTree.NodeMouseDoubleClick += MirServerTree_NodeMouseDoubleClick;

            MirServerTree.Nodes.Clear();

            AddDirectoriesAndFiles(MirServerTree, MirPath);

            MirServerTree.ItemHeight = 26;
        }

        public void AddDirectoriesAndFiles(UITreeView treeView, string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            //TreeNode node = treeView.Nodes.Add(System.IO.Path.GetFileName(path), System.IO.Path.GetFileName(path), 1);
            TreeNode node = treeView.Nodes.Add(path, path, 0);

            AddSubDirectoriesAndFiles(node, new DirectoryInfo(path));
            node.Expand();
        }

        private void AddSubDirectoriesAndFiles(TreeNode node, DirectoryInfo directory)
        {

            foreach (DirectoryInfo subDir in directory.GetDirectories())
            {
                TreeNode subNode = node.Nodes.Add(subDir.FullName, subDir.Name, 0);

                AddSubDirectoriesAndFiles(subNode, subDir);
            }


            foreach (FileInfo file in directory.GetFiles())
            {
                if (treeFileList.Contains(file.Extension))
                {
                    switch (file.Extension.ToLower())
                    {
                        case ".txt": node.Nodes.Add(file.FullName, file.Name, 1); break;
                        case ".ini": node.Nodes.Add(file.FullName, file.Name, 2); break;
                        case ".xml": node.Nodes.Add(file.FullName, file.Name, 3); break;
                        case ".json": node.Nodes.Add(file.FullName, file.Name, 4); break;
                    }
                }
            }
        }

        private void MirServerTree_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // 获取点击点的位置
                Point clickPoint = new Point(e.X, e.Y);
                TreeNode selectedNode = MirServerTree.GetNodeAt(clickPoint);

                // 如果点击在节点上，选中该节点
                if (selectedNode != null)
                {
                    // 选中节点
                    MirServerTree.SelectedNode = selectedNode;

                    CreateTreeRightControl(MirServerTree, clickPoint);
                }
            }
        }

        /// <summary>
        /// 创建右键菜单内容
        /// </summary>
        private void CreateTreeRightControl(UITreeView MirTree, Point clickPoint)
        {
            cmsMain.Items.Clear();
            ToolStripMenuItem tsmCreateFile = new ToolStripMenuItem("新建文件", null, tsm_Clicked, Keys.Control | Keys.N);
            tsmCreateFile.Name = "tsmCreateFile";

            ToolStripMenuItem tsmRenameFile = new ToolStripMenuItem("重命名(文件)", null, tsm_Clicked, Keys.F2);
            tsmRenameFile.Name = "tsmRenameFile";

            ToolStripMenuItem tsmConvertFileDEFAULT = new ToolStripMenuItem("系统默认", null, tsm_Clicked);
            tsmConvertFileDEFAULT.Name = "tsmConvertFileDEFAULT";
            ToolStripMenuItem tsmConvertFileASCII = new ToolStripMenuItem("ASCII", null, tsm_Clicked);
            tsmConvertFileASCII.Name = "tsmConvertFileASCII";
            ToolStripMenuItem tsmConvertFileUNICODE = new ToolStripMenuItem("UNICODE", null, tsm_Clicked);
            tsmConvertFileUNICODE.Name = "tsmConvertFileUNICODE";
            ToolStripMenuItem tsmConvertFileUTF8 = new ToolStripMenuItem("UTF-8", null, tsm_Clicked);
            tsmConvertFileUTF8.Name = "tsmConvertFileUTF8";
            ToolStripMenuItem tsmConvertFileUTF8Bom = new ToolStripMenuItem("UTF-8 BOM", null, tsm_Clicked);
            tsmConvertFileUTF8Bom.Name = "tsmConvertFileUTF8Bom";
            ToolStripMenuItem tsmConvertFileGBK = new ToolStripMenuItem("GBK", null, tsm_Clicked);
            tsmConvertFileGBK.Name = "tsmConvertFileGBK";
            ToolStripMenuItem tsmConvertFileGB2312 = new ToolStripMenuItem("GB2312", null, tsm_Clicked);
            tsmConvertFileGB2312.Name = "tsmConvertFileGB2312";
            ToolStripMenuItem tsmConvertFileGB18030 = new ToolStripMenuItem("GB18030", null, tsm_Clicked);
            tsmConvertFileGB18030.Name = "tsmConvertFileGB18030";

            ToolStripMenuItem tsmConvertFile = new ToolStripMenuItem("文件转换格式", null, tsm_Clicked);
            tsmConvertFile.Name = "tsmConvertFile";

            tsmConvertFile.DropDownItems.AddRange(new ToolStripItem[] {
            tsmConvertFileDEFAULT,
            tsmConvertFileASCII,
            tsmConvertFileUNICODE,
            tsmConvertFileUTF8,
            tsmConvertFileUTF8Bom,
            tsmConvertFileGBK,
            tsmConvertFileGB2312,
            tsmConvertFileGB18030,
            });
            tsmConvertFile.Enabled = false;

            ToolStripMenuItem tsmDelFile = new ToolStripMenuItem("删除文件", null, tsm_Clicked, Keys.Delete);
            tsmDelFile.Name = "tsmDelFile";

            ToolStripMenuItem tsmCreateFolder = new ToolStripMenuItem("新建文件夹", null, tsm_Clicked, Keys.Control | Keys.Shift | Keys.N);
            tsmCreateFolder.Name = "tsmCreateFolder";

            ToolStripMenuItem tsmRenameFolder = new ToolStripMenuItem("重命名(文件夹)", null, tsm_Clicked, Keys.Control | Keys.F2);
            tsmRenameFolder.Name = "tsmRenameFolder";

            ToolStripMenuItem tsmDelFolder = new ToolStripMenuItem("删除文件夹", null, tsm_Clicked, Keys.Control | Keys.Delete);
            tsmDelFolder.Name = "tsmDelFolder";

            ToolStripMenuItem tsmOpenFile = new ToolStripMenuItem("打开文件", null, tsm_Clicked, Keys.Control | Keys.T);
            tsmOpenFile.Name = "tsmOpenFile";

            ToolStripMenuItem tsmOpenFolder = new ToolStripMenuItem("打开文件夹", null, tsm_Clicked, Keys.Control | Keys.O);
            tsmOpenFolder.Name = "tsmOpenFolder";

            ToolStripMenuItem tsmRefresh = new ToolStripMenuItem("刷新", null, tsm_Clicked, Keys.Control | Keys.F5);
            tsmRefresh.Name = "tsmRefresh";


            if (MirTree.SelectedNode.Text.IndexOf('.') > 0)
            {
                tsmRenameFolder.Enabled = false;
                tsmDelFolder.Enabled = false;
                tsmOpenFolder.Enabled = false;
            }
            else
            {
                tsmRenameFile.Enabled = false;
                tsmDelFile.Enabled = false;
                tsmOpenFile.Enabled = false;
            }
            cmsMain.Items.Add(tsmCreateFile);
            cmsMain.Items.Add(tsmRenameFile);
            //cmsMain.Items.Add(tsmConvertFile);
            cmsMain.Items.Add(tsmDelFile);
            cmsMain.Items.Add(new ToolStripSeparator());   // 添加分割行
            cmsMain.Items.Add(tsmCreateFolder);
            cmsMain.Items.Add(tsmRenameFolder);
            cmsMain.Items.Add(tsmDelFolder);
            cmsMain.Items.Add(new ToolStripSeparator());   // 添加分割行

            cmsMain.Items.Add(tsmOpenFile);
            cmsMain.Items.Add(tsmOpenFolder);
            cmsMain.Items.Add(tsmRefresh);

            cmsMain.Show(MirTree, clickPoint);
        }

        private void tsm_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            switch (item.Name)
            {
                case "tsmCreateFile": CreateFile(); break;
                case "tsmRenameFile": RenameFile(); break;
                case "tsmDelFile": DelFile(); break;
                case "tsmCreateFolder": CreateFolder(); break;
                case "tsmRenameFolder": RenameFolder(); break;
                case "tsmDelFolder": DelFolder(); break;
                case "tsmOpenFile": OpenFile(); break;
                case "tsmOpenFolder": OpenFolder(); break;
                case "tsmRefresh": MirRefresh(); break;
                case "tsmConvertFileDEFAULT": ConvertAllFileEncoding("系统默认"); break;
                case "tsmConvertFileASCII": ConvertAllFileEncoding("ASCII"); break;
                case "tsmConvertFileUNICODE": ConvertAllFileEncoding("UNICODE"); break;
                case "tsmConvertFileUTF8": ConvertAllFileEncoding("UTF8"); break;
                case "tsmConvertFileUTF8Bom": ConvertAllFileEncoding("UTF8BOM"); break;
                case "tsmConvertFileGBK": ConvertAllFileEncoding("GBK"); break;
                case "tsmConvertFileGB2312": ConvertAllFileEncoding("GB2312"); break;
                case "tsmConvertFileGB18030": ConvertAllFileEncoding("GB18030"); break;
            }
        }
        private void ConvertAllFileEncoding(string encodingStr)
        {
            TreeNode tn = MirServerTree.SelectedNode;
            string path = tn.Name;
            string res = "";
            if (path.IndexOf('.') > 0)
            {
                if (this.ShowAskDialog2("提示", "确定转换当前文件吗?"))
                {
                    //switch (encodingStr)
                    //{
                    //    case "系统默认": res = FileHelper.ConvertFileEncoding(path, Encoding.Default, null); break;
                    //    case "ASCII": res = FileHelper.ConvertFileEncoding(path, Encoding.ASCII, null); break;
                    //    case "UNICODE": res = FileHelper.ConvertFileEncoding(path, Encoding.Unicode, null); break;
                    //    case "UTF8": res = FileHelper.ConvertFileEncoding(path, Encoding.UTF8, null); break;
                    //    case "UTF8BOM": res = FileHelper.ConvertFileEncoding(path, null, "utf-8 bom"); break;
                    //    case "GBK": res = FileHelper.ConvertFileEncoding(path, null, "gbk"); break;
                    //    case "GB2312": res = FileHelper.ConvertFileEncoding(path, null, "gb2312"); break;
                    //    case "GB18030": res = FileHelper.ConvertFileEncoding(path, null, "gb18030"); break;
                    //}
                    if (FileHelper.ContainsChinese(res))
                    {
                        if (this.ShowAskDialog2("提示", "转换完成，是否打开查看一下?"))
                        {
                            OpenFile(path);
                        }
                    }
                    else
                    {
                        this.ShowInfoDialog2("提示", "转换完成，但内容不是中文，点击确定打开文件查看。");
                        {

                        }
                    }
                }
            }
            else
            {
                if (this.ShowAskDialog2("提示", "确定转换当前文件夹内的文件吗?"))
                {
                    foreach (var file in Directory.GetFiles(path))
                    {
                        //switch (encodingStr)
                        //{
                        //    case "系统默认": res = FileHelper.ConvertFileEncoding(path, Encoding.Default, null); break;
                        //    case "ASCII": res = FileHelper.ConvertFileEncoding(path, Encoding.ASCII, null); break;
                        //    case "UNICODE": res = FileHelper.ConvertFileEncoding(path, Encoding.Unicode, null); break;
                        //    case "UTF8": res = FileHelper.ConvertFileEncoding(path, Encoding.UTF8, null); break;
                        //    case "UTF8BOM": res = FileHelper.ConvertFileEncoding(path, null, "utf-8 bom"); break;
                        //    case "GBK": res = FileHelper.ConvertFileEncoding(path, null, "gbk"); break;
                        //    case "GB2312": res = FileHelper.ConvertFileEncoding(path, null, "gb2312"); break;
                        //    case "GB18030": res = FileHelper.ConvertFileEncoding(path, null, "gb18030"); break;
                        //}

                    }
                }
            }
        }
        private void OpenFolder()
        {
            TreeNode tn = MirServerTree.SelectedNode;
            //string path = tn.FullPath.Substring(0, tn.FullPath.LastIndexOf('\\'));

            OpenFile(tn.FullPath);
        }
        private void OpenFile()
        {
            TreeNode tn = MirServerTree.SelectedNode;
            OpenFile(tn.FullPath);
        }

        private void MirRefresh()
        {
            BindTree(dbInfo.MirPath);
        }
        private void DelFolder()
        {
            TreeNode tn = MirServerTree.SelectedNode;
            string path = tn.FullPath;
            string text = tn.Text;
            if (File.Exists(path))
            {
                this.ShowErrorTip("不能删除文件！");
                return;
            }
            if (this.ShowAskDialog2("提示", "确定删除当前文件夹吗?\r\n[" + text + "]\r\n提示:删除的文件夹可以在回收站中找回。"))
            {
                if (Directory.Exists(path))
                {
                    // 检查是否还有子文件夹
                    if (Directory.GetDirectories(path).Length > 0)
                    {
                        this.ShowErrorTip("文件夹内含有子文件夹，无法删除！");
                        return;
                    }
                    // 检查是否还有子文件
                    if (Directory.GetFiles(path).Length > 0)
                    {
                        this.ShowErrorTip("文件夹内含有文件，无法删除！");
                        return;
                    }

                    //删除树节点
                    MirServerTree.Nodes.Remove(tn);
                    // 移动文件到回收站
                    FileSystem.DeleteDirectory(path, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                }
            }
        }
        private void RenameFolder()
        {
            TreeNode tn = MirServerTree.SelectedNode;
            string path = tn.FullPath;
            string oldPath = path;
            if (File.Exists(path))
            {
                this.ShowErrorDialog2("路径错误", "文件不能修改文件夹名！");
                return;
            }

            if (Directory.Exists(path))
            {
                FrmEditFolder frmEditDirectory = new FrmEditFolder(path);
                DialogResult dir = frmEditDirectory.ShowDialog();
                if (dir == DialogResult.OK)
                {
                    tn.Text = frmEditDirectory.DirectoryName;
                    tn.Name = frmEditDirectory.DirectoryPath;
                    // 重命名文件夹
                    Directory.Move(oldPath, frmEditDirectory.DirectoryPath);
                }
            }
        }
        private void CreateFolder()
        {
            TreeNode tn = MirServerTree.SelectedNode;
            tn.Expand();
            string path = tn.FullPath;

            if (Directory.Exists(path))
            {
                //在文件夹中创建文件夹
                MirServerTree.LabelEdit = true;
                FrmCretFolder frmCretDirectory = new FrmCretFolder();
                DialogResult dir = frmCretDirectory.ShowDialog();
                if (dir == DialogResult.OK)
                {
                    path = path + "\\" + frmCretDirectory.DirectoryName;
                    tn.Nodes.Add(path, frmCretDirectory.DirectoryName, 0);
                    Directory.CreateDirectory(path);
                }
            }
            if (File.Exists(path))
            {
                //在当前节点创建文件夹
                FrmCretFolder frmCretDirectory = new FrmCretFolder();
                DialogResult dir = frmCretDirectory.ShowDialog();
                if (dir == DialogResult.OK)
                {
                    path = path.Substring(0, path.LastIndexOf('\\')) + "\\" + frmCretDirectory.DirectoryName;
                    tn.Parent.Nodes.Add(path, frmCretDirectory.DirectoryName, 0);
                    Directory.CreateDirectory(path);
                }
            }
        }
        private void CreateFile()
        {
            TreeNode tn = MirServerTree.SelectedNode;
            tn.Expand();
            string path = tn.FullPath;

            if (Directory.Exists(path))
            {
                FrmCretFile frmCretFile = new FrmCretFile();
                frmCretFile.MirFilePath = path;
                DialogResult dr = frmCretFile.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    tn.Nodes.Add(frmCretFile.AddFilePath, frmCretFile.AddFileName, frmCretFile.ImageIndex);
                    File.Create(frmCretFile.AddFilePath).Dispose();
                }
            }
            if (File.Exists(path))
            {
                path = path.Substring(0, path.LastIndexOf('\\'));
                FrmCretFile frmCretFile = new FrmCretFile();
                frmCretFile.MirFilePath = path;
                DialogResult dr = frmCretFile.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    tn.Parent.Nodes.Add(frmCretFile.AddFilePath, frmCretFile.AddFileName, frmCretFile.ImageIndex);
                    File.Create(frmCretFile.AddFilePath).Dispose();
                }
            }
        }
        private void RenameFile()
        {
            TreeNode tn = MirServerTree.SelectedNode;
            string path = tn.FullPath;
            string oldPath = path;
            string oldText = tn.Text;

            tn.Expand();
            if (File.Exists(path))
            {
                path = path.Substring(0, path.LastIndexOf('\\'));
                FrmEditFile frmEditFile = new FrmEditFile(tn.Text, tn.ImageIndex);
                frmEditFile.MirFilePath = path;
                DialogResult dr = frmEditFile.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    //树控件修改
                    tn.Text = frmEditFile.EditFileName;
                    tn.Name = frmEditFile.EditFilePath;
                    tn.ImageIndex = frmEditFile.ImageIndex;
                    MirServerTree.Refresh();

                    //文件修改
                    File.Move(oldPath, frmEditFile.EditFilePath);

                    //打开的文件修改
                    foreach (TabPage tabPage in tbContent.TabPages)
                    {
                        if (tabPage.Text == oldText)
                        {
                            tabPage.Text = tn.Text;
                            tabPage.Name = tn.Name;
                        }
                    }
                }
            }
        }
        private void DelFile()
        {
            TreeNode tn = MirServerTree.SelectedNode;
            string path = tn.FullPath;
            string text = tn.Text;

            if (this.ShowAskDialog2("提示", "确定删除当前文件吗?\r\n[" + text + "]\r\n提示:删除的文件可以在回收站中找回。"))
            {
                if (File.Exists(path))
                {
                    //删除文档
                    for (int i = 0; i < tbContent.TabPages.Count; i++)
                    {
                        if (tbContent.TabPages[i].Text == text)
                        {
                            tbContent.TabPages.Remove(tbContent.TabPages[i]);
                            break;
                        }
                    }
                    //删除树节点
                    MirServerTree.Nodes.Remove(tn);

                    //删除路径文件
                    //File.Delete(path);
                    // 移动文件到回收站
                    FileSystem.DeleteFile(path, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);

                    //FileSystem.DeleteDirectory(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);

                }
            }
        }

        private void MirServerTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // 获取选中的节点
            TreeNode selectedNode = e.Node;

            string text = selectedNode.Text;
            if (text.IndexOf('.') > 0)
            {
                //单击鼠标左键才响应
                BindTabPageData(text, selectedNode.Name);
            }
        }



        private void BindTabPageData(string tbText, string path)
        {
            string nt = tbText;
            if (tbText.Length > 6)
                nt = tbText.Substring(0, 6) + " ..";

            //添加文档
            TabPage tp = new TabPage();
            tp.ToolTipText = tbText;
            tp.Text = nt;
            tp.Name = path;
            tp.Tag = tbText;
            tp.BackColor = System.Drawing.Color.White;


            //添加下拉框
            UIComboBox comboBox = new UIComboBox();
            comboBox.Dock = DockStyle.Top;
            comboBox.Margin = new Padding(15, 3, 3, 3);
            comboBox.Radius = 1;
            comboBox.Style = UIStyle.Inherited;
            comboBox.DropDownStyle = UIDropDownStyle.DropDownList;
            comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;

            comboBox.Items.Clear();

            foreach (TabPage item in tbContent.TabPages)
            {
                if (item.Text != "主页")
                    comboBox.Items.Add(tp.Name);
            }
            comboBox.Items.Add(tp.Name);
            comboBox.SelectedItem = tp.Name;

            UIPanel p1 = new UIPanel();
            p1.BackColor = System.Drawing.Color.Red;
            p1.Location = new Point(0, 0);
            p1.Size = new Size(tbContent.Width, comboBox.Height);
            p1.Controls.Add(comboBox);
            p1.Dock = DockStyle.Top;
            p1.Name = "p1";
            tp.Controls.Add(p1);

            //添加文档内的WPF控件
            UIPanel p2 = new UIPanel();
            p2.Resize += UIPanal_Resize;
            p2.AutoSizeMode = AutoSizeMode.GrowOnly;
            p2.Size = new System.Drawing.Size(tbContent.Width, tbContent.Height);
            p2.Name = "p2";
            p2.BackColor = System.Drawing.Color.Red;
            p2.Dock = DockStyle.Fill;
            p2.Padding = new Padding(0, 30, 0, 30);

            ElementHost elHost = new ElementHost();
            elHost.Dock = DockStyle.Fill;

            elHost.ContextMenuStrip = cmsWinMain;
            UserControl1 uc = new UserControl1(dbInfo.MirType);
            uc.TextEditor.Background = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(dbInfo.MirBackground));

            string fileContent = FileHelper.ReadTxt(path);

            if (string.IsNullOrEmpty(fileContent.Trim()))
                fileContent = "";
            uc.TextEditor.Text = fileContent.ToUpper();
            uc.TextEditor.FontSize = 16;

            uc.KeyDown += userControl_KeyDown;
            uc.TextEditor.TextArea.Caret.PositionChanged += CaretOnPositionChanged;

            RadioBtnChecked(uc);
            fileStringList = FileHelper.ReadTxtReturnList(path);

            elHost.Child = uc;

            p2.Controls.Add(elHost);
            tp.Controls.Add(p2);

            bool isExti = true;
            foreach (TabPage item in tbContent.TabPages)
            {
                if (item.Name == tp.Name) isExti = false;
            }

            if (isExti)
            {
                tbContent.TabPages.Add(tp);
                tbContent.SelectedTab = tp;
            }


        }

        private void UIPanal_Resize(object sender, EventArgs e)
        {
            UIPanel uIPanel = (UIPanel)sender;
            if (uIPanel.Name == "p2")
            {
                uIPanel.Size = new System.Drawing.Size(tbContent.Width, tbContent.Height);
            }

        }
        private void userControl_KeyDown(object sender, EventArgs e)
        {
            System.Windows.Input.KeyEventArgs kea = e as System.Windows.Input.KeyEventArgs;
            if (kea.KeyboardDevice.Modifiers == System.Windows.Input.ModifierKeys.Control && kea.Key == Key.S)
            {
                SaveDocument(tbContent.SelectedTab);
            }
            else
            {
                //操作某个tabpage
                plMsg.Text = "正在操作 [" + tbContent.SelectedTab.Text + "] 文档";
            }
        }
        private void RadioBtnChecked(UserControl1 uc)
        {
            switch (dbInfo.MirType)
            {
                case "GOM": break;
                case "GEE\\GXX\\LF": uc.GEE_GXX_LFSetting(); break;
                case "Blue": uc.BlueSetting(); break;
                case "Hero": break;
            }
            CompletionData.MirType = dbInfo.MirType;
        }
        private void SaveDocument(TabPage tabPage)
        {

            string filePath = "";
            switch (fileType)
            {
                case "QManage": filePath = dbInfo.MirPath + "\\Mir200\\Envir\\MapQuest_Def\\QManage.txt"; break;
                case "MapInfo": filePath = dbInfo.MirPath + "\\Mir200\\Envir\\MapInfo.txt"; break;
                case "MonGen": filePath = dbInfo.MirPath + "\\Mir200\\Envir\\MonGen.txt"; break;
                case "QFunction": filePath = dbInfo.MirPath + "\\Mir200\\Envir\\Market_Def\\QFunction-0.txt"; break;
                case "MerChant": filePath = dbInfo.MirPath + "\\Mir200\\Envir\\MerChant.txt"; break;
                case "MonSayMsg": filePath = dbInfo.MirPath + "\\Mir200\\Envir\\MonSayMsg.txt"; break;
                default: filePath = tabPage.Name; break;
            }

            string content = "";
            ElementHost elHost = tabPage.Controls[1].Controls[0] as ElementHost;
            UserControl1 userControl = elHost.Child as UserControl1;
            if (userControl != null)
            {
                content = userControl.TextEditor.Text.ToUpper();
            }

            SetEditorByPath(filePath, content);

            string pathTemp = tabPage.Name.Insert(tabPage.Name.LastIndexOf('.'), "_bak");
            FileHelper.DeleteFile(pathTemp);
            plMsg.Text = $"[{tabPage.Text}]保存成功！";
            this.ShowSuccessTip("保存成功！");
        }
        private void SetEditorByPath(string path, string content)
        {
            if (!File.Exists(path))
            {
                this.ShowErrorDialog2("路径错误", "保存路径错误，请检查MirServer路径。");
                return;
            }

            FileHelper.WriteAllText(path, content);
        }
        private void CaretOnPositionChanged(object sender, EventArgs eventArgs)
        {
            TabPage tabPage = tbContent.SelectedTab;
            ElementHost elementHost = tabPage.Controls[1].Controls[0] as ElementHost;
            UserControl1 userControl = elementHost.Child as UserControl1;

            uiPanel4.Text = $"行 {userControl.TextEditor.TextArea.Caret.Line} 列 {userControl.TextEditor.TextArea.Caret.Column}";
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UIComboBox cbx = sender as UIComboBox;

            foreach (TabPage tabPage in tbContent.TabPages)
            {
                if (cbx.SelectedItem != null)
                {
                    if (tabPage.Name == cbx.SelectedItem.ToString())
                        tbContent.SelectedTab = tabPage;
                }
            }
        }

        private void btnQManage_Click(object sender, EventArgs e)
        {
            fileType = "QManage";
            string path = dbInfo.MirPath + "\\Mir200\\Envir\\MapQuest_Def\\QManage.txt";
            if (!File.Exists(path))
            {
                this.ShowErrorDialog2("路径错误", "请设置正确的MirServer路径。");
                return;
            }
            BindTabPageData("QManage.txt", path);
            plMsg.Text = "读取QManage.txt成功！";
        }

        private void btnQFunction_Click(object sender, EventArgs e)
        {
            fileType = "QFunction";
            string path = dbInfo.MirPath + "\\Mir200\\Envir\\Market_Def\\QFunction-0.txt";
            if (!File.Exists(path))
            {
                this.ShowErrorDialog2("路径错误", "请设置正确的MirServer路径。");
                return;
            }
            BindTabPageData("QFunction.txt", path);
            plMsg.Text = "读取QFunction.txt成功！";
        }

        private void btnMapInfo_Click(object sender, EventArgs e)
        {
            fileType = "MapInfo";
            string path = dbInfo.MirPath + "\\Mir200\\Envir\\MapInfo.txt";
            if (!File.Exists(path))
            {
                this.ShowErrorDialog2("路径错误", "请设置正确的MirServer路径。");
                return;
            }
            BindTabPageData("MapInfo.txt", path);
            plMsg.Text = "读取MapInfo.txt成功！";
        }

        private void btnMonGen_Click(object sender, EventArgs e)
        {
            fileType = "MonGen";
            string path = dbInfo.MirPath + "\\Mir200\\Envir\\MonGen.txt";
            if (!File.Exists(path))
            {
                this.ShowErrorDialog2("路径错误", "请设置正确的MirServer路径。");
                return;
            }
            BindTabPageData("MonGen.txt", path);
            plMsg.Text = "读取MonGen.txt成功！";
        }

        private void btnMerChant_Click(object sender, EventArgs e)
        {
            fileType = "MerChant";
            string path = dbInfo.MirPath + "\\Mir200\\Envir\\MerChant.txt";
            if (!File.Exists(path))
            {
                this.ShowErrorDialog2("路径错误", "请设置正确的MirServer路径。");
                return;
            }
            BindTabPageData("MerChant.txt", path);
            plMsg.Text = "读取MerChant.txt成功！";
        }

        FrmRightTip frt = null;
        private void lbSearch_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (lbSearch.SelectedItem != null)
            {
                string searchStr = lbSearch.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(searchStr))
                {
                    if (frt != null)
                    {
                        frt.Close();
                    }
                    frt = new FrmRightTip(searchStr, this.frmMain);

                    frt.Attach();
                }
            }

        }

        private void btnSearch_ButtonClick(object sender, EventArgs e)
        {
            string txtSc = btnSearch.Text.ToUpper().Trim();
            if (!string.IsNullOrEmpty(txtSc))
            {
                lbSearch.Items.Clear();

                var blueList = BlueCompletionData.BlueCheckedData.Where(s => s.Value.ToUpper().Contains(txtSc) || s.Key.ToUpper().Contains(txtSc)).ToList();

                foreach (var item in BlueCompletionData.CompletionDataList)
                {
                    blueList.ForEach(t =>
                    {
                        if (!lbSearch.Items.Contains(t.Key))
                            lbSearch.Items.Add(t.Key);
                    });
                    if (item.Description.ToString().ToUpper().Contains(txtSc) || item.Text.ToUpper().Contains(txtSc))
                    {
                        if (!lbSearch.Items.Contains(item.Text))
                            lbSearch.Items.Add(item.Text);
                    }
                }
            }
        }

        private void lbParagraphIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbParagraphIndex.SelectedItem != null)
            {
                string str = lbParagraphIndex.Text;
                TabPage tabPage = tbContent.SelectedTab;
                if (tabPage.Text != "主页")
                {
                    //绑定右侧listbox内容
                    if (tabPage.Controls.Count > 0)
                    {
                        ElementHost elementHost = tabPage.Controls[1].Controls[0] as ElementHost;

                        UserControl1 userControl = elementHost.Child as UserControl1;
                        if (userControl != null)
                        {
                            int highlightStart = fileStringList.IndexOf(str) + 1;
                            int highlightLength = str.Length;
                            //int textIndex = userControl.TextEditor.Text.IndexOf(str);
                            var dom = userControl.TextEditor.Document.GetLineByNumber(highlightStart);
                            if (dom.LineNumber > 0)
                            {
                                // 设置选择区域
                                userControl.TextEditor.Select(dom.Offset, highlightLength);
                                // 滚动到选择区域
                                userControl.TextEditor.ScrollTo(highlightStart, highlightLength);
                            }
                        }
                    }
                }

            }
        }

        private void cbMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMain.SelectedIndex > 0)
            {
                dbInfo.MirType = cbMain.SelectedItem.ToString();

                foreach (TabPage tabPage in tbContent.TabPages)
                {
                    if (tabPage.Text == "主页") continue;
                    ElementHost elementHost = tabPage.Controls[1].Controls[0] as ElementHost;
                    UserControl1 userControl = elementHost.Child as UserControl1;

                    switch (dbInfo.MirType)
                    {
                        case "GOM": break;
                        case "GEE\\GXX\\LF": userControl.GEE_GXX_LFSetting(); break;
                        case "Blue": userControl.BlueSetting(); break;
                        case "Hero": break;
                    }
                }
                CompletionData.MirType = dbInfo.MirType;

                Mir.Core.utils.ConfigHelper.UpdateAppConfig("MirType", dbInfo.MirType);
                this.ShowSuccessTip("服务端类型设置为：" + dbInfo.MirType);
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocument(tbContent.SelectedTab);
        }

        private void 格式化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormatText();
        }
        private void FormatText()
        {
            TabPage tabPage = tbContent.SelectedTab;
            ElementHost elementHost = tabPage.Controls[1].Controls[0] as ElementHost;
            UserControl1 userControl = elementHost.Child as UserControl1;
            if (userControl != null)
            {
                string fomatStr = userControl.TextEditor.Text;
                fomatStr = fomatStr.Replace("\r\n", "や");
                string[] strArray = fomatStr.Split(new char[] { 'や' });
                string newStr = "";
                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex("[ ]{2,}", options);
                for (int i = 0; i < strArray.Length; i++)
                {
                    newStr = strArray[i].Replace("\t", " ");
                    newStr = regex.Replace(newStr, " ");
                    newStr = newStr.Replace(" ", "\t\t");
                    strArray[i] = newStr;
                }

                userControl.TextEditor.Text = string.Join("\r\n", strArray);
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tabPage = tbContent.SelectedTab;
            ElementHost elementHost = tabPage.Controls[1].Controls[0] as ElementHost;
            UserControl1 userControl = elementHost.Child as UserControl1;
            if (!string.IsNullOrEmpty(userControl.TextEditor.SelectedText))
            {
                ClipboardText.CopyToClipboard(userControl.TextEditor.SelectedText);
                this.ShowSuccessNotifier("复制成功！");
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tabPage = tbContent.SelectedTab;
            ElementHost elementHost = tabPage.Controls[1].Controls[0] as ElementHost;
            UserControl1 userControl = elementHost.Child as UserControl1;

            userControl.TextEditor.SelectedText = ClipboardText.PasteFromClipboard();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tabPage = tbContent.SelectedTab;
            ElementHost elementHost = tabPage.Controls[1].Controls[0] as ElementHost;
            UserControl1 userControl = elementHost.Child as UserControl1;

            ClipboardText.CutToClipboardByWPF(userControl.TextEditor);

            this.ShowSuccessNotifier("剪切成功！");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.ShowInfoNotifier("请直接按标注的快捷键！Ctrl+Z");
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.ShowInfoNotifier("请直接按标注的快捷键！Ctrl+Y");
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TabPage tabPage = tbContent.SelectedTab;
            OpenFile(tabPage.Name);
        }

        private void 打开所在文件夹ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TabPage tabPage = tbContent.SelectedTab;
            string path = tabPage.Name.Substring(0, tabPage.Name.LastIndexOf('\\'));

            OpenFile(path);
        }

        private void 关闭CtrlQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SaveDocument(tbContent.SelectedTab);
            TabPage page = tbContent.SelectedTab;
            tbContent.TabPages.Remove(page);
            lbParagraphIndex.Items.Clear();
            //this.ShowSuccessNotifier("数据已保存！");
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            ConvertFileEncodingByTxT("UNICODE");
        }

        private void ConvertFileEncodingByTxT(string encodingStr)
        {
            bool isBak = false;
            if (this.ShowAskDialog2("提示", "是否需要备份当前文件进行转换?\n点击确定则对备份转换，点击取消则对当前文件转换。\n(提醒:对当前文件转换如果错误是不能返回撤销操作的)\n保存后会自动删除备份文件(带_bak的文件)"))
            {
                isBak = true;
            }
            TabPage tabPage = tbContent.SelectedTab;
            ElementHost elementHost = tabPage.Controls[1].Controls[0] as ElementHost;
            UserControl1 userControl = elementHost.Child as UserControl1;

            string path = tabPage.Name;
            string res = "";
            switch (encodingStr)
            {
                case "系统默认": res = FileHelper.ConvertFileEncoding(path, Encoding.Default, null, isBak); break;
                case "ASCII": res = FileHelper.ConvertFileEncoding(path, Encoding.ASCII, null, isBak); break;
                case "UNICODE": res = FileHelper.ConvertFileEncoding(path, Encoding.Unicode, null, isBak); break;
                case "UTF8": res = FileHelper.ConvertFileEncoding(path, Encoding.UTF8, null, isBak); break;
                case "UTF8BOM": res = FileHelper.ConvertFileEncoding(path, null, "utf-8 bom", isBak); break;
                case "GBK": res = FileHelper.ConvertFileEncoding(path, null, "gbk", isBak); break;
                case "GB2312": res = FileHelper.ConvertFileEncoding(path, null, "gb2312", isBak); break;
                case "GB18030": res = FileHelper.ConvertFileEncoding(path, null, "gb18030", isBak); break;
            }
            userControl.TextEditor.Text = res;

            string pattern = @"^\[.*\]$";
            lbParagraphIndex.Items.Clear();
            foreach (string line in fileStringList)
            {
                if (Regex.IsMatch(line, pattern))
                {
                    lbParagraphIndex.Items.Add(line);
                }
            }

            this.ShowSuccessTip("转换成功！");

            Mir.Core.utils.ConfigHelper.UpdateAppConfig("FileEncodingStr", encodingStr);
        }

        private void uTF8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertFileEncodingByTxT("UTF8");
        }

        private void uTF32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertFileEncodingByTxT("UTF8BOM");
        }

        private void aSCIIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertFileEncodingByTxT("ASCII");
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertFileEncodingByTxT("GBK");
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ConvertFileEncodingByTxT("系统默认");
        }

        private void gB2312ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertFileEncodingByTxT("GB2312");
        }

        private void gBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertFileEncodingByTxT("GB18030");
        }
        private void cpSize_ValueChanged(object sender, System.Drawing.Color value)
        {
            foreach (TabPage tabPage in tbContent.TabPages)
            {
                if (tabPage.Text == "主页") continue;
                ElementHost elementHost = tabPage.Controls[1].Controls[0] as ElementHost;
                UserControl1 userControl = elementHost.Child as UserControl1;

                string bkColor = value.ToHTML();
                dbInfo.MirBackground = bkColor;
                userControl.TextEditor.Background = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(bkColor));
            }

            Mir.Core.utils.ConfigHelper.UpdateAppConfig("MirBackground", dbInfo.MirBackground);
            this.ShowSuccessTip("代码背景色设置成功！");
        }

        private void uiButton18_Click(object sender, EventArgs e)
        {
            UIMessageBox.ShowInfo("嘿！你还真点啊！");
        }
 
    }
}
