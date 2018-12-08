using Hitai.Math;
using NUnit.Framework;

namespace Hitai.Test.Math
{
    [TestFixture]
    public class PrimeGeneratorTests
    {
        [Test]
        public void RandomOddIntegerTest()
        {
            for (int i = 0; i < 5; i++) {
                var bi = PrimeGenerator.RandomOddBigInteger(512);
                Assert.IsTrue(!bi.IsEven);
            }
        }

        [Test, Timeout(4 * 60 * 1000)]
        public void GetTwoProbablePrimesParallelTest()
        {
            var res = PrimeGenerator.GetTwoProbablePrimesParallel(64);
            Assert.AreEqual(2, res.Length);
            Assert.IsFalse(res[0] == res[1]);
            Assert.Pass(string.Join(", ", res));
        }

        [Test, Timeout(4 * 60 * 1000)]
        public void GetProbablePrimeTest()
        {
            var res = PrimeGenerator.GetProbablePrime(64);
            Assert.Pass(res.ToString());
        }

        [Test]
        public void SieveOfEratosthenesTest()
        {
            var primes = PrimeGenerator.SieveOfEratosthenes(2741);
            Assert.AreEqual(400, primes.Count);
            CollectionAssert.AreEqual(PrimeGenerator.Primes, primes);
        }
    }
}