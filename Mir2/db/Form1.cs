using Mir.Core.file;
using Mir.Core.image;
using Mir.Models.DTO;
using Spire.Xls.Core;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using Tesseract;

namespace Mir2.db
{
    public partial class Form1 : UIForm2
    {
        Bitmap _map = null;
        UnCodebase unCodebase = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void BindPic(string path)
        {
            Image imgModel = Image.FromFile(path);   //加载图片
            MemoryStream mstr = new MemoryStream(); //创建新的MemoryStream
            imgModel.Save(mstr, System.Drawing.Imaging.ImageFormat.Png);// 保存这个对象
            Bitmap map = new Bitmap(imgModel);
            unCodebase = new UnCodebase(map);

            this.Invoke((EventHandler)delegate
            {
                pictureBox1.Image = Image.FromStream(mstr); //显示
            });

            imgModel.Dispose();//释放占用
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ofdMain.Filter = "图片文件(*.png)|*.png";
            ofdMain.Multiselect = false;
            ofdMain.Title = "请选择图片";
            if (ofdMain.ShowDialog() == DialogResult.OK)
            {

                string fileName = ofdMain.FileName;
                BindPic(fileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _map = new Bitmap(pictureBox2.Image);
            //解析验证码
            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.TesseractAndLstm))
            {
                engine.DefaultPageSegMode = PageSegMode.SingleLine;
                // 只取这些字符
                engine.SetVariable("tessedit_char_whitelist", "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");

                using (var entity = engine.Process(_map))
                {
                    richTextBox1.Text = entity.GetText().Trim().Substring(0, 4);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int code = int.Parse(textBox1.Text);
            pictureBox2.Image = unCodebase.ClearPicBorder(code);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = unCodebase.GrayByPixels();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = unCodebase.GrayByLine();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Bitmap map = new Bitmap(pictureBox2.Image);
            pictureBox2.Image = unCodebase.GrayTranWhite(map);
        }
    }

}
