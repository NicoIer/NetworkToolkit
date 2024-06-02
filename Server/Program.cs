using Client;
using NetworkToolkit.kcp2k;

namespace Server;

static class Program
{
    static async Task Main(string[] args)
    {
        await Telepathy_TEST.Test();
    }
}