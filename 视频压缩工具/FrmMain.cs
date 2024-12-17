using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 视频压缩工具
{
    public partial class FrmMain : Form
    {
        #region 成员变量
        bool flag = true;// 控制器开关
        //Task[] rT; // 子线程，分配多少个线程进行执行压缩任务
        Thread t1 = null;//主线程，启动和停止
        Thread TSearch = null;//查询进度
        object lockObj = new object();
        int ItemIndex = 0;
        List<ZipEntity> Ziplist = null;//全局对象集合
        int TimeCount = 0;

        #endregion


        public FrmMain()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSelectPath.Text.Trim()) && !string.IsNullOrEmpty(txtFileExtension.Text.Trim()))
            {
                if (button2.Text == "开始")
                {
                    RecordMsg("压缩开始!");
                    button2.Text = "停止";
                    flag = true;

                    //DelayTask.Add(DateTime.Now.AddMilliseconds(1000), ZipVideo);
                    //DelayTask.Add(DateTime.Now.AddMilliseconds(3000), Run);
                    TSearch = new Thread(new ThreadStart(ZipVideo));
                    TSearch.IsBackground = true;
                    TSearch.Start();

                    t1 = new Thread(new ThreadStart(Run));
                    t1.IsBackground = true;
                    t1.Start();

                }
                else
                {
                    button2.Text = "开始";
                    flag = false;
                    RecordMsg("压缩停止!");
                    t1.Abort();
                    TSearch.Abort();
                }

            }
            else
            {
                BindData();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Ziplist = null;
                OperateData.FileItems = null;
                BindData();
            }
            catch (Exception ex)
            {
                RecordMsg(ex.Message);
            }

        }

        private void BindData()
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.Description = "请选择文件路径";
            path.SelectedPath = WinConfig.GetConfigValue("SelectedPath");
            path.ShowDialog();
            txtSelectPath.Text = path.SelectedPath;
            if (WinConfig.SetConfigValue("SelectedPath", path.SelectedPath))
                InitVideo();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            txtSelectPath.Text = WinConfig.GetConfigValue("SelectedPath");
            InitVideo();
        }

        /// <summary>
        /// 消息显示
        /// </summary>
        /// <param name="msg"></param>
        private void RecordMsg(string msg)
        {
            this.Invoke((EventHandler)delegate { lbmsg.Text = string.Format("{0}:{1} 线程ID:{2}", DateTime.Now, msg, Thread.CurrentThread.ManagedThreadId); });
        }


        private void ZipVideo()
        {
            try
            {
                while (flag)
                {
                    if (OperateData.FileItems != null && OperateData.FileItems.Count > 0)
                    {
                        InitVideo();
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                flag = false;
                RecordMsg(ex.Message);
            }
        }


        /// <summary>
        /// 压缩视频
        /// </summary>
        private void Run()
        {

            try
            {

                while (flag)
                {
                    if (!Ziplist[0].IsOcc)
                    {
                        Ziplist[0].ZipFile();
                    }

                    Thread.Sleep(200);
                    Ziplist.Remove(Ziplist[0]);
                    ItemIndex++;
                    TimeCount = 0;
                }
            }
            catch (Exception ex)
            {
                flag = false;
                throw ex;
            }

        }


        /// <summary>
        /// 绑定视频文件
        /// </summary>
        private void InitVideo()
        {
            try
            {
                if (Ziplist != null && Ziplist.Count > 0)
                {
                    if (listView1.Items.Count > ItemIndex)
                    {
                        this.Invoke((EventHandler)delegate
                        {
                            listView1.Items[ItemIndex].SubItems[2].Text = OperateData.GetTime(TimeCount++);
                            listView1.Items[ItemIndex].SubItems[3].Text = OperateData.GetTime(Ziplist[0].EstTime - TimeCount);
                            listView1.Items[ItemIndex].SubItems[5].Text = Thread.CurrentThread.ManagedThreadId.ToString();
                            listView1.Items[ItemIndex].SubItems[6].Text = Ziplist[0].ZipProgress + "%";
                        });
                    }

                }
                else
                {
                    string SelectPath = txtSelectPath.Text.Trim();
                    if (!string.IsNullOrEmpty(SelectPath))
                    {
                        string[] paths = Directory.GetFiles(SelectPath);

                        if (paths != null && paths.Length > 0)
                        {
                            #region 将文件加载到字典集合中
                            if (OperateData.FileItems == null)
                                OperateData.FileItems = new Dictionary<string, ZipEntity>();

                            ZipEntity entity = null;
                            FileInfo fileInfo = null;

                            // 过滤视频格式
                            for (int i = 0; i < paths.Length; i++)
                            {
                                string p = paths[i].Substring(paths[i].LastIndexOf('.') + 1);
                                if (!txtFileExtension.Text.Contains(p))
                                    paths[i] = "";
                            }

                            foreach (string path in paths)
                            {
                                if (!string.IsNullOrEmpty(path))
                                {
                                    entity = new ZipEntity();
                                    fileInfo = new FileInfo(path);

                                    FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(path);
                                    entity.FileName = versionInfo.FileName.Substring(versionInfo.FileName.LastIndexOf("\\") + 1);
                                    entity.FilePath = versionInfo.FileName;
                                    entity.ZipSize = fileInfo.Length;
                                    entity.EstTime = fileInfo.Length / long.Parse(txtYSSize.Text);
                                    entity.FileExtensions = txtFileExtension.Text.Trim();
                                    entity.ZipFileType = rbZip.Checked ? rbZip.Text : rbRar.Text;
                                    entity.YSSize = int.Parse(txtYSSize.Text);
                                    if (!OperateData.FileItems.ContainsKey(entity.FileName))
                                        OperateData.FileItems.Add(entity.FileName, entity);
                                }

                            }

                            #endregion
                            //this.Invoke((EventHandler)delegate
                            //{
                            this.listView1.Items.Clear();
                            //});

                            #region 从字典集合中将数据加载到窗体界面上

                            ListViewItem listItem = null;
                            Ziplist = OperateData.FileItems.Values.OrderByDescending(s => s.ZipSize).ToList();
                            foreach (var item in Ziplist)
                            {
                                listItem = new ListViewItem();

                                listItem.Text = item.FileName;
                                listItem.SubItems.Add(OperateData.GetFileSize(item.ZipSize));
                                listItem.SubItems.Add(item.ElaTime);
                                listItem.SubItems.Add(item.RemTime);
                                listItem.SubItems.Add(OperateData.GetTime(item.EstTime));
                                listItem.SubItems.Add("等待压缩...");
                                listItem.SubItems.Add("暂无");

                                //this.Invoke((EventHandler)delegate
                                //{
                                listView1.Items.Add(listItem);
                                //});
                            }
                            #endregion

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RecordMsg(ex.Message);
            }
        }

    }
}
