using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace 视频压缩工具
{
    public class OperateData
    {
        /// <summary>
        /// 文件项
        /// </summary>
        public static Dictionary<string, ZipEntity> FileItems { get; set; }


        /// <summary>
        /// 计算文件大小函数(保留两位小数),Size为字节大小
        /// </summary>
        /// <param name="size">初始文件大小</param>
        /// <returns></returns>
        public static string GetFileSize(long size)
        {
            var num = 1024.00; //byte

            if (size < num)
                return size + "B";
            if (size < Math.Pow(num, 2))
                return (size / num).ToString("f2") + "K"; //kb
            if (size < Math.Pow(num, 3))
                return (size / Math.Pow(num, 2)).ToString("f2") + "M"; //M
            if (size < Math.Pow(num, 4))
                return (size / Math.Pow(num, 3)).ToString("f2") + "G"; //G


            return (size / Math.Pow(num, 4)).ToString("f2") + "T"; //T
        }

        /// <summary>
        /// 时、分、秒转换
        /// </summary>
        /// <param name="time">总时间数</param>
        /// <returns></returns>
        public static string GetTime(long time)
        {
            TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(time));
            string str = "";
            if (ts.Hours > 0)
            {
                str = ts.Hours.ToString() + "时" + ts.Minutes.ToString() + "分" + ts.Seconds + "秒";
            }
            if (ts.Hours == 0 && ts.Minutes > 0)
            {
                str = ts.Minutes.ToString() + "分" + ts.Seconds + "秒";
            }
            if (ts.Hours == 0 && ts.Minutes == 0)
            {
                str = ts.Seconds + "秒";
            }
            return str;
        }
    }
}
