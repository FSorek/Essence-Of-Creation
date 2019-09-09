using UnityEngine;

[System.Serializable]
public class GeneratedWave
{
    public float TimeBetweenSpawns;
    public int AmountToSpawn;
    public GameObject Model;
    public UnitData unitStats;
    private int waveStrength;
    private int wave;

    public GeneratedWave(int waveStrength, int wave)
    {
        this.waveStrength = waveStrength;
        this.wave = wave;
        Generate();
    }

    private void Generate()
    {
        TimeBetweenSpawns = 1;
        AmountToSpawn = 5;
    }
}