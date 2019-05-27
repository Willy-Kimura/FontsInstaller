namespace WK.Libraries.FontsInstallerNS.Views.Editors
{
    partial class DialogStringEditorView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogStringEditorView));
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.infoTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlContent = new System.Windows.Forms.Panel();
            this.txtEditString = new System.Windows.Forms.TextBox();
            this.btnAppNameTemplate = new System.Windows.Forms.Button();
            this.btnAppVersionTemplate = new System.Windows.Forms.Button();
            this.btnFontsTemplate = new System.Windows.Forms.Button();
            this.btnFontsCountTemplate = new System.Windows.Forms.Button();
            this.btnInstallButtonTextTemplate = new System.Windows.Forms.Button();
            this.btnFontsRequiredExpressionTemplate = new System.Windows.Forms.Button();
            this.btnFontsExpressionTemplate = new System.Windows.Forms.Button();
            this.pnlTags = new System.Windows.Forms.Panel();
            this.pnlEditor = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTopBar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlTags.SuspendLayout();
            this.pnlEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.Controls.Add(this.label1);
            this.pnlTopBar.Controls.Add(this.btnCancel);
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Size = new System.Drawing.Size(346, 29);
            this.pnlTopBar.TabIndex = 0;
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
            this.btnCancel.Location = new System.Drawing.Point(311, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(35, 29);
            this.btnCancel.TabIndex = 6;
            this.infoTip.SetToolTip(this.btnCancel, "Click or alternatively press \'Escape\' to cancel && close");
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.OnClickCancel);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.pnlEditor);
            this.pnlContent.Controls.Add(this.pnlTags);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 29);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(346, 309);
            this.pnlContent.TabIndex = 1;
            // 
            // txtEditString
            // 
            this.txtEditString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEditString.Location = new System.Drawing.Point(0, 0);
            this.txtEditString.Multiline = true;
            this.txtEditString.Name = "txtEditString";
            this.txtEditString.Size = new System.Drawing.Size(346, 251);
            this.txtEditString.TabIndex = 0;
            // 
            // btnAppNameTemplate
            // 
            this.btnAppNameTemplate.AutoSize = true;
            this.btnAppNameTemplate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAppNameTemplate.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAppNameTemplate.Location = new System.Drawing.Point(7, 6);
            this.btnAppNameTemplate.Name = "btnAppNameTemplate";
            this.btnAppNameTemplate.Size = new System.Drawing.Size(59, 23);
            this.btnAppNameTemplate.TabIndex = 1;
            this.btnAppNameTemplate.Text = "AppName";
            this.btnAppNameTemplate.UseVisualStyleBackColor = true;
            this.btnAppNameTemplate.Click += new System.EventHandler(this.btnAppNameTemplate_Click);
            // 
            // btnAppVersionTemplate
            // 
            this.btnAppVersionTemplate.AutoSize = true;
            this.btnAppVersionTemplate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAppVersionTemplate.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAppVersionTemplate.Location = new System.Drawing.Point(66, 6);
            this.btnAppVersionTemplate.Name = "btnAppVersionTemplate";
            this.btnAppVersionTemplate.Size = new System.Drawing.Size(77, 23);
            this.btnAppVersionTemplate.TabIndex = 2;
            this.btnAppVersionTemplate.Text = "AppVersion";
            this.btnAppVersionTemplate.UseVisualStyleBackColor = true;
            this.btnAppVersionTemplate.Click += new System.EventHandler(this.btnAppVersionTemplate_Click);
            // 
            // btnFontsTemplate
            // 
            this.btnFontsTemplate.AutoSize = true;
            this.btnFontsTemplate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFontsTemplate.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFontsTemplate.Location = new System.Drawing.Point(7, 29);
            this.btnFontsTemplate.Name = "btnFontsTemplate";
            this.btnFontsTemplate.Size = new System.Drawing.Size(47, 23);
            this.btnFontsTemplate.TabIndex = 3;
            this.btnFontsTemplate.Text = "Fonts";
            this.btnFontsTemplate.UseVisualStyleBackColor = true;
            this.btnFontsTemplate.Click += new System.EventHandler(this.btnFontsTemplate_Click);
            // 
            // btnFontsCountTemplate
            // 
            this.btnFontsCountTemplate.AutoSize = true;
            this.btnFontsCountTemplate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFontsCountTemplate.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFontsCountTemplate.Location = new System.Drawing.Point(262, 6);
            this.btnFontsCountTemplate.Name = "btnFontsCountTemplate";
            this.btnFontsCountTemplate.Size = new System.Drawing.Size(77, 23);
            this.btnFontsCountTemplate.TabIndex = 4;
            this.btnFontsCountTemplate.Text = "FontsCount";
            this.btnFontsCountTemplate.UseVisualStyleBackColor = true;
            this.btnFontsCountTemplate.Click += new System.EventHandler(this.btnFontsCountTemplate_Click);
            // 
            // btnInstallButtonTextTemplate
            // 
            this.btnInstallButtonTextTemplate.AutoSize = true;
            this.btnInstallButtonTextTemplate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnInstallButtonTextTemplate.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInstallButtonTextTemplate.Location = new System.Drawing.Point(143, 6);
            this.btnInstallButtonTextTemplate.Name = "btnInstallButtonTextTemplate";
            this.btnInstallButtonTextTemplate.Size = new System.Drawing.Size(119, 23);
            this.btnInstallButtonTextTemplate.TabIndex = 5;
            this.btnInstallButtonTextTemplate.Text = "InstallButtonText";
            this.btnInstallButtonTextTemplate.UseVisualStyleBackColor = true;
            this.btnInstallButtonTextTemplate.Click += new System.EventHandler(this.btnInstallButtonTextTemplate_Click);
            // 
            // btnFontsRequiredExpressionTemplate
            // 
            this.btnFontsRequiredExpressionTemplate.AutoSize = true;
            this.btnFontsRequiredExpressionTemplate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFontsRequiredExpressionTemplate.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFontsRequiredExpressionTemplate.Location = new System.Drawing.Point(161, 29);
            this.btnFontsRequiredExpressionTemplate.Name = "btnFontsRequiredExpressionTemplate";
            this.btnFontsRequiredExpressionTemplate.Size = new System.Drawing.Size(155, 23);
            this.btnFontsRequiredExpressionTemplate.TabIndex = 6;
            this.btnFontsRequiredExpressionTemplate.Text = "FontsRequiredExpression";
            this.btnFontsRequiredExpressionTemplate.UseVisualStyleBackColor = true;
            this.btnFontsRequiredExpressionTemplate.Click += new System.EventHandler(this.btnFontsRequiredExpressionTemplate_Click);
            // 
            // btnFontsExpressionTemplate
            // 
            this.btnFontsExpressionTemplate.AutoSize = true;
            this.btnFontsExpressionTemplate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFontsExpressionTemplate.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFontsExpressionTemplate.Location = new System.Drawing.Point(54, 29);
            this.btnFontsExpressionTemplate.Name = "btnFontsExpressionTemplate";
            this.btnFontsExpressionTemplate.Size = new System.Drawing.Size(107, 23);
            this.btnFontsExpressionTemplate.TabIndex = 7;
            this.btnFontsExpressionTemplate.Text = "FontsExpression";
            this.btnFontsExpressionTemplate.UseVisualStyleBackColor = true;
            this.btnFontsExpressionTemplate.Click += new System.EventHandler(this.btnFontsExpressionTemplate_Click);
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
            this.pnlTags.Size = new System.Drawing.Size(346, 58);
            this.pnlTags.TabIndex = 8;
            // 
            // pnlEditor
            // 
            this.pnlEditor.Controls.Add(this.txtEditString);
            this.pnlEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEditor.Location = new System.Drawing.Point(0, 58);
            this.pnlEditor.Name = "pnlEditor";
            this.pnlEditor.Size = new System.Drawing.Size(346, 251);
            this.pnlEditor.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(7, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "DIALOG STRINGS EDITOR";
            // 
            // DialogStringEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(346, 338);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlTopBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "DialogStringEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FontsInstaller: Dialog String Editor";
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlTags.ResumeLayout(false);
            this.pnlTags.PerformLayout();
            this.pnlEditor.ResumeLayout(false);
            this.pnlEditor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip infoTip;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TextBox txtEditString;
        private System.Windows.Forms.Button btnFontsExpressionTemplate;
        private System.Windows.Forms.Button btnFontsRequiredExpressionTemplate;
        private System.Windows.Forms.Button btnInstallButtonTextTemplate;
        private System.Windows.Forms.Button btnFontsCountTemplate;
        private System.Windows.Forms.Button btnFontsTemplate;
        private System.Windows.Forms.Button btnAppVersionTemplate;
        private System.Windows.Forms.Button btnAppNameTemplate;
        private System.Windows.Forms.Panel pnlEditor;
        private System.Windows.Forms.Panel pnlTags;
        private System.Windows.Forms.Label label1;
    }
}