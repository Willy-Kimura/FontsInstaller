using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;

using WK.Libraries.FontsInstallerNS.Models;
using WK.Libraries.FontsInstallerNS.Editors;

/*
 * Contains predefined property class models used 
 * when creating properties in Fonts Installer.
 * 
 */
namespace WK.Libraries.FontsInstallerNS.Models
{
    /// <summary>
    /// Defines a base class that hosts the necessary
    /// font information to be used when parsing any 
    /// font required by the target application.
    /// </summary>
    [Serializable]
    public class UserFont
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="UserFont"/>.
        /// </summary>
        public UserFont()
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="UserFont"/>.
        /// </summary>
        /// <param name="fontName">Provide a valid font name.</param>
        public UserFont(string fontName)
        {
            Name = fontName;
        }

        #endregion

        #region Fields

        private string _name;
        private string _path;

        #endregion

        #region Properties

        /// <summary>
        /// When set to true, the font name will be extracted
        /// from the font-file specified. However, when set to 
        /// false, the font-file's name name will be used instead.
        /// </summary>
        [Browsable(false)]
        public bool ExtractFontName { get; set; } = true;

        /// <summary>
        /// As an alternative to adding/selecting a font's name using 
        /// the <see cref="Name"/> property, this property allows you
        /// to add a font by providing the path to any TrueType font-file.
        /// </summary>
        [Category("Font Options")]
        [DisplayName("Path (Alternative)")]
        [Description("As an alternative to adding/selecting a font using the " +
                     "'Name' property, this property allows you to add a " +
                     "font by providing the path to any TrueType font-file.")]
        [Editor(typeof(FontFileSelectionEditor), typeof(UITypeEditor))]
        public string Path
        {
            get { return _path; }
            set {

                if (File.Exists(value))
                {
                    if (System.IO.Path.GetExtension(value) != ".ttf")
                    {
                        if (GetFontName(value) == string.Empty)
                        {
                            throw new Exception($"The file '{System.IO.Path.GetFileName(value)}' is not an actual font-file. " +
                                                $"Please try selecting a valid font-file.");
                        }
                        else if (System.IO.Path.GetExtension(value) == ".otf")
                        {
                            throw new Exception("OpenType fonts are currently not supported. " +
                                                "Try using an alternative TrueType font.");
                        }
                    }
                    else
                    {
                        _path = value;
                        _name = $"{(ExtractFontName ? GetFontName(_path) : System.IO.Path.GetFileNameWithoutExtension(_path))}";

                        FontBytes = File.ReadAllBytes(_path);
                    }
                }
                else
                {
                    if (FontsInstaller.DebugMode())
                        throw new FileNotFoundException("The path to the font-file specified doesn't exist.");
                }

            }
        }

        /// <summary>
        /// Gets or sets the font to be installed on the user's machine.
        /// </summary>
        [DisplayName("Font")]
        [Category("Font Options")]
        [Editor(typeof(FontSelectionEditor), typeof(UITypeEditor))]
        [Description("Sets the font to be installed on the user's machine.")]
        public string Name
        {
            get { return _name; }
            set {

                _name = value;

                if (!string.IsNullOrWhiteSpace(value))
                    Path = GetFontPath(value, true).Replace("WINDOWS", "Windows");

            }
        }

