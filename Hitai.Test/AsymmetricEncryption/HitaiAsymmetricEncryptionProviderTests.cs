using System;
using System.Numerics;
using NUnit.Framework;

namespace Hitai.AsymmetricEncryption.Tests
{
    [TestFixture]
    public class HitaiAsymmetricEncryptionProviderTests
    {
        private readonly Random random = new Random();
        private HitaiAsymmetricEncryptionProvider rsa;
        private HitaiAsymmetricEncryptionProvider rsaPublic;
        private readonly BigInteger E = 3;

        private BigInteger D =
            BigInteger.Parse(
                "5633086042154212468602475019498853956714303554065992614538914857077893587793969931228710221472174386118401816369632280712921259195619998928486083177340789297357027233834022295198368492722158718870619592162986811261695079066370484154684961745813606717075866823388605388570498550896278441303898649902529516667");

        private readonly BigInteger N = BigInteger.Parse(
            "8449629063231318702903712529248280935071455331098988921808372285616840381690954896843065332208261579177602724554448421069381888793429998392729124766011191073521096574194414700424457938678784680074830802437327103950005249633718813765804170106063468311136930656378962222911687523944107148305556517064809230151");

        [OneTimeSetUp]
        public void Setup() {
            rsa = new HitaiAsymmetricEncryptionProvider();
            rsaPublic = new HitaiAsymmetricEncryptionProvider();
            var kp = new KeyPair {
                Exponent = E,
                Modulus = N
            };
            kp.SetPrivateExponentAsync(D.ToByteArray(), "password").Wait();
            rsa.SetKeyPair(kp, "password");
            rsaPublic.SetKeyPair(kp.ToPublic());
        }

        [Test]
        public void EncryptDecryptTest() {
            var data = new byte[64];
            random.NextBytes(data);
            byte[] result = rsaPublic.Encrypt(data);
            CollectionAssert.AreNotEqual(data, result);
            byte[] back = rsa.Decrypt(result);
            CollectionAssert.AreEqual(data, back);
            Assert.Throws<InvalidOperationException>(() => rsaPublic.Decrypt(result));
        }
    }
}
