using w2g.core.standart;
using w2g.core.standart.Models;

namespace w2g.ui.Helpers
{
    public class Service : Types.Singleton<Service>
    {
        public Client client;
        public Server server;
        public ClientState State { get; private set; }
        public Service()
        {

        }

        public void Connect(string server, int port)
        {
            client = new Client(server, port);
            client.Connect();
            State = ClientState.Client;
        }

        public void Host(int port)
        {
            server = new Server(port);
            server.Start();
            State = ClientState.Host;
        }
    }

    public enum ClientState
    {
        Host,
        Client
    }
}
