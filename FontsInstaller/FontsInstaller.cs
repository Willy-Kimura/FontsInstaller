/*
 * Developer    : Willy Kimura (WK).
 * Library      : FontsInstaller.
 * License      : MIT.
 * 
 * This awesome library was fashioned to assist .NET developers
 * worry less when building stunning Typography-rich applications 
 * without the fear of losing-in on the beauty of their well crafted
 * apps and products when running them across various user machines. 
 * FontsInstaller allows developers to choose the fonts required
 * by their applications and once run on any PC, it checks whether
 * the fonts are installed or not; if not installed, the missing 
 * fonts are automatically installed in the initial app-loading 
 * phase and once done, the application is launched seamlessly. 
 * It takes the cycle "check => install => run" whenever the
 * application is being launched from any user PC, therefore
 * providing worry-less experience when building your apps.
 * 
 * Improvements are welcome.
 * 
 */


using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.Design;

using WK.Libraries.FontsInstallerNS.Models;
using WK.Libraries.FontsInstallerNS.Editors;
using WK.Libraries.FontsInstallerNS.Designer;
using WK.Libraries.FontsInstallerNS.Views.Forms;

using Ookii.Dialogs;

namespace WK.Libraries.FontsInstallerNS
{
    /// <summary>
    /// A handy library that lets developers choose the fonts 
    /// required by their applications, then auto-checks and 
    /// installs them if not available on any client machine.
    /// </summary>
    [DefaultProperty("Fonts")]
    [Designer(typeof(WKDesigner))]
    [DefaultEvent("InstallingFonts")]
    [Description("A handy library that lets developers choose the fonts " +
                 "required by their applications, then auto-checks and " +
                 "installs them if not available on any client machine.")]
    public partial class FontsInstaller : Component
    {
        #region Constructors

        public FontsInstaller()
        {
            InitializeComponent();

            SetDefaults();
        }

        public FontsInstaller(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            SetDefaults();
        }

        #endregion

        #region Fields

        private bool _dialogShown;

        private static string _temp = Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData) +
            $"\\WK.Libraries.FontsInstaller\\";
        private static string _tempUnavailable = $"{_temp}\\Unavailable\\";

        private string _fontsBullet;
        private string _installerPath;

        internal TaskWait _task = new TaskWait();
        internal TaskDialog _installerDialog = new TaskDialog();
        internal TaskDialog _installerDialogPreview = new TaskDialog();

        private TaskDialogButton _cancelOption = new TaskDialogButton();
        private TaskDialogButton _installOption = new TaskDialogButton();

        private ContainerControl _containerControl = null;
        private InstallerDialogOptions _dialogOptions =
            new InstallerDialogOptions();

        private static FontsInstaller _instance;
        internal static Views.Editors.FontSelector FontSelectionEditor =
            new Views.Editors.FontSelector();

        #endregion

        #region Enumerations

        /// <summary>
        /// Provides a list of supported string templates
        /// that are generated at runtime providing a
        /// qualified name based on the template selected.
        /// </summary>
        public enum StringTemplates
        {
            /// <summary>
            /// Represents the default application's name.
            /// </summary>
            AppName = 0,

            /// <summary>
            /// Represents the default application's version.
            /// </summary>
            AppVersion = 1,

            /// <summary>
            /// Represents the list of user-fonts added.
            /// </summary>
            Fonts = 2,

            /// <summary>
            /// Represents the number of user-fonts added.
            /// </summary>
            FontsCount = 3,

            /// <summary>
            /// Represents the text applied to 
            /// the installation button.
            /// </summary>
            InstallButtonText = 4,

            /// <summary>
            /// Represents an empty string template.
            /// </summary>
            Empty = 5,

            /// <summary>
            /// Represents a statement auto-generated based on the
            /// number of fonts that are to be installed.
            /// </summary>
            FontsRequiredExpression = 6,

            /// <summary>
            /// Represents a statement auto-generated based on the
            /// number of fonts to be viewed within the collapsed-
            /// content area of the Installer dialog.
            /// </summary>
            FontsExpression = 7
        }

        #endregion

        #region Properties

        #region Browsable

        /// <summary>
        /// When set to true, the installation process will be 
        /// run when debugging from Visual Studio for you to have 
        /// a test preview of how the process will look like 
        /// whenever fonts require to be installed. This however 
        /// won't affect the execution process once your application 
        /// is deployed to all users.
        /// </summary>
        [Category("Development Options")]
        [Description("When set to true, the installation process will be " +
                     "run when debugging from Visual Studio for you to have " +
                     "a test preview of how the process will look like " +
                     "whenever fonts require to be installed. This however " +
                     "won't affect the execution process once your application " +
                     "is deployed to all users.")]
        public bool TestingMode { get; set; } = false;

