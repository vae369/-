using CsQuery;
using Mir.Core.file;
using Mir.Core.utils;
using Mir.Models.Forum;
using Mir2.Helper;
using MySqlX.XDevAPI.Common;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mir2.Forum
{
    public partial class FrmAutoReplyLBLT : UIPage
    {
        Thread TMain = null, TSearch = null;
        bool IsRun = true, IsLogin = false, IsForumReply = false, IsRunSearchTopic = true, isInterval = false;
        int UserIndex = 0, TopicIndex = 0, IndexCount = 0, ms = 0;
        IForumBase forumBase = new ForumLBHelper();
        List<string> filterUser = new List<string>();
        UserInfo user = null;

        // 取某一页中的所有帖子
        List<Topic> list = new List<Topic>();
        Topic topic = null;

        public FrmAutoReplyLBLT()
        {
            InitializeComponent();
            this.Render();
        }

        private void uiLinkLabel1_Click(object sender, EventArgs e)
        {
            Process.Start("IExplore", ForumConfig.LBDomain + "forum.php");
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
                });
            }
        }

        private void btnGetNew_Click(object sender, EventArgs e)
        {

        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            string item = uiTextBox1.Text;
            lbuser.Items.Add(item);
            FileHelper.AppendLine(ForumConfig.LBUserinfoPath, item);
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

        private void uiButton4_Click(object sender, EventArgs e)
        {
            string item = uiTextBox2.Text;
            lbhuashu.Items.Add(item);
            FileHelper.AppendLine(ForumConfig.LBHuaShuPath, item);
        }

        private void FrmAutoReplyLBLT_Load(object sender, EventArgs e)
        {
            var list1 = FileHelper.ReadTxtReturnList(ForumConfig.LBUserinfoPath);
            foreach (var item in list1)
            {
                lbuser.Items.Add(item);
            }

            var list2 = FileHelper.ReadTxtReturnList(ForumConfig.LBHuaShuPath);
            foreach (var item in list2)
            {
                lbhuashu.Items.Add(item);
            }
            user = new UserInfo();
            filterUser.Add("萝卜论坛");
            filterUser.Add("兂標題");
            filterUser.Add("我爱吃西瓜");
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

        private void Search()
        {
            HttpHelper http = new HttpHelper();
            while (IsRunSearchTopic)
            {
                try
                {

                    ForumConfig.ShowMessage(lbMsg, $"正在获取帖子数据...");
                    // 取帖子总共多少页
                    string url = $"{ForumConfig.LBDomain}forum-2-1.html";
                    string html = http.SendRequest(url, "get", "");
                    if (string.IsNullOrEmpty(html))
                    {
                        Thread.Sleep(5000);
                        continue;
                    }
                    CQ domObjects = CQ.CreateDocument(html);
                    string pageSizeTitle = domObjects.Find("#fd_page_top span").Attr("title").Split(' ')[1];
                    int.TryParse(pageSizeTitle, out int topPage);
                    if (topPage <= 0)// 没有取到停5秒后继续取
                    {
                        Thread.Sleep(5000);
                        continue;
                    }

                    ForumConfig.ShowMessage(lbMsg,  $"获取到 {topPage} 页数据...");

                    int count = 0;
                    for (int i = 1; i <= topPage; i++)
                    {
                        url = $"{ForumConfig.LBDomain}forum-2-{i}.html";
                        html = http.SendRequest(url, "get", "");
                        if (string.IsNullOrEmpty(html))
                        {
                            Thread.Sleep(5000);
                            continue;
                        }
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
                            link = ForumConfig.LBDomain + d.Select("a").Attr("href");


                            // 根据链接进入详情取内容
                            html = http.SendRequest(link, "get", "");
                            if (string.IsNullOrEmpty(html))
                            {
                                continue;
                            }
                            domObjects = CQ.CreateDocument(html);
                            string publisher = domObjects.Find(".authi:first").Text().Trim();
                            if (filterUser.Contains(publisher)) continue;//如果是管理员的帖子就重新抓

                            string name = domObjects.Find("#thread_subject").Text();
                            string afterS = "无售后";
                            if (domObjects.Find(".type-item-content").Count() > 6)
                                afterS = domObjects.Find(".type-item-content")[6].InnerText;
                            string patchSize = "0";
                            if (domObjects.Find(".type-item-content").Count() > 3)
                                patchSize = domObjects.Find(".type-item-content")[3].InnerText;
                            string price = "0";
                            if (domObjects.Find(".type-item-content").Count() > 7)
                                price = domObjects.Find(".type-item-content")[7].InnerText.Replace("萝卜币", "");
                            string vt = "无";
                            if (domObjects.Find(".type-item-content").Count() > 1)
                                vt = domObjects.Find(".type-item-content")[1].InnerText;
                            string engine = "";
                            if (domObjects.Find(".type-item-content").Count() > 0)
                                engine = domObjects.Find(".type-item-content")[0].InnerText;

                            string tattl = domObjects.Find(".buttonright").Text();
                            MatchCollection mc = Regex.Matches(tattl, "下载次数:([0-9]+)");
                            string downCount = "";
                            if (mc.Count > 0)
                                downCount = mc[0].Value.Substring(5);

                            if (string.IsNullOrEmpty(downCount))
                            {
                                downCount = domObjects.Find(".pattl p").Text();
                                string[] downArr = downCount.Replace("\n", "").Split(" ");
                                if (downArr != null)
                                {
                                    price = downArr[5];
                                    if (downArr.Length >= 7)
                                    {
                                        downCount = downArr[4].Replace("售价:", "");
                                    }
                                }
                                else
                                {
                                    downCount = "0";
                                }
                            }

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

        private void RunReply()
        {
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

                // 用户置零
                if (UserIndex >= lbuser.Items.Count)
                    UserIndex = 0;
                // 帖子置零
                if (TopicIndex >= dgvtopic.Rows.Count)
                    TopicIndex = 0;


                // 没有登录进行登录操作
                string[] UserInfo = lbuser.Items[UserIndex].ToString().Split(',');

                this.Invoke((EventHandler)delegate { lbuser.SelectedIndex = UserIndex; });
                try
                {
                    user.UserName = UserInfo[0];
                    user.PassWord = UserInfo[1];
                    user.Uid = forumBase.Login(user.UserName, user.PassWord, "");
                    if (user.Uid > 0)
                    {
                        IsLogin = true;

                        GetUserInfo();

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
                            forumBase.Logout(topic);
                            // 回帖成功
                            ++IndexCount;
                            ++UserIndex;

                            ForumConfig.ShowMessage(lbMsg, $"第 {IndexCount} 次回帖 [{topic.t_Name}] 成功！");
                            if (isInterval) Thread.Sleep(ms * 1000);
                        }
                        else if (result.IndexOf("抱歉，您尚未登录") > 0)
                        {
                            IsLogin = false;
                            Thread.Sleep(15 * 1000);
                        }
                        else if (result.IndexOf("您两次发表间隔少于") > 0)
                        {
                            forumBase.Logout(topic);
                            isInterval = true;
                            MatchCollection mc = Regex.Matches(result, "您两次发表间隔少于 ([0-9]+) 秒");
                            ms = int.Parse(mc[0].Value.Split(' ')[1]);

                            ForumConfig.ShowMessage(lbMsg, $"抱歉，您两次发表间隔少于 {ms} 秒，请稍候再发表...");
                            Thread.Sleep(ms * 1000);

                        }
                        else if (result.IndexOf("指定的主题不存在") >= 0)
                        {

                            ForumConfig.ShowMessage(lbMsg, $"抱歉，指定的主题不存在或已被删除或正在被审核");
                            Thread.Sleep(5 * 1000);
                        }
                    }
                    else
                    {
                        IsLogin = false;

                        ForumConfig.ShowMessage(lbMsg, $"[{UserInfo[0]}] 用户登录失败。");
                        continue;
                    }
                }
                catch (Exception ex)
                {

                    ForumConfig.ShowMessage(lbMsg, "异常：" + ex.Message);
                    forumBase.Logout(topic);
                    IsLogin = false;
                }
            }
        }
        private void GetUserInfo()
        {

            ForumConfig.ShowMessage(lbMsg, $"正在获取用户信息...");
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