        /// <summary>
        /// Gets or sets the font data in <see cref="byte"/> collection format.
        /// </summary>
        internal byte[] FontBytes { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the actual name of the font from the font-file specified.
        /// </summary>
        /// <param name="fontFile">The full path to the TrueType font-file.</param>
        /// <returns>The font-name if found, or <see cref="string.Empty"/> if no name is found.</returns>
        private string GetFontName(string fontFile)
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

        /// <summary>
        /// Gets the full path to any font installed in the system.
        /// </summary>
        /// <param name="fontName">The font name.</param>
        /// <param name="useCache">
        /// If set to true, the current list of fonts installed will 
        /// be used during the search; else, a forced search for the font 
        /// specified will be made in the Windows Fonts directory until found.
        /// </param>
        /// <returns>
        /// The full path to the specified font; else returns 
        /// an empty <see cref="string"/> if the font is not available.
        /// </returns>
        private string GetFontPath(string fontName, bool useCache = false)
        {
            string path = string.Empty;

            if (useCache)
            {
                if (!FontsInstaller.FontSelectionEditor.Initialized)
                    FontsInstaller.FontSelectionEditor.GetFontsList();

                if (FontsInstaller.FontSelectionEditor.InstalledFonts.ContainsKey(fontName))
                    path = FontsInstaller.FontSelectionEditor.InstalledFonts[fontName];
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

        /// <summary>
        /// Checks if any particular font installed using its valid name.
        /// Acts as a short-hand method for the <see cref="IsFontInstalled(string, FontStyle)"/> method.
        /// </summary>
        /// <param name="fontFile">The path to the font-file.</param>
        /// <returns><see cref="true"/>/<see cref="false"/></returns>
        private bool IsFontInstalled(string fontFile)
        {
            bool installed = IsFontInstalled(GetFontName(fontFile), FontStyle.Regular);

            return installed;
        }

        /// <summary>
        /// Checks if any particular font installed using its valid name.
        /// a specified font-style option.
        /// </summary>
        /// <param name="fontFile">The path to the font-file.</param>
        /// <param name="fontStyle">The font-style to check for availability.</param>
        /// <returns><see cref="true"/>/<see cref="false"/></returns>
        private bool IsFontInstalled(string fontFile, FontStyle fontStyle)
        {
            bool installed = false;
            const float emSize = 8.0f;

            try
            {
                using (var testFont = new Font(GetFontName(fontFile), emSize, fontStyle))
                {
                    installed = (0 == string.Compare(GetFontName(fontFile), testFont.Name,
                                 StringComparison.InvariantCultureIgnoreCase));
                }
            }
            catch { }

            return installed;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a <see cref="string"/> containing the 
        /// name of the font-file selected.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return "(Choose Font)";
            else
                return $"{Name}";
        }

        #endregion
    }

    /// <summary>
    /// Defines a base class that hosts the necessary options 
    /// required to display the default Fonts Installer dialog.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class InstallerDialogOptions
    {
        #region Constructor

        /// <summary>
        /// Creates a new Installer dialog options object.
        /// </summary>
        public InstallerDialogOptions()
        {
            WindowTitle = "{AppName}";
            CollapsedContent = "{Fonts}";
            Title = "Fonts installation required";
            Content =
                    "In order for '{AppName}' to look neat and work effectively, " +
                    "{FontsRequiredExpression} to be installed in your system before proceeding. " +
                    "To view {FontsExpression}, click \"See details\".";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Installer dialog's window title.
        /// This refers to the title shown within the 
        /// Title Bar area of the dialog window.
        /// </summary>
        [Category("Installation Options")]
        [Description("Sets the Installer dialog's window title. " +
                     "This refers to the title shown within the " +
                     "Title Bar area of the dialog window.")]
        [Editor(typeof(DialogStringsEditor), typeof(UITypeEditor))]
        public string WindowTitle { get; set; }

        /// <summary>
        /// Gets or sets the Installer dialog's main title.
        /// This refers to the title shown within the 
        /// content area of the dialog window.
        /// </summary>
        [Category("Installation Options")]
        [Description("Sets the Installer dialog's main title. " +
                     "This refers to the title shown within the " +
                     "content area of the dialog window.")]
        [Editor(typeof(DialogStringsEditor), typeof(UITypeEditor))]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Installer dialog's main content.
        /// </summary>
        [Category("Installation Options")]
        [Description("Sets the Installer dialog's main content.")]
        [Editor(typeof(DialogStringsEditor), typeof(UITypeEditor))]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the Installer dialog's collapsed content.
        /// The collapsed content by default is set to be the list of 
        /// fonts that will be installed in a user's computer.
        /// </summary>
        [Category("Installation Options")]
        [Description("Sets the Installer dialog's collapsed content. " +
                     "The collapsed content by default is set to be the " +
                     "list of fonts that will be installed in a user's " +
                     "computer.")]
        [Editor(typeof(DialogStringsEditor), typeof(UITypeEditor))]
        public string CollapsedContent { get; set; }

        /// <summary>
        /// When set to true, a Windows Elevation icon 
        /// will be displayed in the 'Install' button 
        /// in-place of the standard Arrow icon.
        /// </summary>
        [Category("Installation Options")]
        [Description("When set to true, a Windows Elevation icon " +
                     "will be displayed in the 'Install' button " +
                     "in-place of the standard Arrow icon.")]
        public bool UseElevationIcon { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether a 'Cancel' 
        /// button will be displayed in the Installer dialog
        /// to allow exiting the fonts-installation process. 
        /// [Default: <see cref="false"/>]
        /// </summary>
        [Category("Installation Options")]
        [Description("When set to true, a 'Cancel' button will " +
                     "be displayed in the Installer dialog to " +
                     "allow exiting the fonts-installation process.")]
        public bool ShowCancelButton { get; set; } = false;

        /// <summary>
        /// When set to true, the current application's icon 
        /// will be displayed in the Installation dialog. 
        /// This can visually help assure the user of the
        /// dialog's express origin.
        /// </summary>
        [Category("Installation Options")]
        [Description("When set to true, the current application's icon " +
                     "will be displayed in the Installation dialog. " +
                     "This can visually help assure the user of the " +
                     "dialog's express origin.")]
        public bool ShowAppIcon { get; set; } = true;

        /// <summary>
        /// Lets you provide a custom icon file to
        /// be used as the Installer dialog's icon.
        /// (TIP: Pass the string "(default)" without 
        /// quotes to apply the default Fonts Installer icon.)
        /// </summary>
        [Category("Installation Options")]
        [Description("Lets you provide a custom icon file to " +
                     "be used as the Installer dialog's icon. \n" +
                     "(TIP: Type \"(default)\" without quotes " +
                     "to apply the default Fonts Installer icon.)")]
        [Editor(typeof(IconSelectionEditor), typeof(UITypeEditor))]
        public string CustomIcon { get; set; }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns the default <see cref="string"/>
        /// as specified in the Properties window.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"WindowTitle: {WindowTitle}, Title: '{Title}', ShowAppIcon: {ShowAppIcon}, " +
                   $"ShowCancelButton: {ShowCancelButton}, UseElevationIcon: {UseElevationIcon}";
        }

        #endregion
    }
}

/*
 * Contains predefined 'UITypeEditors' used to provide
 * users with a better experience when working with
 * various properties in Fonts Installer.
 * 
 */
namespace WK.Libraries.FontsInstallerNS.Editors
{
    /// <summary>
    /// Provides a custom Font Selection UI Editor
    /// for selecting fonts via the Properties window.
    /// (Please note that this only returns a font's name.)
    /// <para>
    /// Example:
    /// <code>
    /// [Editor(typeof(FontSelectionEditor), typeof(UITypeEditor))]
    /// public string MyFont { get; set; }
    /// </code>
    /// </para>
    /// </summary>
    [DebuggerStepThrough]
    public class FontSelectionEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context,
                                IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService =
               provider.GetService(typeof(IWindowsFormsEditorService)) as
               IWindowsFormsEditorService;

            if (editorService != null)
            {
                FontsInstaller.FontSelectionEditor.EditorService = editorService;
                FontsInstaller.FontSelectionEditor.SelectedFont = (string)value;
                FontsInstaller.FontSelectionEditor.txtSearchFonts.Text = (string)value;

                FontsInstaller.FontSelectionEditor.txtSearchFonts.Focus();
                FontsInstaller.FontSelectionEditor.txtSearchFonts.SelectAll();

                editorService.ShowDialog(FontsInstaller.FontSelectionEditor);
                value = FontsInstaller.FontSelectionEditor.SelectedFont;
            }

            return value;
        }
    }

    /// <summary>
    /// Provides a custom Font File Selection UI Editor
    /// for selecting TrueType fonts using Windows Explorer
    /// from the Properties window.
    /// <para>
    /// Example:
    /// <code>
    /// [Editor(typeof(FontFileSelectionEditor), typeof(UITypeEditor))]
    /// public string MyFontPath { get; set; }
    /// </code>
    /// </para>
    /// </summary>
    [DebuggerStepThrough]
    public class FontFileSelectionEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context,
                                IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService =
               provider.GetService(typeof(IWindowsFormsEditorService)) as
               IWindowsFormsEditorService;

            if (editorService != null)
            {
                OpenFileDialog editor = new OpenFileDialog();

                editor.Multiselect = false;
                editor.CheckFileExists = true;
                editor.Title = "Select a custom font...";
                editor.Filter = "TrueType Fonts | *.ttf";

                if (editor.ShowDialog() == DialogResult.OK)
                    value = editor.FileName;
            }

            return value;
        }
    }

    /// <summary>
    /// Provides a custom Icon Selection UI Editor
    /// for selecting Icons using Windows Explorer
    /// from the Properties window.
    /// <para>
    /// Example:
    /// <code>
    /// [Editor(typeof(IconSelectionEditor), typeof(UITypeEditor))]
    /// public string MyIcon { get; set; }
    /// </code>
    /// </para>
    /// </summary>
    [DebuggerStepThrough]
    public class IconSelectionEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context,
                                IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService =
               provider.GetService(typeof(IWindowsFormsEditorService)) as
               IWindowsFormsEditorService;

            if (editorService != null)
            {
                OpenFileDialog editor = new OpenFileDialog();

                editor.Multiselect = false;
                editor.CheckFileExists = true;
                editor.Title = "Select a custom icon...";
                editor.Filter = "Icons | *.ico";

                if (editor.ShowDialog() == DialogResult.OK)
                    value = editor.FileName;
            }

            return value;
        }
    }

    /// <summary>
    /// Provides a custom Dialog Strings UI Editor
    /// for editing the Installer dialog's strings
    /// from the Properties window.
    /// </summary>
    [DebuggerStepThrough]
    public class DialogStringsEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context,
                                IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService =
               provider.GetService(typeof(IWindowsFormsEditorService)) as
               IWindowsFormsEditorService;

            if (editorService != null)
            {
                Views.Editors.StringsEditor editor = new Views.Editors.StringsEditor();

                editor.EditorService = editorService;
                editor.Content = (string)value;

                editorService.DropDownControl(editor);
                value = editor.Content;
            }

            return value;
        }
    }

    /// <summary>
    /// Provides a dialog-based editor that displays 
    /// the Installer dialog from the Properties window.
    /// </summary>
    [DebuggerStepThrough]
    public class DialogPreviewEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context,
                                IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService =
               provider.GetService(typeof(IWindowsFormsEditorService)) as
               IWindowsFormsEditorService;

            FontsInstaller.Instance.ShowInstallerDialog(true);

            return value;
        }
    }
}

