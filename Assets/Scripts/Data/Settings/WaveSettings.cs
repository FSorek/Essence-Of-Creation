using UnityEngine;

[System.Serializable]
public class WaveSettings
{
    public float TimeToFirstWave;
    public float TimeBetweenWaves;
    public waveGenerationParams[] Waves;
    public UnitData[] waveUnits { get; private set; }
    public void GenerateWaves()
    {
        waveUnits = new UnitData[Waves.Length];
        for (int i = 0; i < Waves.Length; i++)
        {
            waveUnits[i] = UnitStatsGenerator.GenerateUnitArchetype(Waves[i].PowerPoints, i);
        }
    }

    [System.Serializable]
    public struct waveGenerationParams
    {
        public int PowerPoints;
        public GameObject Model;
        public float TimeBetweenSpawns;
        public int AmountToSpawn;
    }
}
