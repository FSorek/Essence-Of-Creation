using System.Collections.Generic;
using Data.Unit;
using UnityEngine;    

namespace DataBehaviors.Game.Waves
{
    public class WaveManagerController
    {
        private readonly TransformList enemyList;
        private Stack<Transform> reachpoints;
        private readonly List<UnitData> waves;

        public WaveManagerController(int seed, Data.Game.GameSettings settings, TransformList enemyList)
        {
            //generator = new WaveGenerator(seed);
            waves = new List<UnitData>();
            //for (int i = 0; i < settings.WaveCount; i++) waves.Add(generator.Generate(settings.WavesPowerPoints[i]));
            GameSettings = settings;
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
            if (CurrentWave >= GameSettings.WaveCount - 1)
            {
                int generatedPP = GameSettings.WavesPowerPoints[GameSettings.WaveCount - 1] +
                                  10 * (CurrentWave - GameSettings.WaveCount);
                //waves.Add(generator.Generate(generatedPP));
            }
        }

        public int CurrentWave { get; private set; }

        public Transform[] Reachpoints => reachpoints.ToArray();
        public UnitData CurrentGeneratedUnit => waves[CurrentWave];
        public Data.Game.GameSettings GameSettings { get; }
    }
}