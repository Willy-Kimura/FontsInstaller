/*
 * Dialog String Editor
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
    public partial class DialogStringEditorView : Form
    {
        #region Constructor

        public DialogStringEditorView()
        {
            InitializeComponent();

            TopLevel = false;
        }

        #endregion

        #region Fields
        
        internal IWindowsFormsEditorService EditorService;

        #endregion

        #region Properties

        #endregion

        #region Methods

        #region Public
        
        #endregion

        #region Private
        
        #endregion

        #endregion

        #region Events

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                
                return true;
            }
            else if (keyData == Keys.F5)
            {
                
                return true;
            }
            else
            {
                return base.ProcessDialogKey(keyData);
            }
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            
        }

        private void OnFormShown(object sender, EventArgs e)
        {
            
        }
        
        private void OnClickOkay(object sender, EventArgs e)
        {
            
            EditorService.CloseDropDown();
        }

        private void OnClickCancel(object sender, EventArgs e)
        {
            EditorService.CloseDropDown();
        }

        private void OnClickRefresh(object sender, EventArgs e)
        {
            
        }

        #endregion

        private void btnAppNameTemplate_Click(object sender, EventArgs e)
        {

        }

        private void btnAppVersionTemplate_Click(object sender, EventArgs e)
        {

        }

        private void btnInstallButtonTextTemplate_Click(object sender, EventArgs e)
        {

        }

        private void btnFontsCountTemplate_Click(object sender, EventArgs e)
        {

        }

        private void btnFontsTemplate_Click(object sender, EventArgs e)
        {

        }

        private void btnFontsExpressionTemplate_Click(object sender, EventArgs e)
        {

        }

        private void btnFontsRequiredExpressionTemplate_Click(object sender, EventArgs e)
        {

        }
    }
}
