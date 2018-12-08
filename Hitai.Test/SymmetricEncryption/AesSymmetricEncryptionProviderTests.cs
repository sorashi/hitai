using System;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hitai.SymmetricEncryption.Tests
{
    [TestFixture]
    public class AesSymmetricEncryptionProviderTests
    {
        [SetUp]
        public void Setup() {
            aes = new AesSymmetricEncryptionProvider();
        }

        private AesSymmetricEncryptionProvider aes;

        [Test]
        public void GenerateKeyPasswordBasedTest() {
            byte[] oldKey = aes.Key;
            aes.GenerateKeyFromPassword("this_is_some_password");
            CollectionAssert.AreNotEqual(oldKey, aes.Key);
            oldKey = aes.Key;
            byte[] salt = aes.Salt;
            aes.GenerateKeyFromPassword("this_is_some_password", salt);
            CollectionAssert.AreEqual(oldKey, aes.Key);
            CollectionAssert.AreEqual(salt, aes.Salt);
        }

        [Test]
        public void GenerateKeyTest() {
            byte[] oldKey = aes.Key;
            aes.GenerateKey();
            CollectionAssert.AreNotEqual(oldKey, aes.Key);
        }

        [Test]
        public void NewIVTest() {
            byte[] oldIv = aes.IV;
            aes.NewIV();
            CollectionAssert.AreNotEqual(oldIv, aes.IV);
        }

        [Test]
        public async Task TransformAsyncTest() {
            var cleartext = "Lorem Ipsum Dolor Sit Amet 2018 Consequer";
            var password = "this is the password";
            aes.GenerateKeyFromPassword(password);
            var salt = new byte[aes.Salt.Length];
            var iv = new byte[aes.IV.Length];
            Array.Copy(aes.Salt, salt, salt.Length);
            Array.Copy(aes.IV, iv, iv.Length);
            byte[] cipher = await aes.TransformAsync(Encoding.ASCII.GetBytes(cleartext));
            Console.WriteLine(Convert.ToBase64String(cipher));
            aes = new AesSymmetricEncryptionProvider();
            aes.GenerateKeyFromPassword(password, salt);
            aes.IV = iv;
            string result = Encoding.ASCII.GetString(await aes.TransformAsync(cipher, false));
            Assert.AreEqual(cleartext, result);
        }
    }
}
