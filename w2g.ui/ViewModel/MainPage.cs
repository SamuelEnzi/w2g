using System;
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
    }
}
