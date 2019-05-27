namespace WK.Libraries.FontsInstallerNS.Views.Forms
{
    partial class TaskWait
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskWait));
            this.taskStatus = new System.Windows.Forms.Label();
            this.taskIcon = new System.Windows.Forms.PictureBox();
            this.taskProgress = new System.Windows.Forms.Label();
            this.progressIndicator1 = new ProgressControls.ProgressIndicator();
            this.left = new System.Windows.Forms.Panel();
            this.right = new System.Windows.Forms.Panel();
            this.top = new System.Windows.Forms.Panel();
            this.bottom = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.taskIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // taskStatus
            // 
            this.taskStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskStatus.AutoSize = true;
            this.taskStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskStatus.Location = new System.Drawing.Point(129, 56);
            this.taskStatus.Name = "taskStatus";
            this.taskStatus.Size = new System.Drawing.Size(70, 15);
            this.taskStatus.TabIndex = 0;
            this.taskStatus.Text = "{TaskStatus}";
            this.taskStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // taskIcon
            // 
            this.taskIcon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.taskIcon.Image = ((System.Drawing.Image)(resources.GetObject("taskIcon.Image")));
            this.taskIcon.Location = new System.Drawing.Point(149, 16);
            this.taskIcon.Name = "taskIcon";
            this.taskIcon.Size = new System.Drawing.Size(30, 30);
            this.taskIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.taskIcon.TabIndex = 1;
            this.taskIcon.TabStop = false;
            this.taskIcon.Visible = false;
            // 
            // taskProgress
            // 
            this.taskProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskProgress.AutoSize = true;
            this.taskProgress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskProgress.ForeColor = System.Drawing.Color.Gray;
            this.taskProgress.Location = new System.Drawing.Point(164, 75);
            this.taskProgress.Name = "taskProgress";
            this.taskProgress.Size = new System.Drawing.Size(0, 15);
            this.taskProgress.TabIndex = 2;
            this.taskProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressIndicator1
            // 
            this.progressIndicator1.AnimationSpeed = 80;
            this.progressIndicator1.AutoStart = true;
            this.progressIndicator1.Location = new System.Drawing.Point(142, 7);
            this.progressIndicator1.Name = "progressIndicator1";
            this.progressIndicator1.Percentage = 0F;
            this.progressIndicator1.Size = new System.Drawing.Size(45, 45);
            this.progressIndicator1.TabIndex = 3;
            this.progressIndicator1.Text = "progressIndicator1";
            // 
            // left
            // 
            this.left.BackColor = System.Drawing.Color.Silver;
            this.left.Dock = System.Windows.Forms.DockStyle.Left;
            this.left.Location = new System.Drawing.Point(0, 0);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(1, 101);
            this.left.TabIndex = 4;
            // 
            // right
            // 
            this.right.BackColor = System.Drawing.Color.Silver;
            this.right.Dock = System.Windows.Forms.DockStyle.Right;
            this.right.Location = new System.Drawing.Point(327, 0);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(1, 101);
            this.right.TabIndex = 5;
            // 
            // top
            // 
            this.top.BackColor = System.Drawing.Color.Silver;
            this.top.Dock = System.Windows.Forms.DockStyle.Top;
            this.top.Location = new System.Drawing.Point(1, 0);
            this.top.Name = "top";
            this.top.Size = new System.Drawing.Size(326, 1);
            this.top.TabIndex = 6;
            // 
            // bottom
            // 
            this.bottom.BackColor = System.Drawing.Color.Silver;
            this.bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom.Location = new System.Drawing.Point(1, 100);
            this.bottom.Name = "bottom";
            this.bottom.Size = new System.Drawing.Size(326, 1);
            this.bottom.TabIndex = 7;
            // 
            // TaskWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(328, 101);
            this.ControlBox = false;
            this.Controls.Add(this.bottom);
            this.Controls.Add(this.top);
            this.Controls.Add(this.right);
            this.Controls.Add(this.left);
            this.Controls.Add(this.progressIndicator1);
            this.Controls.Add(this.taskProgress);
            this.Controls.Add(this.taskIcon);
            this.Controls.Add(this.taskStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "TaskWait";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "{TaskName}";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.taskIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox taskIcon;
        public System.Windows.Forms.Label taskStatus;
        public System.Windows.Forms.Label taskProgress;
        private ProgressControls.ProgressIndicator progressIndicator1;
        private System.Windows.Forms.Panel left;
        private System.Windows.Forms.Panel right;
        private System.Windows.Forms.Panel top;
        private System.Windows.Forms.Panel bottom;
    }
}