using CsQuery;
using Mir.Core.cache;
using Mir.Core.utils;
using Mir.Models.Forum;
using Mir2.Forum;
using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using static System.Net.WebRequestMethods;

namespace Mir2.Helper
{
    public class ForumGM016Helper : IForumBase
    {
        HttpHelper http = new HttpHelper();
        Random rnd = new Random();
        string loginhash = "", formhash = "", idhash = "";
        string refer = ForumConfig.GM016Domain + "forum.php";
        string setCookie = "";

        public bool AutoSingIn(UserInfo user)
        {
            string url = $"{ForumConfig.GM016Domain}plugin.php?id=k_misign:sign";
            string html = http.SendRequest(url, refer, "get", "", "utf-8");
            CQ dom = CQ.CreateDocument(html);
            formhash = dom.Find("input[name='formhash']").Val();
            string isSingin = dom.Find(".paiming.cl .font").Text().Trim();
            if (isSingin.Contains("您今天还没有签到"))
            {
                url = $"{ForumConfig.GM016Domain}plugin.php?id=k_misign:sign&operation=qiandao&formhash={formhash}&format=empty&inajax=1&ajaxtarget=";
                html = http.SendRequest(url, refer, "get", "", "utf-8");

                url = $"{ForumConfig.GM016Domain}plugin.php?id=k_misign:sign";
                html = http.SendRequest(url, refer, "get", "", "utf-8");
                isSingin = dom.Find(".paiming.cl .font").Text().Trim();
                if (isSingin.Contains("您的签到排名"))
                    return true;
                else
                    return false;
            }
            return true;
        }

        public UserInfo GetUserInfo(UserInfo user)
        {
            string url = $"{ForumConfig.GM016Domain}forum-2-1.html";
            string html = http.SendRequest(url, refer, "get", "", "gbk");
            MatchCollection mc = Regex.Matches(html, $"{ForumConfig.GM016Domain}space-uid-([0-9a-zA-Z]+).html");

            html = http.SendRequest(mc[0].Value, refer, "get", "", "gbk");
            refer = url;
            CQ dom = CQ.CreateDocument(html);

            string UserName = dom.Find(".mt").Text().Trim();
            if (string.IsNullOrEmpty(UserName))
            {
                if (Login(user.UserName, user.PassWord, "") > 0)
                {
                    user = GetUserInfo(user);
                }
            }
            else
            {
                user.Uid = int.Parse(dom.Find(".xw0").Text().Substring(6).TrimEnd(')'));
                user.ReplyCount = int.Parse(dom.Select(".mbm li a")[2].InnerText.Substring(4));
                user.ThreadCount = int.Parse(dom.Select(".mbm li a")[3].InnerText.Substring(4));
                user.AuthMethod = dom.Find(".pf_l.cl.pbm.mbm li:first").Text().Substring(4);
                user.GroupName = dom.Find(".pbm.mbm.bbda.cl a")[4].InnerText;
                user.RegistTime = dom.Find("#pbbs li")[1].InnerText.Substring(4);
                user.FinalyTime = dom.Find("#pbbs li")[2].InnerText.Substring(4);
                user.RegistIP = dom.Find("#pbbs li")[3].InnerText.Substring(3);
                user.LastIP = dom.Find("#pbbs li")[4].InnerText.Substring(7);
                user.LastActiveTime = dom.Find("#pbbs li")[5].InnerText.Substring(6);
                user.LastPushTime = dom.Find("#pbbs li")[6].InnerText.Substring(6);
                user.space = dom.Find("#psts .pf_l li")[0].InnerText.Substring(4).Trim();
                user.jf = int.Parse(dom.Find("#psts .pf_l li")[1].InnerText.Substring(2).Trim());
                user.yb = int.Parse(dom.Find("#psts .pf_l li")[2].InnerText.Substring(2).Trim());
                user.jb = int.Parse(dom.Find("#psts .pf_l li")[3].InnerText.Substring(2).Trim());
                user.hdb = int.Parse(dom.Find("#psts .pf_l li")[4].InnerText.Substring(2).Trim());
            }


            return user;
        }

