﻿#pragma checksum "..\..\..\Dialog\SectDialog.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E32D2DB8C86F5C4EFC24C98D668F98E9F733DA386A1203F6A26F5CAEAEA11BF7"
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
    /// SectDialog
    /// </summary>
    public partial class SectDialog : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\Dialog\SectDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LF.FictionWorld.Project.Dialog.SectDialog dialog;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Dialog\SectDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TxbSubTitle;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Dialog\SectDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CmbAdd1;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Dialog\SectDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CmbAdd2;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\Dialog\SectDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CmbAdd3;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\Dialog\SectDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CmbAdd4;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\Dialog\SectDialog.xaml"
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
            System.Uri resourceLocater = new System.Uri("/LF.FictionWorld.Project;component/dialog/sectdialog.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Dialog\SectDialog.xaml"
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
            this.dialog = ((LF.FictionWorld.Project.Dialog.SectDialog)(target));
            
            #line 12 "..\..\..\Dialog\SectDialog.xaml"
            this.dialog.Loaded += new System.Windows.RoutedEventHandler(this.Dialog_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 19 "..\..\..\Dialog\SectDialog.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TopGrid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TxbSubTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.CmbAdd1 = ((System.Windows.Controls.ComboBox)(target));
            
            #line 59 "..\..\..\Dialog\SectDialog.xaml"
            this.CmbAdd1.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CmbAdd1_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CmbAdd2 = ((System.Windows.Controls.ComboBox)(target));
            
            #line 62 "..\..\..\Dialog\SectDialog.xaml"
            this.CmbAdd2.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CmbAdd2_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CmbAdd3 = ((System.Windows.Controls.ComboBox)(target));
            
            #line 65 "..\..\..\Dialog\SectDialog.xaml"
            this.CmbAdd3.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CmbAdd3_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.CmbAdd4 = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            
            #line 86 "..\..\..\Dialog\SectDialog.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnClose_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.BtnOK = ((System.Windows.Controls.Button)(target));
            
            #line 87 "..\..\..\Dialog\SectDialog.xaml"
            this.BtnOK.Click += new System.Windows.RoutedEventHandler(this.BtnOK_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

