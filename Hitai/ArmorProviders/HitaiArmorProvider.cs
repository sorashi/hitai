using Hitai.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hitai.ArmorProviders
{
    public class HitaiArmorProvider : IArmorProvider
    {
        private readonly List<(string identifer, ArmorType type)> _armorTypeIdentifiers = new List<(string identifer, ArmorType type)>(){
            ("MESSAGE", ArmorType.Message),
            ("SIGNATURE", ArmorType.Signature),
            ("SIGNED MESSAGE", ArmorType.SignedMessage),
            ("DETACHED SIGNED MESSAGE", ArmorType.DetachedSignedMessage),
            ("PUBLIC KEY", ArmorType.PublicKey),
            ("PRIVATE KEY", ArmorType.PrivateKey)
        };

        private readonly Encoding encoding = Encoding.ASCII;

        public (byte[] rawData, ArmorType armorType) FromArmor(byte[] armor) {
            var armorString = encoding.GetString(armor).Trim();
            var armorType = GetArmorType(armor);
            var parts = armorString.Split('.');
            if (parts.Length > 3) throw new FormatException("Armor is in a wrong format");
            var base62 = parts[1].Replace(" ", "");
            var converter = new Base62Converter();
            var data = converter.FromBase62String(base62);
            return (data, armorType);
        }

        public ArmorType GetArmorType(byte[] armor) {
            var armorString = encoding.GetString(armor);
            var typeIdentifier = armorString.Split('.').First().Trim();
            var armorType = _armorTypeIdentifiers.FirstOrDefault(x => "BEGIN HITAI " + x.identifer == typeIdentifier);
            if (armorType.Equals(default)) throw new Exception("Armor type could not be determined.");
            return armorType.type;
        }
        public byte[] ToArmor(byte[] rawData, ArmorType armorType) {
            var converter = new Base62Converter();
            var base62 = converter.ToBase62String(rawData);
            var identifierPair = _armorTypeIdentifiers.Where(x => x.type == armorType).FirstOrDefault();
            if (identifierPair.Equals(default)) throw new Exception();
            var identifier = identifierPair.identifer;
            var sb = new StringBuilder();
            sb.Append($"BEGIN HITAI {identifier}.");
            const int chunkSize = 15;
            for (int i = 0; i < base62.Length; i += chunkSize) {
                sb.Append(" ");
                sb.Append(base62.Substring(i, System.Math.Min(chunkSize, base62.Length - i)));
            }
            sb.Append($". END HITAI {identifier}");
            return encoding.GetBytes(sb.ToString());
        }
    }
}