using CsQuery;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace 测试
{
    public class biaomeiHepler
    {
        string domain = "https://www.biaomei.vip/";
        Random rnd = new Random();
        HttpHelper http = new HttpHelper();
        UnCodebase uc = null;
        public bool GetBiaoMeiYZMPIC(string path)
        {
            bool flag = false;
            try
            {
                string refer = domain;
                // 取登录页
                string url = $"{domain}member.php?mod=logging&action=login&phonelogin=no&infloat=yes&handlekey=login&inajax=1&ajaxtarget=fwin_content_login";
                string html = http.SendRequest(url, refer, "get", "", "utf-8", "");

                string idhash = html.Substring(html.IndexOf("updateseccode") + 15, 6);

                // 取验证码页
                url = $"{domain}misc.php?mod=seccode&action=update&idhash={idhash}&modid=member::logging&{rnd.NextDouble()}";
                html = http.SendRequest(url, refer, "get", "", "utf-8", "application/javascript");

                // 取验证码图片
                string res = html.Substring(html.IndexOf("misc.php"));
                res = res.Substring(0, res.IndexOf("class") - 2);
                url = domain + res;
                http.accept = "image/avif,image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8";
                Bitmap map = http.SendImageStreamRequest(url, refer, "image/png");
                uc = new UnCodebase(map);
                map = uc.GrayByPixels();
                map = uc.GrayTranWhite(map);
                // 图片存本地
                map.Save(path, System.Drawing.Imaging.ImageFormat.Png);

                flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }
    }
}
