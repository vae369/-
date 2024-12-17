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
using System.Windows.Forms;

namespace Mir2.Forum
{
    public partial class FrmAutoReplyRCWLT : UIPage
    {
        Thread TMain = null, TSearch = null;
        bool IsRun = true, IsLogin = false, IsForumReply = true, IsRunSearchTopic = true, isInterval = false;
        int UserIndex = 0, TopicIndex = 0, IndexCount = 0, ms = 0;
        IForumBase forumBase = new ForumRCWHelper();
        List<string> filterUser = new List<string>();
        UserInfo user = null;



        // 取某一页中的所有帖子
        List<Topic> list = new List<Topic>();
        Topic topic = null;
        public FrmAutoReplyRCWLT()
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

        private void FrmAutoReplyRCWLT_Load(object sender, EventArgs e)
        {
            var list1 = FileHelper.ReadTxtReturnList(ForumConfig.RCWUserinfoPath);
            foreach (var item in list1)
            {
                lbuser.Items.Add(item);
            }

            //var list2 = FileHelper.ReadTxtReturnList(ForumConfig.RCWHuaShuPath);
            //foreach (var item in list2)
            //{
            //    lbhuashu.Items.Add(item);
            //}

            user = new UserInfo();
            filterUser.Add("wololn");
            //filterUser.Add("我喜欢");
            //filterUser.Add("什么时候");
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            string item = uiTextBox1.Text;
            lbuser.Items.Add(item);
            FileHelper.AppendLine(ForumConfig.RCWUserinfoPath, item);
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
            FileHelper.AppendLine(ForumConfig.RCWHuaShuPath, item);
        }

