
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Mir.Core.utils
{
    public class TypeParse
    {
        
        /// <summary>
        /// 转换为Int类型
        /// </summary>
        /// <param name="val">要转换的值</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns>返回值</returns>
        public static int ObjToInt(object val, int defaultVal)
        {
            try
            {
                if (val == null)
                    return -1;
                return Convert.ToInt32(val);
            }
            catch
            {
                return defaultVal;
            }
        }

        /// <summary>
        /// 将千分位字符串转换成数字
        /// 说明：将诸如"–111,222,333的千分位"转换成-111222333数字
        /// 若转换失败则返回-1
        /// </summary>
        /// <param name="val">需要转换的千分位</param>
        /// <returns>数字</returns>
        public static int ObjToInt(string val)
        {
            double _value = -1;
            if (!string.IsNullOrEmpty(val))
            {
                try
                {
                    double.TryParse(val, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"), out _value);

                    //_value = int.Parse(val, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                }
                catch (Exception ex)
                {
                    _value = -1;
                    Debug.WriteLine(string.Format("将千分位字符串{0}转换成数字异常，原因:{0}", val, ex.Message));
                }
            }
            return (int)_value;
        }


        /// <summary>
        /// 转换为Long类型
        /// </summary>
        /// <param name="val">要转换的值</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns>返回值</returns>
        public static long ObjToLong(object val, long defaultVal)
        {
            try
            {
                return Convert.ToInt64(val);
            }
            catch
            {
                return defaultVal;
            }
        }

        /// <summary>
        /// 转换为float类型
        /// </summary>
        /// <param name="val">要转换的值</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns>返回值</returns>
        public static float ObjToFloat(object val, float defaultVal)
        {
            try
            {
                if (val == null)
                    return Convert.ToSingle(-1);
                return Convert.ToSingle(val);
            }
            catch
            {
                return defaultVal;
            }
        }

        /// <summary>
        /// 字符串转字节
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static byte[] StrToByte(string strValue)
        {
            byte[] bt = System.Text.Encoding.Default.GetBytes(strValue);
            return bt;
        }

        /// <summary>
        /// 字节转字符串
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string ByteToStr(byte[] strValue)
        {
            string result = "";
            for (int i = 0; i < strValue.Length; i++)
            {
                result += strValue[i];
            }
            return result;
        }
        /// <summary>
        /// 转换为字符串，默认为空
        /// </summary>
        /// <param name="val">object值</param>
        /// <returns>返回值</returns>
        public static string ObjToString(object val)
        {
            try
            {
                string result = val == null ? string.Empty : val.ToString();
                return result;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// string型转换为datetime型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的datetime类型结果</returns>
        public static DateTime ObjToDateTime(object strValue, DateTime defValue)
        {
            if (strValue != null)
            {
                DateTime DateTimeValue;
                if (DateTime.TryParse(strValue.ToString(), out DateTimeValue))
                    return DateTimeValue;
                else
                    return defValue;
            }
            return defValue;
        }

        /// <summary>
        /// 替换HTML标签
        /// </summary>
        /// <param name="html">需要替换的字符串</param>
        /// <param name="length">替换的长度</param>
        /// <returns></returns>
        public static string ReplaceHtmlTag(string html, int length = 0)
        {
            string strText = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
            strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");

            if (length > 0 && strText.Length > length)
                return strText.Substring(0, length);

            return strText;
        }

        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="d">double 型数字</param>
        /// <returns>DateTime</returns>
        public static DateTime ConvertIntDateTime(string timeStamp)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            double ts = double.Parse(timeStamp) / 1000;
            return epoch.AddSeconds(ts).ToLocalTime();
        }

        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>double</returns>
        public static long ConvertDateTimeInt(DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (long)(time - startTime).TotalSeconds * 1000;
        }
    }
}
