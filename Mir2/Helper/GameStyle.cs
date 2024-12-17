using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mir2.Helper
{
    public class GameStyle
    {
        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullName">命名空间.类型名</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string fullName)
        {
            Type o = Type.GetType(fullName);
            dynamic obj = Activator.CreateInstance(o, true);
            return (T)obj;//类型转换并返回
        }
        public static UIStyle BindStyle()
        {
            UIStyle st = new UIStyle();
            string stt = ConfigurationManager.AppSettings["StyleManager"];
            switch (stt.ToLower())
            {
                case "green": st = UIStyle.Green; break;
                case "gray": st = UIStyle.Gray; break;
                case "red": st = UIStyle.Red; break;
                case "purple": st = UIStyle.Purple; break;
                case "inherited": st = UIStyle.Inherited; break;
                case "black": st = UIStyle.Black; break;
                case "blue": st = UIStyle.Blue; break;
                case "colorful": st = UIStyle.Colorful; break;
                case "custom": st = UIStyle.Custom; break;
                case "darkblue": st = UIStyle.DarkBlue; break;
                case "layuigreen": st = UIStyle.LayuiGreen; break;
                case "layuiorange": st = UIStyle.LayuiOrange; break;
                case "layuired": st = UIStyle.LayuiRed; break;
                case "orange": st = UIStyle.Orange; break;
            }

            return st;
        }
    }
}
