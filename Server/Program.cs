using NetworkToolkit.kcp2k;

namespace Server;

static class Program
{
    static void Main(string[] args)
    {
        KcpConfig config = new KcpConfig();
        // 测试KCP
        KcpServer server = new KcpServer(OnConnected, OnData, OnDisconnected, OnError, config);
        server.Start(24419);
        while (true)
        {
            server.TickIncoming();
            server.TickOutgoing();
        }
    }

    private static void OnError(int arg1, ErrorCode arg2, string arg3)
    {
        Console.WriteLine($"OnError, connectionId={arg1}, errorCode={arg2}, message={arg3}");
    }

    private static void OnDisconnected(int obj)
    {
        Console.WriteLine($"OnDisconnected, connectionId={obj}");
    }

    private static void OnData(int arg1, ArraySegment<byte> arg2, KcpChannel arg3)
    {
        Console.WriteLine($"OnData, connectionId={arg1}, channel={arg3}");
    }

    private static void OnConnected(int obj)
    {
        Console.WriteLine($"OnConnected, connectionId={obj}");
    }
}