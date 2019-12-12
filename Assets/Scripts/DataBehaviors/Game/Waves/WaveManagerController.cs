using System.Collections.Generic;
using Data.Game;
using Data.Interfaces.Game.Waves;
using Data.Unit;
using Monobehaviors.Unit;
using UnityEngine;    

namespace DataBehaviors.Game.Waves
{
    public class WaveManagerController : IWaveManager
    {
        private readonly IWaveGenerator generator;
        private readonly List<Transform> unitsAlive;
        private Stack<Transform> reachpoints;
        private readonly List<UnitData> waves;

        public WaveManagerController(int seed, WaveSettings settings)
        {
            generator = new WaveGenerator(seed);
            waves = new List<UnitData>();
            unitsAlive = new List<Transform>();
            for (int i = 0; i < settings.WaveCount; i++) waves.Add(generator.Generate(settings.WavesPowerPoints[i]));
            WaveSettings = settings;

            UnitComponent.OnUnitDeath += u => unitsAlive.Remove(u.transform);
            UnitComponent.OnUnitSpawn += u => unitsAlive.Add(u.transform);
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
            CurrentWave++;
            if (CurrentWave >= WaveSettings.WaveCount - 1)
            {
                int generatedPP = WaveSettings.WavesPowerPoints[WaveSettings.WaveCount - 1] +
                                  10 * (CurrentWave - WaveSettings.WaveCount);
                waves.Add(generator.Generate(generatedPP));
            }
        }

        public int CurrentWave { get; private set; }

        public Transform[] Reachpoints => reachpoints.ToArray();
        public Transform[] UnitsAlive => unitsAlive.ToArray();
        public UnitData CurrentGeneratedUnit => waves[CurrentWave];
        public WaveSettings WaveSettings { get; }
    }
}