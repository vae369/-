
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Application = System.Windows.Forms.Application;

namespace 测试
{
    public class HttpHelper
    {
        public string domain = "";
        public string Authorization = "";//JWT token
        public string accept = "";//http请求头
        public string location = "";//重定向地址
        public string user_agent = "";//用户请求
        CookieContainer sendcookies = null;//发送的cookies
        public CookieContainer outcookies = new CookieContainer();//返回的cookies

        public Dictionary<string, string> RequestHeaders = new Dictionary<string, string>();
        public Dictionary<string, string> ResponseHeaders = new Dictionary<string, string>();
        public HttpStatusCode StatusCode { get; set; }
        static readonly object o = new object();

        public HttpHelper()
        {
            domain = "http://101.34.17.168/Mir/";//外网
            //domain = "http://localhost:8090/Mir/";//本地测试
            //domain = "https://localhost:44356/Mir/";//本地调试

            //用于伪装成浏览器请求
            accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";

            //谷歌内核
            user_agent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.64 Safari/537.36";

            //edge内核
            //user_agent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36 Edg/124.0.0.0";
            //user_agent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36 Edg/109.0.1518.140";
        }

        public string SendRequest(string url, string method, string value)
        {
            if (string.IsNullOrWhiteSpace(url)) return "";
            ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            //创建http请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = user_agent;
            request.Accept = accept;
            request.Method = method;
            request.ContentType = "application/json";
            string result = "";//返回的结果

            StreamReader reader = null;
            HttpWebResponse response = null;//响应流对象

            try
            {
                if (method.ToUpper().Equals("POST"))
                {
                    //将URL编码后的字符串转化为字节
                    byte[] payload = System.Text.Encoding.UTF8.GetBytes(value);
                    //设置请求的 ContentLength 
                    request.ContentLength = payload.Length;
                    //获得请 求流
                    Stream writer = request.GetRequestStream();
                    //将请求参数写入流
                    writer.Write(payload, 0, payload.Length);
                    // 关闭请求流
                    writer.Close();
                }
                try
                {
                    // 获得响应流
                    response = (HttpWebResponse)request.GetResponse();
                    reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                }
                catch (WebException resEx)
                {
                    response = (HttpWebResponse)resEx.Response;
                }
                if (reader != null)
                {
                    result = reader.ReadToEnd();
                    reader.Close();
                }
                response.Close();
                request.Abort();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }


        /// <summary>
        /// 发送http请求
        /// </summary>
        /// <param name="url">请求的页面</param>
        /// <param name="referer">上一个页面</param>
        /// <param name="method">请求得类型[get,post]</param>
        /// <param name="postData">请求的参数[只有post才需要传]</param>
        /// <param name="encoding">请求的编码</param>
        /// <param name="contentType">上下文类型</param>
        /// <param name="ip">匿名IP</param>
        /// <param name="prot">匿名端口</param>
        /// <returns></returns>
        public string SendRequest(string url, string referer = "", string method = "get", string value = "", string encoding = "utf-8", string contentType = "text/html; charset=utf-8", string ip = "", string prot = "")
        {

            if (string.IsNullOrWhiteSpace(url)) return "";
            ResponseHeaders.Clear();
            sendcookies = new CookieContainer();

            ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            //这一句一定要写在创建连接的前面。使用回调的方法进行证书验证。
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.SystemDefault |
                                                   SecurityProtocolType.Tls | SecurityProtocolType.Tls11 |
                                                   SecurityProtocolType.Tls12;
            //创建http请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.AutomaticDecompression = DecompressionMethods.GZip;

            foreach (var key in RequestHeaders)
            {
                request.Headers.Add(key.Key, key.Value);
            }
            request.UserAgent = user_agent;
            request.Accept = accept;
            request.Timeout = 10000;
            request.Method = method;
            request.ContentType = contentType;
            //request.AllowAutoRedirect = false;
            request.Referer = referer;
            //request.ServicePoint.ConnectionLimit = int.MaxValue;

            request.CookieContainer = new CookieContainer();
            if (outcookies.Count > 0) sendcookies = outcookies;
            request.CookieContainer = sendcookies;

            //添加http代理,用于隐藏IP
            if (!string.IsNullOrEmpty(ip) && !string.IsNullOrEmpty(prot))
            {
                WebProxy wp = new WebProxy(ip, Convert.ToInt32(prot));
                request.Proxy = wp;
            }

            string result = "";//返回的结果

            StreamReader reader = null;
            HttpWebResponse response = null;//响应流对象
            Stream htmlStream = null;
            try
            {
                if (method.ToUpper().Equals("POST"))
                {
                    UTF8Encoding ec = new UTF8Encoding();
                    byte[] bytepostData = ec.GetBytes(value);
                    request.ContentLength = bytepostData.Length;
                    //发送数据 using结束代码段释放
                    using (Stream requestStm = request.GetRequestStream())
                    {
                        requestStm.Write(bytepostData, 0, bytepostData.Length);
                    }
                }
                try
                {
                    // 获得响应流
                    response = (HttpWebResponse)request.GetResponse();
                    StatusCode = response.StatusCode;

                    //从Internet资源返回数据流
                    htmlStream = response.GetResponseStream();
                    if (response.CharacterSet != null)
                    {
                        switch (response.CharacterSet.ToLower())
                        {
                            case "iso-8859-1": reader = new StreamReader(htmlStream, Encoding.GetEncoding(encoding)); break;
                            default: reader = new StreamReader(htmlStream, Encoding.GetEncoding(encoding)); break;
                        }
                    }
                    else
                    {
                        reader = new StreamReader(htmlStream, Encoding.GetEncoding(encoding));
                    }
                }
                catch (WebException resEx)
                {
                    response = (HttpWebResponse)resEx.Response;

                    if (response != null)
                    {
                        htmlStream = response.GetResponseStream();
                        reader = new StreamReader(htmlStream, Encoding.GetEncoding(encoding));
                    }
                }

                if (response != null)
                {
                    outcookies.Add(response.Cookies);
                    //string strCookie = response.Headers["Set-Cookie"];
                    //if (!string.IsNullOrEmpty(strCookie))
                    //{
                    //    string[] ary = strCookie.Split(';');
                    //    Cookie ck = null;
                    //    //outcookies = new CookieContainer();
                    //    for (int i = 0; i < ary.Length; i++)
                    //    {
                    //        ck = GetCookieFromString(ary[i].Trim(), response.ResponseUri.Host);
                    //        if (ck != null)
                    //        {
                    //            outcookies.Add(ck);
                    //        }
                    //    }
                    //}

                    foreach (var item in response.Headers)
                    {
                        ResponseHeaders.Add(item.ToString(), response.Headers[item.ToString()]);
                    }
                    location = response.Headers["Location"];
                }
                if (reader != null)
                {
                    result = reader.ReadToEnd();
                    reader.Close();
                    response.Close();
                }

                request.Abort();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;

        }


        #region 读取某一个 Cookie 字符串到 Cookie 变量中
        private static Cookie GetCookieFromString(string cookieString, string defaultDomain)
        {
            string[] ary = cookieString.Split(',');
            Hashtable hs = new Hashtable();
            for (int i = 0; i < ary.Length; i++)
            {
                string s = ary[i].Trim();
                int index = s.IndexOf("=");
                if (index > 0)
                {
                    hs.Add(s.Substring(0, index), s.Substring(index + 1));
                }
            }

            Cookie ck = new Cookie();

            foreach (object Key in hs.Keys)
            {
                string key = Key.ToString().ToLower();
                string value = hs[Key].ToString().ToLower();
                switch (key)
                {
                    case "path": ck.Path = hs[Key].ToString(); break;
                    case "max-age":
                    case "expires":
                        //ck.Expires = DateTime.Parse(hs[Key].ToString());
                        break;
                    case "domain": ck.Domain = hs[Key].ToString(); break;
                    default:
                        if (value != "none" && !string.IsNullOrEmpty(value))
                        {
                            ck.Name = Key.ToString();
                            ck.Value = hs[Key].ToString();
                        }
                        break;
                }

            }
            if (string.IsNullOrEmpty(ck.Name)) return null;
            if (string.IsNullOrEmpty(ck.Domain)) ck.Domain = defaultDomain;
            return ck;
        }
        #endregion

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">请求的页面</param>
        /// <param name="path">文件地址</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="refer">上一个页面</param>
        /// <param name="ip">匿名IP</param>
        /// <param name="prot">匿名端口</param>
        public void GetStream(string url, string path, string fileName, string refer = "", string ip = "", string prot = "")
        {
            if (string.IsNullOrWhiteSpace(url)) return;
            ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = user_agent;
            request.Accept = accept;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.CookieContainer = new CookieContainer();
            if (outcookies.Count > 0) sendcookies = outcookies;
            request.CookieContainer = sendcookies;
            request.Method = "GET";
            request.Referer = refer;
            request.KeepAlive = false;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ProtocolVersion = HttpVersion.Version11;
            request.ServicePoint.ConnectionLimit = int.MaxValue;
            //添加http代理,用于隐藏IP
            if (!string.IsNullOrEmpty(ip) && !string.IsNullOrEmpty(prot))
            {
                WebProxy wp = new WebProxy(ip, Convert.ToInt32(prot));
                request.Proxy = wp;
            }

            HttpWebResponse response;
            Stream resStream;
            response = (HttpWebResponse)request.GetResponse();
            resStream = response.GetResponseStream();

            int count = (int)response.ContentLength;
            int offset = 0;
            byte[] buf = new byte[count];
            while (count > 0)
            {
                int n = resStream.Read(buf, offset, count);
                if (n == 0)
                    break;
                count -= n;
                offset += n;
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string jsonFilePath = path + fileName;

            if (File.Exists(jsonFilePath))
                File.Delete(jsonFilePath);

            outcookies.Add(response.Cookies);

            FileStream fs = new FileStream(jsonFilePath, FileMode.Create, FileAccess.Write);
            fs.Write(buf, 0, buf.Length);
            fs.Flush();
            fs.Close();

        }


        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="url">请求的页面</param>
        /// <param name="referer">上一个页面</param>
        /// <param name="ip">匿名IP</param>
        /// <param name="prot">匿名端口</param>
        /// <returns></returns>
        public Bitmap SendImageStreamRequest(string url, string refer = "", string contentType = "image/jpg", string ip = "", string prot = "")
        {
            if (string.IsNullOrWhiteSpace(url)) return null;
            ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpWebRequest request = null;
            HttpWebResponse response = null;

            request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.AddRange(0, 10000000);
            request.CookieContainer = new CookieContainer();
            if (outcookies.Count > 0) sendcookies = outcookies;
            request.CookieContainer = sendcookies;
            request.ContentType = contentType;
            request.Referer = refer;
            request.Timeout = 150000;
            //添加http代理,用于隐藏IP
            if (!string.IsNullOrEmpty(ip) && !string.IsNullOrEmpty(prot))
            {
                WebProxy wp = new WebProxy(ip, Convert.ToInt32(prot));
                request.Proxy = wp;
            }

            request.Accept = accept;
            request.UserAgent = user_agent;
            request.Method = "GET";
            request.ServicePoint.ConnectionLimit = int.MaxValue;
            response = (HttpWebResponse)request.GetResponse();
            outcookies.Add(response.Cookies);
            Stream responseStream = response.GetResponseStream();
            Bitmap map = new Bitmap(responseStream, false);

            response.Close();
            responseStream.Close();
            return map;
        }

        /// <summary>
        /// 回调验证证书问题
        /// </summary>
        /// <param name="sender">流对象</param>
        /// <param name="certificate">证书</param>
        /// <param name="chain">X509Chain</param>
        /// <param name="errors">SslPolicyErrors</param>
        /// <returns>bool</returns>
        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="url">请求的页面</param>
        /// <param name="formData">上传的文件</param>
        /// <param name="callback">上传成功后回调</param>
        /// <param name="refer">上一个页面</param>
        /// <param name="ip">匿名IP</param>
        /// <param name="prot">匿名端口</param>
        /// <returns></returns>
        public void UploadRequest(string url, string filePath, Dictionary<string, string> formData, Action<string> callback, string refer = "", string ip = "", string prot = "")
        {
            if (string.IsNullOrWhiteSpace(url)) throw new Exception("url参数不能为空!");
            //ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            //这一句一定要写在创建连接的前面。使用回调的方法进行证书验证。
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            string timestamp = DateTime.Now.Ticks.ToString("x");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = new CookieContainer();
            if (outcookies.Count > 0) sendcookies = outcookies;
            request.CookieContainer = sendcookies;
            request.Accept = accept;
            request.UserAgent = user_agent;
            request.Referer = refer;
            request.Method = "POST";
            request.AllowWriteStreamBuffering = false;//对发送的数据不使用缓存

            request.ServicePoint.ConnectionLimit = int.MaxValue;
            request.ContentType = "multipart/form-data; boundary=" + timestamp;
            //添加http代理,用于隐藏IP
            if (!string.IsNullOrEmpty(ip) && !string.IsNullOrEmpty(prot))
            {
                WebProxy wp = new WebProxy(ip, Convert.ToInt32(prot));
                request.Proxy = wp;
            }

            //读取文件
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);


            try
            {
                if (!File.Exists(filePath)) throw new Exception("没有找到文件!");

                //表单信息
                StringBuilder from = new StringBuilder();

                string boundary = "--" + timestamp;
                string fromFormat = boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n";
                string fromEnd = boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\";\r\nContent-Type:application/octet-stream\r\n\r\n";

                foreach (var item in formData)
                {
                    from.Append(string.Format(fromFormat, item.Key, item.Value));
                }
                from.Append(string.Format(fromEnd, "file", Path.GetFileName(filePath)));
                byte[] postHeaderBytes = Encoding.UTF8.GetBytes(from.ToString());
                byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + timestamp + "--\r\n");
                long length = fileStream.Length + postHeaderBytes.Length + boundaryBytes.Length;
                request.ContentLength = length;//请求内容长度

                //每次上传4K
                int bufferLength = 4096;
                byte[] buffer = new byte[bufferLength];

                //已上传字节数
                long offset = 0;
                int size = binaryReader.Read(buffer, 0, bufferLength);
                Stream postStream = request.GetRequestStream();

                postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                while (size > 0)
                {
                    postStream.Write(buffer, 0, size);
                    offset += size;
                    size = binaryReader.Read(buffer, 0, bufferLength);
                }

                //添加尾部边界
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStreadm = new StreamReader(receiveStream, Encoding.UTF8);
                    string returnValue = readStreadm.ReadToEnd();
                    callback?.Invoke(returnValue);
                    outcookies.Add(response.Cookies);

                    receiveStream.Close();
                    readStreadm.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fileStream.Close();
                binaryReader.Close();
                //request.Abort();
            }


            return;
        }

        /// <summary>
        /// 利用phantomjs 爬取AJAX加载完成之后的页面
        /// JS脚本刷新时间间隔为1秒，防止页面AJAX请求时间过长导致数据无法获取
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetAjaxHtml(string url, HttpConfig config, int interval = 5000)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "Scripts\\phantomjs\\";
                ProcessStartInfo start = new ProcessStartInfo(path + "phantomjs.exe");//设置运行的命令行文件问ping.exe文件，这个文件系统会自己找到
                start.WorkingDirectory = path;

                ////设置命令参数
                string commond = string.Format("{0} {1} {2} {3} {4} {5}", path + "codes.js", url, interval, config.UserAgent, config.Accept, config.Referer);
                start.Arguments = commond;
                StringBuilder sb = new StringBuilder();
                start.CreateNoWindow = true;//不显示dos命令行窗口
                start.RedirectStandardOutput = true;//
                start.RedirectStandardInput = true;//
                start.UseShellExecute = false;//是否指定操作系统外壳进程启动程序
                Process p = Process.Start(start);

                StreamReader reader = p.StandardOutput;//截取输出流
                string line = reader.ReadToEnd();//每次读取一行
                string strRet = line;// sb.ToString();
                p.WaitForExit();//等待程序执行完退出进程
                p.Close();//关闭进程
                reader.Close();//关闭流
                return strRet;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }


