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
using static Spire.Xls.Core.Spreadsheet.XlsWorkbook;
using static System.Net.WebRequestMethods;

namespace Mir2.Helper
{
    public class ForumCQSCHelper : IForumBase
    {
        HttpHelper http = new HttpHelper();
        Random rnd = new Random();
        string loginhash = "", formhash = "", idhash = "";
        string refer = ForumConfig.SCDomain + "forum.php";
        string setCookie = "";
        List<string> listFace = new List<string>();
        List<string> listSingin = new List<string>();
        public ForumCQSCHelper()
        {
            for (int i = 25; i < 41; i++)
            {
                listFace.Add("{:2_" + i + ":}");
            }
            for (int i = 41; i < 65; i++)
            {
                listFace.Add("{:3_" + i + ":}");
            }
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/111.gif[/img]\n来给我抱抱、亲爱的素材我来啦");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/114.gif[/img]\n还记得当年大明湖畔的大萝卜吗？");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/117.gif[/img]\n这个素材真的不错、没错、就是你、站住别跑！");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/129.gif[/img]\n有了这个素材、我就可以实现版本大变身、玩家充值嗷嗷起来~");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/124.gif[/img]\n看楼主一本正经的胡扯、就知道这个素材一定是被扒拉下来的");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/17.gif[/img]\n这个素材找很久了、爱你");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/130.gif[/img]\n楼主你信不信再逼我我就充钱给你看！！！！");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/12.gif[/img]\n什么传奇素材来的、对我来说、没上手的都是浮云");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/15.gif[/img]\n在五百皮论坛里、我能拿素材淹死你");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/115.gif[/img]\n充值的钱到现在都用不完、我也是醉了、站长把价格卖贵点啊！");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/120.gif[/img]\n很好很好、传奇素材下载、就是五百皮");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/112.gif[/img]\\n啥也别说了、拿起素材咱俩跑路吧！");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/20.gif[/img]\n这个传奇素材帖子很无聊、建议站长把他关起来");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/128.gif[/img]\n静静的看着你们装逼、告诉你们这个素材对我来说没什么起伏~");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/13.gif[/img]\n这个素材不巧刚好我很喜欢、拿走拿走~");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/116.gif[/img]\n这个价格太便宜了、再多加两个0我就买了！");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/121.gif[/img]\n楼主你好、借一步说话、有句红包当讲不当讲");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/16.gif[/img]\n我想把楼主打的满地找牙、");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/127.gif[/img]\n亲爱的金币不要离开我、为什么下载素材就差一点点金币");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/119.jpg[/img]\n告诉你、这个论坛里面、要是说素材、我敢说第一没人敢说第二");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/118.gif[/img]\n叫你不听话、传奇素材网教你怎么回复");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/126.gif[/img]\n这样的素材你都能搞的到、楼主你真的很刁！无敌叼");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/125.gif[/img]\n传奇素材网里的东西、只要是看上的、毫不犹豫、拖走！");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/123.gif[/img]\n你这个帖子发的我无言以对啊楼主、素材哪里");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/122.gif[/img]\n传奇脚本下载、传奇素材网、五百皮");
            listFace.Add($"[img]{ForumConfig.SCDomain}source/plugin/nciaer_facecomment/template/images/113.gif[/img]\n这个传奇素材网真的良心站,东西确实便宜");

            listSingin.Add("kx");
            listSingin.Add("ng");
            listSingin.Add("ym");
            listSingin.Add("wl");
            listSingin.Add("nu");
            listSingin.Add("ch");
            listSingin.Add("fd");
            listSingin.Add("yl");
            listSingin.Add("shuai");

        }
        public bool AutoSingIn(UserInfo user)
        {
            bool flag = true;
            string url = $"{ForumConfig.SCDomain}dsu_paulsign-sign.html";

            string html = http.SendRequest(url, refer, "get", "", "gbk");
            CQ dom = CQ.CreateDocument(html);
            formhash = dom.Find("input[name='formhash']").Val();
            int count = dom.Find(".qdsmile img").Count();
            if (count > 0)
            {
                int n = rnd.Next(0, listSingin.Count - 1);
                string qdxq = listSingin[n];
                url = $"{ForumConfig.SCDomain}plugin.php?id=dsu_paulsign:sign&operation=qiandao&infloat=1&inajax=1";
                html = http.SendRequest(url, refer, "post", $"formhash={formhash}&qdxq="+ qdxq, "gbk", "application/x-www-form-urlencoded");
                if (html.IndexOf("恭喜你签到成功") > 0)
                    flag = true;
                else
                    flag = false;
            }
            return flag;
        }

        public UserInfo GetUserInfo(UserInfo user)
        {
            string url = $"{ForumConfig.SCDomain}forum-77-1.html";
            string html = http.SendRequest(url, refer, "get", "", "gbk");
            MatchCollection mc = Regex.Matches(html, $"space-uid-([0-9a-zA-Z]+).html");
            url = ForumConfig.SCDomain + mc[0].Value;
            html = http.SendRequest(url, refer, "get", "", "gbk");
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
                user.ReplyCount = int.Parse(dom.Select(".mbm li a")[1].InnerText.Substring(4));
                user.ThreadCount = int.Parse(dom.Select(".mbm li a")[2].InnerText.Substring(4));
                user.AuthMethod = dom.Find(".pf_l.cl.pbm.mbm li:first").Text().Substring(4);
                user.GroupName = dom.Find(".pbm.mbm.bbda.cl a")[3].InnerText;
                user.RegistTime = dom.Find("#pbbs li")[0].InnerText.Substring(4);
                user.FinalyTime = dom.Find("#pbbs li")[1].InnerText.Substring(4);
                user.RegistIP = dom.Find("#pbbs li")[2].InnerText.Substring(3);
                user.LastIP = dom.Find("#pbbs li")[3].InnerText.Substring(7);
                user.LastActiveTime = dom.Find("#pbbs li")[4].InnerText.Substring(6);
                user.LastPushTime = dom.Find("#pbbs li")[5].InnerText.Substring(6);
                user.space = dom.Find("#psts .pf_l li")[0].InnerText.Substring(4).Trim();
                user.jf = int.Parse(dom.Find("#psts .pf_l li")[1].InnerText.Substring(2).Trim());
                user.yb = int.Parse(dom.Find("#psts .pf_l li")[2].InnerText.Substring(2).Trim());
                user.jb = int.Parse(dom.Find("#psts .pf_l li")[3].InnerText.Substring(2).Trim());
                user.hdb = int.Parse(dom.Find("#psts .pf_l li")[5].InnerText.Substring(2).Trim());
            }

            return user;
        }

        public Bitmap GetVerCode()
        {
            // 取登录页
            string url = $"{ForumConfig.SCDomain}member.php?mod=logging&action=login&phonelogin=no&infloat=yes&handlekey=login&inajax=1&ajaxtarget=fwin_content_login";
            string html = http.SendRequest(url, refer, "get", "", "gbk");
            CQ dom = CQ.CreateDocument(html);
            loginhash = html.Substring(html.IndexOf("loginhash"), 15);
            formhash = dom.Find("input[name='formhash']").Val();
            idhash = html.Substring(html.IndexOf("updateseccode") + 15, 6);

            // 取验证码页
            url = $"{ForumConfig.SCDomain}misc.php?mod=seccode&action=update&idhash={idhash}&modid=member::logging&{rnd.NextDouble()}";
            html = http.SendRequest(url, refer, "get", "", "gbk", "application/javascript");

            // 取验证码图片
            string res = html.Substring(html.IndexOf("misc.php"));
            res = res.Substring(0, res.IndexOf("class") - 2);
            url = ForumConfig.SCDomain + res;
            http.accept = "image/avif,image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8";
            Bitmap map = http.SendImageStreamRequest(url, refer, "image/png");

            // 图片存本地
            string path = ForumConfig.StartupPath + "\\images\\yzm.png";
            map.Save(path, System.Drawing.Imaging.ImageFormat.Png);

            return map;
        }

        public int Login(string userName, string passWord, string verCode)
        {
            verCode = ForumConfig.SCDomain + "forum-77-1.html";

            string url = $"{ForumConfig.SCDomain}member.php?mod=logging&action=login&infloat=yes&handlekey=login&inajax=1&ajaxtarget=fwin_content_login";
            string html = http.SendRequest(url, refer, "get", "", "gbk");
            CQ dom = CQ.CreateDocument(html);
            formhash = dom.Find("input[name='formhash']").Val();
            string action = dom.Select("form").Attr("action");

            string values = $"formhash={formhash}&referer={HttpUtility.UrlEncode(verCode)}&username={HttpUtility.UrlEncode(userName)}&password={HttpUtility.UrlEncode(passWord)}&questionid=0&loginfield=username";
            url = $"{ForumConfig.SCDomain + action}";
            html = http.SendRequest(url, verCode, "post", values, "gbk", "application/x-www-form-urlencoded");
            if (html.Contains(verCode) || html.Contains("欢迎您回来"))
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


            string url = $"{ForumConfig.SCDomain}member.php?mod=logging&action=logout&formhash=" + formhash;
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
                int index = rnd.Next(0, listFace.Count - 1);

                if (index <= 39)
                    message = message + listFace[index];
                else
                    message = listFace[index];
            }
            string formhash = dom.Find("input[name='formhash']").Val();

            url = $"{ForumConfig.SCDomain}forum.php?mod=post&action=reply&fid=77&tid={topic.t_Id}&extra=page%3D1&replysubmit=yes&infloat=yes&handlekey=fastpost&inajax=1";

            Encoding myEncoding = Encoding.GetEncoding("gbk");

            string values = $"message={HttpUtility.UrlEncode(message, myEncoding)}&posttime={DateTimeHelper.GetTimeStamp(DateTime.Now)}&formhash={formhash}";

            html = http.SendRequest(url, refer, "post", values, "gbk", "application/x-www-form-urlencoded");
            return html;
        }
    }
}
