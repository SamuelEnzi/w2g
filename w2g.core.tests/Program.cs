using w2g.core;
using w2g.core.Models;

Console.WriteLine("1) server\n2)client");
var selection = Console.ReadLine();

if(selection == "1")
    StartServer();
else
    StartClient();

while(true)
    Console.ReadLine();


void StartClient()
{
    var client = new Client("127.0.0.1", 6061);

    client.SetCurrent += (s, i) => Display(i);
    client.SetTime += (s, i) => Display(i);
    client.VideoSet += (s, i) =>
    {
        Display(i);
    };
    client.VideoPlay += (s, i) => Display(i);
    client.VideoStop += (s, i) => Display(i);

    client.Connect();


    Console.WriteLine("client connected to '127.0.0.1:6061'");
    client.RequestCurrent(new CurrentModel());
}

void Display(object i)
{
    Console.WriteLine(i.ToString());
}

void StartServer()
{
    var server = new Server();

    server.OnCurrentRequest += (s, i) => Display(i);

    server.Start();

    Console.WriteLine("press a key to send requests");
    Console.ReadLine();


    server.Play(new PlayModel { Seconds = 12 });
    server.Stop(new StopModel { Seconds = 123 });
    server.SendVideoUrl(new UrlModel { Url = "diocae" });
    server.Current(new CurrentModel { Url = "diocane2", Seconds = 124 });
    server.SetTime(new TimeModel { Seconds = 12345 });

    
}
