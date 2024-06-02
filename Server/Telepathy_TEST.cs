using NetworkToolkit.Telepathy;

namespace Client;


public static class Telepathy_TEST
{
    public static void Test()
    {
        TelepathyServer server = new TelepathyServer(16*1024);
        server.Start(24419);
        server.Tick(10000, () => true);
    }
}