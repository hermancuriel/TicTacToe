﻿#pragma checksum "..\..\StartPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EA988768C94ECF7C6B652FC129DC378C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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


namespace TicTacToe {
    
    
    /// <summary>
    /// StartPage
    /// </summary>
    public partial class StartPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\StartPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainGrid;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\StartPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonLoginVsComputer;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\StartPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonLoginVsGuest;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\StartPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonLoginVsPlayer;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\StartPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonGuest;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\StartPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonHistory;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\StartPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonRegister;
        
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
            System.Uri resourceLocater = new System.Uri("/TicTacToe;component/startpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\StartPage.xaml"
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
            
            #line 8 "..\..\StartPage.xaml"
            ((TicTacToe.StartPage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.mainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.ButtonLoginVsComputer = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\StartPage.xaml"
            this.ButtonLoginVsComputer.Click += new System.Windows.RoutedEventHandler(this.ButtonLoginVsComputer_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ButtonLoginVsGuest = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\StartPage.xaml"
            this.ButtonLoginVsGuest.Click += new System.Windows.RoutedEventHandler(this.ButtonLoginVsGuest_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ButtonLoginVsPlayer = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\StartPage.xaml"
            this.ButtonLoginVsPlayer.Click += new System.Windows.RoutedEventHandler(this.ButtonLoginVsPlayer_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ButtonGuest = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\StartPage.xaml"
            this.ButtonGuest.Click += new System.Windows.RoutedEventHandler(this.ButtonGuest_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ButtonHistory = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\StartPage.xaml"
            this.ButtonHistory.Click += new System.Windows.RoutedEventHandler(this.ButtonHistory_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ButtonRegister = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\StartPage.xaml"
            this.ButtonRegister.Click += new System.Windows.RoutedEventHandler(this.ButtonRegister_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 31 "..\..\StartPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExitButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

