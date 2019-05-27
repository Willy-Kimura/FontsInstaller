namespace WK.Libraries.FontsInstallerNS.Views.Editors
{
    partial class StringsEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StringsEditor));
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.btnTemplates = new System.Windows.Forms.Button();
            this.btnShowDIalog = new System.Windows.Forms.Button();
            this.btnOkay = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.infoTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnFontsExpressionTemplate = new System.Windows.Forms.Button();
            this.btnAppNameTemplate = new System.Windows.Forms.Button();
            this.btnAppVersionTemplate = new System.Windows.Forms.Button();
            this.btnFontsRequiredExpressionTemplate = new System.Windows.Forms.Button();
            this.btnFontsTemplate = new System.Windows.Forms.Button();
            this.btnInstallButtonTextTemplate = new System.Windows.Forms.Button();
            this.btnFontsCountTemplate = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlEditor = new System.Windows.Forms.Panel();
            this.txtEditString = new System.Windows.Forms.TextBox();
            this.pnlTags = new System.Windows.Forms.Panel();
            this.pnlTopBar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlEditor.SuspendLayout();
            this.pnlTags.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.pnlTopBar.Controls.Add(this.btnTemplates);
            this.pnlTopBar.Controls.Add(this.btnShowDIalog);
            this.pnlTopBar.Controls.Add(this.btnOkay);
            this.pnlTopBar.Controls.Add(this.lblTitle);
            this.pnlTopBar.Controls.Add(this.btnCancel);
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Size = new System.Drawing.Size(351, 29);
            this.pnlTopBar.TabIndex = 0;
            // 
            // btnTemplates
            // 
            this.btnTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTemplates.BackColor = System.Drawing.Color.LightGray;
            this.btnTemplates.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTemplates.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnTemplates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTemplates.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTemplates.ForeColor = System.Drawing.Color.Black;
            this.btnTemplates.Image = ((System.Drawing.Image)(resources.GetObject("btnTemplates.Image")));
            this.btnTemplates.Location = new System.Drawing.Point(248, 0);
            this.btnTemplates.Name = "btnTemplates";
            this.btnTemplates.Size = new System.Drawing.Size(35, 29);
            this.btnTemplates.TabIndex = 9;
            this.infoTip.SetToolTip(this.btnTemplates, "Click to show/hide  the list of string templates \r\nthat you can add to your conte" +
        "nt.");
            this.btnTemplates.UseVisualStyleBackColor = false;
            this.btnTemplates.Click += new System.EventHandler(this.OnClickTemplates);
            // 
            // btnShowDIalog
            // 
            this.btnShowDIalog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowDIalog.BackColor = System.Drawing.Color.LightGray;
            this.btnShowDIalog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowDIalog.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnShowDIalog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowDIalog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnShowDIalog.ForeColor = System.Drawing.Color.Black;
            this.btnShowDIalog.Image = ((System.Drawing.Image)(resources.GetObject("btnShowDIalog.Image")));
            this.btnShowDIalog.Location = new System.Drawing.Point(282, 0);
            this.btnShowDIalog.Name = "btnShowDIalog";
            this.btnShowDIalog.Size = new System.Drawing.Size(35, 29);
            this.btnShowDIalog.TabIndex = 8;
            this.infoTip.SetToolTip(this.btnShowDIalog, "Click or alternatively press \'F5\' to preview the Installer dialog.");
            this.btnShowDIalog.UseVisualStyleBackColor = false;
            this.btnShowDIalog.Click += new System.EventHandler(this.OnClickPlay);
            // 
            // btnOkay
            // 
            this.btnOkay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOkay.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnOkay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOkay.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.btnOkay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOkay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnOkay.ForeColor = System.Drawing.Color.White;
            this.btnOkay.Image = ((System.Drawing.Image)(resources.GetObject("btnOkay.Image")));
            this.btnOkay.Location = new System.Drawing.Point(214, 0);
            this.btnOkay.Name = "btnOkay";
            this.btnOkay.Size = new System.Drawing.Size(35, 29);
            this.btnOkay.TabIndex = 7;
            this.infoTip.SetToolTip(this.btnOkay, "Click to save and close.");
            this.btnOkay.UseVisualStyleBackColor = false;
            this.btnOkay.Click += new System.EventHandler(this.OnClickOkay);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkGray;
            this.lblTitle.Location = new System.Drawing.Point(7, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(134, 13);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "DIALOG STRINGS EDITOR";
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
            this.btnCancel.Location = new System.Drawing.Point(316, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(35, 29);
            this.btnCancel.TabIndex = 6;
            this.infoTip.SetToolTip(this.btnCancel, "Click or alternatively press \'Escape\' to cancel & close.");
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.OnClickCancel);
            // 
            // infoTip
            // 
            this.infoTip.ShowAlways = true;
            // 
            // btnFontsExpressionTemplate
            // 
            this.btnFontsExpressionTemplate.AutoSize = true;
            this.btnFontsExpressionTemplate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFontsExpressionTemplate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnFontsExpressionTemplate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnFontsExpressionTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFontsExpressionTemplate.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFontsExpressionTemplate.Location = new System.Drawing.Point(55, 30);
            this.btnFontsExpressionTemplate.Name = "btnFontsExpressionTemplate";
            this.btnFontsExpressionTemplate.Size = new System.Drawing.Size(109, 25);
            this.btnFontsExpressionTemplate.TabIndex = 7;
            this.btnFontsExpressionTemplate.Text = "FontsExpression";
            this.infoTip.SetToolTip(this.btnFontsExpressionTemplate, "Add the \'{FontsExpression}\' string template to your content");
            this.btnFontsExpressionTemplate.UseVisualStyleBackColor = false;
            this.btnFontsExpressionTemplate.Click += new System.EventHandler(this.btnFontsExpressionTemplate_Click);
            // 
            // btnAppNameTemplate
            // 
            this.btnAppNameTemplate.AutoSize = true;
            this.btnAppNameTemplate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAppNameTemplate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAppNameTemplate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnAppNameTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAppNameTemplate.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAppNameTemplate.Location = new System.Drawing.Point(7, 6);
            this.btnAppNameTemplate.Name = "btnAppNameTemplate";
            this.btnAppNameTemplate.Size = new System.Drawing.Size(61, 25);
            this.btnAppNameTemplate.TabIndex = 1;
            this.btnAppNameTemplate.Text = "AppName";
            this.infoTip.SetToolTip(this.btnAppNameTemplate, "Add the \'{AppName}\' string template to your content");
            this.btnAppNameTemplate.UseVisualStyleBackColor = false;
            this.btnAppNameTemplate.Click += new System.EventHandler(this.btnAppNameTemplate_Click);
            // 
            // btnAppVersionTemplate
            // 
            this.btnAppVersionTemplate.AutoSize = true;
            this.btnAppVersionTemplate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAppVersionTemplate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnAppVersionTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAppVersionTemplate.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAppVersionTemplate.Location = new System.Drawing.Point(67, 6);
            this.btnAppVersionTemplate.Name = "btnAppVersionTemplate";
            this.btnAppVersionTemplate.Size = new System.Drawing.Size(79, 25);
            this.btnAppVersionTemplate.TabIndex = 2;
            this.btnAppVersionTemplate.Text = "AppVersion";
            this.infoTip.SetToolTip(this.btnAppVersionTemplate, "Add the \'{AppVersion}\' string template to your content");
            this.btnAppVersionTemplate.UseVisualStyleBackColor = true;
            this.btnAppVersionTemplate.Click += new System.EventHandler(this.btnAppVersionTemplate_Click);
            // 
            // btnFontsRequiredExpressionTemplate
            // 
            this.btnFontsRequiredExpressionTemplate.AutoSize = true;
            this.btnFontsRequiredExpressionTemplate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFontsRequiredExpressionTemplate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnFontsRequiredExpressionTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFontsRequiredExpressionTemplate.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFontsRequiredExpressionTemplate.Location = new System.Drawing.Point(163, 30);
            this.btnFontsRequiredExpressionTemplate.Name = "btnFontsRequiredExpressionTemplate";
            this.btnFontsRequiredExpressionTemplate.Size = new System.Drawing.Size(157, 25);
            this.btnFontsRequiredExpressionTemplate.TabIndex = 6;
            this.btnFontsRequiredExpressionTemplate.Text = "FontsRequiredExpression";
            this.infoTip.SetToolTip(this.btnFontsRequiredExpressionTemplate, "Add the \'{FontsRequiredExpression}\' string template to your content");
            this.btnFontsRequiredExpressionTemplate.UseVisualStyleBackColor = true;
            this.btnFontsRequiredExpressionTemplate.Click += new System.EventHandler(this.btnFontsRequiredExpressionTemplate_Click);
            // 
            // btnFontsTemplate
            // 
            this.btnFontsTemplate.AutoSize = true;
            this.btnFontsTemplate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFontsTemplate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnFontsTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFontsTemplate.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFontsTemplate.Location = new System.Drawing.Point(7, 30);
            this.btnFontsTemplate.Name = "btnFontsTemplate";
            this.btnFontsTemplate.Size = new System.Drawing.Size(49, 25);
            this.btnFontsTemplate.TabIndex = 3;
            this.btnFontsTemplate.Text = "Fonts";
            this.infoTip.SetToolTip(this.btnFontsTemplate, "Add the \'{Fonts}\' string template to your content");
            this.btnFontsTemplate.UseVisualStyleBackColor = true;
            this.btnFontsTemplate.Click += new System.EventHandler(this.btnFontsTemplate_Click);
            // 
            // btnInstallButtonTextTemplate
            // 
            this.btnInstallButtonTextTemplate.AutoSize = true;
            this.btnInstallButtonTextTemplate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnInstallButtonTextTemplate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnInstallButtonTextTemplate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnInstallButtonTextTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInstallButtonTextTemplate.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInstallButtonTextTemplate.Location = new System.Drawing.Point(145, 6);
            this.btnInstallButtonTextTemplate.Name = "btnInstallButtonTextTemplate";
            this.btnInstallButtonTextTemplate.Size = new System.Drawing.Size(121, 25);
            this.btnInstallButtonTextTemplate.TabIndex = 5;
            this.btnInstallButtonTextTemplate.Text = "InstallButtonText";
            this.infoTip.SetToolTip(this.btnInstallButtonTextTemplate, "Add the \'{InstallButtonText}\' string template to your content");
            this.btnInstallButtonTextTemplate.UseVisualStyleBackColor = false;
            this.btnInstallButtonTextTemplate.Click += new System.EventHandler(this.btnInstallButtonTextTemplate_Click);
            // 
            // btnFontsCountTemplate
            // 
            this.btnFontsCountTemplate.AutoSize = true;
            this.btnFontsCountTemplate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFontsCountTemplate.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnFontsCountTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFontsCountTemplate.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFontsCountTemplate.Location = new System.Drawing.Point(265, 6);
            this.btnFontsCountTemplate.Name = "btnFontsCountTemplate";
            this.btnFontsCountTemplate.Size = new System.Drawing.Size(79, 25);
            this.btnFontsCountTemplate.TabIndex = 4;
            this.btnFontsCountTemplate.Text = "FontsCount";
            this.infoTip.SetToolTip(this.btnFontsCountTemplate, "Add the \'{FontsCount}\' string template to your content");
            this.btnFontsCountTemplate.UseVisualStyleBackColor = true;
            this.btnFontsCountTemplate.Click += new System.EventHandler(this.btnFontsCountTemplate_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.pnlEditor);
            this.pnlContent.Controls.Add(this.pnlTags);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 29);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(351, 279);
            this.pnlContent.TabIndex = 1;
            // 
            // pnlEditor
            // 
            this.pnlEditor.Controls.Add(this.txtEditString);
            this.pnlEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEditor.Location = new System.Drawing.Point(0, 61);
            this.pnlEditor.Name = "pnlEditor";
            this.pnlEditor.Size = new System.Drawing.Size(351, 218);
            this.pnlEditor.TabIndex = 9;
            // 
            // txtEditString
            // 
            this.txtEditString.AcceptsReturn = true;
            this.txtEditString.AcceptsTab = true;
            this.txtEditString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEditString.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEditString.HideSelection = false;
            this.txtEditString.Location = new System.Drawing.Point(0, 0);
            this.txtEditString.Multiline = true;
            this.txtEditString.Name = "txtEditString";
            this.txtEditString.Size = new System.Drawing.Size(351, 218);
            this.txtEditString.TabIndex = 0;
            this.txtEditString.Leave += new System.EventHandler(this.txtEditString_Leave);
            // 
            // pnlTags
            // 
            this.pnlTags.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTags.Controls.Add(this.btnFontsExpressionTemplate);
            this.pnlTags.Controls.Add(this.btnAppNameTemplate);
            this.pnlTags.Controls.Add(this.btnAppVersionTemplate);
            this.pnlTags.Controls.Add(this.btnFontsRequiredExpressionTemplate);
            this.pnlTags.Controls.Add(this.btnFontsTemplate);
            this.pnlTags.Controls.Add(this.btnInstallButtonTextTemplate);
            this.pnlTags.Controls.Add(this.btnFontsCountTemplate);
            this.pnlTags.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTags.Location = new System.Drawing.Point(0, 0);
            this.pnlTags.Name = "pnlTags";
            this.pnlTags.Size = new System.Drawing.Size(351, 61);
            this.pnlTags.TabIndex = 8;
            // 
            // StringsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(351, 308);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlTopBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "StringsEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FontsInstaller: Dialog String Editor";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Shown += new System.EventHandler(this.OnFormShown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlEditor.ResumeLayout(false);
            this.pnlEditor.PerformLayout();
            this.pnlTags.ResumeLayout(false);
            this.pnlTags.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip infoTip;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnFontsExpressionTemplate;
        private System.Windows.Forms.Button btnFontsRequiredExpressionTemplate;
        private System.Windows.Forms.Button btnInstallButtonTextTemplate;
        private System.Windows.Forms.Button btnFontsCountTemplate;
        private System.Windows.Forms.Button btnFontsTemplate;
        private System.Windows.Forms.Button btnAppVersionTemplate;
        private System.Windows.Forms.Button btnAppNameTemplate;
        private System.Windows.Forms.Panel pnlEditor;
        private System.Windows.Forms.Panel pnlTags;
        public System.Windows.Forms.TextBox txtEditString;
        public System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnOkay;
        private System.Windows.Forms.Button btnShowDIalog;
        private System.Windows.Forms.Button btnTemplates;
    }
}