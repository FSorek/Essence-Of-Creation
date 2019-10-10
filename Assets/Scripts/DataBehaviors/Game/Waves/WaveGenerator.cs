using System;
using UnityEngine;
using Random = UnityEngine.Random;

internal class WaveGenerator : IWaveGenerator
{
    private Func<Stat>[] rollFunctions;
    private int seed;

    public WaveGenerator(int seed)
    {
        this.seed = seed;
        Random.InitState(seed);
    }

    public UnitData Generate(int powerPoints)
    {
        UnitData uData = LoadBaseStats();
        int[] pointDistribution = new int[6];
        uData.Type = RollArmorType();
        for (int i = powerPoints; i > 0; i--)
        {
            pointDistribution[Random.Range(0, pointDistribution.Length)]++;
        }

        uData.Health += RollHealth(pointDistribution[0]);
        uData.MoveSpeed += RollMoveSpeed(pointDistribution[1]);
        uData.HealthRegen += RollHealthRegen(pointDistribution[2]);
        uData.ArmorLayers += RollArmorLayers(pointDistribution[3]);
        uData.CrystallineLayers += RollCrystallineLayers(pointDistribution[4]);
        //uData.Abilities = RollAbilities(pointDistribution[5]);

        return uData;
    }

    private int RollHealth(int v)
    {
        return v * 10;
    }

    private float RollMoveSpeed(int v)
    {
        return v * .05f;
    }

    private int RollHealthRegen(int v) // wasting some PP rolling
    {
        return Mathf.RoundToInt(v * .1f);
    }

    private ArmorType RollArmorType()
    {
        var roll = Random.Range(0f, 100f);
        if (roll <= 22.5f)
            return ArmorType.Armoured;
        else if (roll > 22.5 && roll <= 45f)
            return ArmorType.Carapace;
        else if (roll > 45f && roll <= 67.5f)
            return ArmorType.Ethereal;
        else if (roll > 67.5f && roll <= 90)
            return ArmorType.Flesh;
        else return ArmorType.Elemental;
    }

    private int RollArmorLayers(int v) // wasting some PP rolling
    {
        return Mathf.FloorToInt(v / 2f);
    }

    private int RollCrystallineLayers(int v) // wasting some PP rolling
    {
        return Mathf.FloorToInt(v / 4f);
    }

    //private UnitAbility[] RollAbilities(int v)
    //{
    //    return new UnitAbility[0];
    //}

    private UnitData LoadBaseStats()
    {
        UnitData uData = new UnitData();

        uData.Health = 20;
        uData.MoveSpeed = 4f;
        uData.HealthRegen = 0;
        uData.Type = ArmorType.None;
        uData.ArmorLayers = 0;
        uData.CrystallineLayers = 0;
        //uData.Abilities = null;

        return uData;
    }
}
