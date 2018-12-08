using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hitai.AsymmetricEncryption.Tests
{
    [TestFixture]
    public class KeyPairTests
    {
        [SetUp]
        public void Setup() {
            _rsa = new SystemAsymmetricEncryptionProvider();
        }

        private SystemAsymmetricEncryptionProvider _rsa;

        [Test]
        public void CheckPasswordTest() {
            var password = "password";
            KeyPair kp = _rsa.GetPrivateKey(password);
            Assert.IsTrue(kp.CheckPassword(password));
        }

        [Test]
        public async Task GetPrivateExponentAsyncTest() {
            var password = "password";
            KeyPair kp = _rsa.GetPrivateKey(password);
            await kp.GetPrivateExponentAsync(password);
            kp = _rsa.GetPublicKey();
            Assert.Throws<InvalidOperationException>(() =>
                kp.GetPrivateExponentAsync(password).RunSynchronously());
        }

        [Test]
        public void LoadKeyPairAsyncTest() {
            Assert.Pass();
        }

        [Test]
        public void SaveKeyPairAsyncTest() {
            Assert.Pass();
        }

        [Test]
        public async Task SetPrivateExponentAsyncTest() {
            var password = "password";
            KeyPair kp = _rsa.GetPrivateKey(password);
            await kp.SetPrivateExponentAsync(new byte[] {0, 0, 0, 1, 2, 3}, password);
            byte[] exp = await kp.GetPrivateExponentAsync(password);
            CollectionAssert.AreEqual(new byte[] {0, 0, 0, 1, 2, 3}, exp);
        }

        [Test]
        public void ToPublicTest() {
            KeyPair kp = _rsa.GetPrivateKey("password");
            KeyPair pub = kp.ToPublic();
            Assert.IsFalse(pub.IsPrivate);
            Assert.IsTrue(kp.IsPrivate);
            kp.Strip();
            Assert.IsFalse(kp.IsPrivate);
        }
    }
}
