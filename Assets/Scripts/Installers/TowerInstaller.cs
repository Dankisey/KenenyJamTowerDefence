using UnityEngine;
using Zenject;

public class TowerInstaller : MonoInstaller
{
    [SerializeField] private BasicTowerDirector _towerDirector;
    [SerializeField] private TowerUpgradeConfig _upgradeConfig;
    [SerializeField] private TowerConfig _towerConfig;
    [SerializeField] private TowerBuilder _towerBuilder;
    [SerializeField] private RuneSystem _runeSystem;
    [SerializeField] private Tower _prefab;

    public override void InstallBindings()
    {
        Container.Bind<ITowerDirector>().FromInstance(_towerDirector).AsSingle();
        Container.BindInstance(_upgradeConfig).AsSingle();
        Container.BindInstance(_towerBuilder).AsSingle();
        Container.BindInstance(_runeSystem).AsSingle();
        Container.BindInstance(_towerConfig).AsSingle();
        Container.BindInstance(this).AsSingle();
    }

    public Tower GetTower()
    {
        GameObject towerObject = Container.InstantiatePrefab(_prefab);

        return towerObject.GetComponent<Tower>();
    }
}