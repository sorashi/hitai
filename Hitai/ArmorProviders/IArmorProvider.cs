using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitai.ArmorProviders
{
    public interface IArmorProvider
    {
        (byte[] rawData, ArmorType armorType) FromArmor(byte[] armor);
        byte[] ToArmor(byte[] rawData, ArmorType armorType);
        ArmorType GetArmorType(byte[] armor);
    }
}
