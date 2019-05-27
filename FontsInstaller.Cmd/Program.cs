/*
 * Developer    : Willy Kimura (WK).
 * Library      : FontsInstaller.Cmd (Command-line Utility).
 * License      : MIT.
 * 
 * This handy utility is an extension of the FontsInstaller 
 * library, but now for the Command Prompt. It works the same 
 * way as the library, but now in a much easier and user-friendly 
 * manner. You can still use it to install fonts via non .NET 
 * (and likewise .NET apps) if you'd prefer to do so. Typing the 
 * application-name followed by the "--help" command via the Cmd 
 * will assist you to get through the various options available.
 * 
 * Improvements are welcome.
 * 
 */


using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using CommandLine;

namespace WK.Libraries.FontsInstallerNS.Cmd
{
    class Program
    {
        #region Fields
        
        public static FontsInstaller Installer = new FontsInstaller();

        #endregion

        #region Classes

        /// <summary>
        /// Provides the supported command-line options
        /// the user may pass when running the utility.
        /// </summary>
        public class Options
        {
            [Option('i', "install", Required = false, 
                HelpText = "Checks and installs existing fonts, e.g. " +
                           "\"C:\\myfont.ttf\", \"C:\\myfontsfolder\", " +
                           "\"myfont.ttf\", \"relativedir\\myfont.ttf\".")]
            public IEnumerable<string> Fonts { get; set; }

            [Option('p', "prompt", 
                HelpText = "Displays an Installer dialog prompting " +
                           "the user to install fonts.")]
            public bool Prompt { get; set; }

            [Option('c', "check",
                HelpText = "Checks whether a font is installed.")]
            public string CheckFont { get; set; }

            [Option("title", 
                HelpText = "Lets you provide a custom title for the Installer " +
                           "dialog if the \"-prompt\" option has been specified.")]
            public string Title { get; set; }

            [Option("content",
                HelpText = "Lets you provide custom content for the Installer " +
                           "dialog if the \"-prompt\" option has been specified.")]
            public string Content { get; set; }

            [Option("icon", 
                HelpText = "Lets you provide a custom icon for the Installer " +
                           "dialog if the \"-prompt\" option has been specified.")]
            public string ShowIcon { get; set; }

            [Option("cancel",
                HelpText = "Displays a cancel button " +
                           "for the Installer dialog if the " +
                           "\"-prompt\" option has been specified.")]
            public bool ShowCancelButton { get; set; }
        }
        
        #endregion

        #region Main

        #region Helpers

        private static void SetupInstallerDialog(string title, string content, 
            string iconPath, bool showCancelButton)
        {
            Installer.DialogOptions.ShowAppIcon = true;
            Installer.DialogOptions.ShowCancelButton = showCancelButton;

            if (File.Exists(iconPath))
                Installer.DialogOptions.CustomIcon = iconPath;

            if (!string.IsNullOrWhiteSpace(title))
                Installer.DialogOptions.WindowTitle = title;
            else
                Installer.DialogOptions.WindowTitle = "Fonts Installer";

            if (!string.IsNullOrWhiteSpace(content))
            {
                Installer.DialogOptions.Content = content;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(title))
                {
                    Installer.DialogOptions.Content =
                    Installer.ParseString(
                        "{FontsRequiredExpression} to be installed in your system before proceeding. " +
                        "To view {FontsExpression}, click \"See details\" - otherwise, proceed " +
                        "with installation.").Replace("one", "One").Replace("some", "Some");
                }
                else
                {
                    Installer.DialogOptions.Content = Installer.DialogOptions.Content.Replace("{AppName}", title);
                }
            }
        }

        private static int GetInstallCount(string path)
        {
            int count = 0;

            foreach (string font in Directory.GetFiles(path, "*.ttf"))
            {
                if (!Installer.IsFontInstalled(font))
                    count++;
            }
            return count;
        }