/*
 * Provides design-time services for Fonts Installer.
 * This mostly provides the Smart Tags design feature.
 * 
 */
namespace WK.Libraries.FontsInstallerNS.Designer
{
    #region Constructor

    /// <summary>
    /// Provides <see cref="FontsInstaller"/> design-time features.
    /// </summary>
    [DebuggerStepThrough]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class WKDesigner : ComponentDesigner
    {
        private DesignerActionListCollection actionLists;

        /// <summary>
        /// Provides <see cref="FontsInstaller"/> design-time features.
        /// </summary>
        WKDesigner()
        {

        }

        // Use pull model to populate smart tag menu.
        public override DesignerActionListCollection ActionLists
        {
            get {
                if (null == actionLists)
                {
                    actionLists = new DesignerActionListCollection
                        {
                            new WKComponentActionList(this.Component)
                        };
                }

                return actionLists;
            }
        }
    }

    #endregion

    #region Properties

    /// <summary>
    /// Initializes a new instance of the <see cref="WKComponentActionList"/> class.
    /// </summary>
    [DebuggerStepThrough]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class WKComponentActionList : DesignerActionList
    {
        private FontsInstaller WKComponent;
        private DesignerActionUIService designerActionUISvc = null;

        public WKComponentActionList(IComponent component) : base(component)
        {
            this.WKComponent = component as FontsInstaller;

            // Cache a reference to DesignerActionUIService so that the DesignerActionList can be refreshed.
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;

            // Automatically display Smart Tags for quick access to the most common properties needed by users.
            this.AutoShow = true;
        }

        #region Properties Manager

        internal static PropertyDescriptor GetPropertyDescriptor(IComponent component, string propertyName)
        {
            return TypeDescriptor.GetProperties(component)[propertyName];
        }

        internal static IDesignerHost GetDesignerHost(IComponent component)
        {
            return (IDesignerHost)component.Site.GetService(typeof(IDesignerHost));
        }

        internal static IComponentChangeService GetChangeService(IComponent component)
        {
            return (IComponentChangeService)component.Site.GetService(typeof(IComponentChangeService));
        }

        internal static void SetValue(IComponent component, string propertyName, object value)
        {
            PropertyDescriptor propertyDescriptor = GetPropertyDescriptor(component, propertyName);
            IComponentChangeService svc = GetChangeService(component);
            IDesignerHost host = GetDesignerHost(component);
            DesignerTransaction txn = host.CreateTransaction();

            try
            {
                svc.OnComponentChanging(component, propertyDescriptor);
                propertyDescriptor.SetValue(component, value);
                svc.OnComponentChanged(component, propertyDescriptor, null, null);
                txn.Commit();
                txn = null;
            }

            finally
            {
                if (txn != null)
                    txn.Cancel();
            }
        }

        #endregion

        /// <summary>
        /// Implementation of this abstract method creates Smart Tag items,
        /// associates their targets, and collects them into a list.
        /// </summary>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection
                {
                    new DesignerActionMethodItem(this, "ShowFontsCollectionEditor",
                                     "Choose fonts required...", "Common Tasks", true),

                    new DesignerActionMethodItem(this, "PreviewInstallerDialog",
                                     "Preview Installer dialog", "Common Tasks", true)
                };

            return items;
        }

