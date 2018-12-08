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
            _aes = new AesSymmetricEncryptionProvider();
        }

        private AesSymmetricEncryptionProvider _aes;

        [Test]
        public void GenerateKeyPasswordBasedTest() {
            byte[] oldKey = _aes.Key;
            _aes.GenerateKeyFromPassword("this_is_some_password");
            CollectionAssert.AreNotEqual(oldKey, _aes.Key);
            oldKey = _aes.Key;
            byte[] salt = _aes.Salt;
            _aes.GenerateKeyFromPassword("this_is_some_password", salt);
            CollectionAssert.AreEqual(oldKey, _aes.Key);
            CollectionAssert.AreEqual(salt, _aes.Salt);
        }

        [Test]
        public void GenerateKeyTest() {
            byte[] oldKey = _aes.Key;
            _aes.GenerateKey();
            CollectionAssert.AreNotEqual(oldKey, _aes.Key);
        }

        [Test]
        public void NewIvTest() {
            byte[] oldIv = _aes.Iv;
            _aes.NewIv();
            CollectionAssert.AreNotEqual(oldIv, _aes.Iv);
        }

        [Test]
        public async Task TransformAsyncTest() {
            var cleartext = "Lorem Ipsum Dolor Sit Amet 2018 Consequer";
            var password = "this is the password";
            _aes.GenerateKeyFromPassword(password);
            var salt = new byte[_aes.Salt.Length];
            var iv = new byte[_aes.Iv.Length];
            Array.Copy(_aes.Salt, salt, salt.Length);
            Array.Copy(_aes.Iv, iv, iv.Length);
            byte[] cipher = await _aes.TransformAsync(Encoding.ASCII.GetBytes(cleartext));
            Console.WriteLine(Convert.ToBase64String(cipher));
            _aes = new AesSymmetricEncryptionProvider();
            _aes.GenerateKeyFromPassword(password, salt);
            _aes.Iv = iv;
            string result = Encoding.ASCII.GetString(await _aes.TransformAsync(cipher, false));
            Assert.AreEqual(cleartext, result);
        }
    }
}
