﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.237
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Octgn
{


    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    public sealed partial class lobbysettings : global::System.Configuration.ApplicationSettingsBase
    {

        private static lobbysettings defaultInstance = ((lobbysettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new lobbysettings())));

        public static lobbysettings Default
        {
            get
            {
                return defaultInstance;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("www.skylabsonline.com")]
        public string Server
        {
            get
            {
                return ((string)(this["Server"]));
            }
            set
            {
                this["Server"] = value;
            }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("7535")]
        public int ServerPort
        {
            get
            {
                return ((int)(this["ServerPort"]));
            }
            set
            {
                this["ServerPort"] = value;
            }
        }
    }
}
