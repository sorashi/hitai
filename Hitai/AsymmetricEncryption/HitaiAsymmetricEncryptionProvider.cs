using Hitai.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hitai.AsymmetricEncryption
{
    public class HitaiAsymmetricEncryptionProvider : IAsymmetricEncryptionProvider
    {
        private static readonly int[] Exponents = { 1<<1|1, 1<<2|1, 1<<4|1, 1<<8|1, 1<<16|1 };
        private BigInteger? PrivateExponent { get; set; }
        private BigInteger Modulus { get; set; }
        private BigInteger Exponent { get; set; }
        public void GenerateNewKeypair() {
            var primes = PrimeGenerator.GetTwoProbablePrimesParallel(512);
            var p = primes[0];
            var q = primes[1];
            var phiN = (p - 1) * (q - 1);
            var e = -1;
            foreach (var candidate in Exponents) {
                if(BigInteger.GreatestCommonDivisor(phiN, candidate) == 1) {
                    e = candidate;
                    break;
                }
            }
            if (e == -1) throw new CryptographicException("Co-prime exponent was not found.");
            var k = 1;
            while(true) {
                if ((1 + k * phiN) % e == 0) break;
                k++;
            }
            var d = (1 + k * phiN) / e;
            PrivateExponent = d;
            Modulus = p * q;
            Exponent = e;
        }

        public byte[] Decrypt(byte[] data) {
            if (!PrivateExponent.HasValue) throw new InvalidOperationException("Private key is needed for this operation");
            return BigInteger.ModPow(new BigInteger(data), PrivateExponent.Value, Modulus).ToByteArray();
        }

        public byte[] Encrypt(byte[] data) {
            return BigInteger.ModPow(new BigInteger(data), Exponent, Modulus).ToByteArray();
        }

        public KeyPair GetPrivateKey(string password) {
            var kp = new KeyPair();
            kp.Exponent = Exponent;
            kp.Modulus = Modulus;
            if (PrivateExponent == null) throw new InvalidOperationException();
            kp.SetPrivateExponentAsync(PrivateExponent.Value.ToByteArray(), password).Wait();
            return kp;
        }

        public KeyPair GetPublicKey() {
            var kp = new KeyPair();
            kp.Exponent = Exponent;
            kp.Modulus = Modulus;
            return kp;
        }

        public void SetKeyPair(KeyPair kp, string password = null) {
            Exponent = kp.Exponent;
            Modulus = kp.Modulus;
            if (kp.IsPrivate && password == null) throw new ArgumentException("Je potreba heslo");
            if(kp.IsPrivate)
                PrivateExponent = new BigInteger(kp.GetPrivateExponentAsync(password).Result);
        }

        public byte[] SignData(byte[] data) {
            var sha = SHA256.Create();
            var hash = sha.ComputeHash(data);
            return Decrypt(hash);
        }

        public bool VerifyData(byte[] data, byte[] signature) {
            var sha = SHA256.Create();
            var hash = sha.ComputeHash(data);
            return Encrypt(signature).SequenceEqual(hash);
        }
    }
}