        private static string[] GetInstalledFonts(string path)
        {
            List<string> fonts = new List<string>();

            foreach (string font in Directory.GetFiles(path, "*.ttf"))
            {
                if (Installer.IsFontInstalled(font))
                    fonts.Add(Installer.GetFontName(font));
            }

            return fonts.ToArray();
        }

        private static void PrintInstalledFonts(string path)
        {
            int count = 0;
            List<string> fonts = new List<string>();

            foreach (string font in Directory.GetFiles(path, "*.ttf"))
            {
                count++;

                if (!(Installer.IsFontInstalled(font)))
                    fonts.Add($"({count}) {Path.GetFileName(font)}");
            }

            if (fonts.Count >= 1)
            {
                Console.WriteLine("Installation complete.\n");
                Console.WriteLine((fonts.Count > 1 ?
                    $"The following {fonts.Count} fonts were installed successfully:" :
                    "The following font was installed successfully:"));

                Console.WriteLine($"{string.Join("\n", fonts.ToList())}");
            }
            else
            {
                Console.WriteLine(count > 1 ? "The fonts are already installed." : 
                                  "The font is already installed.");
            }
        }

        private static void InstallFonts(string path, bool promptUser)
        {
            if (Path.IsPathRooted(path))
            {
                if (Directory.Exists(path))
                {
                    int fontsCount = GetInstallCount(path);
                    Console.WriteLine(fontsCount > 1 ? "Installing fonts..." : "Installing font(s)...");

                    bool success = Installer.InstallFonts(path, promptUser);

                    if (Installer.UserCancelled)
                    {
                        Console.WriteLine("The installation process was cancelled.");
                        Environment.ExitCode = 0;
                    }
                    else
                    {
                        if (success)
                        {
                            PrintInstalledFonts(path);
                            Environment.ExitCode = 0;
                        }
                        else
                        {
                            Console.WriteLine((fontsCount > 1 ?
                                "The fonts failed to install." :
                                "The font failed to install."));

                            Environment.ExitCode = -1;
                        }
                    }
                }
                else
                {
                    if (!Path.HasExtension(path))
                        path += ".ttf";

                    if (Installer.IsFontInstalled(path))
                    {
                        Console.WriteLine($"The font '{Installer.GetFontName(path)}' " +
                                          $"is already installed.");
                        Environment.ExitCode = 0;
                    }
                    else
                    {
                        Console.WriteLine("Installing font...");

                        bool success = Installer.InstallFont(path, promptUser);

                        if (Installer.UserCancelled)
                        {
                            Console.WriteLine("The installation process was cancelled.");
                            Environment.ExitCode = 0;
                        }
                        else
                        {
                            if (success)
                            {
                                Console.WriteLine($"The font '{Installer.GetFontName(path)}' " +
                                                  $"was installed successfully.");
                                Environment.ExitCode = 0;
                            }
                            else
                            {
                                Console.WriteLine("The font failed to install.");
                                Environment.ExitCode = -1;
                            }
                        }
                    }
                }
            }
            else
            {
                string relativePath = $"{Application.StartupPath}\\{path.Replace("//", "\\")}";
                
                if (Directory.Exists(relativePath))
                {
                    int fontsCount = GetInstallCount(relativePath);
                    Console.WriteLine(fontsCount > 1 ? "Installing fonts..." : "Installing font(s)...");

                    bool success = Installer.InstallFonts(relativePath, promptUser);

                    if (Installer.UserCancelled)
                    {
                        Console.WriteLine("The installation process was cancelled.");
                        Environment.ExitCode = 0;
                    }
                    else
                    {
                        if (success)
                        {
                            PrintInstalledFonts(relativePath);
                            Environment.ExitCode = 0;
                        }
                        else
                        {
                            Console.WriteLine((fontsCount > 1 ?
                                "The fonts failed to install." :
                                "The font failed to install."));

                            Environment.ExitCode = -1;
                        }
                    }
                }
                else
                {
                    if (!Path.HasExtension(relativePath))
                        relativePath += ".ttf";

                    if (Installer.IsFontInstalled(path))
                    {
                        Console.WriteLine($"The font '{Installer.GetFontName(relativePath)}' is already installed.");
                        Environment.ExitCode = 0;
                    }
                    else
                    {
                        Console.WriteLine("Installing font...");

                        bool success = Installer.InstallFont(relativePath, promptUser);

                        if (Installer.UserCancelled)
                        {
                            Console.WriteLine("The installation process was cancelled.");
                            Environment.ExitCode = 0;
                        }
                        else
                        {
                            if (success)
                            {
                                Console.WriteLine($"The font '{Installer.GetFontName(relativePath)}' " +
                                                  $"was installed successfully.");
                                Environment.ExitCode = 0;
                            }
                            else
                            {
                                Console.WriteLine($"The font '{Installer.GetFontName(relativePath)}' " +
                                                  $"failed to install.");
                                Environment.ExitCode = -1;
                            }
                        }
                    }
                }
            }
        }

