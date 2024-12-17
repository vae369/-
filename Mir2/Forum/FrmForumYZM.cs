using Mir2.Helper;
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

namespace Mir2.Forum
{
    public partial class FrmForumYZM : UIForm2
    {
        IForumBase forumBase;
        Bitmap map = null;
        public string YZM { get; set; }
        public FrmForumYZM(IForumBase forumBase, Bitmap map)
        {
            InitializeComponent();
            this.map = map;
            this.forumBase = forumBase;
        }

        private void FrmForumYZM_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = map;
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            YZM = uiTextBox1.Text;
            if (string.IsNullOrEmpty(YZM))
            {
                UIMessageBox.ShowError("验证码不能为空！");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void uiLinkLabel1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = forumBase.GetVerCode();
        }
    }
}
