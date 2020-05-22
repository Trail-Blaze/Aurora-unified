using AuroraServer.UEngine.IO;
using System;
using System.Collections;

using static AuroraServer.Program;

namespace AuroraServer.UEngine.IO
{
    class FBitReader : FArchive
    {
        #region Field Region

        public BitArray _bits;

        #endregion

        #region Property Region

        public int Position { get; protected set; }

        public int Length { get; private set; }

        #endregion

        #region Constructor Region

        public FBitReader(byte[] bytes)
        {
            _bits = new BitArray(bytes);

            Length = _bits.Length;
        }

        public FBitReader(byte[] bytes, int count)
        {
            _bits = new BitArray(bytes);

            Length = count;
        }

        public FBitReader(bool[] bytes)
        {
            _bits = new BitArray(bytes);

            Length = _bits.Length;
        }

        public FBitReader(bool[] bytes, int count)
        {
            _bits = new BitArray(bytes);

            Length = count;
        }

        #endregion

        #region Method Region

        public uint SerializeInt(uint max)
        {
            uint value = 0;

            if (!IsError)
            {
                int position = Position;

                for (uint mask = 1; (value + mask) < max; mask *= 2, position++)
                {
                    if (position >= _bits.Length)
                    {
                        SetOverflowed(position - Position);
                        break;
                    }

                    if (_bits[position])
                        value |= mask;
                }

                Position = position;
            }

            return value;
        }

        public uint ReadInt(uint max) => SerializeInt(max);

        public bool ReadBit()
        {
            if (!IsError)
            {
                if (Position >= _bits.Length)
                    SetOverflowed(1);
                else
                    return _bits[Position++];
            }

            return false;
        }

        public int GetBitsLeft() => _bits.Length - Position;

        public override bool AtEnd() => Position >= Length || Position >= _bits.Length;

        public void SetOverflowed(int length)
        {
            Log.Error($"FBitReader.SetOverflowed() called. " +
                $"(Length = {length}, Remaining = {GetBitsLeft()}, Max: {_bits.Length})");

            IsError = true;
        }

        #endregion
    }
}