        public static string GetWebBrowserByUrl(string url)
        {
            try
            {
                WebBrowser browser = new WebBrowser();
                browser.ScrollBarsEnabled = false;
                browser.ScriptErrorsSuppressed = true;
                browser.Size = new Size(1920, 1080);
                browser.Navigate(url);
                DateTime dtime = DateTime.Now;
                double timespan = 0;
                while (timespan < 10 || browser.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                    DateTime time2 = DateTime.Now;
                    timespan = (time2 - dtime).TotalSeconds;
                }

                string content = browser.DocumentText;

                return content;
                //LogHelper.Logs("测试", "测试消息", content);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 获取 Cookies
        /// </summary>
        /// <param name="setCookie"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public string GetCookies(string setCookie, Uri uri)
        {
            //获取所有Cookie
            var strCookies = string.Empty;
            var cookies = GetCookiesByHeader(setCookie);
            foreach (Cookie cookie in cookies)
            {
                //忽略过期Cookie
                if (cookie.Expires < DateTime.Now && cookie.Expires != DateTime.MinValue)
                {
                    continue;
                }
                if (uri.Host.Contains(cookie.Domain))
                {
                    strCookies += $"{cookie.Name}={cookie.Value}; ";
                }
            }
            return strCookies;
        }

        /// <summary>
        /// 通过Name 获取 Cookie Value
        /// </summary>
        /// <param name="setCookie">Cookies</param>
        /// <param name="name">Name</param>
        /// <returns></returns>
        public string GetCookieValueByName(string setCookie, string name)
        {
            var regex = new Regex($"(?<={name}=).*?(?=; )");
            return regex.IsMatch(setCookie) ? regex.Match(setCookie).Value : string.Empty;
        }
        /// <summary>
        /// 解析Cookie
        /// </summary>
        private readonly Regex RegexSplitCookie2 = new Regex(@"[^,][\S\s]+?;+[\S\s]+?(?=,\S)");

        /// <summary>
        /// 获取所有Cookie 通过Set-Cookie
        /// </summary>
        /// <param name="setCookie"></param>
        /// <returns></returns>
        public CookieCollection GetCookiesByHeader(string setCookie)
        {
            var cookieCollection = new CookieCollection();
            //拆分Cookie
            //var listStr = RegexSplitCookie.Split(setCookie);
            setCookie += ",T";//配合RegexSplitCookie2 加入后缀
            var listStr = RegexSplitCookie2.Matches(setCookie);
            //循环遍历
            foreach (Match item in listStr)
            {
                //根据; 拆分Cookie 内容
                var cookieItem = item.Value.Split(';');
                var cookie = new Cookie();
                for (var index = 0; index < cookieItem.Length; index++)
                {
                    var info = cookieItem[index];
                    //第一个 默认 Cookie Name
                    //判断键值对
                    if (info.Contains("="))
                    {
                        var indexK = info.IndexOf('=');
                        var name = info.Substring(0, indexK).Trim();
                        var val = info.Substring(indexK + 1);
                        if (index == 0)
                        {
                            cookie.Name = name;
                            cookie.Value = val;
                            continue;
                        }
                        if (name.Equals("Domain", StringComparison.OrdinalIgnoreCase))
                        {
                            cookie.Domain = val;
                        }
                        else if (name.Equals("Expires", StringComparison.OrdinalIgnoreCase))
                        {
                            DateTime.TryParse(val, out var expires);
                            cookie.Expires = expires;
                        }
                        else if (name.Equals("Path", StringComparison.OrdinalIgnoreCase))
                        {
                            cookie.Path = val;
                        }
                        else if (name.Equals("Version", StringComparison.OrdinalIgnoreCase))
                        {
                            cookie.Version = Convert.ToInt32(val);
                        }
                    }
                    else
                    {
                        if (info.Trim().Equals("HttpOnly", StringComparison.OrdinalIgnoreCase))
                        {
                            cookie.HttpOnly = true;
                        }
                    }
                }
                cookieCollection.Add(cookie);
            }
            return cookieCollection;
        }

        public string CleanHtml(string strHtml)
        {
            strHtml = Regex.Replace(strHtml, @"(\<script(.+?)\</script\>)|(\<style(.+?)\</style\>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //删除标签
            var r = new Regex(@"</?[^>]*>", RegexOptions.IgnoreCase);
            Match m;
            for (m = r.Match(strHtml); m.Success; m = m.NextMatch())
            {
                strHtml = strHtml.Replace(m.Groups[0].ToString(), "");
            }
            return strHtml.Trim();
        }
    }

    public class HttpConfig
    {
        /// <summary>
        /// 网站cookie信息
        /// </summary>
        public string Cookie { get; set; }

        /// <summary>
        /// 页面Referer信息
        /// </summary>
        public string Referer { get; set; }

        /// <summary>
        /// 默认(text/html)
        /// </summary>
        public string ContentType { get; set; }

        public string Accept { get; set; }

        public string AcceptEncoding { get; set; }

        /// <summary>
        /// 超时时间(毫秒)默认100000
        /// </summary>
        public int Timeout { get; set; }

        public string UserAgent { get; set; }

        /// <summary>
        /// POST请求时，数据是否进行gzip压缩
        /// </summary>
        public bool GZipCompress { get; set; }

        public bool KeepAlive { get; set; }

        public string CharacterSet { get; set; }

        public HttpConfig()
        {
            this.Timeout = 100000;
            this.ContentType = "text/html; charset=" + Encoding.UTF8.WebName;

            this.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/110.0.0.0 Safari/537.36";
            this.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
            this.AcceptEncoding = "gzip,deflate";
            this.GZipCompress = false;
            this.KeepAlive = true;
            this.CharacterSet = "UTF-8";
        }
    }



}
