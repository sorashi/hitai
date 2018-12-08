using System;
using System.Text;

namespace Hitai.ArmorProviders
{
    public class ArmorRecognizer
    {
        public static Type RecognizeArmor(byte[] data) {
            string s = Encoding.ASCII.GetString(data);
            return s.TrimStart().StartsWith("BEGIN HITAI")
                ? typeof(HitaiArmorProvider)
                : typeof(RawDataArmorProvider);
            // po rozpoznání typu armoru můžeme vytvořit instanci pomocí Activator.CreateInstance(Type)
        }
    }
}
