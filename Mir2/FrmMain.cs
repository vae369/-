using Mir.Core.file;
using Mir.Core.LegendEngine;
using Mir.Core.utils;
using Mir.Models.DTO;
using Mir2.db;
using Mir2.EquipmentDesc;
using Mir2.Forum;
using Mir2.Helper;
using Mir2.MonExpRate;
using Mir2.Script;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Mir2
{
    public partial class FrmMain : UIHeaderMainFrame
    {
        public DBInfo db = null;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            BindData();
            BindExtendMenuControls();
            BindNavBarControls();
            BindHeaderControls();

            StyleManager.Style = GameStyle.BindStyle();
            this.ShowInfoNotifier("请先备份服务端再进行操作，丢失任何数据自己负责。", false, 5000);
        }
        /// <summary>
        /// 加载Config默认数据
        /// </summary>
        private void BindData()
        {
            if (db == null)
            {
                db = new DBInfo()
                {
                    MirPath = ConfigurationManager.AppSettings["MirPath"],
                    DataBaseAddr = ConfigurationManager.AppSettings["DataBaseAddr"],
                    DataBaseName = ConfigurationManager.AppSettings["DataBaseName"],
                    DataBasePassWord = ConfigurationManager.AppSettings["DataBasePassWord"],
                    DataBaseUserName = ConfigurationManager.AppSettings["DataBaseUserName"],
                    DataBaseType = ConfigurationManager.AppSettings["DataBaseType"],
                    MirType = ConfigurationManager.AppSettings["MirType"],
                    DataFilePath = ConfigurationManager.AppSettings["DataFilePath"],
                    StyleManager = ConfigurationManager.AppSettings["StyleManager"],
                    MirBackground = ConfigurationManager.AppSettings["MirBackground"],
                    MirDBType = "默认"
                };
            }

            //如果没有设置过数据库会先弹出数据库服务器设置窗体
            if (string.IsNullOrEmpty(db.DataBaseType))
            {
                FrmDBConnection fdbCon = new FrmDBConnection();
                fdbCon.Owner = this;
                this.Hide();
                var dr = fdbCon.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    db = new DBInfo()
                    {
                        MirPath = ConfigurationManager.AppSettings["MirPath"],
                        DataBaseAddr = ConfigurationManager.AppSettings["DataBaseAddr"],
                        DataBaseName = ConfigurationManager.AppSettings["DataBaseName"],
                        DataBasePassWord = ConfigurationManager.AppSettings["DataBasePassWord"],
                        DataBaseUserName = ConfigurationManager.AppSettings["DataBaseUserName"],
                        DataBaseType = ConfigurationManager.AppSettings["DataBaseType"],
                        MirType = ConfigurationManager.AppSettings["MirType"],
                        DataFilePath = ConfigurationManager.AppSettings["DataFilePath"],
                        StyleManager = ConfigurationManager.AppSettings["StyleManager"],
                        MirBackground = ConfigurationManager.AppSettings["MirBackground"],
                        MirDBType = "默认"
                    };
                }
                else
                {
                    Environment.Exit(0);
                    Process.GetCurrentProcess().Kill();
                }
            }

            CompletionData.MirType = db.MirType;
        }

        /// <summary>
        /// 加载主菜单
        /// </summary>
        private void BindHeaderControls()
        {
            //设置关联
            Header.TabControl = MainTabControl;

            //增加页面到Main
            FrmStMain frmStMain = new FrmStMain(db, this);
            frmStMain.Render();

            FrmMerMain frmMerMain = new FrmMerMain(db);
            frmMerMain.Render();

            FrmDBMain frmDBMain = new FrmDBMain(db);
            frmDBMain.Render();

            AddPage(frmStMain, 1001);
            AddPage(frmMerMain, 1002);
            AddPage(frmDBMain, 1003);
            AddPage(new FrmForumMain(), 1004);

            //设置Header节点索引
            Header.SetNodePageIndex(Header.Nodes[0], 1001);
            Header.SetNodePageIndex(Header.Nodes[1], 1002);
            Header.SetNodePageIndex(Header.Nodes[2], 1003);
            Header.SetNodePageIndex(Header.Nodes[3], 1004);

            //显示默认界面
            Header.SelectedIndex = 0;
        }

        /// <summary>
        /// 加载导航菜单
        /// </summary>
        private void BindNavBarControls()
        {
            int pageIndex = 1000;
            Header.Nodes.Add("脚本");
            Header.Nodes.Add("爆率调整");
            Header.Nodes.Add("数据库");
            Header.Nodes.Add("论坛辅助");

            Header.SetNodePageIndex(Header.Nodes[0], pageIndex);
            Header.SetNodeSymbol(Header.Nodes[0], 561296);

            pageIndex = 2000;
            Header.SetNodePageIndex(Header.Nodes[1], pageIndex);
            Header.SetNodeSymbol(Header.Nodes[1], 357443);

            pageIndex = 3000;
            Header.SetNodePageIndex(Header.Nodes[2], pageIndex);
            Header.SetNodeSymbol(Header.Nodes[2], 361888);

            pageIndex = 4000;
            Header.SetNodePageIndex(Header.Nodes[3], pageIndex);
            Header.SetNodeSymbol(Header.Nodes[3], 358701);
        }

        /// <summary>
        /// 加载右上角下拉按钮
        /// </summary>
        private void BindExtendMenuControls()
        {
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem.Text = "主题";
            toolStripMenuItem.Name = "lbStyle";
            var styles = UIStyles.PopularStyles();
            foreach (UIStyle style in styles)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(style.DisplayText()) { Tag = style };
                item.Click += Item_Click;
                toolStripMenuItem.DropDownItems.Add(item);
            }

            uiContextMenuStrip.Items.Add(toolStripMenuItem);

            toolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem.Text = "软件更新";
            toolStripMenuItem.Name = "lbUpdate";
            toolStripMenuItem.Click += ExtendMenu_Click;
            uiContextMenuStrip.Items.Add(toolStripMenuItem);

            toolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem.Text = "服务器连接设置";
            toolStripMenuItem.Name = "lbSysSet";
            toolStripMenuItem.Click += ExtendMenu_Click;
            uiContextMenuStrip.Items.Add(toolStripMenuItem);

            toolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem.Text = "建议/反馈";
            toolStripMenuItem.Name = "lbSuggestions_Feedback";
            toolStripMenuItem.Click += ExtendMenu_Click;
            uiContextMenuStrip.Items.Add(toolStripMenuItem);

            toolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem.Text = "关于";
            toolStripMenuItem.Name = "lbAbout";
            toolStripMenuItem.Click += ExtendMenu_Click;
            uiContextMenuStrip.Items.Add(toolStripMenuItem);

            toolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem.Text = "退出";
            toolStripMenuItem.Name = "lbClose";
            toolStripMenuItem.Click += ExtendMenu_Click;
            uiContextMenuStrip.Items.Add(toolStripMenuItem);
            ExtendMenu = uiContextMenuStrip;
        }

        private void Item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.Tag != null && item.Tag is UIStyle)
            {
                UIStyle style = (UIStyle)item.Tag;
                StyleManager.Style = style;
                Mir.Core.utils.ConfigHelper.UpdateAppConfig("StyleManager", item.Tag.ToString());
            }
        }
        private void ExtendMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            switch (item.Name)
            {
                case "lbSuggestions_Feedback": UIMessageBox.Show("暂时不能反馈哦!", "建议/反馈", Style, UIMessageBoxButtons.OK, false); break;
                case "lbUpdate": UpdateSoft(); break;
                case "lbSysSet":
                    FrmDBConnection fdb = new FrmDBConnection();
                    DialogResult dr = fdb.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        ResetProgram();
                    }
                    break;
                case "lbAbout":
                    string version = FileVersionInfo.GetVersionInfo(System.Windows.Forms.Application.StartupPath + "\\Mir2.exe").FileVersion;
                    UIMessageBox.Show($"Q群:977049285\n\n软件中所有功能免费\n\n使用仅供学习交流之用 - 禁止用于商业用途\n\n版本 {version}", "关于", Style, UIMessageBoxButtons.OK, false);
                    break;
                case "lbClose": System.Windows.Forms.Application.Exit(); Close(); break;
            }
        }

        private void ResetProgram()
        {
            Application.ExitThread();
            Thread thtmp = new Thread(new ParameterizedThreadStart(run));
            object appName = Application.ExecutablePath;
            Thread.Sleep(1);
            thtmp.Start(appName);
        }
        private void run(Object obj)
        {
            Process ps = new Process();
            ps.StartInfo.FileName = obj.ToString();
            ps.Start();
        }

        /// <summary>
        /// 软件更新
        /// </summary>
        private void UpdateSoft()
        {
            try
            {
                //获取本地版本号，与服务器版本号对比，如果低就更新
                string ServerFileList = ConfigurationManager.AppSettings["ServerFileList"];
                string localPath = System.Windows.Forms.Application.StartupPath;
                string filePath = localPath + "\\File\\FileList.json";
                HttpHelper http = new HttpHelper();
                http.GetStream(ServerFileList, localPath + "\\File\\", "FileList.json");//下载服务器版本文件信息
                string json = FileHelper.ReadTxt(filePath);
                json = DESHelper.DecryptDes(json, DESKey.Key, DESKey.IV);//解析服务器版本号及更新文件信息
                UpdateFile updateFile = JsonHelper.JsonDeserialize<UpdateFile>(json);

                string version = FileVersionInfo.GetVersionInfo(localPath + "\\Mir2.exe").FileVersion;//获取本机版本号信息

                Version v1 = new Version(updateFile.Version);//服务器版本号
                Version v2 = new Version(version);//本地版本号

                if (v1 == v2)
                {
                    this.ShowInfoDialog2("当前是最新版本！");
                }
                if (v1 > v2)
                {
                    if (this.ShowAskDialog2("提示", $"发现新的版本：{updateFile.Version} 是否更新?"))
                    {
                        string path = System.Windows.Forms.Application.StartupPath + "\\Mir.Update.exe";
                        string pwd = "mir.update.654321";
                        string[] parameter = { pwd };
                        bool startResult = StartProcess(path, parameter);
                        if (startResult)
                            System.Environment.Exit(0);//退出软件启动器
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorTip(ex.Message);
            }
        }

        /// <summary>
        /// 启动一个软件，并传入参数
        /// </summary>
        /// <param name="runFilePath"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool StartProcess(string runFilePath, params string[] args)
        {
            string s = "";
            foreach (string arg in args)
            {
                s = s + arg + " ";
            }
            s = s.Trim();
            Process process = new Process();//创建进程对象    
            ProcessStartInfo startInfo = new ProcessStartInfo(runFilePath, s); // 括号里是(程序名,参数)
            process.StartInfo = startInfo;
            process.Start();
            return true;
        }

    }
}