        private static void IsFontAvailable(string path)
        {
            path = path.Replace("//", "\\");

            if (Path.IsPathRooted(path))
            {
                if (Directory.Exists(path) || path.EndsWith("\\"))
                {
                    Console.WriteLine($"Please provide a valid TrueType font-file.");
                    Environment.ExitCode = -1;
                }
                else
                {
                    if (!Path.HasExtension(path))
                        path += ".ttf";

                    bool installed = Installer.IsFontInstalled(path);

                    if (installed)
                    {
                        Console.WriteLine($"The font is already installed.");
                        Environment.ExitCode = 0;
                    }
                    else
                    {
                        Console.WriteLine("The font is not installed.");
                        Environment.ExitCode = 1;
                    }
                }
            }
            else
            {
                string relativePath = $"{Application.StartupPath}\\{path}";

                if (Directory.Exists(relativePath) || relativePath.EndsWith("\\"))
                {
                    Console.WriteLine($"Please provide a valid TrueType font-file.");
                    Environment.ExitCode = -1;
                }
                else
                {
                    if (!Path.HasExtension(relativePath))
                        relativePath += ".ttf";
                    
                    bool installed = Installer.IsFontInstalled(relativePath);

                    if (installed)
                    {
                        Console.WriteLine($"The font is already installed.");
                        Environment.ExitCode = 0;
                    }
                    else
                    {
                        Console.WriteLine("The font is not installed.");
                        Environment.ExitCode = 1;
                    }
                }
            }
        }

        private static void ExecuteStatements(Options cmdOptions)
        {
            try
            {
                SetupInstallerDialog(cmdOptions.Title, cmdOptions.Content, 
                    cmdOptions.ShowIcon, cmdOptions.ShowCancelButton);

                bool promptUser = cmdOptions.Prompt;
                
                if (!string.IsNullOrWhiteSpace(cmdOptions.CheckFont))
                    IsFontAvailable(cmdOptions.CheckFont);
                else
                {
                    if (cmdOptions.Fonts.Count<string>() > 0)
                    {
                        if (cmdOptions.Fonts.Count<string>() == 1)
                        {
                            InstallFonts(cmdOptions.Fonts.First().ToString(), promptUser);
                        }
                        else if (cmdOptions.Fonts.Count<string>() > 1)
                        {
                            foreach (string path in cmdOptions.Fonts.ToArray())
                            {
                                InstallFonts(path, promptUser);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Issue: {ex.Message}\n" +
                    $"Please type \"{Path.GetFileNameWithoutExtension(Application.ExecutablePath)} " +
                    $"--help\" and press enter to view all available options.");
            }
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
            // [Reserved]
        }

        #endregion

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(opts => ExecuteStatements(opts))
                .WithNotParsed((errs) => HandleParseError(errs));
        }

        #endregion
    }
}
