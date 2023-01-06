using SimpleTCP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using w2g.core.standart.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace w2g.ui
{
    public sealed partial class MainPage : Page
    {
        public ViewModel.MainPage ViewModel { get; set; } = new ViewModel.MainPage();
        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.SetTitleBar(UI_AppTitleBar);
            this.DataContext = ViewModel;
        }

        #region ui listeners
        private void OnQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args) =>
            ViewModel.LoadVideo(sender.Text);
        #endregion
    }
}
