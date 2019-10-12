using UnityEngine;
using UnityEngine.Experimental.VFX;

[CreateAssetMenu(fileName = "Player VFX Data", menuName = "Essence/Player/VFX Data", order = 1)]
public class PlayerVFXData : ScriptableObject
{
    [SerializeField] private GameObject summonFire;
    [SerializeField] private GameObject summonAir;
    [SerializeField] private GameObject summonEarth;
    [SerializeField] private GameObject summonWater;

    public GameObject SummonFire => summonFire;
    public GameObject SummonAir => summonAir;
    public GameObject SummonEarth => summonEarth;
    public GameObject SummonWater => summonWater;
}