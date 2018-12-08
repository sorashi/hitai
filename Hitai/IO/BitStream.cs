using System;
using System.IO;

namespace Hitai.IO
{
    /// <summary>
    ///     Stream pro čtení a zapisování jednotlivých bitů
    /// </summary>
    [Obsolete]
    public class BitStream : Stream
    {
        private readonly byte[] _source;

        /// <summary>
        ///     Pozice bitu (0-7) v aktuálním bajtu
        /// </summary>
        private long _bitPosition;

        /// <summary>
        ///     Pozice v <see cref="_source" />
        /// </summary>
        private long _bytePosition;

        public BitStream(byte[] source) {
            _source = source;
            Length = source.Length * 8;
        }

        public BitStream(long length) {
            Length = length;
            _source = new byte[length / 8 + (length % 8 > 0 ? 1 : 0)];
        }

        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => true;

        /// <summary>
        ///     Délka streamu v bitech
        /// </summary>
        public override long Length { get; }

        /// <summary>
        ///     Pozice streamu v bitech
        /// </summary>
        public override long Position {
            get => _bytePosition * 8 + _bitPosition;
            set {
                _bytePosition = value / 8;
                _bitPosition = value % 8;
            }
        }

        public override void Flush() {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     Čte bity ze streamu do bufferu
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset">Offset v bitech</param>
        /// <param name="count">Počet bitů k přečtení</param>
        /// <returns></returns>
        public override int Read(byte[] buffer, int offset, int count) {
            if (buffer == null) throw Error.BufferNull();
            if (offset + count > buffer.Length * 8) throw Error.BufferOverflow();
            if (offset < 0 || count < 0) throw Error.OffsetOrCountNegative();
            long toRead = System.Math.Min(Length - Position, count);
            WriteBitsFromTo(_source, buffer, Position, offset, toRead);
            Position += toRead;
            return (int) toRead;
        }

        public override long Seek(long offset, SeekOrigin origin) {
            switch (origin) {
                case SeekOrigin.Current:
                    Position += offset;
                    break;
                case SeekOrigin.Begin:
                    Position = offset;
                    break;
                case SeekOrigin.End:
                    Position = Length + offset;
                    break;
                default:
                    throw new ArgumentException("Invalid SeekOrigin");
            }

            return Position;
        }

        public override void SetLength(long value) {
            throw new NotSupportedException();
        }

        public byte[] ToByteArray() {
            return (byte[]) _source.Clone();
        }

        public override void Write(byte[] buffer, int offset, int count) {
            if (buffer == null) throw Error.BufferNull();
            if (offset + count > buffer.Length * 8) throw Error.BufferOverflow();
            if (offset < 0 || count < 0) throw Error.OffsetOrCountNegative();
            if (Position + count > Length) throw Error.BitStreamNotExpandable();
            WriteBitsFromTo(buffer, _source, offset, Position, count);
            Position += count;
        }

        private void WriteBitsFromTo(byte[] from, byte[] to, long offsetFrom, long offsetTo,
            long count) {
            if (from == null || to == null) throw Error.BufferNull();
            if (offsetFrom + count > from.Length * 8 || offsetTo + count > to.Length * 8)
                throw new ArgumentException();
            if (offsetFrom < 0 || offsetTo < 0 || count < 0) throw Error.OffsetOrCountNegative();
            for (var c = 0; c < count; c++) {
                long fromByte = (offsetFrom + c) / 8;
                var fromBit = (int) ((offsetFrom + c) % 8);
                long toByte = (offsetTo + c) / 8;
                var toBit = (int) ((offsetTo + c) % 8);
                // pokud čteme bit číslo 2 v bajtu, čteme tento bit:
                // 0000 0000
                //       ^  (indexujeme od 0 a čteme od LSB)
                int bit = from[fromByte] & (1 << fromBit);
                bool isSet = bit != 0;
                if (isSet)
                    to[toByte] |= (byte) (1 << toBit);
                else
                    to[toByte] &= (byte) ~(1 << toBit);
            }
        }

        private static class Error
        {
            public static Exception BitStreamNotExpandable() {
                return new NotSupportedException("BitStream's internal buffer cannot be expanded.");
            }

            public static Exception BufferNull() {
                return new ArgumentNullException("buffer");
            }

            public static Exception BufferOverflow() {
                return new ArgumentException(
                    "The sum of offset and count is larger than the buffer length.");
            }

            public static Exception OffsetOrCountNegative() {
                return new ArgumentOutOfRangeException("offset or count");
            }
        }
    }
}
