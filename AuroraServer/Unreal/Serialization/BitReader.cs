using System;
using System.Collections.Generic;
using System.Text;

namespace AuroraServer.Unreal.Serialization
{
    class FBitReader : FArchive
    {
        private byte[] _buffer;

        private long _count, _position;

        public FBitReader(byte[] source, long count)
        {
            _buffer = new byte[(count + 7) >> 3];

            IsLoading = IsPersistent = true;

            _count = count;
            _position = 0;

            if (source != null)
            {
                Array.Copy(source, _buffer, (count + 7) >> 3); // I think this is correct? Dunno.

                if ((_count & 7) != 0)
                    _buffer[_count >> 3] &= GetMask((int)(_count & 7));
            }
        }

        // Alternative for GMask
        private byte GetMask(int index)
        {
            return (byte)(index != 0 ? (2 << index - 1) - 1 : 0);
        }
    }
}
