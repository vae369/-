using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mir2.Script
{
    public partial class FrmEditFile : UIForm
    {
        public string MirFilePath = "";
        public string EditFilePath = "";
        public string EditFileName = "";
        public int ImageIndex = 0;

        public FrmEditFile()
        {
            InitializeComponent();
        }
        public FrmEditFile(string text, int imageIndex)
        {
            InitializeComponent();

            uiTextBox1.Text = text.Substring(0, text.IndexOf('.'));
            switch (imageIndex)
            {
                case 2: rbInI.Checked = true; break;
                case 5: rbJson.Checked = true; break;
                case 6: rbTxT.Checked = true; break;
                case 8: rbXml.Checked = true; break;
            }
        }
        private void uiButton17_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(uiTextBox1.Text.Trim()))
            {
                MessageBox.Show("文件名不能为空！");
                return;
            }
            EditFileName = uiTextBox1.Text.Replace(".", "");

            if (rbTxT.Checked)
            {
                EditFileName += rbTxT.Text;
                ImageIndex = 1;
            }

            if (rbInI.Checked)
            {
                EditFileName += rbInI.Text;
                ImageIndex = 2;
            }

            if (rbXml.Checked)
            {
                EditFileName += rbXml.Text;
                ImageIndex = 3;
            }

            if (rbJson.Checked)
            {
                EditFileName += rbJson.Text;
                ImageIndex = 4;
            }

            EditFilePath = MirFilePath + "\\" + EditFileName;
            this.DialogResult = DialogResult.OK;
        }
    }
}
