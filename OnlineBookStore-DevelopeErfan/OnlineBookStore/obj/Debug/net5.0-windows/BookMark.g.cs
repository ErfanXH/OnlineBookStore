﻿#pragma checksum "..\..\..\BookMark.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BBDB04D04716EDF96D14B58BA76B68FA6A2826E5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using OnlineBookStore;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace OnlineBookStore {
    
    
    /// <summary>
    /// BookMark
    /// </summary>
    public partial class BookMark : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\BookMark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMarked;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\BookMark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBought;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\BookMark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnExit;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\BookMark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border HomePanel;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\BookMark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border MarkedPanel;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\BookMark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid MarkedGrid;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\BookMark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbDescription;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\BookMark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BoughtPanel;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\BookMark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid BoughtGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.6.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/OnlineBookStore;component/bookmark.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\BookMark.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.6.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btnMarked = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\BookMark.xaml"
            this.btnMarked.Click += new System.Windows.RoutedEventHandler(this.btnMarked_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnBought = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\BookMark.xaml"
            this.btnBought.Click += new System.Windows.RoutedEventHandler(this.btnBought_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnExit = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\BookMark.xaml"
            this.btnExit.Click += new System.Windows.RoutedEventHandler(this.btnExit_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.HomePanel = ((System.Windows.Controls.Border)(target));
            return;
            case 5:
            this.MarkedPanel = ((System.Windows.Controls.Border)(target));
            return;
            case 6:
            this.MarkedGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 26 "..\..\..\BookMark.xaml"
            this.MarkedGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.MarkedGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.lbDescription = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.BoughtPanel = ((System.Windows.Controls.Border)(target));
            return;
            case 9:
            this.BoughtGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 44 "..\..\..\BookMark.xaml"
            this.BoughtGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.BoughtGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

