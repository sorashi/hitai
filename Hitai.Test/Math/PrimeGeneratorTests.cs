using System.Collections.Generic;
using System.Numerics;
using Hitai.Math;
using NUnit.Framework;

namespace Hitai.Test.Math
{
    [TestFixture]
    public class PrimeGeneratorTests
    {
        [Test]
        [Timeout(4 * 60 * 1000)]
        public void GetProbablePrimeTest() {
            BigInteger res = PrimeGenerator.GetProbablePrime(64);
            Assert.Pass(res.ToString());
        }

        [Test]
        [Timeout(4 * 60 * 1000)]
        public void GetTwoProbablePrimesParallelTest() {
            BigInteger[] res = PrimeGenerator.GetTwoProbablePrimesParallel(64);
            Assert.AreEqual(2, res.Length);
            Assert.IsFalse(res[0] == res[1]);
            Assert.Pass(string.Join(", ", res));
        }

        [Test]
        public void RandomOddIntegerTest() {
            for (var i = 0; i < 5; i++) {
                BigInteger bi = PrimeGenerator.RandomOddBigInteger(512);
                Assert.IsTrue(!bi.IsEven);
            }
        }

        [Test]
        public void SieveOfEratosthenesTest() {
            List<int> primes = PrimeGenerator.SieveOfEratosthenes(2741);
            Assert.AreEqual(400, primes.Count);
            CollectionAssert.AreEqual(PrimeGenerator.Primes, primes);
        }
    }
}
