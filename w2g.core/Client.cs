using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace w2g.core
{
    public class Client
    {
        public EventHandler<Models.UrlModel>? VideoSet;
        public EventHandler<Models.PlayModel>? VideoPlay;
        public EventHandler<Models.StopModel>? VideoStop;
        public EventHandler<Models.TimeModel>? SetTime;
        public EventHandler<Models.CurrentModel>? SetCurrent;

        private SimpleTcpClient client = new SimpleTcpClient();

        public string Server { get; set; }
        public int Port { get; set; } = 6061;

        public Client(string server, int port)
        {
            this.Server = server;
            this.Port = port;
            Setup();
        }

        public Client(string server)
        {
            this.Server = server;
            Setup();
        }

        private void Setup()
        {
            client.Delimiter = 0x13;
            client.DelimiterDataReceived += ParseData;
            client.StringEncoder = System.Text.ASCIIEncoding.ASCII;
        }

        private void ParseData(object? sender, Message e)
        {
            var request = e.MessageString.Parse();
            if (request == null) return;
            if (request.Value.data == null) return;

            switch (request.Value.type)
            {
                case Models.Base.RequestType.VideoSet:
                    VideoSet?.Invoke(this, (Models.UrlModel)request.Value.data);
                    break;
                case Models.Base.RequestType.TimeSet:
                    SetTime?.Invoke(this, (Models.TimeModel)request.Value.data);
                    break;
                case Models.Base.RequestType.Play:
                    VideoPlay?.Invoke(this, (Models.PlayModel)request.Value.data);
                    break;
                case Models.Base.RequestType.Stop:
                    VideoStop?.Invoke(this, (Models.StopModel)request.Value.data);
                    break;
                case Models.Base.RequestType.Current:
                    SetCurrent?.Invoke(this, (Models.CurrentModel)request.Value.data);
                    break;
                case Models.Base.RequestType.None:
                    break;
                default:
                    break;
            }
        }

        public bool Connect()
        {
            try
            {
                client.Connect(Server, Port);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void RequestCurrent(Models.CurrentModel current)
        {
            current.RequestType = Models.Base.RequestType.Current;
            client.WriteLine(current.Serialize());
        }
    }
}
