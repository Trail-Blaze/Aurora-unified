using AuroraServer.IO;
using Serilog;
using Serilog.Core;
using System.Linq;

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

            BitReader reader = new BitReader(new byte[] { 0x17, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80 });

            _log.Information($"IsValid = {Enumerable.SequenceEqual(reader.ReadBits(4), new bool[] { true, true, true, false })}");
            _log.Information($"IsHandshake = {reader.ReadBit()}");
        }
    }
}
