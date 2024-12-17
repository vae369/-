using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Web;
using System.Net;
using System.Collections;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace Mir.Core.file
{
    public class FileHelper
    {


        #region 日志记录
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="errMsg">信息</param>
        public static void Log(string errMsg)
        {

            try
            {
                string pathDir = string.Format("{0}{1}", System.AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yy-MM-dd"));
                if (!Directory.Exists(pathDir))
                {
                    Directory.CreateDirectory(pathDir);
                }
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("\r\n时间：{0}    信息：{1}\r\n", DateTime.Now.ToString(), errMsg);
                string path = string.Format("{0}/log.txt", pathDir);

                // 自动判断文件编码
                string encoding = DetectFileEncoding(path);


                File.AppendAllText(path, sb.ToString(), Encoding.GetEncoding(encoding));
            }
            catch (IOException ex)
            {
                string path = string.Format("{0}syslog.txt", System.AppDomain.CurrentDomain.BaseDirectory);

                // 自动判断文件编码
                string encoding = DetectFileEncoding(path);

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("时间：{0}    信息：[{1}----{2}----{3}]\r\n", DateTime.Now.ToString(), ex.Message, ex.Source, ex.StackTrace);
                File.AppendAllText(path, sb.ToString(), Encoding.GetEncoding(encoding));
            }
        }
        /// <summary>
        /// 添加文本
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        public static void AppendLine(string path, string content)
        {
            try
            {
                string strPath = Path.GetDirectoryName(path);
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }

                // 自动判断文件编码
                string encoding = DetectFileEncoding(path);


                File.AppendAllText(path, content + "\r\n", Encoding.GetEncoding(encoding));
            }
            catch (Exception e)
            {
                Log(string.Format("{0}----{1}----{2}", e.Message, e.Source, e.StackTrace));
            }
        }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        public static List<string> GetFileInfo(string path)
        {
            List<string> list = new List<string>();
            if (!File.Exists(path))
                return null;

            // 自动判断文件编码
            string encoding = DetectFileEncoding(path);


            StreamReader sr = new StreamReader(path, Encoding.GetEncoding(encoding));
            string s;
            list.Clear();
            while ((s = sr.ReadLine()) != null)
            {
                list.Add(s);
            }
            sr.Close();
            sr.Dispose();
            return list;
        }
        /// <summary>
        /// 添加文本
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        public static void AppendLine2(string path, string content)
        {
            // 自动判断文件编码
            string encoding = DetectFileEncoding(path);

            StreamReader sr = new StreamReader(path, Encoding.GetEncoding(encoding));

            File.AppendAllText(path, content + "\r\n", Encoding.GetEncoding(encoding));
        }
        /// <summary>
        /// 添加内容，文件不存在则创建，存在则覆盖
        /// </summary>
        /// <param name="path"></param>
        /// <param name="strCnt"></param>
        public static void WriteAllText(string path, string strCnt)
        {
            // 自动判断文件编码
            //string encoding = DetectFileEncoding(path);

            // 自动判断文件编码
            string FileEncodingStr = Mir.Core.utils.ConfigHelper.GetAppConfig("FileEncodingStr");
            Encoding ed = null;
            switch (FileEncodingStr)
            {
                case "系统默认": ed = Encoding.Default; break;
                case "ASCII": ed = Encoding.ASCII; break;
                case "UNICODE": ed = Encoding.Unicode; break;
                case "UTF8": ed = Encoding.UTF8; break;
                case "UTF8BOM": ed = new UTF8Encoding(true); break;
                case "GBK": ed = Encoding.GetEncoding("gbk"); break;
                case "GB2312": ed = Encoding.GetEncoding("gb2312"); break;
                case "GB18030": ed = Encoding.GetEncoding("gb18030"); break;
                default: ed = Encoding.Default; break;
            }

            File.WriteAllText(path, strCnt, ed);
        }
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="errMsg">内容</param>
        public static void WriteLine(string path, string strCnt)
        {
            try
            {
                // 自动判断文件编码
                string encoding = DetectFileEncoding(path);

                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrEmpty(strCnt))
                    sb.AppendFormat("{0}\r\n", strCnt);
                File.AppendAllText(path, sb.ToString(), Encoding.GetEncoding(encoding));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 遍历文件夹中的子文件

        /// <summary>
        /// 获取目录下的子目录的完整路径
        /// </summary>
        /// <param name="path">查找目录</param>
        /// <returns>子文件路径</returns>
        public static List<string> GetFileDirectory(string path)
        {
            if (!Directory.Exists(path))
                return null;
            DirectoryInfo theFolder = new DirectoryInfo(path);
            DirectoryInfo[] folders = theFolder.GetDirectories();
            List<string> list = new List<string>();
            if (folders.Length <= 0)
                return null;
            foreach (DirectoryInfo di in folders)
            {
                list.Add(di.FullName);
            }
            return list;
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>是否删除</returns>
        public static bool DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (IOException e)
                {
                    Log(e.Message);
                    return false;
                }
            }
            return true;
        }

        public static List<string> GetStreamByText(string path)
        {
            List<string> list = new List<string>();
            if (!File.Exists(path))
                return null;
            // 自动判断文件编码
            string encoding = DetectFileEncoding(path);

            StreamReader sr = new StreamReader(path, Encoding.GetEncoding(encoding));

            string s;
            list.Clear();
            while ((s = sr.ReadLine()) != null)
            {
                list.Add(s);
            }
            sr.Close();
            sr.Dispose();
            return list;
        }
        #endregion

        #region 读文件
        /// <summary>
        /// 读取txt第一行
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadTxtLastLine(string path)
        {
            if (!File.Exists(path))
                return string.Empty;

            // 自动判断文件编码
            string encoding = DetectFileEncoding(path);

            List<string> list = new List<string>();
            StreamReader sr = new StreamReader(path, Encoding.GetEncoding(encoding));
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                list.Add(s);
            }
            sr.Close();
            sr.Dispose();
            if (list.Count > 0)
                return list[list.Count - 1];
            return string.Empty;
        }

        /// <summary>
        /// 读取txt,返回List每行
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> ReadTxtReturnList(string path)
        {
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            if (!File.Exists(path))
            {
                FileHelper.WriteLine(path, "");
            }
            List<string> list = new List<string>();

            // 自动判断文件编码
            string FileEncodingStr = Mir.Core.utils.ConfigHelper.GetAppConfig("FileEncodingStr");
             
            Encoding ed = null;
            switch (FileEncodingStr)
            {
                case "系统默认": ed = Encoding.Default; break;
                case "ASCII": ed = Encoding.ASCII; break;
                case "UNICODE": ed = Encoding.Unicode; break;
                case "UTF8": ed = Encoding.UTF8; break;
                case "UTF8BOM": ed = new UTF8Encoding(true); break;
                case "GBK": ed = Encoding.GetEncoding("gbk"); break;
                case "GB2312": ed = Encoding.GetEncoding("gb2312"); break;
                case "GB18030": ed = Encoding.GetEncoding("gb18030"); break;
                default: ed = Encoding.Default; break;
            }

            StreamReader sr = new StreamReader(path, ed);
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                //if (!string.IsNullOrWhiteSpace(s))
                list.Add(s);
            }
            sr.Close();
            sr.Dispose();
            return list;
        }

        /// <summary>
        /// 读取txt
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadTxt(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    FileHelper.WriteLine(path, "");
                }
                string res = "";
                string FileEncodingStr = Mir.Core.utils.ConfigHelper.GetAppConfig("FileEncodingStr");
                Encoding ed = null;
                switch (FileEncodingStr)
                {
                    case "系统默认": ed = Encoding.Default; break;
                    case "ASCII": ed = Encoding.ASCII; break;
                    case "UNICODE": ed = Encoding.Unicode; break;
                    case "UTF8": ed = Encoding.UTF8; break;
                    case "UTF8BOM": ed = new UTF8Encoding(true); break;
                    case "GBK": ed = Encoding.GetEncoding("gbk"); break;
                    case "GB2312": ed = Encoding.GetEncoding("gb2312"); break;
                    case "GB18030": ed = Encoding.GetEncoding("gb18030"); break;
                    default: ed = Encoding.Default; break;
                }

                StreamReader sr =  new StreamReader(path, ed);
                
                res = sr.ReadToEnd();
                return res;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        static string DetectFileEncoding(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return "UTF-8";
            }
            byte[] buffer = new byte[4096];
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                int readBytes = fileStream.Read(buffer, 0, buffer.Length);

                if (readBytes == 0)
                {
                    Console.WriteLine("文件为空。");
                    return null; // 或根据需要抛出异常
                }

                string detectedEncodingName = null;

                // 遍历常用编码列表，尝试解码文件内容
                foreach (var encoding in GetCommonEncodings())
                {
                    try
                    {
                        string decodedString = encoding.GetString(buffer, 0, readBytes);
                        if (!decodedString.Contains("\ufffd")
                            && !decodedString.Contains("\\uFFFD")
                            && !decodedString.Contains("?")
                            && !decodedString.Contains("\u312f")
                            )
                        {
                            // 确定编码后，将正确名称赋值给detectedEncodingName，并跳出循环
                            detectedEncodingName = encoding == Encoding.UTF8 ? "UTF-8" : encoding.BodyName;
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"使用 {encoding.EncodingName} 解码时出错: {ex.Message}");
                    }
                }

                if (detectedEncodingName == null)
                    detectedEncodingName = "UTF-8";

                return detectedEncodingName;
            }
        }

        public static void ConvertFileEncoding(string filePath, Encoding newEncoding)
        {
            // 读取文件内容并使用默认编码
            string content = File.ReadAllText(filePath, Encoding.UTF8);

            // 将读取的内容按照新编码写回文件
            File.WriteAllText(filePath, content, newEncoding);
        }

        public static bool ContainsChinese(string input)
        {
            return Regex.IsMatch(input, @"[\u4e00-\u9fa5]");
        }

        public static string ConvertFileEncoding(string filePath, Encoding ed, string encodingStr, bool isBak)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            string pathTemp = fileInfo.FullName.Insert(fileInfo.FullName.LastIndexOf('.'), "_bak");
            StreamReader sr = null;
            StreamWriter sw = null;
            if (ed != null)
            {
                sr = new StreamReader(filePath, ed);
                string str = sr.ReadToEnd();
                sr.Close();

                if (isBak) sw = new StreamWriter(pathTemp, false, ed);
                else sw = new StreamWriter(filePath, false, ed);
                sw.Write(str);
            }
            else
            {
                switch (encodingStr)
                {
                    case "utf-8 bom": ed = new UTF8Encoding(true); break;
                    case "gbk": ed = Encoding.GetEncoding("gbk"); break;
                    case "gb2312": ed = Encoding.GetEncoding("gb2312"); break;
                    case "gb18030": ed = Encoding.GetEncoding("gb18030"); break;
                }

                sr = new StreamReader(filePath, ed);
                string str = sr.ReadToEnd();
                sr.Close();

                if (isBak) sw = new StreamWriter(pathTemp, false, ed);
                else sw = new StreamWriter(filePath, false, ed);
                sw.Write(str);
            }
            sw.Close();
            if (ed != null)
            {
                sr = new StreamReader(filePath, ed);
            }
            else
            {
                sr = new StreamReader(pathTemp, Encoding.GetEncoding(encodingStr));
            }
            string res = sr.ReadToEnd();

            sr.Close();
            return res;
        }

        /// <summary>
        /// 获取常用的文本编码列表。
        /// </summary>
        /// <returns>包含常用编码的数组。</returns>
        private static Encoding[] GetCommonEncodings()
        {
            return new[]
            {
                Encoding.UTF8,
                Encoding.Unicode,
                Encoding.BigEndianUnicode,
                Encoding.UTF32,
                Encoding.ASCII,
                Encoding.Default // 此项结果可能因系统区域设置不同而变化
            };
        }
        #endregion

    }
}
