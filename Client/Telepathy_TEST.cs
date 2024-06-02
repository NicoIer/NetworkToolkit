using NetworkToolkit.Telepathy;

namespace Client;


public static class Telepathy_TEST
{
    public static void Test()
    {
        TelepathyServer server = new TelepathyServer(16*1024);
        server.Start(24419);
        TelepathyClient client = new TelepathyClient(16*1024);
        client.Connect("127.0.0.1", 24419);
    }
}