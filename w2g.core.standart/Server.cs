using SimpleTCP;
using System;
using System.Net.Sockets;
using w2g.core.standart.Models.Base;

namespace w2g.core.standart
{
    public class Server
    {
        public EventHandler<Message> OnCurrentRequest;
        public EventHandler<TcpClient> ClientConnected;
        public EventHandler<TcpClient> ClientDisconnected;
        private SimpleTcpServer server = new SimpleTcpServer();
        public int Port { get; private set; } = 6061;

        public Server() { Setup(); }

        public Server(int port)
        {
            this.Port = port;
            Setup();
        }

        private void Setup()
        {
            server.Delimiter = 0x13;
            server.ClientConnected += (sender, tcpClient) => ClientConnected?.Invoke(this, tcpClient);
            server.ClientDisconnected += (sender, tcpClient) => ClientDisconnected?.Invoke(this, tcpClient);
            server.DelimiterDataReceived += ParseData;
            server.StringEncoder = System.Text.ASCIIEncoding.ASCII;
        }

        private void ParseData(object sender, Message e)
        {
            var request = e.MessageString.Parse();
            if (request == null) return;
            if (request.Value.data == null) return;

            switch (request.Value.type)
            {
                case RequestType.VideoSet:
                    break;
                case RequestType.TimeSet:
                    break;
                case RequestType.Play:
                    break;
                case RequestType.Stop:
                    break;
                case RequestType.Current:
                    OnCurrentRequest?.Invoke(this, e);
                    break;
                case RequestType.None:
                    break;
                default:
                    break;
            }
        }

        public void Start()
        {
            server.Start(this.Port);
        }

        public void SendVideoUrl(Models.UrlModel url)
        {
            if (!server.IsStarted) return;
            url.RequestType = RequestType.VideoSet;
            server.BroadcastLine(url.Serialize());
        }

        public void SetTime(Models.TimeModel time)
        {
            if (!server.IsStarted) return;
            time.RequestType = RequestType.TimeSet;
            server.BroadcastLine(time.Serialize());
        }

        public void Play(Models.PlayModel play)
        {
            if (!server.IsStarted) return;
            play.RequestType = RequestType.Play;
            server.BroadcastLine(play.Serialize());
        }

        public void Stop(Models.StopModel stop)
        {
            if (!server.IsStarted) return;
            stop.RequestType = RequestType.Stop;
            server.BroadcastLine(stop.Serialize());
        }

        public void Current(Models.CurrentModel current)
        {
            if (!server.IsStarted) return;
            current.RequestType = RequestType.Current;
            server.BroadcastLine(current.Serialize());
        }
    }
}
