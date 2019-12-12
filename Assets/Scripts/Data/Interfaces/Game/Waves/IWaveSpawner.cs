using UnityEngine;

namespace Data.Interfaces.Game.Waves
{
    public interface IWaveSpawner
    {
        void Spawn(Vector3 position, int amount = 1);
    }
}