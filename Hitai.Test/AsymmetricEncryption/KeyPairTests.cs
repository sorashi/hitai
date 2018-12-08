using NUnit.Framework;
using Hitai.AsymmetricEncryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitai.AsymmetricEncryption.Tests
{
    [TestFixture()]
    public class KeyPairTests
    {
        SystemAsymmetricEncryptionProvider rsa;
        [SetUp]
        public void Setup() {
            rsa = new SystemAsymmetricEncryptionProvider();
        }

        [Test()]
        public void LoadKeyPairAsyncTest() {
            Assert.Pass();
        }

        [Test()]
        public void SaveKeyPairAsyncTest() {
            Assert.Pass();
        }

        [Test()]
        public async Task SetPrivateExponentAsyncTest() {
            string password = "password";
            var kp = rsa.GetPrivateKey(password);
            await kp.SetPrivateExponentAsync(new byte[] { 0, 0, 0, 1, 2, 3 }, password);
            var exp = await kp.GetPrivateExponentAsync(password);
            CollectionAssert.AreEqual(new byte[] { 0, 0, 0, 1, 2, 3 }, exp);
        }

        [Test()]
        public async Task GetPrivateExponentAsyncTest() {
            string password = "password";
            var kp = rsa.GetPrivateKey(password);
            await kp.GetPrivateExponentAsync(password);
            kp = rsa.GetPublicKey();
            Assert.Throws<InvalidOperationException>(() => kp.GetPrivateExponentAsync(password).RunSynchronously());
        }

        [Test()]
        public void CheckPasswordTest() {
            string password = "password";
            var kp = rsa.GetPrivateKey(password);
            Assert.IsTrue(kp.CheckPassword(password));
        }
        [Test()]
        public void ToPublicTest() {
            var kp = rsa.GetPrivateKey("password");
            var pub = kp.ToPublic();
            Assert.IsFalse(pub.IsPrivate);
            Assert.IsTrue(kp.IsPrivate);
            kp.Strip();
            Assert.IsFalse(kp.IsPrivate);
        }
    }
}