using UnityEngine;
[CreateAssetMenu(fileName = "Wave Settings", menuName = "Essence/Game", order = 0)]
public class WaveSettings : ScriptableObject
{
    public float TimeToFirstWave;
    public float TimeBetweenWaves;
    public int[] WavesPowerPoints; // Random model selection for enemies
    public int WaveCount => WavesPowerPoints.Length;
    public GameObject[] ModelPool;
}