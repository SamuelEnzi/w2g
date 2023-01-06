using System;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace w2g.ui.Helpers
{
    internal class VideoLoader
    {
        YoutubeClient youtube = new YoutubeClient();

        public async Task<Uri> LoadVideoSource(string video)
        {
            try
            {
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(video);

                var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();

                return new Uri(streamInfo.Url);
            }
            catch { return null; }
        }
    }
}
