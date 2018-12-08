using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading;

namespace Hitai.Math
{
    public static class PrimeGenerator
    {
        public static RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

        /// <summary>
        /// First 400 primes
        /// </summary>
        public static readonly int[] Primes = new[] {
            2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83,
            89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179,
            181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271,
            277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379,
            383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479,
            487, 491, 499, 503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599,
            601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691, 701,
            709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823,
            827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941,
            947, 953, 967, 971, 977, 983, 991, 997, 1009, 1013, 1019, 1021, 1031, 1033, 1039, 1049,
            1051, 1061, 1063, 1069, 1087, 1091, 1093, 1097, 1103, 1109, 1117, 1123, 1129, 1151,
            1153, 1163, 1171, 1181, 1187, 1193, 1201, 1213, 1217, 1223, 1229, 1231, 1237, 1249,
            1259, 1277, 1279, 1283, 1289, 1291, 1297, 1301, 1303, 1307, 1319, 1321, 1327, 1361,
            1367, 1373, 1381, 1399, 1409, 1423, 1427, 1429, 1433, 1439, 1447, 1451, 1453, 1459,
            1471, 1481, 1483, 1487, 1489, 1493, 1499, 1511, 1523, 1531, 1543, 1549, 1553, 1559,
            1567, 1571, 1579, 1583, 1597, 1601, 1607, 1609, 1613, 1619, 1621, 1627, 1637, 1657,
            1663, 1667, 1669, 1693, 1697, 1699, 1709, 1721, 1723, 1733, 1741, 1747, 1753, 1759,
            1777, 1783, 1787, 1789, 1801, 1811, 1823, 1831, 1847, 1861, 1867, 1871, 1873, 1877,
            1879, 1889, 1901, 1907, 1913, 1931, 1933, 1949, 1951, 1973, 1979, 1987, 1993, 1997,
            1999, 2003, 2011, 2017, 2027, 2029, 2039, 2053, 2063, 2069, 2081, 2083, 2087, 2089,
            2099, 2111, 2113, 2129, 2131, 2137, 2141, 2143, 2153, 2161, 2179, 2203, 2207, 2213,
            2221, 2237, 2239, 2243, 2251, 2267, 2269, 2273, 2281, 2287, 2293, 2297, 2309, 2311,
            2333, 2339, 2341, 2347, 2351, 2357, 2371, 2377, 2381, 2383, 2389, 2393, 2399, 2411,
            2417, 2423, 2437, 2441, 2447, 2459, 2467, 2473, 2477, 2503, 2521, 2531, 2539, 2543,
            2549, 2551, 2557, 2579, 2591, 2593, 2609, 2617, 2621, 2633, 2647, 2657, 2659, 2663,
            2671, 2677, 2683, 2687, 2689, 2693, 2699, 2707, 2711, 2713, 2719, 2729, 2731, 2741
        };

        public static List<int> SieveOfEratosthenes(int n) {
            var sito = new BitArray(n + 1, true);
            sito.Set(0, false);
            sito.Set(1, false);
            for (var i = 2; i < sito.Length; i++) {
                if (!sito.Get(i)) continue;
                for (var j = 2 * i; j < sito.Length; j += i) {
                    sito.Set(j, false);
                }
            }

            var list = new List<int>();
            for (var i = 0; i < sito.Length; i++) {
                if (sito.Get(i)) list.Add(i);
            }

            return list;
        }

        /// <summary>
        /// Checks whether <paramref name="n"/> is a prime with absolute certainty. The complexity is O(sqrt(n)).
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsPrime(BigInteger n) {
            if ((n & 1) == 0)
                return n == 2;
            for (BigInteger i = 3; (i * i) <= n; i += 2)
                if ((n % i) == 0)
                    return false;
            return n != 1;
        }

