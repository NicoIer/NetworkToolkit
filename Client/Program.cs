using System.Text;
using NetworkToolkit.kcp2k;

namespace Client;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        await KCP_Test.Test();
    }
}