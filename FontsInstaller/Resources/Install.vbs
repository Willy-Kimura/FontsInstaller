' Modified for better readability & UX.
' *************************************
' This script simply installs the fonts located within 
' its current directory; if any/all of the fonts are 
' already installed, then they'll be ignored. 

' Please Preserve Contents.
' *************************************

Set filesystem_object = CreateObject("Scripting.FileSystemObject")
SourceFolder = filesystem_object.GetParentFolderName(Wscript.ScriptFullName)

Const FONTS = &H14&

Set shell_object  = CreateObject("Shell.Application")
Set source_folder = shell_object.Namespace(SourceFolder)
Set windows_fonts = shell_object.Namespace(FONTS)

Set regex_ttf_selector = New RegExp

regex_ttf_selector.IgnoreCase = True
regex_ttf_selector.Pattern = "\.ttf$"

For Each FontFile In source_folder.Items()

	lcase_filename = lcase(FontFile.Name) + ".ttf"
	
	If regex_ttf_selector.Test(FontFile.Path) Then 
		
		windows_fonts.CopyHere FontFile.Path
		
	End If
	
Next