        private void uiLinkLabel1_Click(object sender, EventArgs e)
        {
            Process.Start("IExplore", ForumConfig.RCWDomain + "forum.php");
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
                //if (rb1.Checked)
                //{
                //    // 话术
                //    if (lbhuashu.Items.Count <= 0)
                //    {
                //        this.Invoke((EventHandler)delegate { this.ShowErrorDialog2("请导入回帖的话术!"); });
                //        Thread.Sleep(5000);
                //        continue;
                //    }
                //}
                //else
                //{
                //    IsForumReply = true;
                //}

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
                    if (IsLogin)
                    {
                        // 随即取帖子
                        Random rnd = new Random();
                        TopicIndex = rnd.Next(list.Count - 1);
                        topic = list[TopicIndex];
                        string tiename = "";
                        this.Invoke((EventHandler)delegate
                        {
                            dgvtopic.SelectedIndex = TopicIndex;
                        });
                        ForumConfig.ShowMessage(lbMsg, "正在回帖...");


                        // 回帖
                        string result = forumBase.PublishReply(topic, user, tiename, IsForumReply);

                        if (result.IndexOf("回复发布成功") > 0)
                        {
                            user = forumBase.GetUserInfo(user);
                            this.Invoke((EventHandler)delegate
                            {
                                lbJB.Text = user.jb.ToString();
                                lbJF.Text = user.jf.ToString();
                                lbYB.Text = user.yb.ToString();
                                lbWW.Text = user.ww.ToString();
                                lbGX.Text = user.gx.ToString();
                                lbtCount.Text = user.ReplyCount.ToString();
                                lbRcount.Text = user.ThreadCount.ToString();
                            });
                            ForumConfig.ShowMessage(lbMsg, $"正在获取用户信息...");

                            ++IndexCount;
                            ++TopicIndex;
                            ms = rnd.Next(30, 60);
                            ForumConfig.ShowMessage(lbMsg, $"第 {IndexCount} 次回帖 [{topic.t_Name}] 成功！{ms} 秒后继续回复...");
                            Thread.Sleep(ms * 1000);
                        }
                        else if (result.IndexOf("您所在的用户组每小时限制发回") > 0)
                        {
                            ++TopicIndex;
                            ++UserIndex;
                            IndexCount = 0;
                            IsLogin = false;

                            ForumConfig.ShowMessage(lbMsg, $"[{user.UserName}]您所在的用户组每小时限制发回!");
                        }
                        else if (result.IndexOf("指定的主题不存在") > 0)
                        {
                            ++TopicIndex;
                            ForumConfig.ShowMessage(lbMsg, $"抱歉，指定的主题不存在或已被删除或正在被审核...");
                        }
                        else if (result.IndexOf("您两次发表间隔少于") > 0)
                        {
                            ++TopicIndex;
                            isInterval = true;
                            MatchCollection mc = Regex.Matches(result, "您两次发表间隔少于 ([0-9]+) 秒");
                            ms = int.Parse(mc[0].Value.Split(' ')[1]);

                            ForumConfig.ShowMessage(lbMsg, $"抱歉，您两次发表间隔少于 {ms} 秒，请稍候再发表...");
                            Thread.Sleep(ms * 1000);
                        }
                        else if (result.IndexOf("检查到您为非手机注册用户") > 0)
                        {
                            ++TopicIndex;
                            ++UserIndex;
                            IndexCount = 0;
                            IsLogin = false;

                            ForumConfig.ShowMessage(lbMsg, $"[{user.UserName}] 检查到您为非手机注册用户，请前往论坛重新设置帐号进行手机绑定!");
                        }
                        else if (result.IndexOf("回复失败") > 0)
                        {
                            ++TopicIndex;
                            ForumConfig.ShowMessage(lbMsg, $"[{user.UserName}] 回复失败或用户取消回复!");
                            Thread.Sleep(5 * 1000);
                        }

                        //forumBase.Logout(topic);
                    }
                    else
                    {
                        var map = forumBase.GetVerCode();
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
                            ForumConfig.ShowMessage(lbMsg, $"正在获取用户信息...");

                            // 登录后取用户信息
                            user = forumBase.GetUserInfo(user);
                            this.Invoke((EventHandler)delegate
                            {
                                lbJB.Text = user.jb.ToString();
                                lbJF.Text = user.jf.ToString();
                                lbYB.Text = user.yb.ToString();
                                lbWW.Text = user.ww.ToString();
                                lbGX.Text = user.gx.ToString();
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
        }

        private void GetUserInfo()
        {

            ForumConfig.ShowMessage(lbMsg, $"正在获取用户信息...");
            // 登录后取用户信息
            user = forumBase.GetUserInfo(user);
            this.Invoke((EventHandler)delegate
            {
                lbJB.Text = user.Uid.ToString();
                lbWW.Text = user.jf.ToString();
                lbGX.Text = user.yb.ToString();
                lbYB.Text = user.jb.ToString();
                lbJF.Text = user.hdb.ToString();
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


        private void Search()
        {
            HttpHelper http = new HttpHelper();
            while (IsRunSearchTopic)
            {
                try
                {

                    ForumConfig.ShowMessage(lbMsg, $"正在获取帖子数据...");
                    // 取帖子总共多少页
                    string url = $"{ForumConfig.RCWDomain}forum-2-1.html";
                    string html = http.SendRequest(url, ForumConfig.RCWDomain, "get", "", "gbk");
                    if (string.IsNullOrEmpty(html))
                    {
                        Thread.Sleep(5000);
                        continue;
                    }
                    CQ domObjects = CQ.CreateDocument(html);
                    string pageSizeTitle = domObjects.Find("#fd_page_bottom .last").Text().Substring(4);
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
                        url = $"{ForumConfig.RCWDomain}forum-2-{i}.html";
                        html = http.SendRequest(url, ForumConfig.RCWDomain, "get", "", "gbk");
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
                            link = ForumConfig.RCWDomain + d.Select("a").Attr("href");


                            // 根据链接进入详情取内容
                            html = http.SendRequest(link, url, "get", "", "gbk");
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
                            string vt = "无";
                            MatchCollection mc = null;
                            if (domObjects.Select(".t_f").Count() > 0)
                            {
                                var rows = domObjects.Select(".t_f");
                                foreach (var r in rows)
                                {
                                    mc = Regex.Matches(r.InnerText, "传奇补丁大小：\\d+\\.?\\d+[a-zA-z+]");
                                    if (mc.Count > 0)
                                    {
                                        patchSize = mc[0].Value.Replace("传奇补丁大小：", "");
                                    }
                                    mc = Regex.Matches(r.InnerText, "游戏版本类型：[\u4e00-\u9fa5]+");
                                    if (mc.Count > 0)
                                    {
                                        vt = mc[0].Value.Replace("游戏版本类型：", "");
                                    }
                                }
                            }
                            string price = "0";
                            if (domObjects.Find(".xg1").Count() > 7)
                            {
                                var xg = domObjects.Find(".xg1");
                                if (xg.Count() > 0)
                                {
                                    string p = xg.Text();
                                    mc = Regex.Matches(p, "售价: \\d+");
                                    if (mc.Count > 0)
                                    {
                                        price = mc[0].Value.Replace("售价: ", "");
                                    }
                                }
                            }

                            string engine = "";
                            if (domObjects.Find(".ts").Count() > 0)
                                engine = domObjects.Find(".ts a").Text();

                            string tattl = domObjects.Find(".xg1").Text();
                            mc = Regex.Matches(tattl, "下载次数: ([0-9]+)");
                            string downCount = "";
                            if (mc.Count > 0)
                                downCount = mc[0].Value.Replace("下载次数: ", "");


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

    }
}
