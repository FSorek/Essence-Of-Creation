using System;
using UnityEngine;
public static class DamageSystem
{
    public static int GetDamageToArmor(this Damage damage, ArmorType armorType)
    {
        int final = 0;

        switch (armorType)
        {
            default:
                final = damage.Water + damage.Air + damage.Earth + damage.Fire;
                break;
            case ArmorType.Armoured:
                final = damage.Earth * 2 +
                        Mathf.FloorToInt(damage.Water * .75f) +
                        damage.Fire +
                        Mathf.FloorToInt(damage.Air * .5f);
                break;
            case ArmorType.Carapace:
                final = Mathf.FloorToInt(damage.Earth * .75f) +
                        damage.Water * 2 +
                        Mathf.FloorToInt(damage.Fire * .5f) +
                        damage.Air;
                break;
            case ArmorType.Ethereal:
                final = Mathf.FloorToInt(damage.Earth * .5f) +
                        damage.Water +
                        damage.Fire * 2 +
                        Mathf.FloorToInt(damage.Air * .75f);
                break;
            case ArmorType.Flesh:
                final = damage.Earth +
                        Mathf.FloorToInt(damage.Water * .5f) +
                        Mathf.FloorToInt(damage.Fire * .75f) +
                        damage.Air * 2;
                break;
            case ArmorType.Elemental:
                final = Mathf.FloorToInt(damage.Earth * 1.2f) +
                        Mathf.FloorToInt(damage.Water * 1.2f) +
                        Mathf.FloorToInt(damage.Fire * 1.2f) +
                        Mathf.FloorToInt(damage.Air * 1.2f);
                break;
        }

        return final;
    }
}