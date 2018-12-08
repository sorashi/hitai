using System;
using System.IO;
using Hitai.IO;
using NUnit.Framework;

namespace Hitai.Test.IO
{
    [TestFixture]
    public class BitStreamTests
    {
#pragma warning disable CS0612 // Type or member is obsolete
        private BitStream dummyBitStream;

        [SetUp]
        public void Setup() {
            dummyBitStream = new BitStream(1);
        }

        [Test]
        public void ReadTest() {
            BitStream bs;
            Assert.Throws<ArgumentNullException>(() => {
                bs = new BitStream(new byte[] {1});
                bs.Read(null, 0, 1);
            });
            var testSource = new byte[] {
                0b0000_0001,
                0b1010_1010,
                0b0111_1111,
                0b1111_1111
            };
            bs = new BitStream(testSource);
            var buffer = new byte[1];
            bs.Seek(8, SeekOrigin.Begin);
            bs.Read(buffer, 0, 6);
            Assert.AreEqual(0b0010_1010, buffer[0]);
        }

        [Test]
        public void WriteTest() {
            BitStream bs;
            Assert.Throws<ArgumentNullException>(() => {
                bs = new BitStream(new byte[] {1});
                bs.Write(null, 0, 1);
            });
            bs = new BitStream(10);
            Assert.AreEqual(10, bs.Length);
            bs.Write(new byte[] {0}, 0, 1);
            Assert.AreEqual(1, bs.Position);
            bs.Write(new byte[] {
                0b1111_1111, 0b1111_1111
            }, 0, 9);
            // zapisujeme přes kapacitu
            Assert.Throws<NotSupportedException>(() => { bs.Write(new byte[] {1}, 0, 1); });
            CollectionAssert.AreEqual(new byte[] {
                0b1111_1110, 0b0000_0011
                //        ^ první zapsaný bit byl 0, zbytek byly bity 1
            }, bs.ToByteArray());
        }

        [Test]
        public void CanReadWriteSeekTest() {
            Assert.IsTrue(dummyBitStream.CanRead);
            Assert.IsTrue(dummyBitStream.CanWrite);
            Assert.IsTrue(dummyBitStream.CanSeek);
        }

        [Test]
        public void NotSupportedTest() {
            Assert.Throws<NotSupportedException>(() => dummyBitStream.Flush());
            Assert.Throws<NotSupportedException>(() => dummyBitStream.SetLength(1));
        }

        [Test]
        public void SeekTest() {
            var bs = new BitStream(new byte[] {
                0x1, 0x5, 0x10, 0x7f, 0xbb, 0xff
            });
            Assert.AreEqual(bs.Length - 5, bs.Seek(-5, SeekOrigin.End));
            Assert.AreEqual(bs.Length - 5, bs.Position);
            Assert.AreEqual(bs.Length - 4, bs.Seek(1, SeekOrigin.Current));
            Assert.AreEqual(bs.Length - 4, bs.Position);
            Assert.AreEqual(1, bs.Seek(1, SeekOrigin.Begin));
            Assert.AreEqual(1, bs.Position);
            Assert.Throws<ArgumentException>(() => bs.Seek(0, (SeekOrigin) 5));
        }
    }
}