        /// <summary>
        /// Provides a list of customization options for the
        /// default Installer dialog that will be displayed once 
        /// the fonts-installation process begins.
        /// </summary>
        [Category("Installation Options")]
        [Description("Provides a list of customization options for the " +
                     "default Installer dialog that will be displayed once " +
                     "the fonts-installation process begins.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Editor(typeof(DialogPreviewEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public InstallerDialogOptions DialogOptions { get; set; } = new InstallerDialogOptions();

        /// <summary>
        /// When set to true, the fonts-installation process 
        /// will begin immediately the hosting or parent Form 
        /// is launched. Otherwise when set to false, installation 
        /// can be done manually using the method <see cref="Install"/> or one of 
        /// its variant methods: <see cref="InstallFont(string, bool)"/>, 
        /// <see cref="InstallFonts(string[], bool)"/>,  
        /// <see cref="InstallFonts(string, bool)"/>.
        /// </summary>
        [Category("Installation Options")]
        [Description("When set to true, the fonts-installation process " +
                     "will begin immediately the hosting or parent Form " +
                     "is launched. Otherwise when set to false, installation " +
                     "can be done manually using the method 'Install()' or one of " +
                     "its variant methods: 'InstallFont()', 'InstallFonts()'.")]
        public bool AutoInstall { get; set; } = true;

        /// <summary>
        /// When set to true, once the fonts-installation process 
        /// is completed, the application will be restarted for the 
        /// font changes to take effect at the application-level. 
        /// Please note that restarting your application is highly 
        /// recommended since fonts initialization occurs on startup.
        /// Otherwise, no changes will take effect.
        /// </summary>
        [Category("Installation Options")]
        [Description("When set to true, once the fonts-installation process " +
                     "is completed, the application will be restarted for the " +
                     "font changes to take effect at the application-level. " +
                     "Please note that restarting your application is highly " +
                     "recommended since fonts initialization occurs on startup. " +
                     "Otherwise, no changes will take effect.")]
        public bool AutoRestart { get; set; } = true;

        /// <summary>
        /// Gets or sets the list of fonts that will be installed
        /// on the user's machine once the application is launched.
        /// </summary>
        [DisplayName("Choose Fonts...")]
        [Category("Installation Options")]
        [Description("Choose the list of fonts that will be installed on " +
                     "a user's machine once the installation process begins.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<UserFont> Fonts { get; set; } = new List<UserFont>();

        #endregion

        #region Non-browsable

        /// <summary>
        /// Gets or sets the as the parent form.
        /// </summary>
        [Browsable(false)]
        public Form ParentForm
        {
            get { return ((Form)ContainerControl); }
            set { ContainerControl = value; }
        }

        /// <summary>
        /// Gets or sets a custom Installer dialog 
        /// to be used instead of the default dialog.
        /// (Ensure you provide a button that returns 
        /// <see cref="DialogResult.OK"/>; <see cref="DialogResult.Cancel"/>)
        /// can also come in-handy.
        /// </summary>
        [Browsable(false)]
        public Form CustomInstallerDialog { get; set; }
        
        /// <summary>
        /// Gets or sets the bullet character(s) that will
        /// be applied to every font item displayed within
        /// the list of fonts that will be installed.
        /// </summary>
        [Browsable(false)]
        public string FontsListBullet
        {
            get { return _fontsBullet; }
            set { _fontsBullet = $"{value}"; }
        }

        /// <summary>
        /// Gets a value indicating whether the user cancelled
        /// the fonts installation dialog or process.
        /// </summary>
        [Browsable(false)]
        public bool UserCancelled { get; private set; }

        /// <summary>
        /// Gets the list of fonts to be installed on a user's 
        /// machine from the <see cref="Fonts"/> provided.
        /// </summary>
        [Browsable(false)]
        public List<UserFont> UnavailableFonts
        {
            get { return GetUnavailableFontsList(); }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether
        /// the fonts installation process took place 
        /// and was completed, successfully or unsuccessfully.
        /// </summary>
        [Browsable(false)]
        internal bool InstallTaskCompleted { get; set; }

        /// <summary>
        /// Gets an instance of <see cref="FontsInstaller"/>.
        /// </summary>
        [Browsable(false)]
        internal static FontsInstaller Instance
        {
            get {

                if (_instance != null)
                    return _instance;
                else
                    return new FontsInstaller();

            }
        }
        
        #region Parent Form Management

        /// <summary>
        /// Gets or sets the container control within the parent form.
        /// In most cases, this refers and results to the parent form.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ContainerControl ContainerControl
        {
            get { return _containerControl; }
            set {

                try
                {
                    _containerControl = value;

                    if (_containerControl is Control)
                    {
                        value.FindForm().Load += OnLoadParentForm;
                    }
                    else
                    {
                        ((Form)value).Load += OnLoadParentForm;
                    }
                }
                catch (Exception) { }

            }
        }

        /// <summary>
        /// Overrides the ISite functionality, getting the main or parent
        /// container control in the Form. This is overriden to get the
        /// component's host or parent form.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ISite Site
        {
            get { return base.Site; }
            set {

                base.Site = value;

                if (value == null)
                {
                    return;
                }

                if (value.GetService(typeof(IDesignerHost)) is IDesignerHost host)
                {
                    IComponent componentHost = host.RootComponent;

                    if (componentHost is ContainerControl)
                    {
                        ContainerControl = componentHost as ContainerControl;
                    }
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region Methods

        #region Private

        /// <summary>
        /// Sets the library-default settings.
        /// </summary>
        private void SetDefaults()
        {
            try
            {
                _instance = this;
                FontsListBullet = "+";

                if (Site.Component != null)
                    AutoInstall = true;
                else
                    AutoInstall = false;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Invokes a new <see cref="InstallationCompleted"/> event.
        /// </summary>
        /// <param name="success">
        /// Was the fonts installation process successful?
        /// </param>
        private void InvokeInstallationCompletedEvent(bool success)
        {
            if (InstallTaskCompleted)
            {
                if (ParentForm != null)
                {
                    ParentForm.Invoke((Action)delegate
                    {
                        InstallationCompleted?.Invoke(this,
                            new InstallationCompletedEventArgs(success, UserCancelled,
                            GetInstalledFonts(), GetUninstalledFonts()));
                    });
                }
                else
                {
                    try
                    {
                        var names = Application.OpenForms.Cast<Form>().
                                    Select(form => form.Name).ToList();

                        Application.OpenForms[names[0]].Invoke((Action)delegate
                        {
                            InstallationCompleted?.Invoke(this,
                                new InstallationCompletedEventArgs(success, UserCancelled,
                                GetInstalledFonts(), GetUninstalledFonts()));
                        });
                    }
                    catch (Exception)
                    {
                        if (!Environment.UserInteractive)
                        {
                            throw new Exception("Please use the 'ParentForm' property " +
                                               $"to set the Form hosting '{this.Site.Name}'. " +
                                               $"If you've hosted the component inside a control" +
                                               $"use the method 'Control.FindForm()' in the " +
                                               $"'ParentForm' property.");
                        }
                    }
                }
                
                InvokeApplicationRestartingEvent(success);
            }
        }

        /// <summary>
        /// Invokes a new <see cref="ApplicationRestarting"/> event.
        /// </summary>
        private void InvokeApplicationRestartingEvent(bool success)
        {
            if (ApplicationRestarting == null)
            {
                if (AutoRestart)
                {
                    _task.taskProgress.Text = "Restarting application...";
                    _task.Show($"Restarting '{Application.ProductName}'",
                                "Please wait for the application to restart...", 
                                2000, true);
                }
            }
            else
            {
                if (ParentForm != null)
                {
                    ParentForm.Invoke((Action)delegate
                    {
                        ApplicationRestarting?.Invoke(this, new ApplicationRestartingEventArgs(success));
                    });
                }
                else
                {
                    try
                    {
                        var names = Application.OpenForms.Cast<Form>().
                                    Select(form => form.Name).ToList();

                        Application.OpenForms[names[0]].Invoke((Action)delegate
                        {
                            ApplicationRestarting?.Invoke(this, new ApplicationRestartingEventArgs(success));
                        });
                    }
                    catch (Exception)
                    {
                        throw new Exception("Please use the 'ParentForm' property " +
                                           $"to set the Form hosting '{this.Site.Name}'. " +
                                           $"If you've hosted the component inside a control" +
                                           $"use the method 'Control.FindForm()' in the " +
                                           $"'ParentForm' property.");
                    }
                }
            }
        }

        /// <summary>
        /// Creates the master temporary font directory.
        /// </summary>
        private void CreateTempDirectory()
        {
            try
            {
                if (!Directory.Exists(_temp))
                    Directory.CreateDirectory(_temp);

                if (!Directory.Exists(_tempUnavailable))
                    Directory.CreateDirectory(_tempUnavailable);

                WriteInstaller();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Writes the VBScript Installer 
        /// from the library's Resources.
        /// </summary>
        private void WriteInstaller()
        {
            try
            {
                _installerPath = $"{_tempUnavailable}Installer.vbs";
                File.WriteAllText(_installerPath, Properties.Resources.Install);
            }
            catch (Exception) { }
        }
        
        /// <summary>
        /// Deletes the existing master temporary font directory.
        /// </summary>
        private void DeleteTempDirectory()
        {
            try
            {
                Directory.Delete(_temp, true);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Parses the Installer dialog's string 
        /// templates to human-readable strings.
        /// (Called when <see cref="CustomInstallerDialog"/>
        /// has been provided.)
        /// </summary>
        private void ParseDialogStrings()
        {
            try
            {
                DialogOptions.Title = ParseString(DialogOptions.Title);
                DialogOptions.Content = ParseString(DialogOptions.Content);
                DialogOptions.WindowTitle = ParseString(DialogOptions.WindowTitle);
                DialogOptions.CollapsedContent = ParseString(DialogOptions.CollapsedContent);
            }
            catch (Exception) { }
        }
        
        /// <summary>
        /// Launches VBScript as a process, installing 
        /// all pre-requisite fonts for the application.
        /// </summary>
        private bool StartProcess()
        {
            try
            {
                WriteInstaller();
                
                var process = new Process();
                
                process.StartInfo.FileName = "cscript";
                process.StartInfo.Arguments = $"\"{_installerPath}\"";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.EnableRaisingEvents = true;

                if (AutoInstall)
                    HideParentWindow();

                UserCancelled = false;
                InstallTaskCompleted = false;
                NotifyOnPreparation(false);
                InstallingFonts?.Invoke(this, EventArgs.Empty);

                process.Start();

                process.Exited += delegate
                {
                    _dialogShown = false;
                    InstallTaskCompleted = true;
                    
                    if (UserCancelled)
                    {
                        InvokeInstallationCompletedEvent(false);
                        InvokeApplicationRestartingEvent(false);
                    }
                    else
                    {
                        InvokeInstallationCompletedEvent(true);
                        InvokeApplicationRestartingEvent(true);
                    }

                    DeleteTempDirectory();
                };

                SpinWait.SpinUntil((Func<bool>)delegate
                {
                    return InstallTaskCompleted;
                });

                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// Installs the fonts via 'FontReg' 
        /// using all the provided parameters.
        /// </summary>
        /// <param name="showDialog"></param>
        /// <see cref="true"/> if installation was
        /// successful; else returns <see cref="false"/>.
        /// </returns>
        private bool RunInstaller(bool showDialog = true)
        {
            try
            {
                if (TestingMode)
                {
                    if (!DebugMode())
                    {
                        if (showDialog)
                        {
                            if ((CustomInstallerDialog != null) && (AutoInstall == false))
                                GetUnavailableFontsList();
                        }
                        else
                        {
                            GetUnavailableFontsList();
                        }
                    }
                }
                else
                {
                    if (showDialog)
                    {
                        if ((CustomInstallerDialog != null) && (AutoInstall == false))
                            GetUnavailableFontsList();
                    }
                    else
                    {
                        GetUnavailableFontsList();
                    }
                }

                return StartProcess();
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// [Reserved] Displays an indeterminate progress
        /// window as the required fonts are being installed.
        /// </summary>
        /// <param name="show">Display the progress task window?</param>
        private void NotifyOnPreparation(bool show = true)
        {
            try
            {
                if (show)
                {
                    _task.taskProgress.Text = "Installing Fonts...";
                    _task.Show($"Installing the required fonts", "The fonts are currently being installed...", 3000);
                }
            }
            catch (Exception) { }
        }
        
        /// <summary>
        /// Displays the Fonts Installer dialog window.
        /// </summary>
        /// <returns>The selected <see cref="TaskDialogButton"/>.</returns>
        internal TaskDialogButton ShowInstallerDialog(bool testPreview = false)
        {
            var unavailableFonts = GetUnavailableFontsList();
            
            if (testPreview)
            {
                try
                {
                    _installerDialogPreview.Buttons.Clear();

                    _installerDialogPreview.Buttons.Add(_installOption);
                    _installerDialogPreview.Buttons.Add(_cancelOption);

                    _installOption.Text = $"Install {(unavailableFonts.Count == 1 ? "Font" : "Fonts")}";
                    _installOption.CommandLinkNote = ParseString(
                        "This will install the required " +
                        $"{(unavailableFonts.Count == 1 ? "font in your system." : "fonts in your system.")}");
                    
                    if (DialogOptions.ShowAppIcon)
                    {
                        if (string.IsNullOrWhiteSpace(DialogOptions.CustomIcon))
                        {
                            _installerDialogPreview.CustomMainIcon =
                                Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                        }
                        else
                        {
                            if (DialogOptions.CustomIcon.ToLower() == "(default)")
                            {
                                _installerDialogPreview.CustomMainIcon =
                                    Icon.FromHandle(Properties.Resources.wk_fonts_installer.Handle);
                            }
                            else
                            {
                                _installerDialogPreview.CustomMainIcon =
                                Icon.ExtractAssociatedIcon(DialogOptions.CustomIcon);
                            }
                        }
                    }
                    else
                    {
                        if (DialogOptions.CustomIcon.ToLower() == "(default)")
                        {
                            _installerDialogPreview.CustomMainIcon =
                                Icon.FromHandle(Properties.Resources.wk_fonts_installer.Handle);
                        }
                    }

                    _installerDialogPreview.ButtonStyle = TaskDialogButtonStyle.CommandLinks;

                    _installerDialogPreview.Content = DialogOptions.Content;
                    _installerDialogPreview.MainInstruction = DialogOptions.Title;
                    _installerDialogPreview.ExpandedInformation = GetFormattedFontsList(true);

                    if (DialogOptions.UseElevationIcon)
                        _installOption.ElevationRequired = true;
                    else
                        _installOption.ElevationRequired = false;

                    _cancelOption.ButtonType = ButtonType.Close;

                    _installOption.Text = ParseString(_installOption.Text);
                    _installOption.CommandLinkNote = ParseString(_installOption.CommandLinkNote);
                    _installerDialogPreview.Content = ParseString(_installerDialogPreview.Content);
                    _installerDialogPreview.WindowTitle = ParseString(_installerDialogPreview.WindowTitle);
                    _installerDialogPreview.MainInstruction = ParseString(_installerDialogPreview.MainInstruction);
                    _installerDialogPreview.ExpandedInformation = ParseString(_installerDialogPreview.ExpandedInformation);
                }
                catch (Exception) { }

                return _installerDialogPreview.ShowDialog();
            }
            else
            {
                if ((unavailableFonts.Count > 0))
                {
                    try
                    {
                        _installerDialog.Buttons.Add(_installOption);

                        if (DialogOptions.ShowCancelButton)
                            _installerDialog.Buttons.Add(_cancelOption);

                        _installOption.Text = $"Install {(unavailableFonts.Count == 1 ? "Font" : "Fonts")}";
                        _installOption.CommandLinkNote = ParseString(
                            "This will install the required " +
                            $"{(unavailableFonts.Count == 1 ? "font in your system." : "fonts in your system.")}");

                        if (DialogOptions.ShowAppIcon)
                        {
                            if (string.IsNullOrWhiteSpace(DialogOptions.CustomIcon))
                            {
                                _installerDialog.CustomMainIcon =
                                    Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                            }
                            else
                            {
                                if (DialogOptions.CustomIcon.ToLower() == "(default)")
                                {
                                    _installerDialog.CustomMainIcon =
                                        Icon.FromHandle(Properties.Resources.wk_fonts_installer.Handle);
                                }
                                else
                                {
                                    _installerDialog.CustomMainIcon =
                                        Icon.ExtractAssociatedIcon(DialogOptions.CustomIcon);
                                }
                            }
                        }
                        else
                        {
                            if (DialogOptions.CustomIcon.ToLower() == "(default)")
                            {
                                _installerDialog.CustomMainIcon =
                                    Icon.FromHandle(Properties.Resources.wk_fonts_installer.Handle);
                            }
                        }

                        _installerDialog.ButtonStyle = TaskDialogButtonStyle.CommandLinks;

                        _installerDialog.Content = DialogOptions.Content;
                        _installerDialog.MainInstruction = DialogOptions.Title;
                        _installerDialog.ExpandedInformation = 
                            (DialogOptions.CollapsedContent != string.Empty ? 
                            DialogOptions.CollapsedContent : GetFormattedFontsList());
                        _installerDialog.WindowTitle = DialogOptions.WindowTitle;

                        if (DialogOptions.UseElevationIcon)
                            _installOption.ElevationRequired = true;

                        _cancelOption.ButtonType = ButtonType.Close;

                        _installOption.Text = ParseString(_installOption.Text);
                        _installerDialog.Content = ParseString(_installerDialog.Content);
                        _installerDialog.WindowTitle = ParseString(_installerDialog.WindowTitle);
                        _installOption.CommandLinkNote = ParseString(_installOption.CommandLinkNote);
                        _installerDialog.MainInstruction = ParseString(_installerDialog.MainInstruction);
                        _installerDialog.ExpandedInformation = ParseString(_installerDialog.ExpandedInformation);
                    }
                    catch (Exception) { }
                    
                    return _installerDialog.ShowDialog();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Parses and returns a <see cref="StringTemplates"/>
        /// for any specified <see cref="StringTemplates"/> option.
        /// </summary>
        /// <param name="template">The string template name.</param>
        /// <returns>The specific <see cref="StringTemplates"/> option.</returns>
        internal StringTemplates GetRawTemplateFromString(string template)
        {
            if (template == "{AppName}")
                return StringTemplates.AppName;
            else if (template == "{AppVersion}")
                return StringTemplates.AppVersion;
            else if (template == "{FontsCount}")
                return StringTemplates.FontsCount;
            else if (template == "{Fonts}")
                return StringTemplates.Fonts;
            else if (template == "{InstallButtonText}")
                return StringTemplates.InstallButtonText;
            else if (template == "{FontsExpression}")
                return StringTemplates.FontsExpression;
            else if (template == "{FontsRequiredExpression}")
                return StringTemplates.FontsRequiredExpression;
            else
                return StringTemplates.Empty;
        }

        /// <summary>
        /// Parses and returns a literal string specified
        /// from the list of supported <see cref="StringTemplates"/>.
        /// </summary>
        /// <param name="template">The string template name.</param>
        /// <param name="formatFonts">Enables/disables formatting the list of fonts.</param>
        /// <returns>The readable string.</returns>
        internal string GetStringFromTemplate(StringTemplates template)
        {
            if (template == StringTemplates.AppName)
                return Application.ProductName;
            else if (template == StringTemplates.AppVersion)
                return Application.ProductVersion;
            else if (template == StringTemplates.FontsCount)
                return UnavailableFonts.Count.ToString();
            else if (template == StringTemplates.Fonts)
                return GetFormattedFontsList();
            else if (template == StringTemplates.InstallButtonText)
                return _installOption.Text;
            else if (template == StringTemplates.FontsExpression)
                return $"{(UnavailableFonts.Count == 1 ? "this font" : "these fonts")}";
            else if (template == StringTemplates.FontsRequiredExpression)
                return $"{(UnavailableFonts.Count == 1 ? "one font is required" : "some fonts are required")}";
            else
                return string.Empty;
        }

        /// <summary>
        /// Parses a raw string from the specified list of 
        /// supported <see cref="StringTemplates"/> and 
        /// returns in readable format.
        /// </summary>
        /// <param name="template">The raw string template name.</param>
        /// <returns>The readable string.</returns>
        internal string GetStringFromRawTemplate(string template)
        {
            if (template == "{AppName}")
                return Application.ProductName;
            else if (template == "{AppVersion}")
                return Application.ProductVersion;
            else if (template == "{FontsCount}")
                return UnavailableFonts.Count.ToString();
            else if (template == "{Fonts}")
                return GetFormattedFontsList();
            else if (template == "{InstallButtonText}")
                return _installOption.Text;
            else if (template == "{FontsExpression}")
                return $"{(UnavailableFonts.Count == 1 ? "this font" : "these fonts")}";
            else if (template == "{FontsRequiredExpression}")
                return $"{(UnavailableFonts.Count == 1 ? "one font is required" : "some fonts are required")}";
            else
                return template;
        }

        /// <summary>
        /// Returns the list of formatted fonts
        /// based on the <see cref="Fonts"/> property.
        /// </summary>
        internal string GetFormattedFontsList(bool dialogPreview = false)
        {
            List<string> fonts = new List<string>();

            foreach (UserFont font in UnavailableFonts)
                fonts.Add($"{FontsListBullet} {font.Name}");

            return string.Join("\n", fonts);
        }

        /// <summary>
        /// Determines whether the library is being run
        /// from Visual Studio. This works in both the 'Debug'
        /// and 'Release' modes of an executing application.
        /// </summary>
        internal static bool DebugMode()
        {
            if (Debugger.IsAttached)
                return true;
            else
                return false;
        }

        #endregion

        #region Public

        #region Fonts Management

        /// <summary>
        /// Gets the actual name of the font from the font-file specified.
        /// </summary>
        /// <param name="fontFile">The full path to the TrueType font-file.</param>
        /// <returns>The font-name if found, or <see cref="string.Empty"/> if no name is found.</returns>
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
        public string GetFontPath(string fontName, bool useCache = false)
        {
            string path = string.Empty;

            if (useCache)
            {
                if (!FontSelectionEditor.Initialized)
                    FontSelectionEditor.GetFontsList();

                path = FontSelectionEditor.InstalledFonts[fontName];
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
        /// Checks if any particular font installed using its font-file.
        /// Acts as a short-hand method for the <see cref="IsFontInstalled(string, FontStyle)"/> method.
        /// </summary>
        /// <param name="fontFile">The path to the font-file.</param>
        /// <returns><see cref="true"/>/<see cref="false"/></returns>
        public bool IsFontInstalled(string fontFile)
        {
            bool installed = IsFontInstalled(fontFile, FontStyle.Regular);

            if (!installed) { installed = IsFontInstalled(fontFile, FontStyle.Bold); }
            if (!installed) { installed = IsFontInstalled(fontFile, FontStyle.Italic); }
            
            return installed;
        }
        
        /// <summary>
        /// Checks if any particular font installed using its font-file.
        /// </summary>
        /// <param name="fontFile">The path to the font-file.</param>
        /// <param name="fontStyle">The font-style to check for availability.</param>
        /// <returns><see cref="true"/>/<see cref="false"/></returns>
        public bool IsFontInstalled(string fontFile, FontStyle fontStyle = FontStyle.Regular)
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

        #region Internal

        /// <summary>
        /// Gets the list of unavailable fonts in the system.
        /// </summary>
        internal List<UserFont> GetUnavailableFontsList()
        {
            List<UserFont> fonts = new List<UserFont>();

            CreateTempDirectory();

            foreach (UserFont font in Fonts)
            {
                string fontPath = $"{_temp}{font.Name}.ttf";
                string unavailableFontPath = $"{_tempUnavailable}{font.Name}.ttf";

                try
                {
                    if (!File.Exists(fontPath))
                        File.WriteAllBytes(fontPath, font.FontBytes);
                }
                catch (Exception) { }

                try
                {
                    if (TestingMode)
                    {
                        if (DebugMode())
                        {
                            fonts.Add(font);

                            if (!File.Exists(unavailableFontPath))
                                File.Move(fontPath, unavailableFontPath);
                        }
                        else
                        {
                            if (!IsFontInstalled(fontPath))
                            {
                                fonts.Add(font);

                                if (!File.Exists(unavailableFontPath))
                                    File.Move(fontPath, unavailableFontPath);
                            }
                        }
                    }
                    else
                    {
                        if (!IsFontInstalled(fontPath))
                        {
                            fonts.Add(font);

                            if (!File.Exists(unavailableFontPath))
                                File.Move(fontPath, unavailableFontPath);
                        }
                    }
                }
                catch (Exception) { }
            }
            
            if (fonts.Count == 0)
                DeleteTempDirectory();
            
            return fonts;
        }

        /// <summary>
        /// Gets the list of installed fonts in the system
        /// as per the list of added fonts to install.
        /// </summary>
        internal List<UserFont> GetInstalledFonts()
        {
            List<UserFont> fonts = new List<UserFont>();

            try
            {
                if (!Directory.Exists(_temp))
                    Directory.CreateDirectory(_temp);

                if (!Directory.Exists(_tempUnavailable))
                    Directory.CreateDirectory(_tempUnavailable);
            }
            catch (Exception)
            {
                _temp = $"{Application.StartupPath}\\Fonts\\";
            }

            foreach (UserFont font in Fonts)
            {
                string fontPath = $"{_temp}{font.Name}.ttf";
                string unavailableFontPath = $"{_tempUnavailable}{font.Name}.ttf";

                try
                {
                    if (!File.Exists(fontPath))
                        File.WriteAllBytes(fontPath, font.FontBytes);
                }
                catch (Exception) { }

                if (!IsFontInstalled(fontPath))
                {
                    fonts.Add(font);
                }
            }

            return fonts;
        }

        /// <summary>
        /// Gets the list of uninstalled fonts in the system
        /// as per the list of added fonts to install.
        /// </summary>
        internal List<UserFont> GetUninstalledFonts()
        {
            List<UserFont> fonts = new List<UserFont>();

            try
            {
                if (!Directory.Exists(_temp))
                    Directory.CreateDirectory(_temp);
                
                if (!Directory.Exists(_tempUnavailable))
                    Directory.CreateDirectory(_tempUnavailable);
            }
            catch (Exception)
            {
                _temp = $"{Application.StartupPath}\\Fonts\\";
            }

            foreach (UserFont font in Fonts)
            {
                string fontPath = $"{_temp}{font.Name}.ttf";
                string unavailableFontPath = $"{_tempUnavailable}{font.Name}.ttf";

                try
                {
                    if (!File.Exists(fontPath))
                        File.WriteAllBytes(fontPath, font.FontBytes);
                }
                catch (Exception) { }

                if (!IsFontInstalled(fontPath))
                {
                    fonts.Add(font);
                }
            }

            return fonts;
        }

        /// <summary>
        /// Refreshes the list of fonts installed in the system.
        /// </summary>
        internal void RefreshFontsCollection()
        {
            FontSelectionEditor.GetFontsList();
        }

        #endregion

        #endregion

        #region Installation Management

        /// <summary>
        /// Checks and installs all the fonts added in 
        /// the <see cref="Fonts"/> collection property.
        /// </summary>
        /// <returns>
        /// <see cref="true"/> if installation was
        /// successful or the font was available, 
        /// else returns <see cref="false"/>.
        /// </returns>
        public bool Install()
        {
            try
            {
                if (CustomInstallerDialog != null)
                {
                    ParseDialogStrings();

                    if (!_dialogShown)
                    {
                        var selection = CustomInstallerDialog.ShowDialog();

                        if (selection == DialogResult.OK)
                        {
                            RunInstaller(true);
                        }
                        else if (selection == DialogResult.Cancel)
                        {
                            UserCancelled = true;
                        }

                        _dialogShown = true;
                    }
                }
                else
                {
                    if (!_dialogShown)
                    {
                        TaskDialogButton selection = ShowInstallerDialog();

                        if (selection == _installOption)
                        {
                            RunInstaller(true);
                        }
                        else if (selection == _cancelOption)
                        {
                            UserCancelled = true;
                        }

                        _dialogShown = true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks and installs any TrueType font-file.
        /// </summary>
        /// <param name="fontFile">
        /// The path to any TrueType font-file.
        /// </param>
        /// <param name="showDialog">
        /// Displays a fonts installation 
        /// dialog when set to <see cref="true"/>.
        /// </param>
        /// <returns>
        /// <see cref="true"/> if installation was
        /// successful or the fonts were available; 
        /// else returns <see cref="false"/>.
        /// </returns>
        public bool InstallFont(string fontFile, bool showDialog = true)
        {
            try
            {
                if (File.Exists(fontFile))
                {
                    Fonts.Add(new UserFont()
                    {
                        ExtractFontName = false,
                        Path = fontFile
                    });
                }

                if (showDialog)
                {
                    if (CustomInstallerDialog != null)
                    {
                        ParseDialogStrings();

                        var selection = CustomInstallerDialog.ShowDialog();

                        if (selection == DialogResult.OK)
                        {
                            RunInstaller(showDialog);
                        }
                        else if (selection == DialogResult.Cancel)
                        {
                            UserCancelled = true;
                        }
                    }
                    else
                    {
                        TaskDialogButton selection = ShowInstallerDialog();

                        if (selection == _installOption)
                        {
                            RunInstaller(showDialog);
                        }
                        else if (selection == _cancelOption)
                        {
                            UserCancelled = true;
                        }
                    }
                }
                else
                {
                    RunInstaller(showDialog);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks and installs a list of TrueType font-files.
        /// </summary>
        /// <param name="fontFiles">
        /// An array of TrueType font-files.
        /// </param>
        /// <param name="showDialog">
        /// Displays a fonts installation 
        /// dialog when set to <see cref="true"/>.
        /// </param>
        /// <returns>
        /// <see cref="true"/> if installation was
        /// successful or the fonts were available; 
        /// else returns <see cref="false"/>.
        /// </returns>
        public bool InstallFonts(string[] fontFiles, bool showDialog = true)
        {
            try
            {
                foreach (string fontFile in fontFiles)
                {
                    if (File.Exists(fontFile))
                    {
                        Fonts.Add(new UserFont()
                        {
                            ExtractFontName = false,
                            Path = fontFile
                        });
                    }
                }

                if (showDialog)
                {
                    if (CustomInstallerDialog != null)
                    {
                        ParseDialogStrings();

                        var selection = CustomInstallerDialog.ShowDialog();

                        if (selection == DialogResult.OK)
                        {
                            RunInstaller(showDialog);
                        }
                        else if (selection == DialogResult.Cancel)
                        {
                            UserCancelled = true;
                        }
                    }
                    else
                    {
                        TaskDialogButton selection = ShowInstallerDialog();

                        if (selection == _installOption)
                        {
                            RunInstaller(showDialog);
                        }
                        else if (selection == _cancelOption)
                        {
                            UserCancelled = true;
                        }
                    }
                }
                else
                {
                    RunInstaller(showDialog);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks and installs all TrueType fonts within a specified folder.
        /// </summary>
        /// <param name="fontsFolder">
        /// Path to the folder containing the fonts.
        /// </param>
        /// <param name="showDialog">
        /// Displays a fonts installation 
        /// dialog when set to <see cref="true"/>.
        /// </param>
        /// <returns>
        /// <see cref="true"/> if installation was
        /// successful or the fonts were available; 
        /// else returns <see cref="false"/>.
        /// </returns>
        public bool InstallFonts(string fontsFolder, bool showDialog = true)
        {
            try
            {
                if (Directory.Exists(fontsFolder))
                {
                    foreach (string fontFile in Directory.GetFiles(fontsFolder))
                    {
                        if (File.Exists(fontFile) && (Path.GetExtension(fontFile) == ".ttf"))
                        {
                            Fonts.Add(new UserFont()
                            {
                                ExtractFontName = false,
                                Path = fontFile
                            });
                        }
                    }

                    if (showDialog)
                    {
                        if (CustomInstallerDialog != null)
                        {
                            ParseDialogStrings();

                            var selection = CustomInstallerDialog.ShowDialog();

                            if (selection == DialogResult.OK)
                            {
                                RunInstaller(showDialog);
                            }
                            else if (selection == DialogResult.Cancel)
                            {
                                UserCancelled = true;
                            }
                        }
                        else
                        {
                            TaskDialogButton selection = ShowInstallerDialog();

                            if (selection == _installOption)
                            {
                                RunInstaller(showDialog);
                            }
                            else if (selection == _cancelOption)
                            {
                                UserCancelled = true;
                            }
                        }
                    }
                    else
                    {
                        RunInstaller(showDialog);
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Displays the Restart dialog window. This dialog blocks and
        /// displays for a set period of time before closing to inform 
        /// users that a restart event is about to occur. Such a scenario
        /// is applicable only after the fonts installation process has been
        /// completed, both via manual and automatic installation. 
        /// </summary>
        /// <param name="timeout">
        /// The time (in milliseconds) to wait before 
        /// the dialog auto-closes.
        /// </param>
        /// <param name="restartOnFinish">
        /// When set to true, the application will
        /// be restarted once the dialog closes.
        /// </param>
        public void ShowRestartDialog(int timeout = 2000, bool restartOnFinish = false)
        {
            _task.UserInvoked = true;
            _task.RestartOnFinish = restartOnFinish;

            _task.taskProgress.Text = $"Restarting application...";
            _task.Show($"Restarting application",
                       $"Please wait for the application to restart...", timeout);
        }

        #endregion

        #region Miscellaneous

        /// <summary>
        /// Hides the <see cref="ParentForm"/> window by
        /// minimizing it and fully reducing its opacity.
        /// </summary>
        public void HideParentWindow()
        {
            try
            {
                ParentForm.Opacity = 0.0;
                ParentForm.WindowState = FormWindowState.Minimized;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Parses a <see cref="string"/> containing the predefined string 
        /// template options used in the <see cref="DialogOptions"/> property, 
        /// and returns the parsed content in the same <see cref="string"/> format.
        /// </summary>
        /// <param name="content">The string to parse.</param>
        /// <returns>The parsed content.</returns>
        public string ParseString(string content)
        {
            List<string> templates =
                Enum.GetNames(typeof(StringTemplates))
                .OfType<string>().ToList();

            for (int i = 0; i < templates.Count; i++)
                templates[i] = "{" + templates[i] + "}";

            foreach (string item in templates)
                content = content.Replace(item, GetStringFromRawTemplate(item));

            return content;
        }
        
        #endregion

        #endregion

        #endregion

        #region Events

        #region Public

        #region Event Handlers

        /// <summary>
        /// Raised once the fonts installation process begins.
        /// </summary>
        [Category("Installation Process")]
        [Description("Raised once the fonts installation process begins.")]
        public event EventHandler<EventArgs> InstallingFonts = null;

        /// <summary>
        /// Raised once the fonts installation 
        /// process is completed.
        /// </summary>
        [Category("Installation Process")]
        [Description("Raised once the fonts installation process is completed.")]
        public event EventHandler<InstallationCompletedEventArgs> InstallationCompleted = null;

        /// <summary>
        /// Raised before the application restarts once the fonts 
        /// installation process is completed. Adding this event 
        /// will disable the default 'AutoRestart' feature and allow 
        /// you to manually restart your application if you need to 
        /// perform other tasks beforehand. Please note that restarting 
        /// your application is highly recommended in order to effect 
        /// the font changes at the application level.
        /// </summary>
        [Category("Installation Process")]
        [Description("Raised before the application restarts once the fonts " +
                     "installation process is completed. Adding this event " +
                     "will disable the default 'AutoRestart' feature and allow " +
                     "you to manually restart your application if you need to " +
                     "perform other tasks beforehand. Please note that restarting " +
                     "your application is highly recommended in order to effect " +
                     "the font changes at the application level.")]
        public event EventHandler<ApplicationRestartingEventArgs> ApplicationRestarting = null;
        
        #endregion

        #region Event Arguments

        /// <summary>
        /// Provides data for the <see cref="InstallationCompleted"/> event.
        /// </summary>
        public class InstallationCompletedEventArgs : EventArgs
        {
            #region Constructor

            /// <summary>
            /// Provides data for the <see cref="InstallationCompleted"/> event.
            /// </summary>
            /// <param name="fontsInstalled">
            /// The list of fonts that have been installed successfully.
            /// </param>
            /// <param name="fontsNotInstalled">
            /// The list of fonts that have not been installed successfully.
            /// </param>
            /// <param name="success">
            /// Was the fonts installation process successful?
            /// </param>
            public InstallationCompletedEventArgs(bool success, bool userCancelled,
                                                  List<UserFont> fontsInstalled, 
                                                  List<UserFont> fontsNotInstalled)
            {
                InstallationSucceeded = success;
                
                FontsInstalled = fontsInstalled;
                FontsNotInstalled = fontsNotInstalled;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets a value indicating whether the 
            /// fonts installation process succeeded.
            /// </summary>
            [Browsable(false)]
            public bool InstallationSucceeded { get; private set; }

            /// <summary>
            /// Gets a value indicating whether the user cancelled the
            /// fonts installation process. This is only necessary when
            /// the <see cref="DialogOptions.ShowCancelButton"/> property
            /// is set to true, or a <see cref="CustomInstallerDialog"/> 
            /// with a <see cref="DialogResult.Cancel"/> option has been provided.
            /// </summary>
            [Browsable(false)]
            public bool UserCancelled { get; private set; }

            /// <summary>
            /// Gets the list of fonts that
            /// have been installed successfully.
            /// </summary>
            [Browsable(false)]
            public List<UserFont> FontsInstalled { get; private set; }

            /// <summary>
            /// Gets the list of fonts that have
            /// not been installed successfully.
            /// </summary>
            [Browsable(false)]
            public List<UserFont> FontsNotInstalled { get; private set; }

            #endregion
        }

        /// <summary>
        /// Provides data for the <see cref="ApplicationRestarting"/> event.
        /// </summary>
        public class ApplicationRestartingEventArgs : EventArgs
        {
            #region Constructor

            /// <summary>
            /// Provides data for the <see cref="ApplicationRestarting"/> event.
            /// </summary>
            /// <param name="success">
            /// Was the fonts installation process successful?
            /// </param>
            public ApplicationRestartingEventArgs(bool success)
            {
                InstallationSucceeded = success;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets a value indicating whether the 
            /// fonts installation process succeeded.
            /// </summary>
            [Browsable(false)]
            public bool InstallationSucceeded { get; private set; }
            
            #endregion
        }

        #endregion

        #endregion

        #region Private

        private void OnLoadParentForm(object sender, EventArgs e)
        {
            if (AutoInstall && !InstallTaskCompleted)
            {
                if (UnavailableFonts.Count > 0)
                {
                    ParentForm.TopMost = true;
                    ParentForm.Activate();

                    HideParentWindow();

                    ParentForm.Shown += delegate
                    {
                        ParentForm.Hide();

                        Install();
                    };
                }
                else
                {
                    ParentForm.TopMost = true;
                    ParentForm.TopMost = false;
                    
                    ParentForm.Activate();
                }
            }
        }

        #endregion

        #endregion
    }
}