using System.Net.WebSockets;
using NetworkToolkit.Telepathy;

namespace Client;


public static class Telepathy_TEST
{
    public static  async Task Test()
    {
        TelepathyServer server = new TelepathyServer(16*1024);
        server.Start(24419);
        server.OnConnected = (connectionId) => Console.WriteLine("Connected: " + connectionId);
        server.OnData = (connectionId, data) => Console.WriteLine($"OnData:{connectionId}");
        server.OnDisconnected = (connectionId) => Console.WriteLine("Disconnected: " + connectionId);
        CancellationTokenSource cts = new CancellationTokenSource();
        while (cts.IsCancellationRequested == false)
        {
            server.Tick(1000);
            await Task.Delay(TimeSpan.FromSeconds(1/60f), cts.Token);
        }
    }
}