﻿#pragma checksum "..\..\Chat.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F3F2C689CEA70DCE3A7207E17F1F324DC9FEB7C8"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfApp1;


namespace WpfApp1 {
    
    
    /// <summary>
    /// Chat
    /// </summary>
    public partial class Chat : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 64 "..\..\Chat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label name;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\Chat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label time;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\Chat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel MsgBox;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\Chat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label_send;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\Chat.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox text_msg;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/chat.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Chat.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.name = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.time = ((System.Windows.Controls.Label)(target));
            
            #line 65 "..\..\Chat.xaml"
            this.time.Loaded += new System.Windows.RoutedEventHandler(this.Time_Loaded);
            
            #line default
            #line hidden
            return;
            case 3:
            this.MsgBox = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.Label_send = ((System.Windows.Controls.Label)(target));
            
            #line 84 "..\..\Chat.xaml"
            this.Label_send.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Send_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.text_msg = ((System.Windows.Controls.TextBox)(target));
            
            #line 86 "..\..\Chat.xaml"
            this.text_msg.KeyDown += new System.Windows.Input.KeyEventHandler(this.TextBox_KeyDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

