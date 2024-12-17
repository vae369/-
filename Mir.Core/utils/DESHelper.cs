using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mir.Core.utils
{
    public class DESKey
    {
        /// <summary>
        /// Des默认密钥向量
        /// </summary>
        public static byte[] IV = { 0x13, 0x35, 0x16, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// <summary>
        /// Des加解密钥必须8位
        /// </summary>
        public const string Key = "mir.scrp";
    }

    public class DESHelper
    {
        public static string GetMD5Hash(string input)
        {
            // 创建一个MD5对象
            using (MD5 md5 = MD5.Create())
            {
                // 将输入字符串转换为字节数组
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                // 计算输入字节数组的哈希值
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                // 将字节数组转换为字符串
                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.AppendFormat("{0:x2}", b);
                }
                return hashString.ToString();
            }
        }

        /// <summary>
        /// Des加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">des密钥，长度必须8位</param>
        /// <param name="iv">密钥向量</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptDes(string source, string key, byte[] iv)
        {
            using (DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider())
            {
                byte[] rgbKeys = GetDesKey(key),
                    rgbIvs = iv,
                    inputByteArray = Encoding.UTF8.GetBytes(source);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desProvider.CreateEncryptor(rgbKeys, rgbIvs), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                        cryptoStream.FlushFinalBlock();
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// Des解密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">des密钥，长度必须8位</param>
        /// <param name="iv">密钥向量</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptDes(string source, string key, byte[] iv)
        {
            using (DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider())
            {
                byte[] rgbKeys = GetDesKey(key), rgbIvs = iv, inputByteArray = Convert.FromBase64String(source);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desProvider.CreateDecryptor(rgbKeys, rgbIvs), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                        cryptoStream.FlushFinalBlock();
                        return Encoding.UTF8.GetString(memoryStream.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// 获取Des8位密钥
        /// </summary>
        /// <param name="key">Des密钥字符串</param>
        /// <returns>Des8位密钥</returns>
        private static byte[] GetDesKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "Des密钥不能为空");
            }
            if (key.Length > 8)
            {
                key = key.Substring(0, 8);
            }
            if (key.Length < 8)
            {
                // 不足8补全
                key = key.PadRight(8, '0');
            }
            return Encoding.UTF8.GetBytes(key);
        }
         
    }
}
