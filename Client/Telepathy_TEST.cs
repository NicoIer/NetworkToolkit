using System.Text;
using NetworkToolkit.Telepathy;

namespace Client;


public static class Telepathy_TEST
{
    public static async Task Test()
    {
        TelepathyClient client = new TelepathyClient(16*1024);
        client.Connect("127.0.0.1", 24419);
        client.OnConnected = () => Console.WriteLine("Connected");
        client.OnData = (data) => Console.WriteLine("OnData");
        client.OnDisconnected = () => Console.WriteLine("Disconnected");
        CancellationTokenSource cts = new CancellationTokenSource();
        ArraySegment<byte> msg = new ArraySegment<byte>(Encoding.UTF8.GetBytes("Hello"));
        while (cts.IsCancellationRequested == false)
        {
            client.Tick(1000);
            await Task.Delay(TimeSpan.FromSeconds(1/60f), cts.Token);
            client.Send(msg);
        }
    }
}