/*
 * Dialog Strings Editor
 * ------------------------------------------+
 * This window is primarily used in editing  |
 * the Fonts Installer's dialog strings.     |
 * ------------------------------------------+
 * 
 */


using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Windows.Forms.Design;

namespace WK.Libraries.FontsInstallerNS.Views.Editors
{
    public partial class StringsEditor : Form
    {
        #region Constructor

        public StringsEditor()
        {
            InitializeComponent();

            TopLevel = false;
        }

        #endregion

        #region Fields

        private string _content;

        private int _currentPosition;

        internal IWindowsFormsEditorService EditorService;

        #endregion

        #region Properties

        public string Content
        {
            get { return _content; }
            set {

                _content = value;
                txtEditString.Text = _content;

            }
        }

        #endregion

        #region Methods

        #region Public

        #endregion

        #region Private

        public void AlignTagButtons()
        {
            btnAppNameTemplate.Location = new Point(7, 6);
            btnAppVersionTemplate.Location = new Point(67, 6);
            btnInstallButtonTextTemplate.Location = new Point(145, 6);
            btnFontsCountTemplate.Location = new Point(265, 6);
            btnFontsTemplate.Location = new Point(7, 30);
            btnFontsExpressionTemplate.Location = new Point(55, 30);
            btnFontsRequiredExpressionTemplate.Location = new Point(163, 30);

            pnlTags.Height = 63;
            Size = new Size(385, 338);

            txtEditString.Focus();
        }

        private void AddStringTemplate(string template)
        {
            string word = $"{{{template}}}";

            int endOfWordPosition = _currentPosition + word.Length;

            txtEditString.Text = txtEditString.Text.Insert(txtEditString.SelectionStart, word);
            txtEditString.SelectionStart = _currentPosition;
            
            txtEditString.Focus();
        }

        #endregion

        #endregion

        #region Events

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                return true;
            }
            else
            {
                return base.ProcessDialogKey(keyData);
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnShowDIalog.PerformClick();
            }
        }

        private void OnFormLoad(object sender, EventArgs e)
        {

        }

        private void OnFormShown(object sender, EventArgs e)
        {
            AlignTagButtons();
        }

        private void OnClickOkay(object sender, EventArgs e)
        {
            Content = txtEditString.Text;

            EditorService.CloseDropDown();
        }

        private void OnClickCancel(object sender, EventArgs e)
        {
            EditorService.CloseDropDown();
        }

        private void OnClickPlay(object sender, EventArgs e)
        {
            FontsInstaller.Instance.ShowInstallerDialog(true);
        }

        private void OnClickTemplates(object sender, EventArgs e)
        {
            if (pnlTags.Height >= 61)
                pnlTags.Height = 0;
            else
                pnlTags.Height = 63;
        }

        private void txtEditString_Leave(object sender, EventArgs e)
        {
            _currentPosition = txtEditString.SelectionStart;
        }

        private void btnAppNameTemplate_Click(object sender, EventArgs e)
        {
            AddStringTemplate("AppName");
        }

        private void btnAppVersionTemplate_Click(object sender, EventArgs e)
        {
            AddStringTemplate("AppVersion");
        }

        private void btnInstallButtonTextTemplate_Click(object sender, EventArgs e)
        {
            AddStringTemplate("InstallButtonText");
        }

        private void btnFontsCountTemplate_Click(object sender, EventArgs e)
        {
            AddStringTemplate("FontsCount");
        }

        private void btnFontsTemplate_Click(object sender, EventArgs e)
        {
            AddStringTemplate("Fonts");
        }

        private void btnFontsExpressionTemplate_Click(object sender, EventArgs e)
        {
            AddStringTemplate("FontsExpression");
        }

        private void btnFontsRequiredExpressionTemplate_Click(object sender, EventArgs e)
        {
            AddStringTemplate("FontsRequiredExpression");
        }

        #endregion
    }
}
