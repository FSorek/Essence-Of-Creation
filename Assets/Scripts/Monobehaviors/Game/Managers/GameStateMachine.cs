﻿using Data.Game;
using DataBehaviors.Game.Waves;
using Monobehaviors.Pooling;
using UnityEngine;

namespace Monobehaviors.Game.Managers
{
    public class GameStateMachine : MonoBehaviour
    {
        [SerializeField] private WaveSettings wave; // temporary single wave
        [SerializeField] private ObjectPool enemyPool;
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private GameStateData stateData;
        private StateMachine<GameStates> stateMachine;


        private void Awake()
        {
            stateMachine = new StateMachine<GameStates>(stateData);
            var started = new GameStarted(gameSettings, stateData);
            var spawningWave = new GameSpawningWave(enemyPool, stateData, wave);
            var awaitingNextWave = new GameAwaitingNextWave(gameSettings, stateData);
            
            stateMachine.RegisterState(GameStates.Started, started);
            stateMachine.RegisterState(GameStates.SpawningWave, spawningWave);
            stateMachine.RegisterState(GameStates.AwaitingNextWave, awaitingNextWave);
            
            stateData.ChangeState(GameStates.Started);
        }

        private void Update()
        {
            stateMachine.Tick();
        }
    }
}