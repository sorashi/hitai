using System;
using System.Security.Cryptography;
using Hitai.SymmetricEncryption;
using NUnit.Framework;

namespace Hitai.AsymmetricEncryption.Tests
{
    [TestFixture]
    public class SystemAsymmetricEncryptionProviderTests
    {
        private readonly Random _random = new Random();
        private SystemAsymmetricEncryptionProvider _rsa;
        private SystemAsymmetricEncryptionProvider _rsaPublic;

        [OneTimeSetUp]
        public void Setup() {
            _rsa = new SystemAsymmetricEncryptionProvider();
            _rsaPublic = new SystemAsymmetricEncryptionProvider();
            _rsaPublic.SetKeyPair(_rsa.GetPublicKey());
            Assert.IsFalse(_rsa.PublicOnly);
            Assert.IsTrue(_rsaPublic.PublicOnly);
        }

        [Test]
        public void EncryptDecryptTest() {
            byte[] data = new AesSymmetricEncryptionProvider().Key;
            Assert.IsNotNull(data);
            byte[] cipher = _rsaPublic.Encrypt(data);
            Console.WriteLine(Convert.ToBase64String(cipher));
            byte[] cleartext = _rsa.Decrypt(cipher);
            CollectionAssert.AreEqual(data, cleartext);
        }

        [Test]
        public void GetPrivateKeyTest() {
            Assert.Throws<InvalidOperationException>(() => _rsaPublic.GetPrivateKey("password"));
            KeyPair key = _rsa.GetPrivateKey("password");
            Assert.IsNotNull(key.PrivateExponent);
        }

        [Test]
        public void GetPublicKeyTest() {
            KeyPair key = _rsa.GetPublicKey();
            KeyPair key2 = _rsaPublic.GetPublicKey();
            Assert.AreEqual(key.Modulus, key2.Modulus);
            Assert.AreEqual(key.Exponent, key2.Exponent);
            Assert.IsNull(key.PrivateExponent);
            Assert.AreEqual(key.PrivateExponent, key2.PrivateExponent);
        }

        [Test]
        public void SignVerifyDataTest() {
            var data = new byte[1024 * 1024];
            _random.NextBytes(data);
            byte[] signature = _rsa.SignData(data);
            Console.WriteLine($"signature: {Convert.ToBase64String(signature)}");
            Assert.IsTrue(_rsaPublic.VerifyData(data, signature));
        }

        [Test]
        public void SignVerifyHashTest() {
            var data = new byte[1024 * 1024];
            _random.NextBytes(data);
            byte[] hash = SHA256.Create().ComputeHash(data);
            byte[] signature = _rsa.SignHash(hash);
            Console.WriteLine($"hash: {Convert.ToBase64String(hash)}");
            Console.WriteLine($"signature: {Convert.ToBase64String(signature)}");
            Assert.IsTrue(_rsaPublic.VerifyHash(hash, signature));
        }
    }
}
