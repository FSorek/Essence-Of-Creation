using Data.Data_Types;
using Data.Game;
using DataBehaviors.Game.Waves;
using DataBehaviors.Player;
using DataBehaviors.Player.States;
using Monobehaviors.Pooling;
using UnityEngine;

namespace Monobehaviors.Game.Managers
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private TransformList enemiesAlive;
        [SerializeField] private TransformList reachPoints;
        [SerializeField] private ObjectPool unitPool;
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private Transform reachPointParent;
        [SerializeField] private Transform spawnPoint;
        
        private WaveSpawner spawner;
        private float timer;
        
        private void Awake()
        {
            spawner = new WaveSpawner(unitPool, enemiesAlive);
            SetReachpoints();
            timer = gameSettings.TimeToFirstWave;
        }

        private void SetReachpoints()
        {
            for (int i = 0; i < reachPointParent.childCount; i++)
            {
                reachPoints.Add(reachPointParent.GetChild(i));
            }
        }
        private void Update()
        {
            //timer -= Time.deltaTime;
            //if (timer <= 0)
            //{
            //    StartCoroutine(spawner.SpawnWave(spawnPoint.position));
            //    timer = gameSettings.TimeBetweenWaves;
            //}
        }  
    }
}