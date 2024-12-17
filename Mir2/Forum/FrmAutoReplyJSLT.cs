using CsQuery;
using Mir.Core.file;
using Mir.Core.image;
using Mir.Core.utils;
using Mir.Models.Forum;
using Mir2.Helper;
using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Mir2.Forum
{
    public partial class FrmAutoReplyJSLT : UIPage
    {
        Thread TMain = null, TSearch = null;
        bool IsRun = true, IsLogin = false, IsForumReply = false, IsRunSearchTopic = true, isInterval = false;
        int UserIndex = 0, TopicIndex = 0, IndexCount = 0, ms = 0, topicCount = 20;
        IForumBase forumBase = new ForumQJHelper();
        List<string> filterUser = new List<string>();
        UserInfo user = null;
        Dictionary<string, int> topicDic = new Dictionary<string, int>();
        // 取某一页中的所有帖子
        List<Topic> list = new List<Topic>();
        Topic topic = null;

        public FrmAutoReplyJSLT()
        {
            InitializeComponent();
            this.Render();
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            ofdMain.Filter = "文本文件(*.txt)|*.txt";
            ofdMain.Multiselect = false;
            ofdMain.Title = "请选择帐号文本";
            if (ofdMain.ShowDialog() == DialogResult.OK)
            {
                lbuser.Items.Clear();
                string fileName = ofdMain.FileName;
                var list = FileHelper.ReadTxtReturnList(fileName);
                list.ForEach(item =>
                {
                    lbuser.Items.Add(item);
                    string username = item.Split(',')[0];
                    if (!topicDic.ContainsKey(username))
                        topicDic.Add(username, 0);
                });
            }
        }

        private void FrmAutoReplyJSLT_Load(object sender, EventArgs e)
        {
            var list1 = FileHelper.ReadTxtReturnList(ForumConfig.QJUserinfoPath);
            foreach (var item in list1)
            {
                lbuser.Items.Add(item);
                string username = item.Split(',')[0];
                if (!topicDic.ContainsKey(username))
                    topicDic.Add(username, 0);
            }

            var list2 = FileHelper.ReadTxtReturnList(ForumConfig.QJHuaShuPath);
            foreach (var item in list2)
            {
                lbhuashu.Items.Add(item);
            }
            filterUser.Add("admin");
            filterUser.Add("精神小伙");
            user = new UserInfo();

        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            ofdMain.Filter = "文本文件(*.txt)|*.txt";
            ofdMain.Multiselect = false;
            ofdMain.Title = "请选择话术文本";
            if (ofdMain.ShowDialog() == DialogResult.OK)
            {
                lbhuashu.Items.Clear();
                string fileName = ofdMain.FileName;
                var list = FileHelper.ReadTxtReturnList(fileName);
                list.ForEach(item =>
                {
                    lbhuashu.Items.Add(item);
                });
            }
        }

        private void uiLinkLabel1_Click(object sender, EventArgs e)
        {
            Process.Start("IExplore", ForumConfig.QJDomain + "forum.php");
        }

        private void btnGetNew_Click(object sender, EventArgs e)
        {
            if (btnGetNew.Text == "点击获取帖子")
            {
                btnGetNew.Text = "停止获取帖子";
                TSearch = new Thread(new ThreadStart(Search));
                TSearch.IsBackground = true;
                TSearch.Start();
            }
            else
            {
                IsRunSearchTopic = false;
                btnGetNew.Text = "点击获取帖子";
                try
                {
                    TSearch.Abort();
                }
                catch (Exception ex)
                {
                    ForumConfig.ShowMessage(lbMsg, ex.Message);
                }
            }

        }

        private void Search()
        {
            HttpHelper http = new HttpHelper();
            while (IsRunSearchTopic)
            {
                try
                {

                    ForumConfig.ShowMessage(lbMsg, $"正在获取帖子数据...");
                    // 取帖子总共多少页
                    string url = $"{ForumConfig.QJDomain}forum-2-1.html";
                    string html = http.SendRequest(url, "get", "");
                    CQ domObjects = CQ.CreateDocument(html);
                    string pageSizeTitle = domObjects.Find("#fd_page_top span").Attr("title").Split(' ')[1];
                    int.TryParse(pageSizeTitle, out int topPage);
                    if (topPage <= 0)// 没有取到停5秒后继续取
                    {
                        Thread.Sleep(5000);
                        continue;
                    }

                    ForumConfig.ShowMessage(lbMsg, $"获取到 {topPage} 页数据...");
                    int count = 0;
                    for (int i = 1; i <= topPage; i++)
                    {
                        url = $"{ForumConfig.QJDomain}forum-2-{i}.html";
                        html = http.SendRequest(url, "get", "");
                        domObjects = CQ.CreateDocument(html);

                        ForumConfig.ShowMessage(lbMsg, $"当前在取获取第 {i}/{topPage} 页...");
                        var tBody = domObjects.Select("#threadlisttableid tbody");
                        foreach (var item in tBody)
                        {
                            // 取帖子id 和链接
                            string t_Id = item.Id;
                            string[] t = t_Id.Split('_');
                            if (string.IsNullOrEmpty(t_Id)) continue;
                            if (t.Length <= 1) continue;
                            t_Id = t[1];
                            int tid = int.Parse(t_Id);
                            int iCount = list.Where(s => s.t_Id == tid).Count();
                            if (iCount > 0) continue;

                            string link = "";
                            CQ d = CQ.Create(item);
                            link = ForumConfig.QJDomain + d.Select("a").Attr("href");


                            // 根据链接进入详情取内容
                            html = http.SendRequest(link, "get", "");
                            domObjects = CQ.CreateDocument(html);
                            string publisher = domObjects.Find(".authi:first").Text().Trim();
                            if (filterUser.Contains(publisher)) continue;//如果是管理员的帖子就重新抓

                            string afterS = "无售后";
                            string patchSize = "0";
                            string price = "0";
                            string vt = "空";
                            var f = domObjects.Select(".pcb font");
                            if (f.Length >= 5)
                            {
                                if (!string.IsNullOrEmpty(f[1].InnerText.Trim()))
                                    afterS = f[1].InnerText.Trim();
                                if (!string.IsNullOrEmpty(f[3].InnerText.Trim()))
                                    patchSize = f[3].InnerText.Trim();
                                if (!string.IsNullOrEmpty(f[5].InnerText.Trim()))
                                    price = f[5].InnerText.Trim();
                                if (!string.IsNullOrEmpty(f[7].InnerText.Trim()))
                                    vt = f[7].InnerText.Trim();
                            }

                            string name = domObjects.Find("#thread_subject").Text();
                            string[] tattl = domObjects.Find(".tattl .attnm").Next().Next().Next().Text().Split(' ');
                            string downCount = tattl[tattl.Length - 1];
                            if (string.IsNullOrEmpty(downCount))
                            {
                                downCount = domObjects.Find("span .xg1").Text();
                                string[] downArr = downCount.Split(" ");
                                if (downArr != null)
                                {
                                    if (downArr.Length == 7)
                                    {
                                        downCount = downArr[3].TrimEnd(',');
                                    }
                                }
                                else
                                {
                                    downCount = "0";
                                }
                            }
                            string engine = domObjects.Find(".ts a").Text();
                            string publishTime = domObjects.Find(".authi em:first").Text();
                            count++;
                            topic = new Topic
                            {
                                Id = count,
                                t_Id = tid,
                                t_Link = link,
                                t_Name = name,
                                t_AfterSales = afterS,
                                t_PatchSize = patchSize,
                                t_Publisher = publisher,
                                t_Price = price,
                                t_VersionType = vt,
                                t_DownCount = downCount,
                                t_Engine = engine,
                                t_PublishTime = publishTime,
                            };

                            list.Add(topic);

                            // 创建一个新的行对象
                            DataGridViewRow row = new DataGridViewRow();
                            // 添加单元格数据
                            row.CreateCells(dgvtopic);
                            row.Cells[0].Value = topic.Id;
                            row.Cells[1].Value = topic.t_Id;
                            row.Cells[2].Value = topic.t_Name;
                            row.Cells[3].Value = topic.t_Publisher;
                            row.Cells[4].Value = topic.t_Engine;
                            row.Cells[5].Value = topic.t_AfterSales;
                            row.Cells[6].Value = topic.t_PatchSize;
                            row.Cells[7].Value = topic.t_Price;
                            row.Cells[8].Value = topic.t_VersionType;
                            row.Cells[9].Value = topic.t_DownCount;
                            row.Cells[10].Value = topic.t_Link;
                            row.Cells[11].Value = topic.t_PublishTime;
                            this.Invoke((EventHandler)delegate { dgvtopic.Rows.Add(row); });

                            ForumConfig.ShowMessage(lbMsg, $"当前在取获取第 {i}/{topPage} 页，已获取 {count} 条...");
                        }
                    }

                    //this.Invoke((EventHandler)delegate { dgvtopic.DataSource = list; });

                    IsRunSearchTopic = false;
                }
                catch (Exception ex)
                {
                    ForumConfig.ShowMessage(lbMsg, ex.Message);
                }

            }
        }

        private void uiButton4_Click(object sender, EventArgs e)
        {
            string item = uiTextBox2.Text;
            lbhuashu.Items.Add(item);
            FileHelper.AppendLine(ForumConfig.QJHuaShuPath, item);
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            string item = uiTextBox1.Text;
            lbuser.Items.Add(item);
            string username = item.Split(',')[0];
            if (!topicDic.ContainsKey(username))
                topicDic.Add(username, 0);
            FileHelper.AppendLine(ForumConfig.QJUserinfoPath, item);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "开始")
            {
                btnStart.Text = "停止";
                TMain = new Thread(new ThreadStart(RunReply));
                TMain.IsBackground = true;
                TMain.Start();

                TSearch = new Thread(new ThreadStart(Search));
                TSearch.IsBackground = true;
                TSearch.Start();
            }
            else
            {
                IsRun = false;
                IsRunSearchTopic = false;
                btnStart.Text = "开始";
                try
                {
                    TMain.Abort();
                    TSearch.Abort();
                }
                catch (Exception ex)
                {
                    ForumConfig.ShowMessage(lbMsg, ex.Message);
                }
            }
        }

        private void RunReply()
        {
            bool isApplication = true;
            DateTime nextDateTime = DateTime.Now;
            while (IsRun)
            {
                // 用户
                if (lbuser.Items.Count <= 0)
                {
                    this.Invoke((EventHandler)delegate { this.ShowErrorDialog2("请导入登录帐号!"); });
                    Thread.Sleep(5000);
                    continue;
                }
                if (rb1.Checked)
                {
                    // 话术
                    if (lbhuashu.Items.Count <= 0)
                    {
                        this.Invoke((EventHandler)delegate { this.ShowErrorDialog2("请导入回帖的话术!"); });
                        Thread.Sleep(5000);
                        continue;
                    }
                }
                else
                {
                    IsForumReply = true;
                }

                if (dgvtopic.Rows.Count <= 0)
                {
                    ForumConfig.ShowMessage(lbMsg, "请先获取帖子!");
                    Thread.Sleep(5000);
                    continue;
                }

                // 帖子置零
                if (TopicIndex >= dgvtopic.Rows.Count)
                    TopicIndex = 0;

                while (isApplication)
                {
                    // 用户置零
                    if (UserIndex == lbuser.Items.Count)
                    {
                        int hours = 0, minutes = 0, seconds = 0;
                        int tc = topicDic.Where(s => s.Value >= topicCount).Count();
                        if (tc == topicDic.Count)
                        {
                            //登录操作完成后关闭请求，设置一个时间到第二天的随机时间放开请求
                            isApplication = false;
                            nextDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).AddDays(1);

                            Random rndHours = new Random();//生成随机时间早上8点 - 晚上8点
                            hours = rndHours.Next(8, 20);
                            Random rndMinutes = new Random();
                            minutes = rndMinutes.Next(1, 60);
                            Random rndSeconds = new Random();
                            seconds = rndSeconds.Next(1, 60);

                            nextDateTime = nextDateTime.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds);
                            break;
                        }
                    }

                    // 没有登录进行登录操作
                    string[] UserInfo = lbuser.Items[UserIndex].ToString().Split(',');

                    int c = topicDic[UserInfo[0]];
                    if (c > topicCount)
                    {
                        ForumConfig.ShowMessage(lbMsg, $"[{UserInfo[0]}] 帐号已刷满，换下一个帐号！");
                        ++UserIndex;
                        continue;
                    }

                    this.Invoke((EventHandler)delegate { lbuser.SelectedIndex = UserIndex; });

                    try
                    {
                        if (IsLogin)
                        {
                            // 随即取帖子
                            Random rnd = new Random();
                            TopicIndex = rnd.Next(list.Count - 1);
                            topic = list[TopicIndex];

                            int hs = rnd.Next(lbhuashu.Count - 1);

                            string message = lbhuashu.Items[hs].ToString();
                            this.Invoke((EventHandler)delegate
                            {
                                dgvtopic.SelectedIndex = TopicIndex;
                                lbhuashu.SelectedIndex = hs;
                            });

                            // 回帖
                            string result = forumBase.PublishReply(topic, user, message, IsForumReply);
                            if (result.IndexOf("非常感谢") > 0)
                            {
                                GetUserInfo();

                                // 回帖成功
                                if (IndexCount > topicCount)
                                {
                                    ForumConfig.ShowMessage(lbMsg, $"第 {IndexCount} 次回帖 [{topic.t_Name}] 成功！换下一个帐号");
                                    ++UserIndex;
                                    IsLogin = false;
                                    IndexCount = 0;
                                    continue;
                                }
                                else
                                {
                                    IndexCount++;
                                    topicDic[user.UserName] = IndexCount;
                                    ForumConfig.ShowMessage(lbMsg, $"第 {IndexCount} 次回帖 [{topic.t_Name}] 成功！");
                                }

                                if (isInterval) Thread.Sleep(ms * 1000);
                                //forumBase.Logout(topic);
                            }
                            else if (result.IndexOf("抱歉，您尚未登录") > 0)
                            {
                                ++UserIndex;
                                IsLogin = false;
                                IndexCount = 0;
                                Thread.Sleep(15 * 1000);
                                continue;
                            }
                            else if (result.IndexOf("您两次发表间隔少于") > 0)
                            {
                                isInterval = true;
                                MatchCollection mc = Regex.Matches(result, "您两次发表间隔少于 ([0-9]+) 秒");
                                ms = int.Parse(mc[0].Value.Split(' ')[1]);
                                ForumConfig.ShowMessage(lbMsg, $"抱歉，您两次发表间隔少于 {ms} 秒，请稍候再发表...");
                                Thread.Sleep(ms * 1000);
                            }
                            else if (result.IndexOf("您所在的用户组每小时限制发回帖") > 0)
                            {
                                ++UserIndex;
                                IndexCount = 0;
                                IsLogin = false;
                                MatchCollection mc = Regex.Matches(result, "您所在的用户组每小时限制发回帖 ([0-9]+) 个");
                                int xz = int.Parse(mc[0].Value.Split(' ')[1]);
                                ForumConfig.ShowMessage(lbMsg, $"[{user.UserName}] 抱歉，您所在的用户组每小时限制发回帖 {xz} 个，请稍候再发表!");
                                topicDic[user.UserName] = xz;
                            }
                        }
                        else
                        {
                            var map = forumBase.GetVerCode();

                            //Bitmap newMap = Mir.Core.image.Cracker.BinaryThreshold(map,128);
                            //string npath = Application.StartupPath + "\\images\\yzm2.png";
                            //map.Save(npath, System.Drawing.Imaging.ImageFormat.Png);

                            FrmForumYZM frmForumYZM = new FrmForumYZM(forumBase, map);
                            DialogResult dr = frmForumYZM.ShowDialog();
                            if (dr != DialogResult.OK)
                            {
                                Thread.Sleep(5000);
                                continue;
                            }
                            user = new UserInfo
                            {
                                UserName = UserInfo[0],
                                PassWord = UserInfo[1],
                            };
                            user.Uid = forumBase.Login(user.UserName, user.PassWord, frmForumYZM.YZM);
                            if (user.Uid > 0)
                            {
                                IsLogin = true;

                                GetUserInfo();
                            }
                            else
                            {
                                IsLogin = false;
                                ForumConfig.ShowMessage(lbMsg, $"[{UserInfo[0]}] 用户登录失败。");
                                continue;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        ForumConfig.ShowMessage(lbMsg, "异常：" + ex.Message);
                        forumBase.Logout(topic);
                        IsLogin = false;
                    }
                }

                ForumConfig.ShowMessage(lbMsg, $"今天都已刷完，等待明天继续，下次刷帖时间" + nextDateTime.ToString());
                int tnd = new Random().Next(3333, 9999);
                Thread.Sleep(tnd);

                int res = DateTime.Compare(DateTime.Now, nextDateTime);//如果当前时间大于第二天的随机时间就放开请求
                if (res > 0)
                {
                    isApplication = true;

                    foreach (var key in topicDic.Keys.ToList())
                    {
                        topicDic[key] = 0;
                    }
                }
            }


        }

        private void GetUserInfo()
        {

            ForumConfig.ShowMessage(lbMsg, "正在获取用户信息...");
            // 登录后取用户信息
            user = forumBase.GetUserInfo(user);
            this.Invoke((EventHandler)delegate
            {
                lbUID.Text = user.Uid.ToString();
                lbJF.Text = user.jf.ToString();
                lbYB.Text = user.yb.ToString();
                lbJB.Text = user.jb.ToString();
                lbHDB.Text = user.hdb.ToString();
                lbtCount.Text = user.ReplyCount.ToString();
                lbRcount.Text = user.ThreadCount.ToString();
            });

            // 自动签到
            if (cbsignin.Checked)
            {
                if (forumBase.AutoSingIn(user))
                {
                    this.Invoke((EventHandler)delegate
                    {
                        lbQD.ForeColor = Color.Green;
                        lbQD.Text = "已签到";
                    });
                }
            }
        }
    }
}
