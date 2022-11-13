using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace w2g.desktop
{
    
    public static class Helper
    {
        private static YoutubeClient youtube = new YoutubeClient();      
        public static async Task<(string, TimeSpan?)> ConvertToStream(string url)
        {
            var video = await youtube.Videos.GetAsync(url);
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(url);
            var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
            return (streamInfo.Url, video.Duration);
        }   
    }
    
}
