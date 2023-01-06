using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class CreateSessionPage : Page
    {
        public CreateSessionPage()
        {
            this.InitializeComponent();
        }

        private void OnHostClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Helpers.Service.Instance.Host(int.Parse(UI_PortInput.Text));
                SwitchToMainPage();
            }
            catch (Exception ex)
            {
                ContentDialog cd = new ContentDialog
                {
                    Title = "Could not host server",
                    Content = $"{ex.Message}",
                    CloseButtonText = "Hhm?"
                };
                _ = cd.ShowAsync();
            }
        }

        private void OnJoinClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Helpers.Service.Instance.Connect(UI_ServerInput.Text, int.Parse(UI_PortInput.Text));
                SwitchToMainPage();
            }
            catch (Exception ex)
            {
                ContentDialog cd = new ContentDialog
                {
                    Title = "Could not join server",
                    Content = $"{ex.Message}",
                    CloseButtonText = "Hhm?"
                };
                _ = cd.ShowAsync();
            }
        }

        private void SwitchToMainPage()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage), null);
        }
    }
}
