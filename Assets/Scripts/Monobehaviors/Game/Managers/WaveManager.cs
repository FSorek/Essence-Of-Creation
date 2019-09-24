using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour, IWaveManager
{
    public static WaveManager Instance;
    [SerializeField]private Transform[] reachpoints;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private WaveSettings waveSettings;

    private List<ITakeDamage> enemiesAlive = new List<ITakeDamage>();
    private UnitData[] unitGeneratedWaves = new UnitData[50];

    public int CurrentWave { get; private set; }

    public Transform[] Reachpoints => reachpoints;

    private float timer;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            throw new Exception("Two WaveManager scripts on scene!");

        waveSettings.GenerateWaves();
        timer = waveSettings.TimeToFirstWave;
        Unit.OnUnitDeath += RemoveUnitOnDeath;
    }

    public UnitData GetCurrentGeneratedUnit()
    {
        return waveSettings.waveUnits[CurrentWave];
    }

    public ITakeDamage[] GetEnemiesAlive()
    {
        return enemiesAlive.ToArray();
    }

    private void RemoveUnitOnDeath(ITakeDamage unit)
    {
        if(enemiesAlive.Contains(unit))
            enemiesAlive.Remove(unit);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            StartCoroutine(Spawn());
            timer = waveSettings.TimeBetweenWaves;
        }
    }

    private IEnumerator Spawn()
    {
        if (CurrentWave >= waveSettings.Waves.Length)
            CurrentWave = 0;
        var wave = waveSettings.Waves[CurrentWave];
        for (int i = 0; i < wave.AmountToSpawn; i++)
        {
            var enemy = Instantiate(wave.Model, spawnPoint.position, Quaternion.identity);
            enemiesAlive.Add(enemy.GetComponent<Unit>());
            yield return new WaitForSeconds(wave.TimeBetweenSpawns);
        }

        CurrentWave++;
    }

}