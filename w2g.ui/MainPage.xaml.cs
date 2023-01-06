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
            SetupListeners();
        }

        private void SetupListeners()
        {
            switch (Helpers.Service.Instance.State)
            {
                case Helpers.ClientState.Host:
                    SetupHostListeners();
                    break;
                case Helpers.ClientState.Client:
                    SetupClientListeners();
                    break;
                default:
                    break;
            }
        }

        #region ui listeners
        private void OnQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args) =>
            ViewModel.LoadVideo(sender.Text);
        #endregion

        /// <summary>
        /// setup all host listeners
        /// </summary>
        private void SetupHostListeners()
        {
            Helpers.Service.Instance.server.ClientConnected += OnClientConnected;
            Helpers.Service.Instance.server.ClientDisconnected += OnClientDisconnected;
            Helpers.Service.Instance.server.OnCurrentRequest += OnCurrentRequested;
        }

        /// <summary>
        /// setup all client listeners
        /// </summary>
        private void SetupClientListeners()
        {
            Helpers.Service.Instance.client.SetCurrent += OnCurrentSet;
            Helpers.Service.Instance.client.VideoPlay += OnVideoPlay;
            Helpers.Service.Instance.client.SetTime += OnSetTime;
            Helpers.Service.Instance.client.VideoSet += OnVideoSet;
            Helpers.Service.Instance.client.VideoStop += OnVideoStop;
        }

        #region host listeners
        private void OnCurrentRequested(object sender, Message e)
        {
            throw new NotImplementedException();
        }

        private void OnClientDisconnected(object sender, TcpClient e)
        {
            throw new NotImplementedException();
        }

        private void OnClientConnected(object sender, TcpClient e)
        {
            throw new NotImplementedException();
        }
        #endregion
       
        #region client listeners
        private void OnVideoStop(object sender, StopModel e)
        {
            throw new NotImplementedException();
        }

        private void OnVideoSet(object sender, UrlModel e)
        {
            throw new NotImplementedException();
        }

        private void OnSetTime(object sender, TimeModel e)
        {
            throw new NotImplementedException();
        }

        private void OnVideoPlay(object sender, PlayModel e)
        {
            throw new NotImplementedException();
        }

        private void OnCurrentSet(object sender, CurrentModel e)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
