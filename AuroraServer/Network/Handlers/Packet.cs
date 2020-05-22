using System;
using System.Collections.Generic;
using System.Text;

namespace AuroraServer.Network.Handlers
{
    struct ProcessedPacket
    {
        public byte[] Data;

        public int Size;

        public bool Error;

        public ProcessedPacket(byte[] data = null, int size = 0, bool error = false)
        {
            Data = data;
            Size = size;
            Error = error;
        }
    }

    class Packet
    {
        public float Time;
    }
}