        #region Smart Tag Methods

        public void PreviewInstallerDialog()
        {
            WKComponent.ShowInstallerDialog(true);
        }

        public void ShowFontsCollectionEditor()
        {
            var fontsPropertyDescriptor = TypeDescriptor.GetProperties(WKComponent)["Fonts"];
            var context = new TypeDescriptionContext(WKComponent, fontsPropertyDescriptor);
            var editor = (UITypeEditor)fontsPropertyDescriptor.GetEditor(typeof(UITypeEditor));
            var property = (WKComponent).Fonts;
            var result = (List<UserFont>)editor.EditValue(context, context, property);

            if (!result.SequenceEqual(property))
                fontsPropertyDescriptor.SetValue(WKComponent, result);
        }

        #endregion
    }

    #endregion

    #region UI-Type-Editor Invoker

    [DebuggerStepThrough]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class TypeDescriptionContext : ITypeDescriptorContext,
        IServiceProvider, IWindowsFormsEditorService
    {
        private Component component;
        private PropertyDescriptor editingProperty;

        public TypeDescriptionContext(Component component, PropertyDescriptor property)
        {
            this.component = component;
            editingProperty = property;
        }

        public IContainer Container { get { return component.Container; } }
        public object Instance { get { return component; } }

        public void OnComponentChanged()
        {
            var svc = (IComponentChangeService)this.GetService(
                typeof(IComponentChangeService));

            svc.OnComponentChanged(component, editingProperty, null, null);
        }

        public bool OnComponentChanging() { return true; }
        public PropertyDescriptor PropertyDescriptor { get { return editingProperty; } }

        public object GetService(Type serviceType)
        {
            if ((serviceType == typeof(ITypeDescriptorContext)) ||
                (serviceType == typeof(IWindowsFormsEditorService)))
                return this;

            return component.Site.GetService(serviceType);
        }

        public void CloseDropDown() { }
        public void DropDownControl(Control control) { }

        DialogResult IWindowsFormsEditorService.ShowDialog(Form dialog)
        {
            IUIService service = (IUIService)(this.GetService(typeof(IUIService)));
            return service.ShowDialog(dialog);
        }
    }

    #endregion
}
