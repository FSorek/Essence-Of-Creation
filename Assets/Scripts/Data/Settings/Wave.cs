using UnityEngine;

[System.Serializable]
public class Wave
{
    public int PowerPoints = 10;
    public GameObject Model;
    public float TimeBetweenSpawns = 1;
    public int AmountToSpawn = 5;
    public UnitData UnitStats { get; private set; }

    public void GenerateUnitData(int waveNumber)
    {
        UnitStats = UnitStatsGenerator.GenerateUnitArchetype(PowerPoints, waveNumber);
    }
}
