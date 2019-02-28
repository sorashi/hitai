using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading;
using Hitai.Math;

namespace Hitai.AsymmetricEncryption
{
    public class HitaiAsymmetricEncryptionProvider : IAsymmetricEncryptionProvider
    {
        private static readonly int[] Exponents =
            {(1 << 1) | 1, (1 << 2) | 1, (1 << 4) | 1, (1 << 8) | 1, (1 << 16) | 1};

        private BigInteger? PrivateExponent { get; set; }
        private BigInteger Modulus { get; set; }
        private BigInteger Exponent { get; set; }

        public byte[] Decrypt(byte[] data) {
            if (!PrivateExponent.HasValue)
                throw new InvalidOperationException("Private key is needed for this operation");
            return BigInteger.ModPow(new BigInteger(data), PrivateExponent.Value, Modulus)
                .ToByteArray();
        }

        public byte[] Encrypt(byte[] data) {
            return BigInteger.ModPow(new BigInteger(data), Exponent, Modulus).ToByteArray();
        }

        public Keypair GetPrivateKey(string password) {
            var kp = new Keypair {Exponent = Exponent, Modulus = Modulus};
            if (PrivateExponent == null) throw new InvalidOperationException();
            kp.SetPrivateExponentAsync(PrivateExponent.Value.ToByteArray(), password).Wait();
            return kp;
        }

        public Keypair GetPublicKey() {
            var kp = new Keypair {Exponent = Exponent, Modulus = Modulus};
            return kp;
        }

        public void SetKeyPair(Keypair kp, string password = null) {
            Exponent = kp.Exponent;
            Modulus = kp.Modulus;
            if (kp.IsPrivate && password == null) throw new ArgumentException("Je potřeba heslo");
            if (kp.IsPrivate)
                PrivateExponent = new BigInteger(kp.GetPrivateExponentAsync(password).Result);
        }

        public byte[] SignData(byte[] data) {
            SHA256 sha = SHA256.Create();
            byte[] hash = sha.ComputeHash(data);
            return Decrypt(hash);
        }

        public bool VerifyData(byte[] data, byte[] signature) {
            SHA256 sha = SHA256.Create();
            byte[] hash = sha.ComputeHash(data);
            return Encrypt(signature).SequenceEqual(hash);
        }

        public void GenerateNewKeypair(CancellationToken ct = default) {
            BigInteger[] primes = PrimeGenerator.GetTwoProbablePrimesParallel(512, ct: ct);
            if(primes == null || primes.Length <= 0) throw new OperationCanceledException();
            BigInteger p = primes[0];
            BigInteger q = primes[1];
            BigInteger phiN = (p - 1) * (q - 1);
            int e = -1;
            foreach (int candidate in Exponents)
                if (BigInteger.GreatestCommonDivisor(phiN, candidate) == 1) {
                    e = candidate;
                    break;
                }

            if (e == -1) throw new CryptographicException("Co-prime exponent was not found.");
            var k = 1;
            while (true) {
                if ((1 + k * phiN) % e == 0) break;
                k++;
            }

            BigInteger d = (1 + k * phiN) / e;
            PrivateExponent = d;
            Modulus = p * q;
            Exponent = e;
        }
    }
}
