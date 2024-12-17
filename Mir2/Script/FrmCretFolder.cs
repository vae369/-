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
    public partial class FrmCretFolder : UIForm
    {
        public string DirectoryName = "";
        public FrmCretFolder()
        {
            InitializeComponent();
        }

        private void uiButton17_Click(object sender, EventArgs e)
        {
            DirectoryName = uiTextBox1.Text.Trim();
            if (string.IsNullOrEmpty(DirectoryName))
            {
                this.ShowErrorTip("文件夹名称不能为空！");
                return;
            }
            DialogResult = DialogResult.OK;
        }
    }
}
