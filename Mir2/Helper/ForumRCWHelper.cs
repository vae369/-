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
using System.Threading;
using System.Web;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Markup;
using static Spire.Xls.Core.Spreadsheet.XlsWorkbook;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Mir2.Helper
{
    public class ForumRCWHelper : IForumBase
    {
        HttpHelper http = new HttpHelper();
        Random rnd = new Random();
        string loginhash = "", formhash = "", idhash = "";
        string refer = ForumConfig.RCWDomain + "forum.php";
        string setCookie = "";

        public bool AutoSingIn(UserInfo user)
        {
            string url = $"{ForumConfig.RCWDomain}plugin.php?id=k_misign:sign";
            string html = http.SendRequest(url, refer, "get", "", "gbk");
            CQ dom = CQ.CreateDocument(html);
            formhash = dom.Find("input[name='formhash']").Val();
            string isSingin = dom.Find(".paiming.cl .font").Text().Trim();
            if (isSingin.Contains("您今天还没有签到"))
            {
                url = $"{ForumConfig.RCWDomain}plugin.php?id=k_misign:sign&operation=qiandao&formhash={formhash}&format=empty&inajax=1&ajaxtarget=";
                html = http.SendRequest(url, refer, "get", "", "gbk");

                url = $"{ForumConfig.RCWDomain}plugin.php?id=k_misign:sign";
                html = http.SendRequest(url, refer, "get", "", "gbk");
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
            string url = $"{ForumConfig.RCWDomain}home.php?mod=space&do=profile&from=space";
            string html = http.SendRequest(url, refer, "get", "", "gbk");

            CQ dom = CQ.CreateDocument(html);
            string UserName = dom.Find(".spaceProfileCard__line a").Attr("title").Trim();

            user.Uid = int.Parse(dom.Find(".xw0").Text().Substring(6).TrimEnd(')'));
            user.ReplyCount = int.Parse(dom.Select(".mbm li a")[1].InnerText.Substring(4));
            user.ThreadCount = int.Parse(dom.Select(".mbm li a")[2].InnerText.Substring(4));
            user.AuthMethod = dom.Find(".pf_l.cl.pbm.mbm li:first").Text().Substring(4);
            user.GroupName = dom.Find(".pbm.mbm.bbda.cl a")[3].InnerText;
            user.RegistTime = dom.Find("#pbbs li")[0].InnerText.Substring(4);
            user.FinalyTime = dom.Find("#pbbs li")[1].InnerText.Substring(4);
            user.RegistIP = dom.Find("#pbbs li")[2].InnerText.Substring(5);
            user.LastIP = dom.Find("#pbbs li")[3].InnerText.Substring(7);
            user.LastActiveTime = dom.Find("#pbbs li")[4].InnerText.Substring(6);
            user.LastPushTime = dom.Find("#pbbs li")[5].InnerText.Substring(6);

            user.jf = int.Parse(dom.Find("#psts .pf_l li")[1].InnerText.Substring(2).Trim());
            user.ww = int.Parse(dom.Find("#psts .pf_l li")[2].InnerText.Substring(2).Trim());
            user.jb = int.Parse(dom.Find("#psts .pf_l li")[3].InnerText.Substring(2).Trim());
            user.gx = int.Parse(dom.Find("#psts .pf_l li")[4].InnerText.Substring(2).Trim());
            user.yb = int.Parse(dom.Find("#psts .pf_l li")[5].InnerText.Substring(2).Trim());


            return user;
        }

        public Bitmap GetVerCode()
        {
            // 取登录页
            string url = $"{ForumConfig.RCWDomain}member.php?mod=logging&action=login&phonelogin=no&infloat=yes&handlekey=login&inajax=1&ajaxtarget=fwin_content_login";

            string html = http.SendRequest(url, refer, "get", "", "gbk");
            CQ dom = CQ.CreateDocument(html);
            loginhash = html.Substring(html.IndexOf("loginhash"), 15);
            formhash = dom.Find("input[name='formhash']").Val();
            idhash = "cSA";//html.Substring(html.IndexOf("updateseccode") + 15, 6);

            // 取验证码页
            url = $"{ForumConfig.RCWDomain}misc.php?mod=seccode&action=update&idhash={idhash}&{rnd.NextDouble()}&modid=undefined";
            html = http.SendRequest(url, refer, "get", "", "gbk", "application/javascript");

            // 取验证码图片
            string res = html.Substring(html.IndexOf("misc.php"));
            res = res.Substring(0, res.IndexOf("class") - 2);
            url = ForumConfig.RCWDomain + res;
            http.accept = "image/avif,image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8";
            Bitmap map = http.SendImageStreamRequest(url, refer, "image/png");

            // 图片存本地
            string path = ForumConfig.StartupPath + "\\images\\rcwyzm.png";
            map.Save(path, System.Drawing.Imaging.ImageFormat.Png);

            return map;
        }

        public int Login(string userName, string passWord, string verCode)
        {
            string refer = ForumConfig.RCWDomain + "forum-2-1.html";

            //string url = $"{ForumConfig.RCWDomain}member.php?mod=logging&action=login&infloat=yes&handlekey=login&inajax=1&ajaxtarget=fwin_content_login";
            //string html = http.SendRequest(url, refer, "get", "", "gbk");
            //CQ dom = CQ.CreateDocument(html);
            //formhash = dom.Find("input[name='formhash']").Val();
            //string action = dom.Select("form").Attr("action");
            Encoding myEncoding = Encoding.GetEncoding("GBK");

            string values = $"formhash={formhash}&referer={HttpUtility.UrlEncode(refer)}&loginfield=username&username={HttpUtility.UrlEncode(userName)}&password={DESHelper.GetMD5Hash(HttpUtility.UrlEncode(passWord))}&questionid=0&seccodehash=cSA&seccodemodid=member%3A%3Alogging&seccodeverify={HttpUtility.UrlEncode(verCode, myEncoding)}";
            string url = $"{ForumConfig.RCWDomain}member.php?mod=logging&action=login&loginsubmit=yes&handlekey=login&loginhash={loginhash}&inajax=1";
            string html = http.SendRequest(url, refer, "post", values, "gbk", "application/x-www-form-urlencoded");
            if (html.Contains(refer) || html.Contains("欢迎您回来"))
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


            string url = $"{ForumConfig.RCWDomain}member.php?mod=logging&action=logout&formhash=" + formhash;
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
            //if (IsForumReply)
            //{
            // 随即跳一页取评论                
            string pgspan = dom.Find(".pg span").Attr("title").Replace(" ", "");
            pgspan = pgspan.TrimStart('共');
            pgspan = pgspan.TrimEnd('页');
            int pg = Convert.ToInt32(pgspan);
            pg = rnd.Next(1, pg);
            string[] urlArray = url.Split('-');
            urlArray[2] = pg.ToString();
            url = string.Join("-", urlArray);
            html = http.SendRequest(url, refer, "get", "", "gbk");
            dom = CQ.CreateDocument(html);
            // 取论坛帖子内的回复，随机
            MatchCollection mc = Regex.Matches(html, "postmessage_([0-9\\s]+)");
            List<string> list = new List<string>();
            string pattern = @"[\u4e00-\u9fa5]";
            StringBuilder msg = new StringBuilder();
            MatchCollection matches = null;

            foreach (Group id in mc)
            {
                msg.Clear();
                string str = dom.Find("#" + id.Value).Text();
                matches = Regex.Matches(str, pattern);
                foreach (Match match in matches)
                {
                    msg.Append(match.Value);
                }

                list.Add(msg.ToString());
            }
            pg = rnd.Next(1, list.Count - 1);
            message = list[pg];
            //}
            // 取帖子回复弹窗
            url = $"{ForumConfig.RCWDomain}forum.php?mod=post&action=reply&fid=2&tid={topic.t_Id}&infloat=yes&handlekey=reply&inajax=1&ajaxtarget=fwin_content_reply";
            html = http.SendRequest(url, refer, "get", "", "gbk");
            dom = CQ.CreateDocument(html);
            string action = dom.Find("#postform").Attr("action");
            string formhash = dom.Find("input[name='formhash']").Val();

            // 取帖子验证码
            url = $"{ForumConfig.RCWDomain}misc.php?mod=seccode&action=update&idhash=cSA0&{rnd.NextDouble()}&modid=undefined";
            html = http.SendRequest(url, refer, "get", "", "gbk", "application/javascript");

            // 取验证码图片
            string res = html.Substring(html.IndexOf("misc.php"));
            res = res.Substring(0, res.IndexOf("class") - 2);
            url = ForumConfig.RCWDomain + res;
            http.accept = "image/avif,image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8";
            Bitmap map = http.SendImageStreamRequest(url, refer, "image/png");

            FrmForumYZM frmForumYZM = new FrmForumYZM(new ForumRCWHelper(), map);
            DialogResult dr = frmForumYZM.ShowDialog();
            if (dr != DialogResult.OK)
            {
                return "抱歉，回复失败或用户取消回复!";
            }

            url = $"{ForumConfig.RCWDomain + action}&inajax=1";

            Encoding myEncoding = Encoding.GetEncoding("gbk");

            string values = $"formhash={formhash}&handlekey=reply&noticeauthor=&noticetrimstr=&noticeauthormsg=&usesig=0&subject=&file=&message={HttpUtility.UrlEncode(message, myEncoding)}&seccodehash=cSA0&seccodemodid=forum%3A%3Aajax&seccodeverify={HttpUtility.UrlEncode(frmForumYZM.YZM, myEncoding)}";

            html = http.SendRequest(url, refer, "post", values, "gbk", "application/x-www-form-urlencoded");
            return html;
        }
    }
}
