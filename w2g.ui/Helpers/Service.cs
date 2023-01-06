using w2g.core.standart;
using w2g.core.standart.Models;

namespace w2g.ui.Helpers
{
    public class Service
    {
        public Client client;
        public Server server;

        public ClientState clientState { get; private set; }
        public Service(ClientState state)
        {
            this.clientState = state;
        }

        public void Connect(string server, int port)
        {
            if (clientState != ClientState.Client)
                throw new System.Exception("service needs to be in client state");

            client = new Client(server, port);
            client.Connect();
        }

        public void Host(int port)
        {
            if (clientState != ClientState.Host)
                throw new System.Exception("service needs to be in host state");

            server = new Server(port);
            server.Start();
        }
    }


    public enum ClientState
    {
        Host,
        Client
    }
}
