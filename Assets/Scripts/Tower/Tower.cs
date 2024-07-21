using System;
using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private EnemyDetectionZone _detectionZone;
    [SerializeField] private Transform _shootPoint;

    private TowerAttackController _attackController;
    private float _range;
    private float _basicSpeed;
    private float _additionalSpeed;

    public event Action<Transform, Transform> Shooted;

    public void Instantiate(TowerConfig config)
    {
        _attackController = new(config.BasicDamage, config.BasicGoldPerKill);
        _basicSpeed = config.BasicSpeed;
        _range = config.BasicRange;
        _additionalSpeed = 0f;
    }

    private void Awake()
    {
        StartCoroutine(AttackCycle());
    }

    public void SetSpot(TowerSpot spot)
    {
        transform.position = spot.transform.position;
    }

    public void AddDamage(float amount) => _attackController.AddDamage(amount);

    public void AddGoldEaring(int amount) => _attackController.AddGoldEaring(amount);

    public void AddRange(float amount)
    {
        if (amount <= 0)
            throw new System.ArgumentOutOfRangeException(nameof(amount));

        _range += amount;
        _detectionZone.SetRadius(_range);
    }

    public void AddSpeed(float amount)
    {
        if (amount <= 0)
            throw new System.ArgumentOutOfRangeException(nameof(amount));

        _additionalSpeed += amount;
    }

    private IEnumerator AttackCycle()
    {
        while (enabled)
        {
            if (_detectionZone.TryGetEnemy(out Enemy enemy))
            {
                Attack attack = _attackController.GetAttack();
                enemy.TakeAttack(attack);
                Shooted?.Invoke(_shootPoint, enemy.transform);
            }

            yield return new WaitForSeconds(GetDelay());
        }
    }

    private float GetDelay() => Mathf.Clamp01(_basicSpeed - _additionalSpeed);
}