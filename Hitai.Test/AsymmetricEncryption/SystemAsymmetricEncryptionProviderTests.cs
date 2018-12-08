using NUnit.Framework;
using Hitai.AsymmetricEncryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hitai.SymmetricEncryption;
using System.Security.Cryptography;

namespace Hitai.AsymmetricEncryption.Tests
{
    [TestFixture()]
    public class SystemAsymmetricEncryptionProviderTests
    {
        Random random = new Random();
        SystemAsymmetricEncryptionProvider rsa;
        SystemAsymmetricEncryptionProvider rsaPublic;
        [OneTimeSetUp]
        public void Setup()
        {
            rsa = new SystemAsymmetricEncryptionProvider();
            rsaPublic = new SystemAsymmetricEncryptionProvider();
            rsaPublic.SetKeyPair(rsa.GetPublicKey());
            Assert.IsFalse(rsa.PublicOnly);
            Assert.IsTrue(rsaPublic.PublicOnly);
        }
        [Test()]
        public void EncryptDecryptTest()
        {
            byte[] data = (new AesSymmetricEncryptionProvider()).Key;
            Assert.IsNotNull(data);
            byte[] cipher = rsaPublic.Encrypt(data);
            Console.WriteLine(Convert.ToBase64String(cipher));
            byte[] cleartext = rsa.Decrypt(cipher);
            CollectionAssert.AreEqual(data, cleartext);
        }

        [Test()]
        public void SignVerifyDataTest()
        {
            byte[] data = new byte[1024 * 1024];
            random.NextBytes(data);
            byte[] signature = rsa.SignData(data);
            Console.WriteLine($"signature: {Convert.ToBase64String(signature)}");
            Assert.IsTrue(rsaPublic.VerifyData(data, signature));
        }

        [Test()]
        public void SignVerifyHashTest()
        {
            byte[] data = new byte[1024 * 1024];
            random.NextBytes(data);
            byte[] hash = SHA256.Create().ComputeHash(data);
            byte[] signature = rsa.SignHash(hash);
            Console.WriteLine($"hash: {Convert.ToBase64String(hash)}");
            Console.WriteLine($"signature: {Convert.ToBase64String(signature)}");
            Assert.IsTrue(rsaPublic.VerifyHash(hash, signature));
        }

        [Test()]
        public void GetPrivateKeyTest()
        {
            Assert.Throws<InvalidOperationException>(() => rsaPublic.GetPrivateKey("password"));
            var key = rsa.GetPrivateKey("password");
            Assert.IsNotNull(key.PrivateExponent);
        }

        [Test()]
        public void GetPublicKeyTest()
        {
            var key = rsa.GetPublicKey();
            var key2 = rsaPublic.GetPublicKey();
            Assert.AreEqual(key.Modulus, key2.Modulus);
            Assert.AreEqual(key.Exponent, key2.Exponent);
            Assert.IsNull(key.PrivateExponent);
            Assert.AreEqual(key.PrivateExponent, key2.PrivateExponent);
        }
    }
}