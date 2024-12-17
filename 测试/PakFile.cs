using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace 测试
{
    public class PakFile
    {
        public Dictionary<string, byte[]> Resources { get; private set; }

        public PakFile(string filePath, string password = null)
        {
            Resources = new Dictionary<string, byte[]>();
            LoadPakFile(filePath, password);
        }

        private void LoadPakFile(string filePath, string password)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                // 假设文件头
                string header = new string(reader.ReadChars(4)).Trim();
                if (header != "PAK ")
                {
                    throw new FormatException("Invalid PAK file format");
                }

                int resourceCount = reader.ReadInt32();
                for (int i = 0; i < resourceCount; i++)
                {
                    int nameLength = reader.ReadInt32();
                    string resourceName = new string(reader.ReadChars(nameLength));

                    int dataLength = reader.ReadInt32();
                    byte[] encryptedData = reader.ReadBytes(dataLength);

                    // 解密数据（如果有密码）
                    byte[] data = password != null ? Decrypt(encryptedData, password) : encryptedData;

                    Resources[resourceName] = data;
                }
            }
        }

        private byte[] Decrypt(byte[] encryptedData, string password)
        {
            using (Aes aes = Aes.Create())
            {
                // 简单处理，实际应用中应使用更安全的方式
                byte[] key = new byte[32]; // 256 bits
                Array.Copy(Encoding.UTF8.GetBytes(password.PadRight(32)), key, 32);
                aes.Key = key;
                aes.IV = new byte[16]; // 使用零向量（仅为示例）

                aes.Mode = CipherMode.CBC;

                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (MemoryStream msResult = new MemoryStream())
                    {
                        csDecrypt.CopyTo(msResult);
                        return msResult.ToArray();
                    }
                }
            }
        }

        public void SaveImages(string outputDirectory)
        {
            foreach (var resource in Resources)
            {
                try
                {
                    string filePath = Path.Combine(outputDirectory, resource.Key);

                    // 创建输出目录，如果需要的话
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    // 将字节数组转换为 Image
                    using (var ms = new MemoryStream(resource.Value))
                    {
                        using (Image image = Image.FromStream(ms))
                        {
                            image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png); // 保存为 PNG 格式
                        }
                    }

                    Console.WriteLine($"Saved {resource.Key}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving {resource.Key}: {ex.Message}");
                }
            }
        }
    }
}