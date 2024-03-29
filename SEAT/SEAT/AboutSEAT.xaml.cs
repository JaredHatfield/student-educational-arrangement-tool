﻿// <copyright file="AboutSEAT.xaml.cs" company="University of Louisville Speed School of Engineering">
// GNU General Public License v3
// </copyright>
// <summary>About window for the SEAT application.</summary>
namespace SEAT
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// About window for the SEAT application.
    /// </summary>
    public partial class AboutSEAT : Window
    {
        /// <summary>
        /// Initializes a new instance of the AboutSEAT class.
        /// </summary>
        public AboutSEAT()
        {
            InitializeComponent();
            buttonOk.Focus();
            this.AddHandler(Hyperlink.RequestNavigateEvent, new RequestNavigateEventHandler(this.HyperlinkNavigate));
            this.VersionNumber.Text = "Version " + System.Reflection.Assembly.GetCallingAssembly().GetName().Version.ToString();
        }

        /// <summary>
        /// Load the clicked hyperlink in a new web broswer window.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void HyperlinkNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        /// <summary>
        /// Close this window.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonOk(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
