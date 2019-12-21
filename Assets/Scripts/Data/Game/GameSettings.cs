using System;
using UnityEngine;

namespace Data.Game
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Essence/Game/Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public int Seed;
        public int WaveCount;
        public int[] WavesPowerPoints;
        public float TimeToFirstWave;
        public float TimeBetweenWaves;
        public GameStates CurrentState;
        public event Action<GameStates> OnStateEntered = delegate {  };
        public event Action<GameStates> OnStateExit = delegate {  };


        public void ChangeState(GameStates state)
        {
            OnStateExit(CurrentState);
            CurrentState = state;
            OnStateEntered(CurrentState);
        }
    }
}