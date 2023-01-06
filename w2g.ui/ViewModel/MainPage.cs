using SimpleTCP;
using System;
using System.Net.Sockets;
using w2g.core.standart.Models;
using w2g.ui.Types;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace w2g.ui.ViewModel
{
    public class MainPage : Types.ViewModel
    {
        private Uri videoSource;
        private Helpers.VideoLoader videoLoader = new Helpers.VideoLoader();
        private string pageTitle = "w2g.desktop";

        #region gates
        public Uri VideoSource
        {
            get => videoSource;
            set => SetProperty(ref videoSource, value);
        }
        public string PageTitle
        {
            get => pageTitle;
            set => SetProperty(ref pageTitle, value);
        }
        #endregion

        public MainPage()
        {
            LoadVideo("https://music.youtube.com/watch?v=TQTISvU6o2Y");
            SetupListeners();
        }

        public async void LoadVideo(string videoUrl)
        {
            var video = await videoLoader.LoadVideoSource(videoUrl);
            if(video == null) 
            {
                ContentDialog videoLoadingError = new ContentDialog()
                {
                    Title = "Video loading error",
                    Content = "Video could not be loaded... Enter a valid url.",
                    CloseButtonText = "Ok"
                };
                await videoLoadingError.ShowAsync();
                return;
            }
            VideoSource = video;
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
