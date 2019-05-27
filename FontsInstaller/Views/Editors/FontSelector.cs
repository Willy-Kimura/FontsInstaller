/*
 * Font Selector
 * --------------------------------------------------+
 * This window is used in the loading and selection  |
 * of installed fonts from the system. It supports   |
 * font previews, searching, and selection of the    |
 * displayed fonts from the 'Properties' window.     |
 * --------------------------------------------------+
 * 
 */


using System;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms.Design;

using WK.Libraries.FontsInstallerNS.Views.Forms;

namespace WK.Libraries.FontsInstallerNS.Views.Editors
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial class FontSelector : Form
    {
        #region Constructor

        public FontSelector()
        {
            InitializeComponent();

            TopLevel = true;
        }

        #endregion

        #region Fields

        private int caretPosition;

        private string _loadingFontsList = "Loading Fonts...";
        private string _issueLoadingFonts = "There was an issue loading the fonts. Please try again.";

        private TaskWait _taskWait = new TaskWait();
        internal IWindowsFormsEditorService EditorService;

        #endregion

        #region Properties

        public bool Initialized { get; set; }
        public string SelectedFont { get; internal set; }
        public Dictionary<string, string> InstalledFonts { get; set; }
            = new Dictionary<string, string>();

        #endregion

        #region Methods

        #region Public

        public void GetFontsList()
        {
            try
            {
                if (Visible)
                {
                    Cursor = Cursors.WaitCursor;
                    SetStatus(_loadingFontsList);
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(
                    Environment.GetFolderPath(Environment.SpecialFolder.Fonts));

                FileInfo[] fontFiles = directoryInfo.GetFiles("*.ttf");

                foreach (var fontFile in fontFiles)
                {
                    string font = GetFontName(fontFile.FullName);

                    if (!(lstFonts.Items.Contains(font)) && !(string.IsNullOrWhiteSpace(font)))
                    {
                        lstFonts.Items.Add(font);
                        InstalledFonts.Add(font, fontFile.FullName);
                    }
                }

                Initialized = true;

                if (Visible)
                {
                    Cursor = Cursors.Default;
                    HideStatus();
                }
            }
            catch (Exception)
            {
                if (Visible)
                {
                    Cursor = Cursors.Default;
                    SetStatus(_issueLoadingFonts);
                }
            }
        }

        public void RefreshFontsList()
        {
            InstalledFonts.Clear();
            lstFonts.Items.Clear();

            GetFontsList();
        }
        
        public string GetFontName(string fontFile)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();

            try
            {
                pfc.AddFontFile(fontFile);

                return (pfc.Families[0].Name);
            }
            catch (Exception) { return ""; }
            finally { pfc.Dispose(); }
        }

        public string GetFontPath(string fontName, bool useCache = true)
        {
            string path = string.Empty;

            if (useCache)
            {
                if (!Initialized)
                    GetFontsList();

                path = InstalledFonts[fontName];
            }
            else
            {
                try
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(
                        Environment.GetFolderPath(Environment.SpecialFolder.Fonts));

                    FileInfo[] fontFiles = directoryInfo.GetFiles("*.ttf");

                    foreach (var fontFile in fontFiles)
                    {
                        string font = GetFontName(fontFile.FullName);

                        if (font == fontName)
                            path = fontFile.FullName;
                    }
                }
                catch (Exception) { }
            }

            return path;
        }

        #endregion

        #region Private

        private void SearchFont(string searchString, bool scrollItemUp = true, bool selectFont = true)
        {
            // Ensure we have a proper string to search for.
            if (!string.IsNullOrEmpty(searchString))
            {
                // Find the item in the list and store the index to the item.
                int index = lstFonts.FindString(searchString);

                // Determine if a valid index is returned. Select the item if it is valid.
                if (index != -1)
                {
                    lstFonts.SetSelected(index, selectFont);

                    if (scrollItemUp)
                        lstFonts.TopIndex = index;
                }
            }
        }

        public static bool IsValueOdd(int value)
        {
            return value % 2 != 0;
        }

        private void SetStatus(string caption)
        {
            lblInfo.BringToFront();
            lblInfo.Text = caption;
            
            lblInfo.Show();
        }

        private void HideStatus()
        {
            lblInfo.Hide();
        }

        #endregion

        #endregion

        #region Events

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnSelectFont.PerformClick();
                return true;
            }
            else if (keyData == Keys.F5)
            {
                RefreshFontsList();
                return true;
            }
            else
            {
                return base.ProcessDialogKey(keyData);
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            GetFontsList();
        }

        private void OnFormShown(object sender, EventArgs e)
        {
            txtSearchFonts.Text = SelectedFont;
            txtSearchFonts.Focus();
        }
                
        private void OnSearchFontChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(string.IsNullOrWhiteSpace(txtSearchFonts.Text)) && (txtSearchFonts.Focused))
                {
                    SearchFont(txtSearchFonts.Text, true);
                }
                else if (!(string.IsNullOrWhiteSpace(txtSearchFonts.Text)) && !(txtSearchFonts.Focused))
                {
                    SearchFont(txtSearchFonts.Text, false);
                }
            }
            catch (Exception) { }
        }

        private void OnSelectedFontIndexChanged(object sender, EventArgs e)
        {
            txtSearchFonts.Focus();
        }

        private void OnClickOkay(object sender, EventArgs e)
        {
            SelectedFont = lstFonts.SelectedItem.ToString();
            Close();
        }

        private void OnClickCancel(object sender, EventArgs e)
        {
            Close();
        }
        
        private void OnClickRefresh(object sender, EventArgs e)
        {
            RefreshFontsList();
        }

        private void OnClickFont(object sender, MouseEventArgs e)
        {
            txtSearchFonts.Text = lstFonts.SelectedItem.ToString();
        }

        private void OnDoubleClickFont(object sender, MouseEventArgs e)
        {
            btnSelectFont.PerformClick();
        }

        private void OnKeyUpInFontItems(object sender, KeyEventArgs e)
        {
            txtSearchFonts.Focus();
            txtSearchFonts.SelectionStart = caretPosition;
        }

        private void OnKeyDownInFontItems(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSelectFont.PerformClick();
            }
        }
        
        private void OnKeyDownInSearchFont(object sender, KeyEventArgs e)
        {
            caretPosition = txtSearchFonts.SelectionStart;

            if (e.KeyCode == Keys.Enter)
            {
                btnSelectFont.PerformClick();

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (!(lstFonts.SelectedIndex == lstFonts.Items.Count - 1))
                    lstFonts.SelectedIndex = lstFonts.SelectedIndex + 1;

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (!(lstFonts.SelectedIndex == 0))
                    lstFonts.SelectedIndex = lstFonts.SelectedIndex - 1;

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                OnClickCancel(sender, EventArgs.Empty);

                e.Handled = true;
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                txtSearchFonts.SelectAll();

                e.Handled = true;
            }
        }

        private void OnDrawFontItem(object sender, DrawItemEventArgs e)
        {
            Font itemFont = new Font(lstFonts.Items[e.Index].ToString(), 12, FontStyle.Regular);
            SizeF itemStringSize = e.Graphics.MeasureString(lstFonts.Items[e.Index].ToString(), itemFont);

            Font subitemFont = new Font("Segoe UI", 7, FontStyle.Regular);
            SizeF subitemStringSize = e.Graphics.MeasureString(lstFonts.Items[e.Index].ToString(), subitemFont);

            Rectangle itemRectangle = new Rectangle(new Point(e.Bounds.X + 3, e.Bounds.Y +
                (e.Bounds.Height - (int)itemStringSize.Height) / 2),
                new Size(e.Bounds.Width, e.Bounds.Height + 12));

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index,
                    e.State ^ DrawItemState.Selected, Color.Black, Color.DodgerBlue);

                e.DrawBackground();

                e.Graphics.DrawString(lstFonts.Items[e.Index].ToString(),
                    new Font(lstFonts.Items[e.Index].ToString(), 12, FontStyle.Regular),
                    Brushes.White, itemRectangle);

                e.Graphics.DrawString($"({lstFonts.Items[e.Index].ToString()})",
                    subitemFont, Brushes.White,
                    new Point((int)itemStringSize.Width + 5, itemRectangle.Y));
            }
            else
            {
                e.DrawBackground();

                if (IsValueOdd(e.Index))
                {
                    e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index,
                        e.State ^ DrawItemState.Default, Color.Black, Color.WhiteSmoke);
                }
                else
                {
                    e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index,
                        e.State ^ DrawItemState.Default, Color.Black, Color.White);
                }

                e.DrawBackground();

                e.Graphics.DrawString(lstFonts.Items[e.Index].ToString(),
                    new Font(lstFonts.Items[e.Index].ToString(), 12, FontStyle.Regular),
                    Brushes.Black, itemRectangle);

                e.Graphics.DrawString($"({lstFonts.Items[e.Index].ToString()})",
                    subitemFont, Brushes.DimGray,
                    new Point((int)itemStringSize.Width + 5, itemRectangle.Y));
            }

            e.DrawFocusRectangle();
        }

        private void OnMeasureFontItem(object sender, MeasureItemEventArgs e)
        {
            Font font = new Font(lstFonts.Items[e.Index].ToString(), 12, FontStyle.Regular);
            SizeF stringSize = e.Graphics.MeasureString(lstFonts.Items[e.Index].ToString(), font);

            e.ItemHeight = (int)stringSize.Height + 15;
            e.ItemWidth = (int)stringSize.Width;
        }

        private void InitializeFontsList(object sender, DoWorkEventArgs e)
        {
            Invoke((Action)delegate
            {
                GetFontsList();
            });
        }

        #endregion
    }
}
