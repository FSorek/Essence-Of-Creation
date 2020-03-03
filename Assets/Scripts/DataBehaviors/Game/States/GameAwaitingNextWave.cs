using Data.Data_Types.Enums;
using Data.Interfaces.StateMachines;
using Data.ScriptableObjects.Game;
using Data.ScriptableObjects.StateMachines;
using UnityEngine;

namespace DataBehaviors.Game.States
{
    public class GameAwaitingNextWave : IState
    {
        private readonly GameStateData stateData;
        private readonly GameSettings gameSettings;
        private float timeStarted;

        public GameAwaitingNextWave(GameSettings gameSettings, GameStateData stateData)
        {
            this.gameSettings = gameSettings;
            this.stateData = stateData;
        }

        public void StateEnter()
        {
            timeStarted = Time.time;
        }

        public void ListenToState()
        {
            if (Time.time - timeStarted > gameSettings.TimeBetweenWaves)
            {
                stateData.ChangeState(GameStates.SpawningWave);
            }
        }

        public void StateExit()
        {
            
        }
    }
}