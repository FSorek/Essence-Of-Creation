using Data.Game;
using Data.Unit;
using Monobehaviors.Unit;
using UnityEngine;

namespace Data.Interfaces.Game.Waves
{
    public interface IWaveManager
    {
        int CurrentWave { get; }
        Transform[] UnitsAlive { get; }
        UnitData CurrentGeneratedUnit { get; }
        Transform[] Reachpoints { get; }
        WaveSettings WaveSettings { get; }

        void SetReachpoints(Transform reachpointsParent);
        void NextWave();
    }
}