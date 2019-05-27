namespace WK.Libraries.FontsInstallerNS.Views.Editors
{
    partial class FontSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontSelector));
            this.lstFonts = new System.Windows.Forms.ListBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnSelectFont = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtSearchFonts = new System.Windows.Forms.TextBox();
            this.fontTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnRefreshFonts = new System.Windows.Forms.Button();
            this.fontsInitializer = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // lstFonts
            // 
            this.lstFonts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFonts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstFonts.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstFonts.FormattingEnabled = true;
            this.lstFonts.ItemHeight = 30;
            this.lstFonts.Location = new System.Drawing.Point(0, 28);
            this.lstFonts.Name = "lstFonts";
            this.lstFonts.Size = new System.Drawing.Size(382, 334);
            this.lstFonts.TabIndex = 0;
            this.lstFonts.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnClickFont);
            this.lstFonts.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.OnDrawFontItem);
            this.lstFonts.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.OnMeasureFontItem);
            this.lstFonts.SelectedIndexChanged += new System.EventHandler(this.OnSelectedFontIndexChanged);
            this.lstFonts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDownInFontItems);
            this.lstFonts.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUpInFontItems);
            this.lstFonts.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnDoubleClickFont);
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfo.BackColor = System.Drawing.Color.White;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.DarkGray;
            this.lblInfo.Location = new System.Drawing.Point(12, 172);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(361, 19);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "© FontsInstaller | Font Selector";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSelectFont
            // 
            this.btnSelectFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFont.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSelectFont.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectFont.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.btnSelectFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFont.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSelectFont.ForeColor = System.Drawing.Color.White;
            this.btnSelectFont.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectFont.Image")));
            this.btnSelectFont.Location = new System.Drawing.Point(277, 0);
            this.btnSelectFont.Name = "btnSelectFont";
            this.btnSelectFont.Size = new System.Drawing.Size(35, 29);
            this.btnSelectFont.TabIndex = 4;
            this.fontTip.SetToolTip(this.btnSelectFont, "Click or alternatively press \'Enter\' to select font...");
            this.btnSelectFont.UseVisualStyleBackColor = false;
            this.btnSelectFont.Click += new System.EventHandler(this.OnClickOkay);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(346, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(35, 29);
            this.btnCancel.TabIndex = 5;
            this.fontTip.SetToolTip(this.btnCancel, "Click or alternatively press \'Escape\' to cancel & close.");
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.OnClickCancel);
            // 
            // txtSearchFonts
            // 
            this.txtSearchFonts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchFonts.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchFonts.Location = new System.Drawing.Point(0, 0);
            this.txtSearchFonts.Name = "txtSearchFonts";
            this.txtSearchFonts.Size = new System.Drawing.Size(278, 29);
            this.txtSearchFonts.TabIndex = 6;
            this.txtSearchFonts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSearchFonts.TextChanged += new System.EventHandler(this.OnSearchFontChanged);
            this.txtSearchFonts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDownInSearchFont);
            // 
            // fontTip
            // 
            this.fontTip.ShowAlways = true;
            // 
            // btnRefreshFonts
            // 
            this.btnRefreshFonts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshFonts.BackColor = System.Drawing.Color.LightGray;
            this.btnRefreshFonts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshFonts.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnRefreshFonts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshFonts.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefreshFonts.ForeColor = System.Drawing.Color.White;
            this.btnRefreshFonts.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshFonts.Image")));
            this.btnRefreshFonts.Location = new System.Drawing.Point(312, 0);
            this.btnRefreshFonts.Name = "btnRefreshFonts";
            this.btnRefreshFonts.Size = new System.Drawing.Size(35, 29);
            this.btnRefreshFonts.TabIndex = 7;
            this.fontTip.SetToolTip(this.btnRefreshFonts, "Click or alternatively press \'F5\' to refresh the fonts list...");
            this.btnRefreshFonts.UseVisualStyleBackColor = false;
            this.btnRefreshFonts.Click += new System.EventHandler(this.OnClickRefresh);
            // 
            // fontsInitializer
            // 
            this.fontsInitializer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.InitializeFontsList);
            // 
            // FontSelector
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(381, 358);
            this.ControlBox = false;
            this.Controls.Add(this.btnRefreshFonts);
            this.Controls.Add(this.btnSelectFont);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtSearchFonts);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lstFonts);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(397, 35);
            this.Name = "FontSelector";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose Font...";
            this.Load += new System.EventHandler(this.OnLoad);
            this.Shown += new System.EventHandler(this.OnFormShown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstFonts;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnSelectFont;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.TextBox txtSearchFonts;
        private System.Windows.Forms.ToolTip fontTip;
        private System.Windows.Forms.Button btnRefreshFonts;
        public System.ComponentModel.BackgroundWorker fontsInitializer;
    }
}