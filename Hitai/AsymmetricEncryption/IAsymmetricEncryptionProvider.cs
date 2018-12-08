namespace Hitai.AsymmetricEncryption
{
    internal interface IAsymmetricEncryptionProvider
    {
        byte[] Encrypt(byte[] data);
        byte[] Decrypt(byte[] data);

        /// <summary>
        ///     Computes SHA256 from <paramref name="data" /> and signs it.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] SignData(byte[] data);

        /// <summary>
        ///     Computes SHA256 from <paramref name="data" /> and verifies it against <paramref name="signature" />.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        bool VerifyData(byte[] data, byte[] signature);

        KeyPair GetPrivateKey(string password);
        KeyPair GetPublicKey();
        void SetKeyPair(KeyPair kp, string password = null);
    }
}
