namespace Mir2.Forum
{
    partial class FrmForumMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.navMain = new Sunny.UI.UINavMenu();
            this.Tab = new Sunny.UI.UITabControl();
            this.SuspendLayout();
            // 
            // navMain
            // 
            this.navMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.navMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.navMain.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.navMain.Font = new System.Drawing.Font("宋体", 12F);
            this.navMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.navMain.FullRowSelect = true;
            this.navMain.HotTracking = true;
            this.navMain.ItemHeight = 50;
            this.navMain.Location = new System.Drawing.Point(0, 0);
            this.navMain.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.navMain.Name = "navMain";
            this.navMain.ShowLines = false;
            this.navMain.ShowPlusMinus = false;
            this.navMain.ShowRootLines = false;
            this.navMain.Size = new System.Drawing.Size(220, 739);
            this.navMain.TabIndex = 23;
            this.navMain.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.navMain.MenuItemClick += new Sunny.UI.UINavMenu.OnMenuItemClick(this.navMain_MenuItemClick);
            // 
            // Tab
            // 
            this.Tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tab.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.Tab.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Tab.ItemSize = new System.Drawing.Size(150, 40);
            this.Tab.Location = new System.Drawing.Point(220, 0);
            this.Tab.MainPage = "";
            this.Tab.Name = "Tab";
            this.Tab.SelectedIndex = 0;
            this.Tab.ShowCloseButton = true;
            this.Tab.Size = new System.Drawing.Size(886, 739);
            this.Tab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.Tab.TabIndex = 24;
            this.Tab.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // FrmForumMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1106, 739);
            this.Controls.Add(this.Tab);
            this.Controls.Add(this.navMain);
            this.Name = "FrmForumMain";
            this.Text = "FrmForumMain";
            this.Load += new System.EventHandler(this.FrmForumMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UINavMenu navMain;
        private Sunny.UI.UITabControl Tab;
    }
}