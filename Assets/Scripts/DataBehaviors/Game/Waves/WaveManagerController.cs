using System.Collections.Generic;
using UnityEngine;

public class WaveManagerController : IWaveManager
{
    private List<UnitData> waves;
    private int currentWave;
    private readonly IWaveGenerator generator;
    private readonly WaveSettings settings;
    private List<ITakeDamage> enemiesAlive;
    private Stack<Transform> reachpoints;

    public WaveManagerController(int seed, WaveSettings settings)
    {
        generator = new WaveGenerator(seed);
        waves = new List<UnitData>();
        enemiesAlive = new List<ITakeDamage>();
        for (int i = 0; i < settings.WaveCount; i++)
        {
            waves.Add(generator.Generate(settings.WavesPowerPoints[i]));
        }
        this.settings = settings;

        Unit.OnUnitDeath += (u) => enemiesAlive.Remove(u);
        Unit.OnUnitSpawn += (u) => enemiesAlive.Add(u);
    }

    public void SetReachpoints(Transform reachpointsParent)
    {
        int children = reachpointsParent.childCount;
        reachpoints = new Stack<Transform>();
        for (int i = 0; i < children; ++i)
            reachpoints.Push(reachpointsParent.GetChild(i));
    }

    public void NextWave() // later make it generate a new wave for infinite levels
    {
        currentWave++;
        if (currentWave >= settings.WaveCount - 1)
        {
            var generatedPP = settings.WavesPowerPoints[settings.WaveCount - 1] + 10 * (currentWave - settings.WaveCount);
            waves.Add(generator.Generate(generatedPP));
        }
    }

    public int CurrentWave => currentWave;
    public Transform[] Reachpoints => reachpoints.ToArray();
    public ITakeDamage[] EnemiesAlive => enemiesAlive.ToArray();
    public UnitData CurrentGeneratedUnit => waves[currentWave];
    public WaveSettings WaveSettings => settings;
}
