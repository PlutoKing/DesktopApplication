#pragma checksum "..\..\..\Dialog\ActionDialog.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "15FA160223CEABC92CBA081DE8A9A5EC3BBDFF6CA12DA62E649847C83CDA0556"
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
    /// ActionDialog
    /// </summary>
    public partial class ActionDialog : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\Dialog\ActionDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LF.FictionWorld.Project.Dialog.ActionDialog dialog;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Dialog\ActionDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid TopGrid;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Dialog\ActionDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnClose;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Dialog\ActionDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CmbName;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Dialog\ActionDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CmbType;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\Dialog\ActionDialog.xaml"
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
            System.Uri resourceLocater = new System.Uri("/LF.FictionWorld.Project;component/dialog/actiondialog.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Dialog\ActionDialog.xaml"
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
            this.dialog = ((LF.FictionWorld.Project.Dialog.ActionDialog)(target));
            
            #line 11 "..\..\..\Dialog\ActionDialog.xaml"
            this.dialog.Loaded += new System.Windows.RoutedEventHandler(this.Dialog_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TopGrid = ((System.Windows.Controls.Grid)(target));
            
            #line 20 "..\..\..\Dialog\ActionDialog.xaml"
            this.TopGrid.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TopGrid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BtnClose = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\Dialog\ActionDialog.xaml"
            this.BtnClose.Click += new System.Windows.RoutedEventHandler(this.BtnClose_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.CmbName = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.CmbType = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            
            #line 68 "..\..\..\Dialog\ActionDialog.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnClose_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.BtnOK = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\..\Dialog\ActionDialog.xaml"
            this.BtnOK.Click += new System.Windows.RoutedEventHandler(this.BtnOK_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

