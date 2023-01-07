using SimpleTCP;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using w2g.core.standart;
using w2g.core.standart.Models;
using w2g.ui.Types;
using Windows.ApplicationModel.Core;
using Windows.Media.Playback;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace w2g.ui.ViewModel
{
    public class MainPage : Types.ViewModel
    {
        private Uri videoSource;
        private Helpers.VideoLoader videoLoader = new Helpers.VideoLoader();
        private string pageTitle = "w2g.desktop";
        private string currentVideo = "";
        private TimeSpan videoPosition;
        private TimeSpan loadedPosition;
        private MediaElement mediaElement;
        private bool playOnLoad = false;

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
        public Visibility SearchBarVisible
        {
            get => Helpers.Service.Instance.State == Helpers.ClientState.Host 
                ? Visibility.Visible : Visibility.Collapsed;
        }
        public bool VideoControlsVisible
        {
            get => Helpers.Service.Instance.State == Helpers.ClientState.Host;
        }
        public TimeSpan VideoPosition
        {
            get => videoPosition;
            set => videoPosition = value;
        }
        public bool isServer
        {
            get => Helpers.Service.Instance.State == Helpers.ClientState.Host;
        }
        public Server Server
        {
            get => Helpers.Service.Instance.server;
        }
        public Client Client
        {
            get => Helpers.Service.Instance.client;
        }
        public int Seconds
        {
            get => (int)mediaElement.Position.TotalSeconds;
        }
        #endregion

        public MainPage(MediaElement mediaElement)
        {
            this.mediaElement = mediaElement;
            LoadVideo("https://music.youtube.com/watch?v=TQTISvU6o2Y");
            SetupListeners();
            mediaElement.CurrentStateChanged += OnMediaStateChanged;
        }

        private void OnMediaStateChanged(object sender, RoutedEventArgs e)
        {
            if (!isServer) return;
            switch (mediaElement.CurrentState)
            {
                case Windows.UI.Xaml.Media.MediaElementState.Opening:
                    Server.SendVideoUrl(new UrlModel
                    {
                        Url = currentVideo
                    });
                    break;
                case Windows.UI.Xaml.Media.MediaElementState.Playing:
                    Server.Stop(new StopModel
                    {
                        Seconds = this.Seconds
                    });
                    break;
                case Windows.UI.Xaml.Media.MediaElementState.Paused:
                    Server.Stop(new StopModel
                    {
                        Seconds = this.Seconds
                    });
                    break;
                default:
                    break;
            }
        }

        public async void LoadVideo(string videoUrl, int seconds = 0)
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
            currentVideo = videoUrl;
            VideoSource = video;
            loadedPosition = TimeSpan.FromSeconds(seconds);
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

            Helpers.Service.Instance.client.RequestCurrent(new CurrentModel());
        }

        #region host listeners
        private void OnCurrentRequested(object sender, Message e)
        {
            Helpers.Service.Instance.server.Current(new CurrentModel
            {
                Url = currentVideo,
                Seconds = (int)mediaElement.Position.TotalSeconds,
                Playing = mediaElement.CurrentState == Windows.UI.Xaml.Media.MediaElementState.Playing
            });
        }

        private void OnClientDisconnected(object sender, TcpClient e)
        {
            //throw new NotImplementedException();
        }

        private void OnClientConnected(object sender, TcpClient e)
        {
            // throw new NotImplementedException();
        }
        #endregion

        #region client listeners
        private async void OnVideoStop(object sender, StopModel e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                mediaElement.Position = TimeSpan.FromSeconds(e.Seconds);
                mediaElement.Stop();
            });
        }

        private async void OnVideoSet(object sender, UrlModel e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                LoadVideo(e.Url);
            });
        }

        private async void OnSetTime(object sender, TimeModel e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                mediaElement.Position = TimeSpan.FromSeconds(e.Seconds);
            });
        }

        private async void OnVideoPlay(object sender, PlayModel e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                mediaElement.Position = TimeSpan.FromSeconds(e.Seconds);
                mediaElement.Stop();
            });
        }

        private async void OnCurrentSet(object sender, CurrentModel e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                playOnLoad = e.Playing;
                LoadVideo(e.Url, (int)e.Seconds);
            });
        }

        internal void OnMediaOpend(MediaElement sender)
        {
            if (loadedPosition == null) return;
            sender.Position = loadedPosition;
            if (playOnLoad)
                sender.Play();
        }
        #endregion
    }
}
