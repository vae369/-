using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mir2.Helper
{
    public class ForumConfig
    {
        public static string StartupPath = System.Windows.Forms.Application.StartupPath;

        // 奇迹论坛配置
        public static string QJDomain = "https://www.biaomei.vip/";
        public static string QJFolderPath = StartupPath + "\\forum\\jslt";
        public static string QJUserinfoPath = StartupPath + "\\forum\\jslt\\userinfo.txt";
        public static string QJHuaShuPath = StartupPath + "\\forum\\jslt\\默认话术.txt";

        // 传奇帮论坛配置
        public static string CQBDomain = "https://www.sf2.net/";
        public static string CQBFolderPath = StartupPath + "\\forum\\cqblt";
        public static string CQBUserinfoPath = StartupPath + "\\forum\\cqblt\\userinfo.txt";
        public static string CQBHuaShuPath = StartupPath + "\\forum\\cqblt\\默认话术.txt";

        // 腾飞论坛配置
        public static string TFDomain = "https://www.sf05.com/";
        public static string TFFolderPath = StartupPath + "\\forum\\tflt";
        public static string TFUserinfoPath = StartupPath + "\\forum\\tflt\\userinfo.txt";
        public static string TFHuaShuPath = StartupPath + "\\forum\\tflt\\默认话术.txt";

        // 萝卜论坛配置
        public static string LBDomain = "https://bbs.0lb.com/";
        public static string LBFolderPath = StartupPath + "\\forum\\lblt";
        public static string LBUserinfoPath = StartupPath + "\\forum\\lblt\\userinfo.txt";
        public static string LBHuaShuPath = StartupPath + "\\forum\\lblt\\默认话术.txt";

        // 传奇素材配置
        public static string SCDomain = "https://www.500pi.com/";
        public static string SCFolderPath = StartupPath + "\\forum\\500pi";
        public static string SCUserinfoPath = StartupPath + "\\forum\\500pi\\userinfo.txt";
        public static string SCHuaShuPath = StartupPath + "\\forum\\500pi\\默认话术.txt";

        // 翎风论坛配置
        public static string LFDomain = "https://www.haom2.net/";
        public static string LFFolderPath = StartupPath + "\\forum\\lflt";
        public static string LFUserinfoPath = StartupPath + "\\forum\\lflt\\userinfo.txt";
        public static string LFHuaShuPath = StartupPath + "\\forum\\lflt\\默认话术.txt";

        // GM016论坛配置
        public static string GM016Domain = "https://www.gm016.com/";
        public static string GM016FolderPath = StartupPath + "\\forum\\gm016";
        public static string GM016UserinfoPath = StartupPath + "\\forum\\gm016\\userinfo.txt";
        public static string GM016HuaShuPath = StartupPath + "\\forum\\gm016\\默认话术.txt";
        
        // 如此玩论坛配置
        public static string RCWDomain = "https://www.ruciwan.com/";
        public static string RCWFolderPath = StartupPath + "\\forum\\rcwlt";
        public static string RCWUserinfoPath = StartupPath + "\\forum\\rcwlt\\userinfo.txt";
        public static string RCWHuaShuPath = StartupPath + "\\forum\\rcwlt\\默认话术.txt";

        // 东方版本库配置
        public static string DFDomain = "http://yy111.cn/";
        public static string DFFolderPath = StartupPath + "\\forum\\dfbbk";
        public static string DFUserinfoPath = StartupPath + "\\forum\\dfbbk\\userinfo.txt";
        public static string DFHuaShuPath = StartupPath + "\\forum\\dfbbk\\默认话术.txt";



        public static void ShowMessage(UIPanel lbMsg, string msg)
        {
            lbMsg.Invoke((EventHandler)delegate { lbMsg.Text = DateTime.Now.ToString() + ":" + msg; });
        }
    }
}
