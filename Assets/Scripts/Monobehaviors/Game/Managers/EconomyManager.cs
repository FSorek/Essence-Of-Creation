using System;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static IEconomyManager Instance;
    public EconomySettings EconomySettings;

    private void Awake()
    {
        Instance = new EconomyManagerController(EconomySettings, WaveManager.Instance);
    }
}