        /// <summary>
        /// Generates an n-bit integer, which has a very high probability of being a prime
        /// </summary>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static BigInteger GetProbablePrime(int bits) {
            /*
            if (primes == null) {
                primes = EratostenovoSito(2741);
                Console.WriteLine(primes.Count + " first primes loaded.");
            }
            */
            BigInteger candidate = RandomOddBigInteger(bits);
            while (true) {
                candidate += 2;
                if (candidate.IsEven) continue;
                if (Primes.Any(x => candidate % x == 0)) continue;
                if (!FermatTest(candidate, 100)) continue;
                if (MillerRabinTest(candidate, 7)) return candidate;
            }
        }

        /// <summary>
        /// Runs <see cref="GetProbablePrime(int)"/> in multiple threads and returns the first two results aquired
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="n">The number of threads to run, defaults to <see cref="Environment.ProcessorCount"/>, but has to be greater or equal to 2</param>
        /// <returns></returns>
        public static BigInteger[] GetTwoProbablePrimesParallel(int bits, int n = 0) {
            if (n == 0)
                n = Environment.ProcessorCount;
            if (n < 2)
                throw new ArgumentException();
            var results = new List<BigInteger>();
            var threads = new List<Thread>();
            var ts = new ThreadStart(() => results.Add(GetProbablePrime(bits)));
            for (int i = 0; i < n; i++)
                threads.Add(new Thread(ts));
            foreach (var thread in threads)
                thread.Start();
            while (true) {
                if (results.Count >= 2) {
                    foreach (var thread in threads)
                        thread.Abort();
                    break;
                }
            }

            return results.Distinct().Take(2).ToArray();
        }

        /// <summary>
        /// Executes <paramref name="k"/> rounds of the Fermat test on the candidate <paramref name="n"/>
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static bool FermatTest(BigInteger n, int k) {
            if (n <= 3) throw new ArgumentException();
            for (; k > 0; k--) {
                var a = RandomBigInteger(2, n - 2);
                if (BigInteger.ModPow(a, n - 1, n) != 1) return false;
            }

            return true;
        }

        /// <summary>
        /// Executes <paramref name="k"/> rounds of the Miller-Rabin test on the candidate <paramref name="n"/>
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static bool MillerRabinTest(BigInteger n, int k) {
            if (n <= 3) throw new ArgumentException();
            BigInteger d = n - 1;
            BigInteger r = 0;
            while (d.IsEven) {
                r++;
                d /= 2;
            }

            outerLoop:
            for (; k > 0; k--) {
                BigInteger a = RandomBigInteger(2, n - 2);
                var x = BigInteger.ModPow(a, d, n);
                if (x == 1 || x == -1) continue;
                for (BigInteger i = 0; i < r - 1; i++) {
                    x = BigInteger.ModPow(x, 2, n);
                    if (x == 1) return false;
                    if (x == n - 1) goto outerLoop;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Generates a random n-bit odd integer
        /// </summary>
        /// <param name="bits">n</param>
        /// <returns></returns>
        public static BigInteger RandomOddBigInteger(int bits) {
            var buffer = new byte[bits / 8];
            BigInteger result;
            random.GetNonZeroBytes(buffer);
            // zajistíme, aby byl výsledek nezáporný
            buffer[buffer.Length - 1] &= 0b0111_1111;
            // zajistíme, aby byl výsledek lichý
            buffer[0] |= 1;
            result = new BigInteger(buffer);
            return result;
        }

        public static BigInteger RandomBigInteger(BigInteger from, BigInteger to) {
            if (from > to) throw new ArgumentException();
            if (from == to) return to;
            var buffer = to.ToByteArray();
            BigInteger result;
            do {
                random.GetBytes(buffer);
                // zajistíme aby byl výsledek nezáporný
                buffer[buffer.Length - 1] &= 0b0111_1111;
                result = new BigInteger(buffer);
            } while (result > to && result < from);

            return result;
        }
        /// <summary>
        /// Nerekurzivní Euklidův algoritmus
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int GreatestCommonDivisor(int a, int b) {
            while (b != 0) {
                int tmp = a % b;
                a = b;
                b = tmp;
            }
            return a;
        }
    }
}
