using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Hitai.AsymmetricEncryption
{
    public class SystemAsymmetricEncryptionProvider : IAsymmetricEncryptionProvider
    {
        private RSACryptoServiceProvider _rsa = new RSACryptoServiceProvider();
        public bool PublicOnly => _rsa.PublicOnly;

        public byte[] Encrypt(byte[] data) {
            return _rsa.Encrypt(data, true);
        }

        public byte[] Decrypt(byte[] data) {
            return _rsa.Decrypt(data, true);
        }

        /// <summary>
        ///     Computes the SHA256 hash of <paramref name="data" /> and signs it.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] SignData(byte[] data) {
            return _rsa.SignData(data, SHA256.Create());
        }

        /// <summary>
        ///     Verifies SHA256 of <paramref name="data" />
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool VerifyData(byte[] data, byte[] signature) {
            return _rsa.VerifyData(data, SHA256.Create(), signature);
        }

        public Keypair GetPrivateKey(string password) {
            if (_rsa.PublicOnly)
                throw new InvalidOperationException("The keypair contains only public information");
            return RsaParametersToKeyPairAsync(_rsa.ExportParameters(true), password).Result;
        }

        public Keypair GetPublicKey() {
            return RsaParametersToKeyPairAsync(_rsa.ExportParameters(false)).Result;
        }

        public void SetKeyPair(Keypair kp, string password = null) {
            if (kp.IsPrivate && password == null)
                throw new ArgumentException("Missing password");
            _rsa.ImportParameters(new RSAParameters {
                D = password == null ? null : kp.GetPrivateExponentAsync(password).Result,
                Exponent = kp.Exponent.ToByteArray(),
                Modulus = kp.Modulus.ToByteArray(),
                // TODO: find a better way, see Keypair#SystemAsymmetricEncryptionParameters
                P = kp.P,
                Q = kp.Q,
                DP = kp.DP,
                DQ = kp.DQ,
                InverseQ = kp.InverseQ
            });
        }

        public void EnsurePositiveModulus() {
            while (new BigInteger(_rsa.ExportParameters(true).Modulus).Sign != 1 ||
                   new BigInteger(_rsa.ExportParameters(true).D).Sign != 1)
                _rsa = new RSACryptoServiceProvider();
        }

        /// <summary>
        ///     Just signs the <paramref name="hash" />.
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public byte[] SignHash(byte[] hash) {
            return _rsa.SignHash(hash, CryptoConfig.MapNameToOID("SHA256"));
        }

        /// <summary>
        ///     Verifies the SHA256 <paramref name="hash" />
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public bool VerifyHash(byte[] hash, byte[] signature) {
            return _rsa.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA256"), signature);
        }

        private async Task<Keypair> RsaParametersToKeyPairAsync(RSAParameters parameters,
            string password = null) {
            var kp = new Keypair {
                Modulus = new BigInteger(parameters.Modulus),
                Exponent = new BigInteger(parameters.Exponent)
            };
            if (parameters.D != null && password == null)
                throw new ArgumentException("Missing password");
            if (parameters.D != null)
                await kp.SetPrivateExponentAsync(parameters.D, password);
            // TODO: find a better way, see Keypair#SystemAsymmetricEncryptionParameters
            kp.P = parameters.P;
            kp.Q = parameters.Q;
            kp.DP = parameters.DP;
            kp.DQ = parameters.DQ;
            kp.InverseQ = parameters.InverseQ;
            return kp;
        }


        //public class Keypair
        //{
        //    public BigInteger Modulus { get; set; }
        //    public BigInteger Exponent { get; set; }
        //    public BigInteger? PrivateExponent { get; set; }
        //    public static Keypair FromRsaParameters(RSAParameters parameters)
        //    {
        //        var kp = new Keypair();
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
