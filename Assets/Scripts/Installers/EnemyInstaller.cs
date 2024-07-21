using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private EnemyConfig _enemyConfig;
    [SerializeField] private WaveConfig _waveConfig;
    [SerializeField] private Enemy _enemyPrefab;

    public override void InstallBindings()
    {
        Container.BindInstance(_enemyConfig).AsSingle();
        Container.BindInstance(_waveConfig).AsSingle();
        Container.BindInstance(_enemyPrefab).AsSingle();
        Container.BindInstance(this).AsSingle();
    }

    public Enemy GetNewEnemy(Enemy prefab)
    {
        Enemy enemy = Container.InstantiatePrefab(prefab).GetComponent<Enemy>();

        return enemy;
    }
}