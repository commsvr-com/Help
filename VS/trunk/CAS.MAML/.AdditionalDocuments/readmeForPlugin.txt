1) Copy library files:
  * HelpTopicsPlugin.dll - plugin for SHBF
  * HelpTopicsLibrary.dll - common classes for HelpAssistant and plugin
  * HelpAssistant.dll - analysis of the project
  * SiteMapLibrary.dll - generation of the site map for Google 
  those files can be found in VS_Trunk\EX02-MAML\SHFB\Plugins\HelpTopicsPlugin\bin (Debug or Release) 
  to appropriate folder:
- for Win XP it is: %ALLUSERSPROFILE%\Application Data\EWSoftware\Sandcastle Help File Builder\Plug-Ins\
  if this directory is not exist it must be created
  e.g. "c:\Documents and Settings\All Users\Application Data\EWSoftware\Sandcastle Help File Builder\Plug-Ins" 
- for Win Vista it is: %ProgramData%\EWSoftware\Sandcastle Help FileBuilder\Plug-Ins\

2) In Project Properties in SHFB, PlugInConfigurations window, choose HelpTopicsPlugin and add it.

3) Build the project- the allTopics.xml file will be located in the project folder. 
