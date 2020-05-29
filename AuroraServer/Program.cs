using AuroraServer.IO;
using Serilog;
using Serilog.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace AuroraServer
{
    class Program
    {
        public static Logger Log = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        public static Random _rnd = new Random();

        public static bool notinit = false;
        public static UdpClient udpServer;

        public static byte[][] handshakesecret;
        public static byte ActiveSecret = 255;*/

        static void Main(string[] args)
        {
            Log.Information("AuroraServer by Cyuubi, do not redistribute!");

            handshakesecret = new byte[2][];

            byte[] bytes = new byte[0x400];

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 9050);
            udpServer = new UdpClient(endPoint);

            Log.Information("Waiting for a client...");

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);

            updatesecret();

            while (true)
            {
                bytes = udpServer.Receive(ref sender);

                PrintBytes(bytes);

                var lastByte = bytes[^1];
                if (lastByte != 0)
                {
                    var bitSize = (bytes.Length * 8) - 1;

                    // Bit streaming, starts at the Least Significant Bit, and ends at the MSB.
                    while (!((lastByte & 0x80) >= 1))
                    {
                        lastByte *= 2;
                        bitSize--;
                    }

                    var bitArchive = new BitReader(bytes, bitSize);
                    Recieved(bitArchive, sender);
                }
                else
                {
                    Log.Error("Malformed packet: Received packet with 0's in last byte of packet");
                    throw new Exception("Malformed packet: Received packet with 0's in last byte of packet");
                }

                //Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                //udpServer.Send(bytes, bytes.Length, sender);
            }
        }

        static void Recieved(BitReader reader, IPEndPoint sender)
        {
            PrintBits(reader._bits);

            var handshake = reader.ReadBit();
            //var encrypted = reader.ReadBit();

            Log.Information($"is handshake = {handshake}");

            if (!notinit)
            {
                if (handshake) //handshake
                {
                    updatesecret();

                    var writer = new BitWriter(195);

                    writer.Write(true);// handshake packet
                    writer.Write(Convert.ToBoolean(ActiveSecret));//active secret

                    writer.Write(-1.0f);//todo: timestamp

                    HMACSHA1 hmac = new HMACSHA1(handshakesecret[ActiveSecret]); // key

                    using (MemoryStream stream = new MemoryStream())
                    using (BinaryWriter binaryWriter = new BinaryWriter(stream))
                    {
                        binaryWriter.Write(-1.0f);

                        var addr = $"{sender.Address}:{sender.Port}";
                        binaryWriter.Write(addr.Length);
                        binaryWriter.Write(CodePagesEncodingProvider.Instance.GetEncoding(1252).GetBytes(addr));

                        writer.Write(hmac.ComputeHash(stream.ToArray()));
                    }

                    writer.Write(true);//termination

                    byte[] ohno = writer.ToBytes();
                    PrintBytes(ohno, false);
                    udpServer.Send(ohno, ohno.Length, sender);

                    notinit = true;
                }
            }
            else
            {
                var skipAck = false;

                uint packetId = reader.ReadSerializedInt(16384);

                while (!reader.AtEnd())
                {
                    var ack = reader.ReadBit();

                    if (ack)
                    {
                        uint ackpacketId = reader.ReadSerializedInt(16384);
                        bool hasserverframetime = reader.ReadBit();
                        int remoteinkbytespersecond = reader.ReadIntPacked();

                        Log.Information($"ackpacketid = {ackpacketId}, hasserverframetime = {hasserverframetime}, remoteinkbytespersecond = {remoteinkbytespersecond}");
                    }
                    else
                    {
                        bool control = reader.ReadBit();
                        bool open = control ? reader.ReadBit() : false;
                        bool close = control ? reader.ReadBit() : false;
                        bool dormant = close ? reader.ReadBit() : false;
                        bool isReplicationPaused = reader.ReadBit();
                        bool reliable = reader.ReadBit();
                        uint chIndex = reader.ReadSerializedInt(10240);
                        bool hasPackageMapExports = reader.ReadBit();
                        bool hasMustBeMappedGUIDs = reader.ReadBit();
                        bool partial = reader.ReadBit();

                        uint chsequence = 0;
                        if (reliable)
                            chsequence = reader.ReadSerializedInt(1024);

                        bool partialInitial = false;
                        bool partialFinal = false;
                        if (partial)
                        {
                            partialInitial = reader.ReadBit();
                            partialFinal = reader.ReadBit();
                        }

                        uint chType = 0;
                        if (reliable || open)
                        {
                            chType = reader.ReadSerializedInt(8);
                        }

                        var bunchDataBits = reader.ReadSerializedInt(512 * 8);

                        Log.Information($"PacketId = {0}, control = {control}, open = {open}, " +
                            $"close = {close}, dormant = {dormant}, " +
                            $"isReplicationPaused = {isReplicationPaused}, reliable = {reliable}, " +
                            $"chIndex = {chIndex}, hasPackageMapExports = {hasPackageMapExports}, " +
                            $"hasMustBeMappedGUIDs = {hasMustBeMappedGUIDs}, partial = {partial}, " +
                            $"chsequence = {chsequence}, partialInitial = {partialInitial}, partialFinal = {partialFinal}, " +
                            $"chType = {chType}, bunchDataBits = {bunchDataBits}");
                    }
                }

                if (!skipAck)
                {
                    Log.Information($"ack packet");
                }
            }
        }

        static void updatesecret()
        {
            if(ActiveSecret==255)
            {
                handshakesecret[0] = new byte[64];
                handshakesecret[1] = new byte[64];

                _rnd.NextBytes(handshakesecret[1]);

                ActiveSecret = 0;
            }
            else
            {
                ActiveSecret = Convert.ToByte(!Convert.ToBoolean(ActiveSecret));
            }

            _rnd.NextBytes(handshakesecret[ActiveSecret]);
        }

        static void PrintBits(BitArray bits, bool isClient = true)
        {
            StringBuilder builder = new StringBuilder("bit[] { ");

            for (int index = 0; index < bits.Length; index++)
            {
                builder.Append($"{(bits[index] ? 1 : 0)}, ");
            }

            builder.Remove(builder.Length - 2, 1);
            builder.Append("};");

            Log.Information($"Is Client = {isClient} | {builder.ToString()}");
        }

        static void PrintBytes(IEnumerable<byte> bytes, bool isClient = true)
        {
            StringBuilder builder = new StringBuilder("byte[] { ");

            foreach (byte value in bytes)
            {
                builder.Append($"0x{value:X2}, ");
            }

            builder.Remove(builder.Length - 2, 1);
            builder.Append("};");

            Log.Information($"Is Client = {isClient} | {builder.ToString()}");
        }
    }
}
