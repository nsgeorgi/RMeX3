﻿#pragma checksum "C:\Users\user\Desktop\Finalvers\nosingl\RMeX3 (2)\RMeX3\RMeX3\Settings_page.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D96A72921834C25440A9398733A5FB67"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMeX3
{
    partial class Settings_page : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.Button2 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 33 "..\..\..\Settings_page.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.Button2).Click += this.Button2_Click;
                    #line default
                }
                break;
            case 2:
                {
                    this.Button1 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 25 "..\..\..\Settings_page.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.Button1).Click += this.Button1_Click;
                    #line default
                }
                break;
            case 3:
                {
                    this.BackButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 14 "..\..\..\Settings_page.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BackButton).Click += this.BackButton_Click;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

