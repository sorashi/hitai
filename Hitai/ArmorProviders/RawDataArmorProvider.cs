using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitai.ArmorProviders
{
    class RawDataArmorProvider : IArmorProvider
    {

        public ArmorType GetArmorType(byte[] armor) {
            throw new NotImplementedException();
        }

        public byte[] ToArmor(byte[] rawData, ArmorType armorType) {
            throw new NotImplementedException();
        }

        (byte[] rawData, ArmorType armorType) IArmorProvider.FromArmor(byte[] armor) {
            throw new NotImplementedException();
        }
    }
}
