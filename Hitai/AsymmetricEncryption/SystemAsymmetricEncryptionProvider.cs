using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Hitai.AsymmetricEncryption
{
    public class SystemAsymmetricEncryptionProvider : IAsymmetricEncryptionProvider
    {
        private RSACryptoServiceProvider Rsa = new RSACryptoServiceProvider();
        public bool PublicOnly => Rsa.PublicOnly;

        public byte[] Encrypt(byte[] data) {
            return Rsa.Encrypt(data, true);
        }

        public byte[] Decrypt(byte[] data) {
            return Rsa.Decrypt(data, true);
        }

        /// <summary>
        ///     Computes the SHA256 hash of <paramref name="data" /> and signs it.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] SignData(byte[] data) {
            return Rsa.SignData(data, SHA256.Create());
        }

        /// <summary>
        ///     Verifies SHA256 of <paramref name="data" />
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool VerifyData(byte[] data, byte[] signature) {
            return Rsa.VerifyData(data, SHA256.Create(), signature);
        }

        public KeyPair GetPrivateKey(string password) {
            if (Rsa.PublicOnly)
                throw new InvalidOperationException("The keypair contains only public information");
            return RsaParametersToKeyPairAsync(Rsa.ExportParameters(true), password).Result;
        }

        public KeyPair GetPublicKey() {
            return RsaParametersToKeyPairAsync(Rsa.ExportParameters(false)).Result;
        }

        public void SetKeyPair(KeyPair kp, string password = null) {
            if (kp.IsPrivate && password == null)
                throw new ArgumentException("Missing password");
            Rsa.ImportParameters(new RSAParameters {
                D = password == null ? null : kp.GetPrivateExponentAsync(password).Result,
                Exponent = kp.Exponent.ToByteArray(),
                Modulus = kp.Modulus.ToByteArray()
            });
        }

        public void EnsurePositiveModulus() {
            while (new BigInteger(Rsa.ExportParameters(true).Modulus).Sign != 1 ||
                   new BigInteger(Rsa.ExportParameters(true).D).Sign != 1)
                Rsa = new RSACryptoServiceProvider();
        }

        /// <summary>
        ///     Just signs the <paramref name="hash" />.
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public byte[] SignHash(byte[] hash) {
            return Rsa.SignHash(hash, CryptoConfig.MapNameToOID("SHA256"));
        }

        /// <summary>
        ///     Verifies the SHA256 <paramref name="hash" />
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public bool VerifyHash(byte[] hash, byte[] signature) {
            return Rsa.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA256"), signature);
        }

        private async Task<KeyPair> RsaParametersToKeyPairAsync(RSAParameters parameters,
            string password = null) {
            var kp = new KeyPair();
            kp.Modulus = new BigInteger(parameters.Modulus);
            kp.Exponent = new BigInteger(parameters.Exponent);
            if (parameters.D != null && password == null)
                throw new ArgumentException("Missing password");
            if (parameters.D != null)
                await kp.SetPrivateExponentAsync(parameters.D, password);
            return kp;
        }


        //public class KeyPair
        //{
        //    public BigInteger Modulus { get; set; }
        //    public BigInteger Exponent { get; set; }
        //    public BigInteger? PrivateExponent { get; set; }
        //    public static KeyPair FromRsaParameters(RSAParameters parameters)
        //    {
        //        var kp = new KeyPair();
        //        if(parameters.D != null)
        //            kp.PrivateExponent = new BigInteger(parameters.D);
        //        kp.Modulus = new BigInteger(parameters.Modulus);
        //        kp.Exponent = new BigInteger(parameters.Exponent);
        //        return kp;
        //    }
        //    public bool IsPrivate => PrivateExponent != null;
        //}
    }
}
