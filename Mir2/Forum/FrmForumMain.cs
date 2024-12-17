using Mir.Models.DTO;
using Mir.Models;
using Mir2.MonExpRate;
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
using Mir.Core.utils;
using Mir2.Helper;
using SqlSugar;
using System.Xml.Linq;

namespace Mir2.Forum
{
    public partial class FrmForumMain : UIPage
    {
        List<string> filterTreeNode = new List<string>();
        Dictionary<string, string> DicForumTreeNode = new Dictionary<string, string>();
        public FrmForumMain()
        {
            InitializeComponent();
        }

        private void FrmForumMain_Load(object sender, EventArgs e)
        {
            int pageIndex = 1000;

            TreeNode parent = navMain.CreateNode("自动回帖", 561896, 24, pageIndex);
            navMain.CreateChildNode(parent, "东方版本库", ++pageIndex);
            navMain.CreateChildNode(parent, "如此玩论坛", ++pageIndex);
            navMain.CreateChildNode(parent, "GM016论坛", ++pageIndex);
            navMain.CreateChildNode(parent, "萝卜论坛", ++pageIndex);
            navMain.CreateChildNode(parent, "奇速论坛", ++pageIndex);
            navMain.CreateChildNode(parent, "传奇帮论坛", ++pageIndex);
            navMain.CreateChildNode(parent, "腾飞论坛", ++pageIndex);
            navMain.CreateChildNode(parent, "传奇素材", ++pageIndex);
            
            //navMain.CreateChildNode(parent, "传奇版本库", ++pageIndex);
            //navMain.CreateChildNode(parent, "润芒版本库", ++pageIndex);

            pageIndex = 2000;
            parent = navMain.CreateNode("自动注册", 362045, 24, pageIndex);
            //navMain.CreateChildNode(parent, "奇速论坛", ++pageIndex);

            pageIndex = 3000;
            parent = navMain.CreateNode("版本查看/下载", 558048, 24, pageIndex);
            //navMain.CreateChildNode(parent, "奇速论坛", ++pageIndex);

            pageIndex = 4000;
            parent = navMain.CreateNode("我的信息", 57528, 24, pageIndex);
            navMain.CreateChildNode(parent, new FrmMy());

            filterTreeNode.Add("自动回帖");
            filterTreeNode.Add("自动注册");
            filterTreeNode.Add("版本查看/下载");
            filterTreeNode.Add("我的信息");

            //DicForumTreeNode.Add("传奇版本库", "Mir2.Forum.FrmAutoReplyCQBBK");
            //DicForumTreeNode.Add("润芒版本库", "Mir2.Forum.FrmAutoReplyRUNMANG");
            DicForumTreeNode.Add("东方版本库", "Mir2.Forum.FrmAutoReplyDFBBK");
            DicForumTreeNode.Add("如此玩论坛", "Mir2.Forum.FrmAutoReplyRCWLT");
            DicForumTreeNode.Add("GM016论坛", "Mir2.Forum.FrmAutoReplyGM016");
            DicForumTreeNode.Add("传奇帮论坛", "Mir2.Forum.FrmAutoReplyCQB");
            DicForumTreeNode.Add("奇速论坛", "Mir2.Forum.FrmAutoReplyJSLT");
            DicForumTreeNode.Add("腾飞论坛", "Mir2.Forum.FrmAutoReplyTFLT");
            DicForumTreeNode.Add("萝卜论坛", "Mir2.Forum.FrmAutoReplyLBLT");
            DicForumTreeNode.Add("传奇素材", "Mir2.Forum.FrmAutoReplyCQSC");


            navMain.SelectedNode = navMain.Nodes[0];

            Tab.AddPage(new FrmAutoReplyDFBBK());
        }

        private void navMain_MenuItemClick(TreeNode node, NavMenuItem item, int pageIndex)
        {
            if (filterTreeNode.Contains(node.Text))
                return;

            foreach (TabPage page in Tab.TabPages)
            {
                if (page.Tag == null) continue;
                string tag = page.Tag.ToString();
                if (tag == node.FullPath)
                {
                    Tab.SelectedTab = page;
                    return;
                }
            }

            foreach (var kv in DicForumTreeNode)
            {
                if (node.Text == kv.Key)
                {
                    Tab.AddPage(GameStyle.CreateInstance<UIPage>(kv.Value));
                    TabPage tp = Tab.TabPages[Tab.TabPages.Count - 1];
                    tp.Tag = node.FullPath;
                    Tab.SelectedTab = tp;
                    break;
                }
            }
        }
    }
}
