using UnityEngine;
[CreateAssetMenu(fileName = "Wave Settings", menuName = "Essence/Game/Wave Settings", order = 0)]
public class WaveSettings : ScriptableObject
{
    public float TimeToFirstWave;
    public float TimeBetweenWaves = 20;
    public int EnemiesPerWave = 60;
    public int[] WavesPowerPoints; // Random model selection for enemies
    public int WaveCount => WavesPowerPoints.Length;
    public GameObject[] ModelPool;
}