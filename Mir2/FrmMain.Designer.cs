namespace Mir2
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.uiContextMenuStrip = new Sunny.UI.UIContextMenuStrip();
            this.StyleManager = new Sunny.UI.UIStyleManager(this.components);
            this.uiSmoothLabel1 = new Sunny.UI.UISmoothLabel();
            this.Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.Controls.Add(this.uiSmoothLabel1);
            this.Header.Location = new System.Drawing.Point(2, 36);
            this.Header.Size = new System.Drawing.Size(1276, 73);
            // 
            // uiContextMenuStrip
            // 
            this.uiContextMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.uiContextMenuStrip.Font = new System.Drawing.Font("Tahoma", 12F);
            this.uiContextMenuStrip.Name = "uiContextMenuStrip1";
            this.uiContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // StyleManager
            // 
            this.StyleManager.DPIScale = true;
            this.StyleManager.GlobalFontName = "Tahoma";
            // 
            // uiSmoothLabel1
            // 
            this.uiSmoothLabel1.Font = new System.Drawing.Font("Segoe Script", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiSmoothLabel1.Location = new System.Drawing.Point(13, 4);
            this.uiSmoothLabel1.Name = "uiSmoothLabel1";
            this.uiSmoothLabel1.RectSize = 3;
            this.uiSmoothLabel1.Size = new System.Drawing.Size(328, 66);
            this.uiSmoothLabel1.TabIndex = 54;
            this.uiSmoothLabel1.Text = "Mir Of Easy";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1280, 846);
            this.ExtendBox = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.Padding = new System.Windows.Forms.Padding(2, 36, 2, 2);
            this.Resizable = true;
            this.ShowDragStretch = true;
            this.Text = "咕咕鸡小工具 - 本软件不收任何费用，请勿上当。  Q群:977049285";
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, -63, 1132, 846);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Controls.SetChildIndex(this.Header, 0);
            this.Header.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIContextMenuStrip uiContextMenuStrip;
        private Sunny.UI.UIStyleManager StyleManager;
        private Sunny.UI.UISmoothLabel uiSmoothLabel1;
    }
}