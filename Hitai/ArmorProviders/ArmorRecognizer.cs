using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitai.ArmorProviders
{
    public class ArmorRecognizer
    {
        public static Type RecognizeArmor(byte[] data) {
            var s = Encoding.ASCII.GetString(data);
            if (s.TrimStart().StartsWith("BEGIN HITAI")) return typeof(HitaiArmorProvider);
            return typeof(RawDataArmorProvider);
            // po rozpoznání typu armoru můžeme vytvořit instanci pomocí Activator.CreateInstance(Type)
        }
    }
}
