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
    public partial class FrmCretFile : UIForm
    {
        public string MirFilePath = "";
        public string AddFilePath = "";
        public string AddFileName = "";
        public int ImageIndex = 0;

        public FrmCretFile()
        {
            InitializeComponent();
        }

        private void uiButton17_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(uiTextBox1.Text.Trim()))
            {
                MessageBox.Show("文件名不能为空！");
                return;
            }
            AddFileName = uiTextBox1.Text.Replace(".", "");

            if (rbTxT.Checked)
            {
                AddFileName += rbTxT.Text;
                ImageIndex = 1;
            }

            if (rbInI.Checked)
            {
                AddFileName += rbInI.Text;
                ImageIndex = 2;
            }

            if (rbXml.Checked)
            {
                AddFileName += rbXml.Text;
                ImageIndex = 3;
            }

            if (rbJson.Checked)
            {
                AddFileName += rbJson.Text;
                ImageIndex = 4;
            }

            AddFilePath = MirFilePath + "\\" + AddFileName;
            this.DialogResult = DialogResult.OK;
        }
    }
}
