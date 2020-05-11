using Serilog;
using Serilog.Core;
using System;

namespace AuroraServer
{
    class Program
    {
        public static Logger Log;

        static void Main(string[] args)
        {
            Log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Hello, Serilog!");

            Console.WriteLine("AuroraServer by Cyuubi, do not redistribute!");
        }
    }
}
