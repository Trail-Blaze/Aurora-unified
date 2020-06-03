using Serilog;
using Serilog.Core;

namespace AuroraPacket
{
    class Program
    {
        static Logger _log = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        static void Main(string[] args)
        {
            _log.Information("AuroraPacket by Cyuubi, do not redistribute!");
        }
    }
}
