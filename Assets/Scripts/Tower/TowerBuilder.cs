using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TowerBuilder : MonoBehaviour
{
    private TowerUpgradeConfig _upgradeConfig;
    private ITowerDirector _towerDirector;
    private Tower _current;

    [Inject]
    private void Construct(TowerUpgradeConfig towerUpgradeConfig, ITowerDirector towerDirector)
    {
        _upgradeConfig = towerUpgradeConfig;
        _towerDirector = towerDirector;
    }

    public Tower GetTower(IReadOnlyList<Rune> runes)
    {
        Tower tower = _towerDirector.Build();

        foreach (var rune in runes)
        {
            switch (rune.RuneID)
            {
                case RuneIDs.Attack:
                    tower.AddDamage(_upgradeConfig.DamageUpgrade);
                    break;
                case RuneIDs.Range:
                    tower.AddRange(_upgradeConfig.RangeUpgrade);
                    break;
                case RuneIDs.Speed:
                    tower.AddSpeed(_upgradeConfig.SpeedUpgrade);
                    break;
                case RuneIDs.Gold:
                    tower.AddGoldEaring(_upgradeConfig.GoldPerKillUpgrade);
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }

        return tower;
    }
}