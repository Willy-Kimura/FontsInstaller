/*
 * Task Wait Dialog
 * -----------------------------------------+
 * This window is primarily used to inform  |
 * users that an application-restart event  |
 * is about to occur and is used right      |
 * after the fonts installation process has |
 * been completed. It can also be used for  |
 * manual application restarts.             |
 * -----------------------------------------+
 * 
 */


using System;
using System.Windows.Forms;

namespace WK.Libraries.FontsInstallerNS.Views.Forms
{
    public partial class TaskWait : Form
    {
        #region Constructor

        public TaskWait()
        {
            InitializeComponent();
        }

        #endregion

        #region Fields

        private int _progress;
        private System.Threading.Timer _timer;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether
        /// the user invoked this dialog manually.
        /// </summary>
        public bool UserInvoked { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the target 
        /// application should restart once the timeout completes.
        /// </summary>
        public bool RestartOnFinish { get; set; }

        /// <summary>
        /// Gets or sets the progress percentage of any specific task taking place.
        /// </summary>
        public int Progress
        {
            get {

                if (string.IsNullOrWhiteSpace(taskProgress.Text))
                    taskProgress.Hide();
                else
                    taskProgress.Show();

                return _progress;

            }
            set {

                _progress = value;
                taskProgress.Text = $"{value}%";

                if (string.IsNullOrWhiteSpace(taskProgress.Text))
                    taskProgress.Hide();
                else
                    taskProgress.Show();
            }
        }

        #endregion

        #region Methods

        public void Show(string taskName, string taskStatus, int timeout = 2000, bool restartOnFinish = false)
        {
            Text = taskName;
            RestartOnFinish = restartOnFinish;

            this.taskStatus.Text = taskStatus;
            this.taskStatus.Left = (Width - this.taskStatus.Width) / 2;
            this.taskProgress.Left = (Width - this.taskProgress.Width) / 2;
            
            Width = this.taskStatus.Right + (this.taskStatus.Left);

            _timer = new System.Threading.Timer(OnEndTimeout, null, timeout, 0);

            ShowDialog();
        }
        
        public void OnEndTimeout(Object state)
        {
            Invoke((Action)delegate
            {
                Close();

                if (RestartOnFinish)
                    Application.Restart();
            });
        }

        #endregion
    }
}
