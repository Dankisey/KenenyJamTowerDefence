using System;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthView _healthView;

    private EnemyConfig _enemyConfig;
    private PointWalker _walker;
    private Health _health;

    public int GoldForDeath { get; private set; }

    public event Action<Enemy> FinishReached;
    public event Action<Enemy> Dead;

    [Inject]
    private void Construct(EnemyConfig config)
    {
        _enemyConfig = config; 
    }

    private void OnDestroy()
    {
        _walker.FinishReached -= OnFinishReached;
    }

    private void Update()
    {
        _walker.Update();
    }

    public void ResetHealth(int wave)
    {
        if (_health != null)
        {
            _health.Reset(_enemyConfig.BasicHP + _enemyConfig.HPPerWave * wave);

            return;
        }

        _health = new Health(_enemyConfig.BasicHP + _enemyConfig.HPPerWave * wave);
        _healthView.Initialize(_health);
        GoldForDeath = 0;
    }

    public void SetWay(Way way)
    {
        if (_walker != null)
        {
            _walker.Reset(way);

            return;
        }

        _walker = new(transform, _enemyConfig.Speed, way);
        _walker.FinishReached += OnFinishReached;
    }

    public void TakeAttack(Attack attack)
    {
        _health.TakeDamage(attack.Damage);

        if (_health.IsAlive == false)
        {
            GoldForDeath = attack.GoldPerKill;
            Dead?.Invoke(this);
        }
    }

    private void OnFinishReached()
    {
        FinishReached?.Invoke(this);
    }
}