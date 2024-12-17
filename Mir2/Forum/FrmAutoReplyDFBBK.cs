using Mir.Core.file;
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
    public partial class FrmAutoReplyDFBBK : UIPage
    {
        Thread TMain = null, TSearch = null;
        bool IsRun = true, IsLogin = false, IsForumReply = false, IsRunSearchTopic = true, isInterval = false;
        int UserIndex = 0, TopicIndex = 0, IndexCount = 0, ms = 0;
        IForumBase forumBase = new ForumDFHelper();
        List<string> filterUser = new List<string>();
        UserInfo user = null;
        Topic topic = null;
        // 取某一页中的所有帖子
        List<Topic> list = new List<Topic>();
        public FrmAutoReplyDFBBK()
        {
            InitializeComponent();
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

        private void FrmAutoReplyDFBBK_Load(object sender, EventArgs e)
        {
            var list1 = FileHelper.ReadTxtReturnList(ForumConfig.DFUserinfoPath);
            foreach (var item in list1)
            {
                lbuser.Items.Add(item);
            }

            var list2 = FileHelper.ReadTxtReturnList(ForumConfig.DFHuaShuPath);
            foreach (var item in list2)
            {
                lbhuashu.Items.Add(item);
            }
            //filterUser.Add("admin");
            //filterUser.Add("精神小伙");
            user = new UserInfo();
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            string item = uiTextBox1.Text;
            lbuser.Items.Add(item);
            FileHelper.AppendLine(ForumConfig.DFUserinfoPath, item);
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
            FileHelper.AppendLine(ForumConfig.DFHuaShuPath, item);
        }

        private void uiLinkLabel1_Click(object sender, EventArgs e)
        {
            Process.Start("IExplore", ForumConfig.DFDomain + "forum.php");
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "开始")
            {
                btnStart.Text = "停止";
                TMain = new Thread(new ThreadStart(RunReply));
                TMain.IsBackground = true;
                TMain.Start();

                //TSearch = new Thread(new ThreadStart(Search));
                //TSearch.IsBackground = true;
                //TSearch.Start();
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
            throw new NotImplementedException();
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

                //if (dgvtopic.Rows.Count <= 0)
                //{
                //    ForumConfig.ShowMessage(lbMsg, "请先获取帖子!");
                //    Thread.Sleep(5000);
                //    continue;
                //}

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
                            ++IndexCount;
                            ++UserIndex;
                            ForumConfig.ShowMessage(lbMsg, $"第 {IndexCount} 次回帖 [{topic.t_Name}] 成功！");
                            if (isInterval) Thread.Sleep(ms * 1000);
                            //forumBase.Logout(topic);
                        }
                        else if (result.IndexOf("抱歉，您尚未登录") > 0)
                        {
                            IsLogin = false;
                            Thread.Sleep(15 * 1000);
                        }
                        else if (result.IndexOf("您两次发表间隔少于") > 0)
                        {
                            isInterval = true;
                            MatchCollection mc = Regex.Matches(result, "您两次发表间隔少于 ([0-9]+) 秒");
                            ms = int.Parse(mc[0].Value.Split(' ')[1]);
                            ForumConfig.ShowMessage(lbMsg, $"抱歉，您两次发表间隔少于 {ms} 秒，请稍候再发表...");
                            Thread.Sleep(ms * 1000);
                        }
                        else if (result.IndexOf("您的帖子小于") > 0)
                        {
                            ForumConfig.ShowMessage(lbMsg, "抱歉，您的帖子小于 10 个字符的限制！");
                            Thread.Sleep(5000);
                        }
                    }
                    else
                    {
                        var map = forumBase.GetVerCode();

                        
                        user.UserName = UserInfo[0];
                        user.PassWord = UserInfo[1];
                        user.Uid = forumBase.Login(user.UserName, user.PassWord, "");
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
                    ForumConfig.ShowMessage(lbMsg, ex.Message);
                    forumBase.Logout(topic);
                    IsLogin = false;
                }
            }
        }

        private void GetUserInfo()
        {
            throw new NotImplementedException();
        }
    }
}
