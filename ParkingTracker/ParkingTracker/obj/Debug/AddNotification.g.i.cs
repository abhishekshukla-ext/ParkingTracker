﻿#pragma checksum "D:\FL\FiverrGigs\Webd123\Source\ParkingTracker\ParkingTracker\AddNotification.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0BCCBB349C619DA6812249E0E2BC2777"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
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
    
    
    public partial class AddNotification : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock expirationTimeLabel;
        
        internal Microsoft.Phone.Controls.DatePicker expirationDatePicker;
        
        internal Microsoft.Phone.Controls.TimePicker expirationTimePicker;
        
        internal System.Windows.Controls.TextBlock copyright;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/ParkingTracker;component/AddNotification.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.expirationTimeLabel = ((System.Windows.Controls.TextBlock)(this.FindName("expirationTimeLabel")));
            this.expirationDatePicker = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("expirationDatePicker")));
            this.expirationTimePicker = ((Microsoft.Phone.Controls.TimePicker)(this.FindName("expirationTimePicker")));
            this.copyright = ((System.Windows.Controls.TextBlock)(this.FindName("copyright")));
        }
    }
}

