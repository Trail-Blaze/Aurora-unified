using AuroraServer.IO;
using Serilog;
using Serilog.Core;
using System;
using System.IO;
using System.Text;

namespace AuroraServer
{
    class Program
    {
        public static Logger Log = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        static void Main(string[] args)
        {
            Log.Information("AuroraServer by Cyuubi, do not redistribute!");

            BitWriter writer = new BitWriter(195);

            writer.Write(true); // bHandshakePacket
            writer.Write(false); // SecretIdPad

            // PacketSizeFiller
            for (int index = 0; index < 24 * 8; index++) writer.Write(false);

            writer.Write(true); // Termination

            // ...

            StringBuilder builder = new StringBuilder("byte[] { ");

            foreach (byte value in writer.ToArray())
            {
                builder.Append($"0x{value:X2}, ");
            }

            builder.Remove(builder.Length - 2, 1);
            builder.Append("};");

            Log.Information(builder.ToString());
        }
    }
}