        public Bitmap GetVerCode()
        {
            // 取登录页
            string url = $"{ForumConfig.GM016Domain}member.php?mod=logging&action=login&phonelogin=no&infloat=yes&handlekey=login&inajax=1&ajaxtarget=fwin_content_login";
            string html = http.SendRequest(url, refer, "get", "", "utf-8", "");
            CQ dom = CQ.CreateDocument(html);
            loginhash = html.Substring(html.IndexOf("loginhash"), 15);
            formhash = dom.Find("input[name='formhash']").Val();
            idhash = html.Substring(html.IndexOf("updateseccode") + 15, 6);

            // 取验证码页
            url = $"{ForumConfig.GM016Domain}misc.php?mod=seccode&action=update&idhash={idhash}&modid=member::logging&{rnd.NextDouble()}";
            html = http.SendRequest(url, refer, "get", "", "utf-8", "application/javascript");

            // 取验证码图片
            string res = html.Substring(html.IndexOf("misc.php"));
            res = res.Substring(0, res.IndexOf("class") - 2);
            url = ForumConfig.GM016Domain + res;
            http.accept = "image/avif,image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8";
            Bitmap map = http.SendImageStreamRequest(url, refer, "image/png");

            // 图片存本地
            string path = ForumConfig.StartupPath + "\\images\\yzm.png";
            map.Save(path, System.Drawing.Imaging.ImageFormat.Png);

            return map;
        }

        public int Login(string userName, string passWord, string verCode)
        {
            verCode = ForumConfig.GM016Domain + "forum.php";
            string url = $"{ForumConfig.GM016Domain}member.php?mod=logging&action=login&loginsubmit=yes&infloat=yes&lssubmit=yes&inajax=1";

            passWord = DESHelper.GetMD5Hash(passWord);
            string values = $"fastloginfield=username&username={userName}&password={passWord}&quickforward=yes&handlekey=ls";

            string html = http.SendRequest(url, refer, "post", values, "gbk", "application/x-www-form-urlencoded");
            if (html.Contains(ForumConfig.GM016Domain) || html.Contains(verCode) || html.Contains("欢迎您回来"))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public bool Logout(Topic topic)
        {
            string html = http.SendRequest(topic.t_Link, refer, "get", "", "gbk");
            CQ dom = CQ.CreateDocument(html);
            formhash = dom.Find("input[name='formhash']").Val();


            string url = $"{ForumConfig.GM016Domain}member.php?mod=logging&action=logout&formhash=" + formhash;
            html = http.SendRequest(url, refer, "get", "", "gbk");
            if (html.IndexOf("您已退出站点") > 0) return true;
            else return false;
        }

        public string PublishReply(Topic topic, UserInfo user, string message, bool IsForumReply)
        {
            string url = topic.t_Link;
            string html = http.SendRequest(url, refer, "get", "", "gbk");

            refer = url;

            CQ dom = CQ.CreateDocument(html);
            if (IsForumReply)
            {
                // 取论坛帖子内的回复，随机
                MatchCollection msg_ids = Regex.Matches(html, "postmessage_([0-9\\s]+)");
                List<string> list = new List<string>();
                string pattern = @"[\u4e00-\u9fa5]";
                StringBuilder msg = new StringBuilder();
                MatchCollection matches = null;

                foreach (Group id in msg_ids)
                {
                    msg.Clear();
                    string str = dom.Find("#" + id.Value).Text();
                    matches = Regex.Matches(str, pattern);
                    foreach (Match match in matches)
                    {
                        msg.Append(match.Value);
                    }

                    if (msg.Length > 30)
                    {
                        int n = rnd.Next(msg.Length - 1);
                        msg = msg.Remove(n, msg.Length - n);
                    }
                    list.Add(msg.ToString());
                }
                string newMsg = list[rnd.Next(list.Count - 1)];
                if (!string.IsNullOrEmpty(newMsg))
                {
                    message = newMsg;
                }
            }

            string formhash = "";
            do
            {
                url = $"{ForumConfig.GM016Domain}forum.php?mod=post&action=reply&fid=2&tid={topic.t_Id}&infloat=yes&handlekey=reply&inajax=1&ajaxtarget=fwin_content_reply";
                html = http.SendRequest(url, refer, "get", "", "gbk");
                formhash = CQ.Create(html).Find("input[name='formhash']").Val();
            } while (string.IsNullOrEmpty(formhash));

            url = $"{ForumConfig.GM016Domain}forum.php?mod=post&infloat=yes&action=reply&fid=2&extra=&tid={topic.t_Id}&replysubmit=yes&inajax=1";

            Encoding myEncoding = Encoding.GetEncoding("GBK");

            string values = string.Format("formhash={0}&handlekey=reply&usesig=1&message={1}", formhash, HttpUtility.UrlEncode(message, myEncoding));

            html = http.SendRequest(url, refer, "post", values, "gbk", "application/x-www-form-urlencoded");
            return html;
        }
    }
}
