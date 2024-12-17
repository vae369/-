using CsQuery;
using Mir.Core.file;
using Mir.Core.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Mir2.Forum
{
    public partial class FrmSepg : Form
    {
        #region 成员变量
        CookieContainer sendcookie = null;
        CookieContainer outcookie;
        string filehuashupath = Application.StartupPath + "\\sepg\\话术.txt";
        string fileuserpath = Application.StartupPath + "\\sepg\\账号.txt";
        List<string> server = new List<string>();
        Thread TSearch;
        Thread TSearch2;
        Thread TMain;
        bool IsRun = true;
        int UserIndex = 0;
        int TopicIndex = 0;
        int IndexCount = 0;
        HttpHelper http = new HttpHelper();
        #endregion

        public FrmSepg()
        {
            InitializeComponent();
        }

        private void btnImportUser_Click(object sender, EventArgs e)
        {
            ofdMain.Multiselect = false;
            ofdMain.Filter = "文本文件(*.txt)|*.txt";
            if (ofdMain.ShowDialog() == DialogResult.OK)
            {
                lbhuashu.Items.Clear();
                string fileName = ofdMain.FileName;
                List<string> list = FileHelper.GetFileInfo(fileName);
                for (int iList = 0; iList < list.Count; iList++)
                {
                    lbuser.Items.Add(list[iList]);
                }
            }
        }

        private void FrmSepg_Load(object sender, EventArgs e)
        {
            List<string> listhuashu = FileHelper.GetFileInfo(filehuashupath);
            for (int iList = 0; iList < listhuashu.Count; iList++)
            {
                lbhuashu.Items.Add(listhuashu[iList]);
            }

            List<string> listuser = FileHelper.GetFileInfo(fileuserpath);
            for (int iList = 0; iList < listuser.Count; iList++)
            {
                lbuser.Items.Add(listuser[iList]);
            }

            btnStop.Enabled = false;
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUser.Text.Trim()))
            {
                lbuser.Items.Add(txtUser.Text.Trim());
                FileHelper.AppendLine(fileuserpath, txtUser.Text.Trim());
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ofdMain.Multiselect = false;
            ofdMain.Filter = "文本文件(*.txt)|*.txt";
            if (ofdMain.ShowDialog() == DialogResult.OK)
            {
                lbhuashu.Items.Clear();
                string fileName = ofdMain.FileName;
                List<string> list = FileHelper.GetFileInfo(fileName);
                for (int iList = 0; iList < list.Count; iList++)
                {
                    lbhuashu.Items.Add(list[iList]);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txthuashu.Text.Trim()))
            {
                lbhuashu.Items.Add(txthuashu.Text.Trim());
                FileHelper.AppendLine(filehuashupath, txthuashu.Text.Trim());
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                IsRun = true;
                btnStart.Enabled = false;
                btnStop.Enabled = true;

                TSearch = new Thread(new ThreadStart(Search));
                TSearch.IsBackground = true;
                TSearch.Start();

                TSearch2 = new Thread(new ThreadStart(Search2));
                TSearch2.IsBackground = true;
                TSearch2.Start();

                TMain = new Thread(new ThreadStart(Run));
                TMain.IsBackground = true;
                TMain.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 查询GeeM2端版本
        /// </summary>
        private void Search2()
        {
            Thread.Sleep(20 * 1000);

            while (IsRun)
            {
                try
                {
                    #region =============== 获取GeeM2端版本有多少页

                    string url = "http://www.sepg.net.cn/forum-43-1.html";
                    string html = http.SendRequest(url, "", "get", "", "gbk");

                    MatchCollection mcinfo = Regex.Matches(html, "共([0-9a-zA-Z\\s]+)页");


                    int pagesize = -1;
                    int.TryParse(Regex.Replace(mcinfo[0].Value, "\\D+", ""), out pagesize);

                    #endregion

                    #region ================ 筛选私服地址和名称

                    if (pagesize > 0)
                    {


                        for (int i = 1; i <= pagesize; i++)
                        {
                            this.Invoke((EventHandler)delegate { lbmsg.Text = "当前在取获取GeeM2端版本第" + i + "页数据"; });
                            url = "http://www.sepg.net.cn/forum-43-" + i + ".html";
                            html = http.SendRequest(url, "", "get", "", "gbk");

                            var jQuery = CQ.Create(html).Select(".new > a");


                            foreach (var m in jQuery)
                            {
                                ListViewItem listItem = new ListViewItem();
                                string href = m.Attributes["href"].ToString();
                                if (!string.IsNullOrEmpty(m.InnerText) && href.IndexOf("thread") > 0)
                                {

                                    string hrefid = m.Attributes["href"].Split('-')[1];
                                    string text = m.InnerText;
                                    if (!server.Contains(href))
                                    {
                                        server.Add(href);

                                        listItem.Text = text;
                                        listItem.SubItems.Add(hrefid);
                                        listItem.SubItems.Add(href);
                                        this.Invoke((EventHandler)delegate { this.lvtopic.Items.Add(listItem); });
                                    }

                                }
                            }
                        }
                        Thread.Sleep(3600 * 1000);
                    }

                    #endregion




                }
                catch (Exception ex)
                {
                    this.Invoke((EventHandler)delegate { lbmsg.Text = ex.Message; });
                }

            }

        }

        private void Search()
        {

            this.Invoke((EventHandler)delegate
            {
                this.lvtopic.Clear();

                this.lvtopic.BeginUpdate();

                this.lvtopic.Columns.Add("帖子名称", 220, HorizontalAlignment.Center);
                this.lvtopic.Columns.Add("帖子ID", 50, HorizontalAlignment.Center);
                this.lvtopic.Columns.Add("帖子链接", 200, HorizontalAlignment.Center);
                this.lvtopic.View = View.Details;
            });



            while (lvtopic.Items.Count == 0 && IsRun)
            {
                try
                {
                    #region =============== 获取GOM版本有多少页

                    string url = "http://www.sepg.net.cn/forum-2-1.html";
                    string html = http.SendRequest(url, "", "get", "", "gbk");

                    MatchCollection mcinfo = Regex.Matches(html, "共([0-9a-zA-Z\\s]+)页");

                    //foreach (Group item in mcinfo)
                    //{
                    int pagesize = -1;
                    int.TryParse(Regex.Replace(mcinfo[0].Value, "\\D+", ""), out pagesize);
                    //}

                    #endregion

                    #region ================ 筛选私服地址和名称

                    if (pagesize > 0)
                    {


                        for (int i = 1; i <= pagesize; i++)
                        {
                            this.Invoke((EventHandler)delegate { lbmsg.Text = "当前在取获取GOM版本第" + i + "页数据"; });
                            url = "http://www.sepg.net.cn/forum-2-" + i + ".html";
                            html = http.SendRequest(url, "", "get", "", "gbk");

                            var jQuery = CQ.Create(html).Select(".new > a");


                            foreach (var m in jQuery)
                            {
                                ListViewItem listItem = new ListViewItem();
                                string href = m.Attributes["href"].ToString();
                                if (!string.IsNullOrEmpty(m.InnerText) && href.IndexOf("thread") > 0)
                                {

                                    string hrefid = m.Attributes["href"].Split('-')[1];
                                    string text = m.InnerText;
                                    if (!server.Contains(href))
                                    {
                                        server.Add(href);

                                        listItem.Text = text;
                                        listItem.SubItems.Add(hrefid);
                                        listItem.SubItems.Add(href);
                                        this.Invoke((EventHandler)delegate { this.lvtopic.Items.Add(listItem); });
                                    }

                                }
                            }
                        }
                        Thread.Sleep(3600 * 1000);
                    }

                    #endregion

                }
                catch (Exception ex)
                {
                    this.Invoke((EventHandler)delegate { lbmsg.Text = ex.Message; });

                }

            }
        }
        private void Run()
        {
            while (IsRun)
            {
                // 用户
                if (lbuser.Items.Count <= 0)
                {
                    this.Invoke((EventHandler)delegate { lbmsg.Text = "请导入登录帐号!"; });
                    Thread.Sleep(5000);
                    continue;
                }
                // 话术
                if (lbhuashu.Items.Count <= 0)
                {
                    this.Invoke((EventHandler)delegate { lbmsg.Text = "请导入回帖的话术!"; });
                    Thread.Sleep(5000);
                    continue;
                }

                if (lvtopic.Items.Count <= 0)
                {
                    Thread.Sleep(5000);
                    continue;
                }
                // 用户置零
                if (UserIndex >= lbuser.Items.Count)
                    UserIndex = 0;
                // 帖子置零
                if (TopicIndex >= lvtopic.Items.Count)
                    TopicIndex = 0;
                this.Invoke((EventHandler)delegate { lbuser.SelectedIndex = UserIndex; });
                try
                {
                    string[] UserInfo = lbuser.Items[UserIndex].ToString().Split(',');
                    string url = "http://www.sepg.net.cn/forum-2-1.html";
                    string html = http.SendRequest(url, "", "get", "", "gbk");

                    sendcookie = outcookie;
                    string referurl = url;
                    url = "http://www.sepg.net.cn/member.php?mod=logging&action=login&loginsubmit=yes&infloat=yes&lssubmit=yes&inajax=1";
                    string values = string.Format("username={0}&password={1}&quickforward=yes&handlekey=ls", HttpUtility.UrlEncode(UserInfo[0]), HttpUtility.UrlEncode(UserInfo[1]));

                    html = http.SendRequest(url, "", "get", "", "gbk");

                    sendcookie = outcookie;

                    if (html.IndexOf(referurl) > 0)
                    {
                        this.Invoke((EventHandler)delegate { lbmsg.Text = "登录成功!"; });

                        html = http.SendRequest(url, "", "get", "", "gbk");

                        MatchCollection mc = Regex.Matches(html, "http://www.sepg.net.cn/space-uid-([0-9a-zA-Z]+).html");

                        if (mc != null)
                        {
                            url = mc[0].Value;
                            html = http.SendRequest(url, "", "get", "", "gbk");

                            string pf_l = html.Substring(html.LastIndexOf("pf_l"));
                            pf_l = pf_l.Substring(0, pf_l.IndexOf("</div>"));
                            MatchCollection mcinfo = Regex.Matches(pf_l, @"<li><em>([\u4e00-\u9fa5]+)</em>([0-9a-zA-Z\s]+)</li>");

                            this.Invoke((EventHandler)delegate
                            {
                                string space = mcinfo[0].Value.Substring(0, mcinfo[0].Value.LastIndexOf("<"));
                                lbspace.Text = space.Substring(space.IndexOf(' ')).Replace(" ", "");
                                lbjifen.Text = Regex.Replace(mcinfo[1].Value, "\\D+", "");
                                lbww.Text = Regex.Replace(mcinfo[2].Value, "\\D+", "");
                                lbbb.Text = Regex.Replace(mcinfo[3].Value, "\\D+", "");
                                lbgx.Text = Regex.Replace(mcinfo[4].Value, "\\D+", "");

                                string mbn = html.Substring(html.IndexOf("mbn"));
                                lbUserID.Text = string.Format("{0}(UID:{1})", mbn.Substring(7, mbn.IndexOf("<") - 7), Regex.Replace(mc[0].Value, "\\D+", ""));


                            });
                            string tid = string.Empty;
                            string tiename = string.Empty;
                            this.Invoke((EventHandler)delegate
                            {
                                url = lvtopic.Items[TopicIndex].SubItems[2].Text;
                                tid = lvtopic.Items[TopicIndex].SubItems[1].Text;
                                tiename = lvtopic.Items[TopicIndex].SubItems[0].Text;


                                //lvtopic.Items[TopicIndex].Selected = true;
                                //lvtopic.SelectedItems[TopicIndex+1].BackColor = Color.FromArgb(49, 106, 197);
                                //lvtopic.Items[TopicIndex].EnsureVisible();
                            });

                            string result = PublishReply(tid, url, tiename);
                            if (result.IndexOf("回复发布成功") > 0)
                            {
                                ++IndexCount;
                                this.Invoke((EventHandler)delegate { lbmsg.Text = string.Format("第{0}次回帖[{1}]成功！", IndexCount, tid); });
                                ++TopicIndex;

                            }
                            if (result.IndexOf("您所在的用户组每小时限制发回") > 0)
                            {
                                IndexCount = 0;
                                this.Invoke((EventHandler)delegate { lbmsg.Text = string.Format("[{0}]您所在的用户组每小时限制发回!", tid); });
                                ++UserIndex;
                            }
                            Thread.Sleep(15 * 1000);
                        }
                    }
                    if (html.IndexOf("密码错误次数过多") > 0)
                    {

                        this.Invoke((EventHandler)delegate { lbmsg.Text = string.Format("密码错误次数过多,进行下个账号登录!"); });
                        ++UserIndex;

                    }



                }
                catch (Exception ex)
                {
                    this.Invoke((EventHandler)delegate { lbmsg.Text = ex.Message; });
                }
            }

        }

        private string PublishReply(string tid, string url, string tiename)
        {
            // string html = HtmlHelper.SendRequest(url, "get", "", "gbk");

            string refer = url;

            url = "http://www.sepg.net.cn/forum.php?mod=post&action=reply&fid=2&tid=" + tid + "&infloat=yes&handlekey=reply&inajax=1&ajaxtarget=fwin_content_reply";
            string html = http.SendRequest(url, "", "get", "", "gbk");

            string formhash = CQ.Create(html).Select("input:hidden[name='formhash']").Val();
            url = "http://www.sepg.net.cn/forum.php?mod=post&infloat=yes&action=reply&fid=2&extra=&tid=" + tid + "&replysubmit=yes&inajax=1";

            int num = new Random().Next(1, lbhuashu.Items.Count);
            this.Invoke((EventHandler)delegate { lbhuashu.SelectedIndex = num; });
            string message = lbhuashu.Items[num].ToString();
            Encoding myEncoding = Encoding.GetEncoding("GBK");

            string values = string.Format("formhash={0}&handlekey=reply&noticeauthor=&noticetrimstr=&noticeauthormsg=&usesig=1&subject=&message={1}", formhash, HttpUtility.UrlEncode(message, myEncoding));
            html = http.SendRequest(url, refer, "post", values, "gbk", "application/x-www-form-urlencoded");
            return html;


        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            IsRun = false;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            TMain.Abort();
            TSearch.Abort();
        }
    }
}
