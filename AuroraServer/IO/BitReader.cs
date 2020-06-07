using System;
using System.Collections;
using System.Collections.Generic;

namespace AuroraServer.IO
{
    class BitReader
    {
        #region Field Region

        public int Position;

        #endregion

        #region Property Region

        public BitArray Bits { get; private set; }

        #endregion

        #region Constructor Region

        public BitReader(bool[] values)
        {
            Bits = new BitArray(values);
        }

        public BitReader(byte[] bytes)
        {
            Bits = new BitArray(bytes);
        }

        #endregion

        #region Method Region

        public bool ReadBit() => Bits[Position++];

        public byte ReadByte()
        {
            byte result = 0;

            for (int index = 0; index < 8; index++)
            {
                if (ReadBit())
                    result |= (byte)(1 << index);
            }

            return result;
        }

        public IEnumerable<byte> ReadBytes(int count)
        {
            byte[] result = new byte[count];

            for (int index = 0; index < count; index++)
                result[index] = ReadByte();

            return result;
        }

        public short ReadInt16() => BitConverter.ToInt16((byte[])ReadBytes(2));

        public int ReadInt32() => BitConverter.ToInt32((byte[])ReadBytes(4));

        public float ReadSingle() => BitConverter.ToSingle((byte[])ReadBytes(4));

        public long ReadInt64() => BitConverter.ToInt64((byte[])ReadBytes(8));

        #endregion
    }
}
