using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// representing constants
public enum SpawnModes
{
    Fixed,
    Random
}
public class Spawner : MonoBehaviour
{
    public static Action OnWaveCompleted;

    [Header("Settings")]
    [SerializeField] private SpawnModes spawnMode = SpawnModes.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private float delayBtwWaves = 1f;

    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;

    [Header("Random Delay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    [Header("Poolers")]
    [SerializeField] private ObjectPooler EnemyWave10Pooler;
    [SerializeField] private ObjectPooler EnemyWave11to20Pooler;
    [SerializeField] private ObjectPooler EnemyWave21to30Pooler;
    [SerializeField] private ObjectPooler EnemyWave31to40Pooler;
    [SerializeField] private ObjectPooler EnemyWave41to50Pooler;
    [SerializeField] private ObjectPooler EnemyWave51to60Pooler;
    [SerializeField] private ObjectPooler EnemyWave61to70Pooler;
    [SerializeField] private ObjectPooler EnemyWave71to80Pooler;
    [SerializeField] private ObjectPooler EnemyWave81to90Pooler;
    [SerializeField] private ObjectPooler EnemyWave91to100Pooler;
    


    private float _spawnTimer;
    private int _enemiesSpawned;
    // for checking remaining enemy number if they are dead or reached last waypoint
    private int _enemiesRamaining;

    //for fixing Wave Count Problem
    private bool _waveCompleted;

    private Waypoint _waypoint;

    private void Start()
    {
        _waypoint = GetComponent<Waypoint>();
        _enemiesRamaining = enemyCount;
    }
    // Update is called once per frame
    void Update()
    {
        // decrease spawn timer value every frame
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            // take spawn time according to spawn modes
            _spawnTimer = GetSpawnDelay();
            if (_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }
    private void SpawnEnemy()
    {
        GameObject newInstance = GetPooler().GetInstanceFromPool();
        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.Waypoint = _waypoint;
        /*
        <summary>
        every time spawner gets an enemy from the pool,
        this line going to reset the enemy values
        </summary>
        */
        enemy.ResetEnemy();
        enemy.transform.localPosition = transform.position;
        // we already instantiated
        newInstance.SetActive(true);
    }
    // get delay according to spawn modes ( fixed or random )
    private float GetSpawnDelay()
    {
        float delay = 0f;
        if (spawnMode == SpawnModes.Fixed)
        {
            delay = delayBtwSpawns;
        }
        else
        {
            delay = GetRandomDelay();
        }
        return delay;
    }
    // Spawning enemies between two specified delays
    private float GetRandomDelay()
    {
        float randomTimer = UnityEngine.Random.Range(minRandomDelay, maxRandomDelay);
        return randomTimer;
    }

    private ObjectPooler GetPooler()
    {
        int currentWave = LevelManager.Instance.CurrentWave;

        if (currentWave <= 10)    // 1 - 10
        {
            return EnemyWave10Pooler;
        }
        if (currentWave > 10 && currentWave <= 20) // 11 - 20
        {
            return EnemyWave11to20Pooler;
        }
        if (currentWave > 20 && currentWave <= 30) // 11 - 20
        {
            return EnemyWave21to30Pooler;
        }
        if (currentWave > 30 && currentWave <= 40) // 31-40
        {
            return EnemyWave31to40Pooler;
        }
        if (currentWave > 40 && currentWave <= 50) // 41-50
        {
            return EnemyWave41to50Pooler;
        }
        if (currentWave > 50 && currentWave <= 60) // 51-60
        {
            return EnemyWave51to60Pooler;
        }
        if (currentWave > 60 && currentWave <= 70) // 61-70
        {
            return EnemyWave61to70Pooler;
        }
        if (currentWave > 70 && currentWave <= 80) // 71-80
        {
            return EnemyWave71to80Pooler;
        }
        if (currentWave > 80 && currentWave <= 90) // 81-90
        {
            return EnemyWave81to90Pooler;
        }
        if (currentWave > 90 && currentWave <= 100) // 91-100
        {
            return EnemyWave91to100Pooler;
        }
        return null;
    }

    // IEnumerators yield return statement allows to wait for a given time
    private IEnumerator NextWave()
    {
        // allows to suspend the coroutine execution for the given amount of seconds using scaled time.
        yield return new WaitForSeconds(delayBtwWaves);
        _enemiesRamaining = enemyCount;
        _spawnTimer = 0f;
        _enemiesSpawned = 0;

        // fixing wave count
        _waveCompleted = false;
    }
    private void OnEnable()
    {
        Enemy.OnEndReached += RecordEnemy;
        EnemyHealth.OnEnemyKilled += RecordEnemy;
    }
    // checking if enemy reached last waypoint or dead
    private void RecordEnemy(Enemy enemy)
    {
        _enemiesRamaining--;
        if (_enemiesRamaining <= 0 && !_waveCompleted)
        {
            _waveCompleted = true;
            OnWaveCompleted?.Invoke();
            StartCoroutine(NextWave());
        }
    }
    private void OnDisable()
    {
        Enemy.OnEndReached -= RecordEnemy;
        EnemyHealth.OnEnemyKilled -= RecordEnemy;
    }

}
