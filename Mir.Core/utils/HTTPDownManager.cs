using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mir.Core.utils
{
    public class HTTPDownManager
    {
        /// <summary>
        /// 下载实时返回下载进度
        /// </summary>
        /// <param name="URL">下载地址</param>
        /// <param name="savePath">本地存储地址</param>
        /// <param name="downAction">委托回调函数</param>
        public static void DownloadFileData(string URL, string savePath, Action<string, string, int> downAction, Action<string> downEndAction)
        {
            try
            {
                float percent = 0;
                string fileName = Path.GetFileName(savePath);
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength, totalDownloadedByte = 0;
                System.IO.Stream st = myrp.GetResponseStream(), so = new System.IO.FileStream(savePath, System.IO.FileMode.Create);
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    so.Write(by, 0, osize);
                    osize = st.Read(by, 0, (int)by.Length);
                    percent = (float)totalDownloadedByte / (float)totalBytes * 100;//当前位置
                    if (downAction != null) downAction(fileName, GetSize(totalBytes), (int)percent);//totalBytes 是总字节数
                }
                if (downEndAction != null) downEndAction(fileName);
                so.Close();
                st.Close();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private static string GetSize(double size)
        {
            String[] units = new String[] { "B", "KB", "MB", "GB", "TB", "PB" };
            double mod = 1024.0;
            int i = 0;
            while (size >= mod)
            {
                size /= mod;
                i++;
            }
            return Math.Round(size) + units[i];
        }
    }
}
