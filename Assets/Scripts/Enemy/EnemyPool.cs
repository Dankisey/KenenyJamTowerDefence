using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyPool : MonoBehaviour
{
    private Queue<Enemy> _enemies = new();
    private EnemyInstaller _installer;
    private Enemy _prefab;

    [Inject]
    private void Construct(EnemyInstaller installer, Enemy enemy)
    {
        _installer = installer;
        _prefab = enemy;
    }

    public Enemy Pull()
    {
        if (_enemies.Count == 0)
            return GetNew();

        Enemy instance = _enemies.Dequeue();
        instance.gameObject.SetActive(true);

        return instance;
    }

    public void Push(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        _enemies.Enqueue(enemy);
    }

    private Enemy GetNew()
    {
        return _installer.GetNewEnemy(_prefab);
    }
}
