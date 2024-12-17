using System;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Management;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Mir.Core.utils
{
    /// <summary>
    /// 配置文件的读取和修改或插入
    /// </summary>
    public class ConfigHelper
    {

        ///<summary> 
        ///返回*.exe.config文件中appSettings配置节的value项 
        ///</summary> 
        ///<param name="strKey"></param> 
        ///<returns></returns> 
        public static string GetAppConfig(string strKey)
        {
            string file = System.Windows.Forms.Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                if (key == strKey)
                {
                    return config.AppSettings.Settings[strKey].Value.ToString();
                }
            }
            return null;
        }

        ///<summary> 
        ///在*.exe.config文件中appSettings配置节增加一对键值对 
        ///</summary> 
        ///<param name="newKey"></param> 
        ///<param name="newValue"></param> 
        public static void UpdateAppConfig(string newKey, string newValue)
        {
            string file = System.Windows.Forms.Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            bool exist = false;
            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                if (key == newKey)
                {
                    exist = true;
                }
            }
            if (exist)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            config.AppSettings.Settings.Add(newKey, newValue);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static string GetMachineCode()
        {
            string machineCode = getRNum();
            // 计算SHA256加密值
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // 计算输入文本的SHA256哈希值
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(machineCode));

                // 将字节转换为十六进制字符串
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("X2"));//转换大小写（“X”为大写值，“x”为小写值）
                }
                // 计算MD5哈希值
                string md5Hash = ComputeMD5Hash(builder.ToString());
                machineCode = md5Hash;
            }

            return machineCode;
        }

        /// <summary>
        //MD5加密（SHA256值）
        /// </summary>
        private static string ComputeMD5Hash(string input)
        {
            // 创建一个MD5CryptoServiceProvider对象
            using (MD5 md5Hash = MD5.Create())
            {
                // 计算输入字符串的MD5哈希值
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // 创建一个StringBuilder对象来存储哈希值的字符串表示形式
                StringBuilder sBuilder = new StringBuilder();

                // 遍历哈希值的每个字节，并将其格式化为十六进制字符串
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("X2"));//转换大小写（“X”为大写值，“x”为小写值）
                }

                // 返回十六进制字符串
                return sBuilder.ToString();
            }
        }

        /// <summary>
        //获取CPU信息
        /// </summary>
        public static string GetCpuInfo()
        {
            string strCpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
            foreach (ManagementObject myObject in myCpuConnection)
            {
                strCpu = myObject.Properties["Processorid"].Value.ToString();
                break;
            }
            return strCpu;
        }

        /// <summary>
        //获取硬盘信息
        /// </summary>
        public static string GetDiskInfo()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }

        /// <summary>
        /// 获取网卡MAC地址
        /// </summary>
        public static string GetNetCardMACAddress()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE ((MACAddress Is Not NULL) AND (Manufacturer <> 'Microsoft'))");
            string NetCardMACAddress = "";
            foreach (ManagementObject mo in searcher.Get())
            {
                NetCardMACAddress = mo["MACAddress"].ToString().Trim();
            }
            return NetCardMACAddress;

        }


        /// <summary>
        /// 生成24位机器码
        /// </summary>
        public static string getMNum()
        {
            string strNum = GetCpuInfo() + GetDiskInfo() + GetNetCardMACAddress();//获得24位Cpu+硬盘序列号+网卡地址
            string strMNum = strNum.Substring(0, 24);//从生成的字符串中取出前24个字符做为机器码
            return strMNum;
        }
        static int[] intCode = new int[127];//存储密钥
        static int[] intNumber = new int[25];//存机器码的Ascii值
        static char[] Charcode = new char[25];//存储机器码字
        public static void setIntCode()//给数组赋值小于10的数
        {
            for (int i = 1; i < intCode.Length; i++)
            {
                intCode[i] = i % 9;
            }
        }
        private static string getRNum()
        {
            setIntCode();//初始化127位数组
            string MNum = getMNum();//获取注册码
            for (int i = 1; i < Charcode.Length; i++)//把机器码存入数组中
            {
                Charcode[i] = Convert.ToChar(MNum.Substring(i - 1, 1));
            }
            for (int j = 1; j < intNumber.Length; j++)//把字符的ASCII值存入一个整数组中。
            {
                intNumber[j] = intCode[Convert.ToInt32(Charcode[j])] + Convert.ToInt32(Charcode[j]);
            }
            string strAsciiName = "";//用于存储注册码
            for (int j = 1; j < intNumber.Length; j++)
            {
                if (intNumber[j] >= 48 && intNumber[j] <= 57)//判断字符ASCII值是否0－9之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else if (intNumber[j] >= 65 && intNumber[j] <= 90)//判断字符ASCII值是否A－Z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else if (intNumber[j] >= 97 && intNumber[j] <= 122)//判断字符ASCII值是否a－z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else//判断字符ASCII值不在以上范围内
                {
                    if (intNumber[j] > 122)//判断字符ASCII值是否大于z
                    {
                        strAsciiName += Convert.ToChar(intNumber[j] - 10).ToString();
                    }
                    else
                    {
                        strAsciiName += Convert.ToChar(intNumber[j] - 9).ToString();
                    }
                }
            }
            return strAsciiName;//返回注册码

        }

        public static string GetIPAddr()
        {
            try
            {
                using (var client = new WebClient())
                {
                    string ipString = client.DownloadString("http://icanhazip.com");
                    return Regex.Match(ipString, @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}").Value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取IP地址时出错: {ex.Message}");
                return "0.0.0.0";
            }             
        }
    }
}