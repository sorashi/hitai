using NUnit.Framework;
using Hitai.SymmetricEncryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitai.SymmetricEncryption.Tests
{
    [TestFixture()]
    public class AesSymmetricEncryptionProviderTests
    {
        AesSymmetricEncryptionProvider aes;
        [SetUp]
        public void Setup()
        {
            aes = new AesSymmetricEncryptionProvider();
        }

        [Test()]
        public void NewIVTest()
        {
            var oldIv = aes.IV;
            aes.NewIV();
            CollectionAssert.AreNotEqual(oldIv, aes.IV);
        }

        [Test()]
        public void GenerateKeyTest()
        {
            var oldKey = aes.Key;
            aes.GenerateKey();
            CollectionAssert.AreNotEqual(oldKey, aes.Key);
        }

        [Test()]
        public void GenerateKeyPasswordBasedTest()
        {
            var oldKey = aes.Key;
            aes.GenerateKeyFromPassword("this_is_some_password");
            CollectionAssert.AreNotEqual(oldKey, aes.Key);
            oldKey = aes.Key;
            var salt = aes.Salt;
            aes.GenerateKeyFromPassword("this_is_some_password", salt);
            CollectionAssert.AreEqual(oldKey, aes.Key);
            CollectionAssert.AreEqual(salt, aes.Salt);
        }

        [Test()]
        public async Task TransformAsyncTest()
        {
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
            var result = Encoding.ASCII.GetString(await aes.TransformAsync(cipher, false));
            Assert.AreEqual(cleartext, result);
        }
    }
}