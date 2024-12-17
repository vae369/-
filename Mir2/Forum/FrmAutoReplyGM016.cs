using CsQuery;
using Mir.Core.file;
using Mir.Core.utils;
using Mir.Models.Forum;
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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Mir2.Forum
{
    public partial class FrmAutoReplyGM016 : UIPage
    {
        Thread TMain = null, TSearch = null;
        bool IsRun = true, IsLogin = false, IsForumReply = false, IsRunSearchTopic = true, isInterval = false;
        List<string> filterUser = new List<string>();
        // 取某一页中的所有帖子
        List<Topic> list = new List<Topic>();
        Topic topic = null;
        int UserIndex = 0, TopicIndex = 0, IndexCount = 0, ms = 0;
        IForumBase forumBase = new ForumGM016Helper();
        UserInfo user = null;

        public FrmAutoReplyGM016()
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
                });
            }
        }

        private void FrmAutoReplyGM016_Load(object sender, EventArgs e)
        {
            var list1 = FileHelper.ReadTxtReturnList(ForumConfig.GM016UserinfoPath);
            foreach (var item in list1)
            {
                lbuser.Items.Add(item);
            }

            var list2 = FileHelper.ReadTxtReturnList(ForumConfig.GM016HuaShuPath);
            foreach (var item in list2)
            {
                lbhuashu.Items.Add(item);
            }
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            string item = uiTextBox1.Text;
            lbuser.Items.Add(item);
            FileHelper.AppendLine(ForumConfig.GM016UserinfoPath, item);
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
            FileHelper.AppendLine(ForumConfig.GM016HuaShuPath, item);
        }

        private void uiLinkLabel1_Click(object sender, EventArgs e)
        {
            Process.Start("IExplore", ForumConfig.GM016Domain);
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


                this.Invoke((EventHandler)delegate { lbuser.SelectedIndex = UserIndex; });
                try
                {
                    string[] UserInfo = lbuser.Items[UserIndex].ToString().Split(',');
                    user = new UserInfo
                    {
                        UserName = UserInfo[0],
                        PassWord = UserInfo[1],
                    };
                    if (forumBase.Login(UserInfo[0], UserInfo[1], "") == 1)
                    {
                        IsLogin = true;
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

                        // 随即取帖子
                        Random rnd = new Random();
                        TopicIndex = rnd.Next(list.Count - 1);
                        topic = list[TopicIndex];
                        int hs = rnd.Next(lbhuashu.Count - 1);
                        string tiename = lbhuashu.Items[hs].ToString();
                        this.Invoke((EventHandler)delegate
                        {
                            dgvtopic.SelectedIndex = TopicIndex;
                            lbhuashu.SelectedIndex = hs;
                        });
                        ForumConfig.ShowMessage(lbMsg, "正在回帖...");
                        // 回帖
                        string result = forumBase.PublishReply(topic, user, tiename, IsForumReply);

                        if (result.IndexOf("回复发布成功") > 0)
                        {
                            ++IndexCount;
                            ++TopicIndex;
                            ForumConfig.ShowMessage(lbMsg, $"第 {IndexCount} 次回帖 [{topic.t_Name}] 成功！");
                            if (isInterval) Thread.Sleep(ms * 1000);
                        }
                        else if (result.IndexOf("您所在的用户组每小时限制发回") > 0)
                        {
                            ++UserIndex;
                            IndexCount = 0;
                            IsLogin = false;
                            ForumConfig.ShowMessage(lbMsg, "[{user.UserName}]您所在的用户组每小时限制发回!");
                        }
                        else if (result.IndexOf("指定的主题不存在") > 0)
                        {
                            ++TopicIndex;
                            ForumConfig.ShowMessage(lbMsg, "抱歉，指定的主题不存在或已被删除或正在被审核...");
                        }
                        else if (result.IndexOf("您两次发表间隔少于") > 0)
                        {
                            isInterval = true;
                            MatchCollection mc = Regex.Matches(result, "您两次发表间隔少于 ([0-9]+) 秒");
                            ms = int.Parse(mc[0].Value.Split(' ')[1]);
                            
                            ForumConfig.ShowMessage(lbMsg, $"抱歉，您两次发表间隔少于 {ms} 秒，请稍候再发表...");
                            Thread.Sleep(ms * 1000);
                        }

                        forumBase.Logout(topic);
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
                ForumConfig.ShowMessage(lbMsg, "正在获取帖子数据...");
                try
                {
                    // 取帖子总共多少页
                    string url = $"{ForumConfig.GM016Domain}forum-2-1.html";
                    string html = http.SendRequest(url, "", "get", "", "gbk");
                    CQ domObjects = CQ.CreateDocument(html);
                    MatchCollection mcinfo = Regex.Matches(html, "共([0-9a-zA-Z\\s]+)页");

                    int.TryParse(Regex.Replace(mcinfo[0].Value, "\\D+", ""), out int topPage);
                    if (topPage <= 0)// 没有取到停5秒后继续取
                    {
                        Thread.Sleep(5000);
                        continue;
                    }
                    ForumConfig.ShowMessage(lbMsg, $"获取到GOM {topPage} 页数据...");
                    int count = 0;
                    for (int i = 1; i <= topPage; i++)
                    {
                        url = $"{ForumConfig.GM016Domain}forum-2-{i}.html";
                        html = http.SendRequest(url, "", "get", "", "gbk");
                        if (string.IsNullOrEmpty(html))
                        {
                            for (int j = 1; j <= 5; j++)
                            {
                                ForumConfig.ShowMessage(lbMsg, $"重新获取GOM第 {i}/{topPage} 页，第{j}次...");
                                html = http.SendRequest(url, "", "get", "", "gbk");
                                if (!string.IsNullOrEmpty(html))
                                    break;
                            }
                        }
                        domObjects = CQ.CreateDocument(html);
                        ForumConfig.ShowMessage(lbMsg, $"当前在取获取GOM第 {i}/{topPage} 页...");
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
                            link = d.Select("a").Attr("href");

                            // 根据链接进入详情取内容
                            html = http.SendRequest(link, "", "get", "", "gbk");
                            domObjects = CQ.CreateDocument(html);
                            string publisher = domObjects.Find(".authi:first").Text().Trim();
                            if (filterUser.Contains(publisher)) continue;//如果是管理员的帖子就重新抓

                            string afterS = "无售后";
                            string patchSize = "0";

                            string vt = "空";
                            
                            string name = domObjects.Find("#thread_subject").Text();

                            string downCount = "0";
                            mcinfo = Regex.Matches(html, "下载次数: ([0-9\\s]+)");
                            if (mcinfo.Count > 0)
                            {
                                downCount = mcinfo[0].Value.Substring(6);
                            }

                            string price = "0";
                            string xg1 = domObjects.Find("span .xg1").Text();
                            string[] downArr = xg1.Split(" ");
                            if (downArr != null)
                            {
                                if (downArr.Length == 7)
                                {
                                    price = downArr[5].TrimEnd(',');
                                }
                            }

                            string engine = domObjects.Find(".ts a").Text();
                            if (string.IsNullOrEmpty(engine)) engine = "[传奇版本]";
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

                            ForumConfig.ShowMessage(lbMsg, $"当前在取获取GOM第 {i}/{topPage} 页，已获取 {count} 条...");
                        }
                    }


                    IsRunSearchTopic = false;
                }
                catch (Exception ex)
                {
                    ForumConfig.ShowMessage(lbMsg, ex.Message);
                }
            }
        }
    }
}
