# How to create environment for Sandcastle Help File Builder:

## Preface

1. Install Sandcastle (newest version can be downloaded from [here](http://sandcastle.codeplex.com) or `\\Hq.cas.com.pl\cas_dfs\Public\FTP\SandcastleBuilder\Sandcastle.msi`
1. Install SandcastleStyles: it can be found [here](http://sandcastlestyles.codeplex.com/) or `\\Hq.cas.com.pl\cas_dfs\Public\FTP\SandcastleBuilder\`
1. Install SHFB (newest version can be downloaded from [here](http://shfb.codeplex.com) or `\\Hq.cas.com.pl\cas_dfs\Public\FTP\SandcastleBuilder\`
1. If there is a new version of the plugin - copy newest version of `SandcastleBuilder.Utils.dll` and `SandcastleBuilder.Utils.xml` from SHFB installation folder to `../SHFB/SHFBLibraries/`, add new library version to the Plugins VS project and compile the plugin.

Use install_all.cmd (Run as administrator on Windows 7) or, for manual installation, follow instruction below.

1. Copy `VS2005AdWords`, `VS2005AdWordsDP`, `VS2005OPCHelp` and `VS2005UA` (Sandcastle\Presentation\) folder to `c:\Program Files\Sandcastle\Presentation\` (default path) folder to use template for help containing AdSense.
1. Copy content from `\EWSoftware\Web\` folder to  `c:\Program Files\EWSoftware\Sandcastle Help File Builder\Web\` (default path).
1. Copy library files:

* HelpTopicsPlugin.dll - plugin for SHBF
* HelpTopicsLibrary.dll - common classes for HelpAssistant and plugin
* HelpAssistant.dll - analysis of the project
 SiteMapLibrary.dll - generation of the site map for Google 

> IMPORTANT! In Windows 7 and XP rename `HelpTopicsPlugin.dll` to `HelpTopicsPlugin.plugins` and rename `SiteMapLibrary.dll` to `SiteMapLibrary.plugins`.

Those library files can be found in `VS_Trunk\EX02-MAML\SHFB\Plugins\HelpTopicsPlugin\bin` (Debug or Release) to appropriate folder:

* for Win XP it is: `%ALLUSERSPROFILE%\Application Data\EWSoftware\Sandcastle Help File Builder\Components and Plug-Ins\` if this directory is not exist it must be created e.g. "c:\Documents and Settings\All Users\Application Data\EWSoftware\Sandcastle Help File Builder\Plug-Ins"
* for Win Vista it is: %ProgramData%\EWSoftware\Sandcastle Help FileBuilder\Plug-Ins\
* for Win 7 it is: 
  a) for Win 7 x64: %ProgramFiles(x86)%\EWSoftware\Sandcastle Help File Builder\Components and Plug-Ins
   b) for Win 7 x86: %ProgramFiles%\EWSoftware\Sandcastle Help File Builder\Components and Plug-Ins

4. In Project Properties in SHFB, PlugInConfigurations window, choose HelpTopicsPlugin and add it.
1. Build the project- the allTopics.xml and SiteMap.xml file will be located in the project folder.

## See also

* [On-line help](https://commsvr-com.github.io/SHFB.HelpAssistant/Help/index.html)
