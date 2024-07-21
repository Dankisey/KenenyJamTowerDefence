using UnityEngine;
using Zenject;

public class BasicTowerDirector : MonoBehaviour, ITowerDirector
{
    private TowerInstaller _installer;
    private TowerConfig _config;

    [Inject]
    private void Construct(TowerConfig config, TowerInstaller installer)
    {
        _installer = installer;
        _config = config;
    }

    public Tower Build()
    {
        Tower tower = _installer.GetTower();
        tower.Instantiate(_config);

        return tower;
    }
}