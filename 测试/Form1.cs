using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tensorflow;

namespace 测试
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pakFilePath = @"D:\MirServer\登录器\NewopUI.Pak"; // 替换为你的 .pak 文件路径
            string password = "gameofmir"; // 替换为你的密码 (如果需要)

            PakFile pakFile = new PakFile(pakFilePath, password);

            // 使用加载的资源
            foreach (var resource in pakFile.Resources)
            {
                Console.WriteLine($"Resource Name: {resource.Key}, Size: {resource.Value.Length} bytes");
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            TMain = new Thread(new ThreadStart(DownPic));
            TMain.IsBackground = true;
            TMain.Start();
        }
        bool isActive = true;
        Thread TMain = null;
        string path = Application.StartupPath + @"\forum\biaomeitrain";
        private void DownPic()
        {
            isActive = true;
            while (isActive)
            {
                biaomeiHepler bh = new biaomeiHepler();
                int num = int.Parse(txtNum.Text);

                //path = Application.StartupPath + @"\forum\biaomeitest";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                try
                {
                    for (int i = 1; i <= num; i++)
                    {
                        if (bh.GetBiaoMeiYZMPIC($"{path}\\{Guid.NewGuid().ToString("N")}.png"))
                        {
                            ShowMessage($"成功下载 {i} 张验证码图片...");
                        }
                    }
                    isActive = false;
                    TMain.Abort();
                }
                catch (Exception ex)
                {
                    ShowMessage(ex.Message);
                }
            }
        }

        public void ShowMessage(string msg)
        {
            this.Invoke((EventHandler)delegate { lbMsg.Text = DateTime.Now.ToString() + ":" + msg; });
        }


        private void button2_Click(object sender, EventArgs e)
        {
            var fileList = Directory.GetFiles(path, "*.png");
            foreach (var file in fileList)
            {

                Mat image = new Mat(file, ImreadModes.Color);
                // 调整图像大小
                Cv2.Resize(image, image, new OpenCvSharp.Size(100, 40));
                image.ConvertTo(image, MatType.CV_32F, 1.0 / 255.0);


            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "默认": EditCode(Encoding.Default,null); break;
                case "ASCII": EditCode(Encoding.ASCII, null); break;
                case "UNICODE": EditCode(Encoding.Unicode, null); break;
                case "UTF-8": EditCode(Encoding.UTF8, null); break;
                case "UTF-8 BOM": EditCode(null, "utf-8 bom"); break;
                case "GBK": EditCode(null, "gbk"); break;
                case "GB2312": EditCode(null, "gb2312"); break;
                case "GB18030": EditCode(null, "gb18030"); break;
            }
        }

        private void EditCode(Encoding ed, string encodingStr)
        {
            path = "D:\\FFOutput\\1.txt";
            string pathTemp = "D:\\FFOutput\\11.txt";
            StreamReader sr = null;
            StreamWriter sw = null;
            if (ed != null)
            {
                sr = new StreamReader(path, ed);
                sw = new StreamWriter(pathTemp, false, ed);
            }
            else
            {
                sr = new StreamReader(path, Encoding.GetEncoding(encodingStr));
                sw = new StreamWriter(pathTemp, false, Encoding.GetEncoding(encodingStr));
            }

            for (string line = null; (line = sr.ReadLine()) != null;)
            {
                sw.WriteLine(line);
            }
            sw.Close();
            sr.Close();

            if (ed != null)
            {
                sr = new StreamReader(pathTemp, ed); 
            }
            else
            {
                sr = new StreamReader(pathTemp, Encoding.GetEncoding(encodingStr)); 
            }
            string res = sr.ReadToEnd();

            sr.Close();
            rtxtRes.Text = res;
        }
    }
}
