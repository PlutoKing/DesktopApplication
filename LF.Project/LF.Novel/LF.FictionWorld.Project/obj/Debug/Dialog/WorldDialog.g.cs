﻿#pragma checksum "..\..\..\Dialog\WorldDialog.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "736BD3FE57D8171A8C7D35D70C58DEE1F252B945F8DA38AECAEC6E158194B4EB"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using LF.FictionWorld.Project.Dialog;
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


namespace LF.FictionWorld.Project.Dialog {
    
    
    /// <summary>
    /// WorldDialog
    /// </summary>
    public partial class WorldDialog : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\Dialog\WorldDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LF.FictionWorld.Project.Dialog.WorldDialog dialog;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Dialog\WorldDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TxbSubTitle;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Dialog\WorldDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnOK;
        
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
            System.Uri resourceLocater = new System.Uri("/LF.FictionWorld.Project;component/dialog/worlddialog.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Dialog\WorldDialog.xaml"
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
            this.dialog = ((LF.FictionWorld.Project.Dialog.WorldDialog)(target));
            
            #line 11 "..\..\..\Dialog\WorldDialog.xaml"
            this.dialog.Loaded += new System.Windows.RoutedEventHandler(this.Dialog_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 19 "..\..\..\Dialog\WorldDialog.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TopGrid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TxbSubTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            
            #line 35 "..\..\..\Dialog\WorldDialog.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnClose_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BtnOK = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\Dialog\WorldDialog.xaml"
            this.BtnOK.Click += new System.Windows.RoutedEventHandler(this.BtnOK_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
