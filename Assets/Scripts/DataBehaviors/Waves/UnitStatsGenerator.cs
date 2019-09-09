using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class UnitStatsGenerator 
{
    public static UnitData GenerateUnitArchetype(int powerPoints, int wave)
    {
        wave++;

        var unitData = new UnitData();
        var pointsLeft = powerPoints;
        unitData.Health = new Stat(10 * wave);
        unitData.MoveSpeed = new Stat(2);
        unitData.HealthRegen = new Stat(0);
        unitData.ArmorLayers = new Stat(0);

        int currentRoll;
        int previousPrimaryAtt = -1;

        unitData.Type = GetRandomArmorType();
        if (wave <= 7 && unitData.Type == ArmorType.Elemental)
            unitData.Type = ArmorType.Flesh;

        for (int i = 0; i<2; i++)
        {
            int primaryAttributeRoll = Random.Range(1, 4);
            while (primaryAttributeRoll == previousPrimaryAtt)
            {
                primaryAttributeRoll = Random.Range(1, 4);
            }
            switch (primaryAttributeRoll)
            {
                case 1:
                    currentRoll = Random.Range(0, pointsLeft / 3) + Random.Range(0, pointsLeft / 3);
                    pointsLeft -= currentRoll;
                    unitData.Health.BaseValue += 2 * currentRoll;
                    break;
                case 2:
                    currentRoll = Random.Range(0, pointsLeft / 3) + Random.Range(0, pointsLeft / 3);
                    pointsLeft -= currentRoll;
                    unitData.MoveSpeed.BaseValue += Mathf.Lerp(0f, 5f, (float)currentRoll/(float)(pointsLeft));
                    break;
                case 3:
                    if (unitData.Type != ArmorType.Elemental)
                    {
                        currentRoll = Random.Range(0, pointsLeft / 3) + Random.Range(0, pointsLeft / 3);
                        pointsLeft -= Mathf.Min(currentRoll / 10, wave);
                        unitData.HealthRegen.BaseValue += Mathf.Min(currentRoll / 10, wave);
                    }

                    break;
                case 4:
                    currentRoll = Mathf.Min(Random.Range(0, pointsLeft / 3) + Random.Range(0, pointsLeft / 3), wave);
                    pointsLeft -= currentRoll;
                    unitData.ArmorLayers.BaseValue += currentRoll;
                    break;
                case 5:
                    break;
            }

            previousPrimaryAtt = primaryAttributeRoll;
        }

        if (unitData.MoveSpeed.BaseValue < 5f)
        {
            currentRoll = Random.Range(0, pointsLeft / 3) + Random.Range(0, pointsLeft / 3);
            pointsLeft -= Mathf.Min(currentRoll, 7 - Mathf.RoundToInt(unitData.MoveSpeed.BaseValue));
            unitData.MoveSpeed.BaseValue += Mathf.Min(Mathf.Lerp(0f, 5f, (float)currentRoll / (float)(pointsLeft)), 7 - Mathf.RoundToInt(unitData.MoveSpeed.BaseValue));
        }

        if (unitData.Type != ArmorType.Elemental)
        {
            currentRoll = Random.Range(0, pointsLeft / 4) + Random.Range(0, pointsLeft / 4);
            pointsLeft -= Mathf.Min(currentRoll / 10, wave);
            unitData.HealthRegen.BaseValue += Mathf.Min(currentRoll / 10, wave);
        }

        currentRoll = Random.Range(0, pointsLeft / 3) + Random.Range(0, pointsLeft / 3);
        pointsLeft -= Mathf.Min(currentRoll / 5, wave);
        unitData.ArmorLayers.BaseValue += Mathf.Min(currentRoll / 5, wave);

        currentRoll = Random.Range(0, pointsLeft / 2) + Random.Range(0, pointsLeft / 2);
        pointsLeft -= currentRoll;
        unitData.Health.BaseValue += 2 * currentRoll;

        switch (unitData.Type)
        {
            case ArmorType.Armoured:
                unitData.ArmorLayers.BaseValue += 2 * Mathf.Ceil((float)wave/10f);
                unitData.MoveSpeed.BaseValue *= 0.7f;
                break;
            case ArmorType.Carapace:
                unitData.ArmorLayers.BaseValue += Mathf.Ceil((float)wave / 10f);
                unitData.MoveSpeed.BaseValue *= 0.85f;
                break;
            case ArmorType.Ethereal:
                unitData.MoveSpeed.BaseValue *= 1.2f;
                unitData.HealthRegen.BaseValue *= 0.8f;
                break;
            case ArmorType.Flesh:
                unitData.MoveSpeed.BaseValue *= 1.2f;
                unitData.HealthRegen.BaseValue *= 0.8f;
                break;
            case ArmorType.Elemental:
                unitData.Health.BaseValue *= .8f;
                break;
        }

        //unitData.CrystallineLayers = GetCrystallineLayer(pointsLeft, powerPoints);
        if (pointsLeft > 0)
            unitData.Health.BaseValue += 2 * pointsLeft;

       // Debug.Log("_____________UNIT GENERATED_____________");
       // Debug.Log("Health           : " + unitData.Health.BaseValue);
       // Debug.Log("MoveSpeed        : " + unitData.MoveSpeed.BaseValue);
       // Debug.Log("HealthRegen      : " + unitData.HealthRegen.BaseValue);
       // Debug.Log("ArmorLayers      : " + unitData.ArmorLayers.BaseValue);
       //
       // Debug.Log("Type             : " + unitData.Type);
       // Debug.Log("________________________________________");
       // //Debug.Log("Health: " + unitData.Health.BaseValue);

        return unitData;
    }

    private static Stat GetCrystallineLayer(int powerPointsAvailable, int powerPointsOriginal)
    {
        var roll = Random.Range(0f, 100f);
        if(roll <= 60)
            return new Stat(0);
        if((float)powerPointsAvailable / (float)powerPointsOriginal <= .1f)
            return new Stat(1);
        return new Stat();
    }

    private static ArmorType GetRandomArmorType()
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
}
