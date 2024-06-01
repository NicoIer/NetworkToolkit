using System.Text;
using NetworkToolkit.kcp2k;

namespace Client;

internal static class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        KcpConfig config = new KcpConfig();

        List<KcpClient> clients = new List<KcpClient>(1001);
        for (int i = 0; i < 10000; i++)
        {
            KcpClient client = new KcpClient(OnConnected, OnData, OnDisconnected, OnError, config);
            clients.Add(client);
        }

        for (int i=0;i!=clients.Count;++i)
        {
            var client = clients[i];
            client.Connect("127.0.0.1",24419);
            Console.WriteLine($"Connected:{i}");
        }

        byte[] bytes = Encoding.UTF8.GetBytes("Hello, World!");
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        while (!cancellationTokenSource.IsCancellationRequested)
        {
            foreach (var client in clients)
            {
                client.TickIncoming();
                client.TickOutgoing();
                client.Send(bytes, KcpChannel.Reliable);
            }
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }

    private static async Task Test01()
    {
        KcpConfig config = new KcpConfig();
        KcpClient client = new KcpClient(OnConnected, OnData, OnDisconnected, OnError, config);
        client.Connect("127.0.0.1", 24419);
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        Task task = Task.Run(() =>
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                client.TickIncoming();
                client.TickOutgoing();
            }
        }, cancellationTokenSource.Token);

        while (!cancellationTokenSource.IsCancellationRequested)
        {
            string? line = Console.ReadLine();
            if (line == null) continue;
            if (line == "exit")
            {
                await cancellationTokenSource.CancelAsync();
                client.Disconnect();
                break;
            }

            if (string.IsNullOrWhiteSpace(line)) continue;
            client.Send(System.Text.Encoding.UTF8.GetBytes(line), KcpChannel.Reliable);
        }

        await task;
    }

    private static void OnDisconnected()
    {
        Console.WriteLine("Disconnected");
    }

    private static void OnData(ArraySegment<byte> arg1, KcpChannel arg2)
    {
        Console.WriteLine("Data Received");
    }

    private static void OnConnected()
    {
        Console.WriteLine("Connected");
    }

    private static void OnError(ErrorCode arg1, string arg2)
    {
        Console.WriteLine("Error");
    }
}