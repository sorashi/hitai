using System;
using System.Text;
using Hitai.ArmorProviders;
using Hitai.IO;
using NUnit.Framework;

namespace Hitai.Test.ArmorProviders
{
    [TestFixture]
    public class HitaiArmorProviderTests
    {
        [Test]
        public void Base62Test() {
            // string test
            var converter = new Base62Converter();
            var testString = "Lorem ipsum dolor sit amet.";
            byte[] testStringBytes = Encoding.ASCII.GetBytes(testString);
            string testStringBase62 = converter.ToBase62String(testStringBytes);
            byte[] finalBytes = converter.FromBase62String(testStringBase62);
            string finalString = Encoding.ASCII.GetString(finalBytes);
            Assert.AreEqual(testString, finalString);

            // byte test
            var data = new byte[] {
                0, 0, 0, 1, 2, 3, 255, 128, 0, 4, 0
            };
            string dataBase62String = converter.ToBase62String(data);
            byte[] finalData = converter.FromBase62String(dataBase62String);
            CollectionAssert.AreEqual(data, finalData);
        }

        [Test]
        public void ToArmorTest() {
            var data = new byte[] {
                0, 0, 0, 130, 118, 137, 93, 125, 137, 193, 141, 199, 202, 160, 93, 135, 5, 179, 7,
                218, 169, 154, 141, 154, 37, 67, 212, 66, 201, 66, 236, 161, 154, 245, 67, 1,
                102, 248, 69, 125, 188, 194, 166, 27, 233, 157, 16, 55, 42, 59, 87, 236, 39,
                181, 195, 189, 252, 53, 92, 51, 180, 26, 179, 71, 212, 236, 44, 212, 92, 92,
                33, 13, 109, 138, 30, 230, 244, 113, 176, 135, 50, 156, 24, 142, 234, 0, 121,
                229, 50, 133, 4, 48, 155, 18, 200, 161, 15, 94, 71, 252, 113, 60, 219
            };
            var provider = new HitaiArmorProvider();
            string armor = Encoding.ASCII.GetString(provider.ToArmor(data, ArmorType.Message));
            Console.WriteLine(armor);
            Assert.AreEqual(
                "BEGIN HITAI MESSAGE. 0002F2fXmsq8CpV aRuVivsz0fYVMGs qIt1v0vNCICU477 " +
                "O5AitDZUi26JTxH Sq7HkrifMRSzayc gPUkHUuHnPSRxd4 aEsetD6gHZMIGM0 YybKTwNUbaZrPSr " +
                "JaIYLGfU0zSrcBV lSV. END HITAI MESSAGE", armor);
            (byte[] rawData, ArmorType armorType) =
                provider.FromArmor(Encoding.ASCII.GetBytes(armor));
            Assert.AreEqual(ArmorType.Message, armorType);
            CollectionAssert.AreEqual(data, rawData);
        }
    }
}
