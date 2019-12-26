using Data.Data_Types.Enums;
using Data.Interfaces.StateMachines;
using Data.ScriptableObjects.Game;
using Data.ScriptableObjects.StateMachines;
using DataBehaviors.Game.Waves;
using Monobehaviors.Pooling;
using UnityEngine;

namespace DataBehaviors.Game.States
{
    public class GameSpawningWave : IState
    {
        private readonly GameStateData stateData;
        private readonly WaveSettings waveSettings;
        private EnemyFactory enemyFactory;
        private float lastSpawn;
        private int spawnCounter;

        public GameSpawningWave(ObjectPool enemyPool, GameStateData stateData, WaveSettings waveSettings)
        {
            this.stateData = stateData;
            this.waveSettings = waveSettings;
            enemyFactory = new EnemyFactory(waveSettings.Seed, enemyPool);
            //waveGenerator = new WaveGenerator(gameSettings.WavePowerPoints, gameSettings.Seed); || this.waveGenerator = waveGenerator; 
            // create pre-defined waves first then a layer on top of it to generate it?
        }

        public void StateEnter()
        {
            // gameSettings.CurrentWave
            // unitData = waveGenerator[gameSettings.CurrentWave]
            lastSpawn = 0;
            spawnCounter = waveSettings.SpawnAmount;
        }

        public void ListenToState()
        {
            // count down when in between spawns
            // count down the spawn amount
            // waveSpawner.Spawn(unitData)

            if (Time.time - lastSpawn > waveSettings.TimeBetweenSpawns)
            {
                Spawn();
                if(spawnCounter-- <= 0)
                    stateData.ChangeState(GameStates.AwaitingNextWave);
                lastSpawn = Time.time;
            }
        }

        private void Spawn()
        {
            for (int i = 0; i < waveSettings.EnemiesPerSpawn; i++)
            {
                var enemy = enemyFactory.CreateEnemy(waveSettings.EnemyPower, waveSettings.ConversionRates);
                enemy.transform.position = waveSettings.SpawnPosition;
                enemy.SetActive(true);
            }
        }

        public void StateExit()
        {
            
        }
    }
}