using Microsoft.VisualBasic;
using Mir.Core.file;
using Mir.Models;
using Mir.Models.DTO;
using Mir.ORM.SqlSugar;
using Mir2.Helper;
using Spire.Xls.Core;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Mir2.MonExpRate
{
    public partial class FrmGeneral : UIPage
    {
        List<Monster> listMons = null;
        List<StdItems> listItems = null;
        DBInfo dBInfo = null;
        public FrmGeneral(DataTable Monster, DataTable StdItems, DBInfo db)
        {
            InitializeComponent();
            listMons = Monster.AsEnumerable().Select(s => new Mir.Models.Monster { Name = s.Field<string>("Name") }).ToList();
            listItems = StdItems.AsEnumerable().Select(s => new Mir.Models.StdItems { Name = s.Field<string>("Name") }).ToList();
            this.dBInfo = db;
        }

        private void FrmGeneral_Load(object sender, EventArgs e)
        {
            lbMonster.DataSource = listMons;
            lbMonster.DisplayMember = "Name";
            lbMonster.ValueMember = "Name";

            string itemPath = dBInfo.MirPath + "\\Mir200\\Envir\\咕咕鸡配置\\Group\\Items.txt";
            List<string> list = FileHelper.ReadTxtReturnList(itemPath);
            if (list != null)
                list.ForEach(s => listItems.Insert(0, new StdItems { Name = "[分组]" + s }));

            lbItems.DataSource = listItems;
            lbItems.DisplayMember = "Name";
            lbItems.ValueMember = "Name";

            uiToolTip1.SetToolTip(lbMonster, "双击打开爆率文件，右键进行更多操作");
            uiToolTip1.SetToolTip(lbItems, "双击添加爆率，右键进行更多操作");
            uiRichTextBox1.Text = "\r\n\t介绍：\r\n\r\n\t\t1.文本框内直接输入要查询的内容可以直接查询。\r\n\r\n\t\t2.双击左侧列表可以编辑怪物爆率文件。\r\n\r\n\t\t3.双击右侧列表可添加装备爆率。";

            Tab.ShowToolTips = true;
        }





        private void lbMonster_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string text = lbMonster.ActiveControl.Text;
                string path = dBInfo.MirPath + "\\Mir200\\Envir\\MonItems\\" + text + ".txt";
                BindTabPageData(text, path);
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog2(ex.Message);
            }

        }
        static List<FileInfo> GetAllFilesRecursively(string folder)
        {
            List<FileInfo> files = new List<FileInfo>();

            // 获取指定文件夹中的所有文件
            foreach (string filePath in Directory.GetFiles(folder))
            {
                files.Add(new FileInfo(filePath));
            }

            // 递归获取所有子文件夹中的文件
            foreach (string subDir in Directory.GetDirectories(folder))
            {
                files.AddRange(GetAllFilesRecursively(subDir));
            }

            return files;
        }

        private void BindTabPageData(string tbText, string path, bool isArray = false)
        {
            string folderPath = System.IO.Path.GetDirectoryName(path);
            string newPath = "";

            //找新增的分组信息
            List<FileInfo> allFiles = GetAllFilesRecursively(folderPath).Where(s => s.FullName.Contains("分组")).ToList();
            allFiles.ForEach(f =>
            {
                string fname = f.Name.Replace(f.Extension, "");
                if (fname == tbText)
                {
                    newPath = f.FullName;
                }
            });

            List<string> list = FileHelper.ReadTxtReturnList(path);
            string startRes = list.Where(s => s.Contains("[分组]") && s.Contains("开始")).Select(s => s).FirstOrDefault();
            string endRes = list.Where(s => s.Contains("[分组]") && s.Contains("结束")).Select(s => s).FirstOrDefault();
            int startIndex = list.IndexOf(startRes);
            int endIndex = list.IndexOf(endRes);

            for (int i = endIndex; i >= startIndex; i--)
            {
                if (i < 0)// 如果没有分组就清空
                {
                    break;
                }
                list.RemoveAt(i);
            }

            if (!string.IsNullOrEmpty(newPath))
                list.AddRange(FileHelper.ReadTxtReturnList(newPath));

            string nt = tbText;
            if (tbText.Length > 5)
                nt = tbText.Substring(0, 5) + "...";
            //添加文档
            TabPage tp = new TabPage();
            tp.ToolTipText = tbText;
            tp.Text = nt;
            tp.Name = path;
            tp.BackColor = System.Drawing.Color.White;

            if (!isArray)
            {
                bool isExti = true;
                foreach (TabPage item in Tab.TabPages)
                {
                    if (item.Name == tp.Name) isExti = false;
                }
                if (!isExti) return;
            }


            UIPanel panel = new UIPanel();
            //p2.Resize += UIPanal_Resize;
            panel.AutoSizeMode = AutoSizeMode.GrowOnly;
            panel.Size = new System.Drawing.Size(Tab.Width, Tab.Height);
            panel.Name = "panel";
            panel.Dock = DockStyle.Fill;

            ElementHost elHost = new ElementHost();
            elHost.Dock = DockStyle.Fill;
            elHost.ContextMenuStrip = cmsMain;

            UserControl1 uc = new UserControl1(dBInfo.MirType);
            uc.TextEditor.Text = string.Join("\r\n", list);
            uc.TextEditor.FontSize = 18;
            uc.TextSetting();
            uc.KeyDown += userControl_KeyDown;
            uc.TextEditor.TextArea.Caret.PositionChanged += CaretOnPositionChanged;
            elHost.Child = uc;
            tp.Controls.Add(elHost);

            caretLine = 0;
            plMsg.Text = path;

            Tab.TabPages.Add(tp);
            Tab.SelectedTab = tp;
            //uiToolTip1.SetToolTip(tp, tbText);
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
            if (kea.KeyboardDevice.Modifiers == System.Windows.Input.ModifierKeys.Control && kea.Key == Key.S)
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
                    Tab.SelectedTab.Text = text + "*";
                }
            }
        }
        private void SaveDocument(TabPage tabPage)
        {
            string filePath = tabPage.Name;
            List<string> strings = new List<string>();
            ElementHost elementHost = (ElementHost)tabPage.Controls[0];
            UserControl1 userControl = (UserControl1)elementHost.Child;
            int startIndex = 0;
            int endIndex = 0;
            if (userControl != null)
            {
                string text = userControl.TextEditor.Text.Replace("\r\n", "ら");
                strings = text.Split('ら').ToList();
                if (text.Contains("[分组]") && text.Contains("开始"))
                {
                    startIndex = strings.IndexOf(strings.Where(s => s.Contains("[分组]") && s.Contains("开始")).Select(s => s).First());
                    endIndex = strings.IndexOf(strings.Where(s => s.Contains("[分组]") && s.Contains("结束")).Select(s => s).First());
                }
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

            for (int i = endIndex; i >= startIndex; i--)
            {
                strings.RemoveAt(i);
            }
            //var list = FileHelper.ReadTxtReturnList(newPath);
            //strings.AddRange(list);
            string ptext = tabPage.Text;
            if (ptext.Contains("*"))
                tabPage.Text = ptext.Substring(0, ptext.Length - 1);

            string pathTemp = tabPage.Name.Insert(tabPage.Name.LastIndexOf('.'), "_bak");
            FileHelper.DeleteFile(pathTemp);

            FileHelper.WriteAllText(filePath, string.Join("\r\n", strings));
            plMsg.Text = tabPage.Name + "[已保存]";
            this.ShowSuccessTip("保存成功！");
        }

        private void txtMonster_TextChanged(object sender, EventArgs e)
        {
            var query = listMons.Where(s => s.Name.Contains(txtMonster.Text.Trim())).ToList();

            lbMonster.DataSource = query;
            lbMonster.DisplayMember = "Name";
            lbMonster.ValueMember = "Name";
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

        private void lbItems_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        int index = lbItems.IndexFromPoint(e.Location);
                        if (index != System.Windows.Forms.ListBox.NoMatches)
                        {
                            lbItems.SelectedIndex = index;

                            CreateTreeRightControl(lbItems, e.Location);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 创建右键菜单内容
        /// </summary>
        private void CreateTreeRightControl(UIListBox items, System.Drawing.Point clickPoint)
        {
            uiContextMenuStrip1.Items.Clear();
            ToolStripMenuItem addItems = new ToolStripMenuItem("添加选中的物品", null, tsmAddItems_Clicked);
            addItems.Name = "addItems";
            addItems.ShortcutKeyDisplayString = "Ctrl+A";
            addItems.ShortcutKeys = Keys.Control | Keys.A;

            uiContextMenuStrip1.Items.Add(addItems);
            uiContextMenuStrip1.Show(items, clickPoint);
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

        private void 打开所在文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(Tab.SelectedTab.Name);
            OpenFile(path);
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

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage page = Tab.SelectedTab;
            Tab.TabPages.Remove(page);
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

        private void 系统默认ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertFileEncodingByTxT("系统默认");
        }


        private void ConvertFileEncodingByTxT(string encodingStr)
        {
            bool isBak = false;
            if (this.ShowAskDialog2("提示", "是否需要备份当前文件进行转换?\n点击确定则对备份转换，点击取消则对当前文件转换。\n(提醒:对当前文件转换如果错误是不能返回撤销操作的)\n保存后会自动删除备份文件(带_bak的文件)"))
            {
                isBak = true;
            }
            TabPage tabPage = Tab.SelectedTab;
            ElementHost elementHost = tabPage.Controls[0] as ElementHost;
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


            this.ShowSuccessTip("转换成功！");
            Mir.Core.utils.ConfigHelper.UpdateAppConfig("FileEncodingStr", encodingStr);
        }

        private void aSCIIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertFileEncodingByTxT("ASCII");
        }

        private void uNICODEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertFileEncodingByTxT("UNICODE");
        }

        private void uTF8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertFileEncodingByTxT("UTF8");
        }

        private void uTF8BOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertFileEncodingByTxT("UTF8BOM");
        }

        private void gBKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertFileEncodingByTxT("GBK");
        }

        private void gB2312ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertFileEncodingByTxT("GB2312");
        }

        private void gB18030ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertFileEncodingByTxT("GB18030");
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
    }
}
