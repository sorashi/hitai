using System;
using System.Linq;
using Hitai.Models;
using Hitai.SymmetricEncryption;
using MessagePack;

namespace Hitai.AsymmetricEncryption
{
    /// <summary>
    ///     Provides shortcut to encryption and decryption using RSA with AES. The <see cref="IAsymmetricEncryptionProvider" />
    ///     from <see cref="KeyPair.RsaProvider" /> is used, or a default of <see cref="HitaiAsymmetricEncryptionProvider" />
    ///     is used.
    /// </summary>
    public class AsymmetricEncryptionController
    {
        public static Type[] ProviderList = {
            typeof(SystemAsymmetricEncryptionProvider),
            typeof(HitaiAsymmetricEncryptionProvider)
        };

        /// <summary>
        ///     Encrypts <paramref name="data" /> for <paramref name="recipient" />. <paramref name="recipient" /> can be a public
        ///     key.
        /// </summary>
        /// <param name="recipient"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Message Encrypt(KeyPair recipient, byte[] data) {
            IAsymmetricEncryptionProvider provider = GetRsaProvider(recipient.RsaProvider);
            provider.SetKeyPair(recipient.ToPublic());
            var aes = new AesSymmetricEncryptionProvider();
            byte[] cipher = aes.TransformAsync(data).Result;
            byte[] encryptedKey = provider.Encrypt(aes.Key);
            var m = new Message {
                Content = cipher,
                EncryptedKey = encryptedKey,
                Iv = aes.Iv,
                RecipientId = recipient.ShortId
            };
            return m;
        }

        private static IAsymmetricEncryptionProvider GetRsaProvider(int providerIndex) {
            if (providerIndex < 0 || providerIndex > ProviderList.Length - 1)
                return new HitaiAsymmetricEncryptionProvider();
            return (IAsymmetricEncryptionProvider) Activator.CreateInstance(
                ProviderList[providerIndex]);
        }

        public static byte[] Decrypt(Message m, string password, KeyPair kp) {
            if (m.RecipientId != kp.ShortId)
                throw new InvalidOperationException("Keypair does not match the recipient");
            IAsymmetricEncryptionProvider provider = GetRsaProvider(kp.RsaProvider);
            provider.SetKeyPair(kp, password);
            byte[] key = provider.Decrypt(m.EncryptedKey);
            var aes = new AesSymmetricEncryptionProvider {Key = key, Iv = m.Iv};
            return aes.TransformAsync(m.Content, false).Result;
        }

        public static byte[] Decrypt(Message m, string password, Keychain kc) {
            KeyPair key = kc.Keys.FirstOrDefault(x => x.ShortId == m.RecipientId);
            if (key == null) throw new Exception("Recipient's key is absent from keychain");
            return Decrypt(m, password, key);
        }

        public static Signature Sign(byte[] data, string password, KeyPair kp) {
            IAsymmetricEncryptionProvider provider = GetRsaProvider(kp.RsaProvider);
            provider.SetKeyPair(kp, password);
            var signature = new Signature {Data = new byte[data.Length]};
            Array.Copy(data, signature.Data, data.Length);
            // todo include salt
            signature.SignatureData = provider.SignData(signature.Data);
            signature.AuthorId = kp.ShortId;
            return signature;
        }

        public static Signature Sign(Message m, string password, KeyPair kp) {
            return Sign(LZ4MessagePackSerializer.Serialize(m), password, kp);
        }

        public static bool Verify(Signature s, KeyPair kp) {
            IAsymmetricEncryptionProvider provider = GetRsaProvider(kp.RsaProvider);
            provider.SetKeyPair(kp.ToPublic());
            return provider.VerifyData(s.Data, s.SignatureData);
        }

        public static bool Verify(Signature s, Keychain kc) {
            KeyPair key = kc.Keys.FirstOrDefault(x => x.ShortId == s.AuthorId);
            if (key == null) throw new Exception("Recipient's key is absent from keychain");
            return Verify(s, key);
        }

        // todo make everything asynchronous
    }
}
