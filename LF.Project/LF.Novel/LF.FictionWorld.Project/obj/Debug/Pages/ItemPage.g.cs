#pragma checksum "..\..\..\Pages\ItemPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "73AC88960A56115696A974DEE2A5B3BC0E62595BF9388F35911A1BA5D03884B2"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using LF.FictionWorld.Project.Pages;
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


namespace LF.FictionWorld.Project.Pages {
    
    
    /// <summary>
    /// ItemPage
    /// </summary>
    public partial class ItemPage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 84 "..\..\..\Pages\ItemPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnAdd;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\Pages\ItemPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnEdit;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\Pages\ItemPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnDelete;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\Pages\ItemPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DtgItems;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\Pages\ItemPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DtgRoles;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\Pages\ItemPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DtgSites;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\Pages\ItemPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DtgRecipes;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\..\Pages\ItemPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DtgPlots;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\..\Pages\ItemPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DtgSects;
        
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
            System.Uri resourceLocater = new System.Uri("/LF.FictionWorld.Project;component/pages/itempage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\ItemPage.xaml"
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
            
            #line 9 "..\..\..\Pages\ItemPage.xaml"
            ((LF.FictionWorld.Project.Pages.ItemPage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BtnAdd = ((System.Windows.Controls.Button)(target));
            
            #line 85 "..\..\..\Pages\ItemPage.xaml"
            this.BtnAdd.Click += new System.Windows.RoutedEventHandler(this.BtnAdd_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BtnEdit = ((System.Windows.Controls.Button)(target));
            
            #line 87 "..\..\..\Pages\ItemPage.xaml"
            this.BtnEdit.Click += new System.Windows.RoutedEventHandler(this.BtnEdit_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BtnDelete = ((System.Windows.Controls.Button)(target));
            
            #line 89 "..\..\..\Pages\ItemPage.xaml"
            this.BtnDelete.Click += new System.Windows.RoutedEventHandler(this.BtnDelete_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.DtgItems = ((System.Windows.Controls.DataGrid)(target));
            
            #line 102 "..\..\..\Pages\ItemPage.xaml"
            this.DtgItems.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DtgItems_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 102 "..\..\..\Pages\ItemPage.xaml"
            this.DtgItems.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.DtgItems_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.DtgRoles = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            this.DtgSites = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 8:
            this.DtgRecipes = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 9:
            this.DtgPlots = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 10:
            this.DtgSects = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

