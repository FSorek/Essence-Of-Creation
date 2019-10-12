using UnityEngine;

[CreateAssetMenu(fileName = "Economy Settings", menuName = "Essence/Game/Economy Settings", order = 1)]
public class EconomySettings : ScriptableObject
{
    public int EssencePerWave = 100;
    public int EssencePerSummon = 50;
    public int EssencePerBuildspot = 100;
    public int StartingEssence = 100;
}
