using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private EnemyPool _pool;
    [SerializeField] private Way[] _ways;

    private Coroutine _currentCoroutine;
    private WaveConfig _config;
    private int _currentWave = 0;

    public event Action<int> GoldEarned;
    public event Action EnemyFinished;

    [Inject]
    private void Construct(WaveConfig waveConfig)
    {
        _config = waveConfig;
    }

    private void Awake()
    {
        _currentCoroutine = StartCoroutine(WaveSpawningCycle());
    }

    private void OnWaveEnded()
    {
        _currentWave++;
        StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(WaveSpawningCycle());
    }

    private float GetSpawningDelay() => 
        Mathf.Clamp(_config.SpawningDelay - _config.DelayPerWave * _currentWave, _config.MinimumDelay, _config.SpawningDelay);

    private int GetEnemiesAmount() =>
        _config.Enemies + _config.EnemiesPerWave * _currentWave;

    private IEnumerator WaveSpawningCycle()
    {
        var wait = new WaitForSeconds(GetSpawningDelay());
        int enemiesToSpawn = GetEnemiesAmount();

        while (enemiesToSpawn > 0)
        {
            Enemy enemy = _pool.Pull();
            enemy.SetWay(_ways[UnityEngine.Random.Range(0, _ways.Length)]);
            enemy.ResetHealth(_currentWave);
            SubscribeEvents(enemy);

            yield return wait;
        }

        OnWaveEnded();
    }

    private void SubscribeEvents(Enemy enemy)
    {
        enemy.FinishReached += OnEnemyFinished;
        enemy.Dead += OnEnemyDeath;
    }

    private void OnEnemyFinished(Enemy enemy)
    {
        EnemyFinished?.Invoke();
        enemy.FinishReached -= OnEnemyFinished;
        enemy.Dead -= OnEnemyDeath;
        _pool.Push(enemy);
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        GoldEarned?.Invoke(enemy.GoldForDeath);
        enemy.FinishReached -= OnEnemyFinished;
        enemy.Dead -= OnEnemyDeath;
        _pool.Push(enemy);
    }
}