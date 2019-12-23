﻿using Data.Game;
using UnityEngine;

namespace Monobehaviors.Game.Managers
{
    public class GameStarted : IState
    {
        private readonly GameStateData stateData;
        private readonly GameSettings gameSettings;

        private float timeStarted;
        public GameStarted(GameSettings gameSettings, GameStateData stateData)
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
            if (Time.time - timeStarted > gameSettings.TimeToFirstWave)
            {
                stateData.ChangeState(GameStates.SpawningWave);
            }
        }

        public void StateExit()
        {
            
        }
    }
}