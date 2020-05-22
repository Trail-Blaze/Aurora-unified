using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AuroraServer.IO
{
    class FBitWriter
    {
        #region Field Region

        BitArray _bits;

        #endregion

        #region Property Region

        public int Position { get; private set; }

        #endregion

        #region Constructor Region

        public BitWriter(int capacity = 0)
        {
            _bits = new BitArray(capacity);
        }

        #endregion

        #region Method Region

        public void Write(bool value) => _bits[Position++] = value;

        // TODO (Cyuubi): This is kinda messy, but I'm lazy.
        public void Write(byte value)
        {
            var bits = new BitArray(new byte[] { value });

            foreach (var _value in bits.Cast<bool>())
                Write(_value);
        }

        public void Write(float value) => Write(BitConverter.GetBytes(value));

        // TODO (Cyuubi): Support <T> eventually.
        public void Write(IEnumerable<byte> bytes)
        {
            foreach (var value in bytes)
                Write(value);
        }

        public byte[] ToBytes()
        {
            byte[] bytes = new byte[(Position + 7) >> 3];

            var byteIndex = 0;
            var bitIndex = 0;

            for (int index = 0; index < Position; index++)
            {
                if (_bits[index])
                    bytes[byteIndex] |= (byte)(1 << bitIndex);

                bitIndex++;

                if (bitIndex >= 8)
                {
                    byteIndex++;

                    bitIndex = 0;
                }
            }

            return bytes;
        }

        #endregion
    }
}
