﻿#pragma checksum "D:\FL\FiverrGigs\Webd123\Source\ParkingTracker\ParkingTracker\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EB6D2602BF3C0D60477C77CB31C53E60"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Coding4Fun.Phone.Controls.Toolkit;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace ParkingTracker {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.StackPanel LayoutRoot;
        
        internal System.Windows.Controls.TextBlock EmptyTextBlock;
        
        internal System.Windows.Controls.TextBlock TrialInfo;
        
        internal System.Windows.Controls.StackPanel timespanPanel;
        
        internal Coding4Fun.Phone.Controls.Toolkit.TimeSpanPicker timeSpan;
        
        internal System.Windows.Controls.TextBlock ExpirationTime;
        
        internal System.Windows.Controls.TextBlock BeginTime;
        
        internal System.Windows.Controls.TextBlock currentTimeSpan;
        
        internal System.Windows.Controls.Button btnBuyApplication;
        
        internal System.Windows.Controls.TextBlock AppRegistrationStatus;
        
        internal System.Windows.Controls.TextBlock copyright;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton btnAdd;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton btnDelete;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/ParkingTracker;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.StackPanel)(this.FindName("LayoutRoot")));
            this.EmptyTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("EmptyTextBlock")));
            this.TrialInfo = ((System.Windows.Controls.TextBlock)(this.FindName("TrialInfo")));
            this.timespanPanel = ((System.Windows.Controls.StackPanel)(this.FindName("timespanPanel")));
            this.timeSpan = ((Coding4Fun.Phone.Controls.Toolkit.TimeSpanPicker)(this.FindName("timeSpan")));
            this.ExpirationTime = ((System.Windows.Controls.TextBlock)(this.FindName("ExpirationTime")));
            this.BeginTime = ((System.Windows.Controls.TextBlock)(this.FindName("BeginTime")));
            this.currentTimeSpan = ((System.Windows.Controls.TextBlock)(this.FindName("currentTimeSpan")));
            this.btnBuyApplication = ((System.Windows.Controls.Button)(this.FindName("btnBuyApplication")));
            this.AppRegistrationStatus = ((System.Windows.Controls.TextBlock)(this.FindName("AppRegistrationStatus")));
            this.copyright = ((System.Windows.Controls.TextBlock)(this.FindName("copyright")));
            this.btnAdd = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("btnAdd")));
            this.btnDelete = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("btnDelete")));
        }
    }
}

