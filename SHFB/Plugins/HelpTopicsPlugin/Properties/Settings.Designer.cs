﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HelpTopicsPlugin.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("allTopics.xml")]
        public string HelpDocumentationAllTopics {
            get {
                return ((string)(this["HelpDocumentationAllTopics"]));
            }
            set {
                this["HelpDocumentationAllTopics"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("webTocNew.xml")]
        public string HelpDocumentationWebTocNew {
            get {
                return ((string)(this["HelpDocumentationWebTocNew"]));
            }
            set {
                this["HelpDocumentationWebTocNew"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("webToc.xml")]
        public string HelpDocumentationWebTocOld {
            get {
                return ((string)(this["HelpDocumentationWebTocOld"]));
            }
            set {
                this["HelpDocumentationWebTocOld"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://www.commsvr.com/UAModelDesigner/WebTOC.xml")]
        public string HelpDocumentationWebTOC {
            get {
                return ((string)(this["HelpDocumentationWebTOC"]));
            }
            set {
                this["HelpDocumentationWebTOC"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SiteMap.xml")]
        public string SiteMapFile {
            get {
                return ((string)(this["SiteMapFile"]));
            }
            set {
                this["SiteMapFile"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://www.commsvr.com/UAModelDesigner/Index.aspx")]
        public string website {
            get {
                return ((string)(this["website"]));
            }
            set {
                this["website"] = value;
            }
        }
    }
}
