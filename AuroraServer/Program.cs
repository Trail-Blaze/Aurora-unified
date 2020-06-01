using Serilog;
using Serilog.Core;

namespace AuroraServer
{
    class Program
    {
        static Logger _log = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        static void Main(string[] args)
        {
            _log.Information("AuroraServer by Cyuubi, do not redistribute!");
        }
    }
}
