using System;
using UnityEngine;
public class DamageManager : MonoBehaviour
{
    public static DamageManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            throw new Exception("More than one Damage Manager in the instance!");
    }

    public int GetFinalDamageValues(Damage damage, ArmorType type)
    {
        int final = 0;

        switch (type)
        {
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
                final = Mathf.FloorToInt(damage.Earth * .5f) + 
                        Mathf.FloorToInt(damage.Water * .5f) + 
                        Mathf.FloorToInt(damage.Fire * .5f) +
                        Mathf.FloorToInt(damage.Air * .5f);
                break;
        }


        return final;
    }
}