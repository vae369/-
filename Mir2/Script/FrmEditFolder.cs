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
    public partial class FrmEditFolder : UIForm
    {
        public string DirectoryName = "";
        public string DirectoryPath = "";
        public FrmEditFolder(string directoryName)
        {
            InitializeComponent();
            DirectoryName = directoryName.Substring(directoryName.LastIndexOf('\\') + 1);
            DirectoryPath = directoryName.Substring(0, directoryName.LastIndexOf('\\'));
            uiTextBox1.Text = DirectoryName;
        }

        private void uiButton17_Click(object sender, EventArgs e)
        {
            DirectoryName = uiTextBox1.Text.Trim();
            if (string.IsNullOrEmpty(DirectoryName))
            {
                this.ShowErrorTip("文件夹名称不能为空！");
                return;
            }
            DirectoryPath = DirectoryPath + "\\" + DirectoryName;
            DialogResult = DialogResult.OK;
        }
    }
}
