using w2g.core.standart;
using w2g.core.standart.Models;

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
    Console.Write("ip: ");
    var ip = Console.ReadLine();
    Console.Write("port: ");
    var port = int.Parse(Console.ReadLine()!);

    var client = new Client(ip, port);

    client.SetCurrent += (s, i) => Display(i);
    client.SetTime += (s, i) => Display(i);
    client.VideoSet += (s, i) => Display(i);
    client.VideoPlay += (s, i) => Display(i);
    client.VideoStop += (s, i) => Display(i);

    client.Connect();


    Console.WriteLine("client connected to '127.0.0.1:6061'");
    client.RequestCurrent(new CurrentModel());
}

void StartServer()
{
    var server = new Server(3366);

    server.OnCurrentRequest += (s, i) => Display(i);

    server.Start();

    Console.WriteLine("press a key to send requests");
    Console.ReadLine();

    server.Play(new PlayModel { Seconds = 12 });
    server.Stop(new StopModel { Seconds = 123 });
    server.SendVideoUrl(new UrlModel { Url = "diocae" });
    server.Current(new CurrentModel { Url = "https://music.youtube.com/watch?v=lOxI1dcLrJA&list=RDAMVMlOxI1dcLrJA", Seconds = 120, Playing = true });
    server.SetTime(new TimeModel { Seconds = 12345 });
}

void Display(object i) =>
     Console.WriteLine(i.ToString());