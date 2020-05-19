using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AuroraServer.IO
{
    class BitWriter
    {
        #region Field Region

        BitArray _array;

        #endregion

        #region Property Region

        public int Position { get; private set; }

        #endregion

        #region Constructor Region

        public BitWriter(int capacity = 0)
        {
            _array = new BitArray(capacity);
        }

        #endregion

        #region Method Region

        public void Write(bool value) => _array[Position++] = value;

        // TODO (Cyuubi): This is kinda messy, but I'm lazy.
        public void Write(byte value)
        {
            var array = new BitArray(new byte[] { value });

            foreach (var _value in array.Cast<bool>())
                Write(_value);
        }

        // TODO (Cyuubi): Support <T> eventually.
        public void Write(IEnumerable<byte> array)
        {
            foreach (var value in array)
                Write(value);
        }

        public byte[] ToArray()
        {
            byte[] array = new byte[(Position + 7) >> 3];

            var byteIndex = 0;
            var bitIndex = 0;

            for (int index = 0; index < Position; index++)
            {
                if (_array[index])
                    array[byteIndex] |= (byte)(1 << bitIndex);

                bitIndex++;

                if (bitIndex >= 8)
                {
                    byteIndex++;

                    bitIndex = 0;
                }
            }

            return array;
        }

        #endregion
    }
}
