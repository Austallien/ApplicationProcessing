﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApplicationProcessing.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.7.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("{\\\"Id\\\":1,\\\"FirstName\\\":\\\"FirstName\\\",\\\"MiddleName\\\":\\\"MiddleName\\\",\\\"LastName\\\":" +
            "\\\"LastName\\\",\\\"Username\\\":\\\"DatabaseNotImplementerDeature\\\"}")]
        public string UserJSON {
            get {
                return ((string)(this["UserJSON"]));
            }
            set {
                this["UserJSON"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool RememberUser {
            get {
                return ((bool)(this["RememberUser"]));
            }
            set {
                this["RememberUser"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2024-04-12")]
        public global::System.DateTime SignedIn {
            get {
                return ((global::System.DateTime)(this["SignedIn"]));
            }
            set {
                this["SignedIn"] = value;
            }
        }
    }
}
