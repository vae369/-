using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 视频压缩工具
{
    /// <summary>
    /// 压缩类
    /// </summary>
    public class ZipEntity
    {
        public ZipEntity() { }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 压缩文件的大小
        /// </summary>
        public long ZipSize { get; set; }

        /// <summary>
        /// 已用时间
        /// </summary>
        public string ElaTime { get; set; }

        /// <summary>
        /// 剩余时间
        /// </summary>
        public string RemTime { get; set; }

        /// <summary>
        /// 预计时间
        /// </summary>
        public long EstTime { get; set; }
        /// <summary>
        /// 压缩的文件类型
        /// </summary>
        public string ZipFileType { get; set; }

        /// <summary>
        /// 压缩进度
        /// </summary>
        public double ZipProgress { get; set; }

        /// <summary>
        /// 压缩状态
        /// </summary>
        public ZipState ZipState { get; set; }
        /// <summary>
        /// 是否占用
        /// </summary>
        public bool IsOcc { get; set; } = false;
        /// <summary>
        /// 文件地址
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 文件后缀名
        /// </summary>
        public string FileExtensions { get; set; }
        /// <summary>
        /// 压缩大小
        /// </summary>
        public int YSSize { get; set; }


        private object obj = new object();



        /// <summary>
        /// 压缩文件
        /// </summary>
        public void ZipFile()
        {
            lock (obj)
            {
                this.IsOcc = true;
                this.ZipState = ZipState.Process;
                string newPath = "File\\";
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                ZipFile(this.FilePath, "File", "", 9, this.YSSize);


                //Random random = new Random();
                //var time = random.Next(100) * 100;
                //for (int i = 0; i < time; i += 10)
                //{
                //    this.ZipProgress = (double)i % (double)time;
                //    Thread.Sleep(10);
                //}
                this.ZipState = ZipState.Finish;
            }

        }

        /// <summary>  
        /// 压缩单个文件  
        /// </summary>  
        /// <param name="fileToZip">要压缩的文件</param>  
        /// <param name="zipedFile">压缩后的文件全名</param>  
        /// <param name="compressionLevel">压缩程度，范围0-9，数值越大，压缩程序越高</param>  
        /// <param name="blockSize">分块大小</param>  
        public void ZipFile(string fileToZip, string zipedFile, int compressionLevel, long blockSize)
        {
            lock (obj)
            {
                if (!System.IO.File.Exists(fileToZip))//如果文件没有找到，则报错  
                {
                    throw new FileNotFoundException("哦豁，文件地址错误哦！");
                }

                FileStream streamToZip = new FileStream(fileToZip, FileMode.Open, FileAccess.Read);
                FileStream zipFile = File.Create(zipedFile);
                ZipOutputStream zipStream = new ZipOutputStream(zipFile);
                ZipEntry zipEntry = new ZipEntry(fileToZip);
                zipStream.PutNextEntry(zipEntry);
                zipStream.SetLevel(compressionLevel);
                byte[] buffer = new byte[blockSize];
                int size = streamToZip.Read(buffer, 0, buffer.Length);
                zipStream.Write(buffer, 0, size);

                try
                {
                    while (size < streamToZip.Length)
                    {
                        int sizeRead = streamToZip.Read(buffer, 0, buffer.Length);
                        zipStream.Write(buffer, 0, sizeRead);
                        size += sizeRead;

                        double resCount = double.Parse(string.Format("{0:N2} ", (((double)size / (double)streamToZip.Length) * (double)100)));
                        this.ZipProgress = resCount;
                        this.ZipState = ZipState.Process;
                    }
                }
                catch (Exception ex)
                {
                    GC.Collect();
                    throw ex;
                }

                zipStream.Finish();
                zipStream.Close();
                streamToZip.Close();
                GC.Collect();
            }

        }



        /// <summary>
        /// 单个文件进行压缩
        /// </summary>
        /// <param name="fileName">待压缩的文件（绝对路径）</param>
        /// <param name="compressedFilePath">压缩后文件路径（绝对路径）</param>
        /// <param name="aliasFileName">压缩文件的名称（别名）</param>
        /// <param name="compressionLevel">压缩级别0-9，默认为5</param>
        /// <param name="blockSize">缓存大小，每次写入文件大小，默认为2048字节</param>
        /// <param name="isEncrypt">是否加密，默认加密</param>
        /// <param name="encryptPassword">加密的密码（为空的时候，不加密）</param>
        public void ZipFile(string fileName, string compressedFilePath, string aliasFileName = "", int compressionLevel = 5,
               int blockSize = 2048, bool isEncrypt = false, string encryptPassword = "")
        {
            if (File.Exists(fileName) == false) throw new FileNotFoundException("未能找到当前文件！", fileName);
            try
            {
                string zipFileName = null;
                ///获取待压缩文件名称（带后缀名）
                string name = new FileInfo(fileName).Name;
                zipFileName = compressedFilePath + Path.DirectorySeparatorChar +
                    (string.IsNullOrWhiteSpace(aliasFileName) ? name.Substring(0, name.LastIndexOf(".")) : aliasFileName) + "." + this.ZipFileType;
                ///使用using语句，资源使用完毕，自动释放（类需继承IDispose接口）
                using (FileStream fs = File.Create(zipFileName))
                {
                    using (ZipOutputStream outStream = new ZipOutputStream(fs))
                    {
                        using (FileStream inStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                        {
                            ///zip文档的一个条目
                            ZipEntry entry = new ZipEntry(name);
                            ///压缩加密
                            if (isEncrypt)
                            {
                                outStream.Password = encryptPassword;
                            }
                            ///开始一个新的zip条目
                            outStream.PutNextEntry(entry);
                            ///设置压缩级别
                            outStream.SetLevel(compressionLevel);
                            ///缓冲区对象
                            byte[] buffer = new byte[blockSize];
                            ///读入缓冲区的总字节数，执行到最后读取为0时，则读取完毕
                            int sizeRead = 0;
                            int size = 0;

                            do
                            {
                                ///从流中读取字节，将该数据写入缓冲区
                                sizeRead = inStream.Read(buffer, 0, buffer.Length);
                                ///将给定的缓冲区的数据写入当前zip文档条目
                                outStream.Write(buffer, 0, sizeRead);
                                size += sizeRead;
                                // 计算压缩进度
                                this.ZipProgress = double.Parse(string.Format("{0:N2} ", (((double)size / (double)inStream.Length) * (double)100)));
                                // 压缩中的状态
                                this.ZipState = ZipState.Process;

                                //this.Invoke((EventHandler)delegate
                                //{
                                //    this.ZipLvi.SubItems[5].Text = this.ZipProgress + "%";
                                //    this.ZipLvi.SubItems[6].Text = Thread.CurrentThread.ManagedThreadId.ToString();
                                //});




                            }
                            while (sizeRead > 0);
                        }
                        outStream.Finish();
                        File.Delete(fileName);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }


    /// <summary>
    /// 压缩状态
    /// </summary>
    public enum ZipState
    {
        /// <summary>
        /// 默认值,未进行
        /// </summary>
        None = 0,
        /// <summary>
        /// 处理中
        /// </summary>
        Process = 1,
        /// <summary>
        /// 完成
        /// </summary>
        Finish = 2
    }
}
