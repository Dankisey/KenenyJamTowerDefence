using UnityEngine;

[CreateAssetMenu(fileName = nameof(TowerUpgradeConfig), menuName = nameof(TowerUpgradeConfig))]
public class TowerUpgradeConfig : ScriptableObject
{
    [field: SerializeField] public int MaxUpgrades { get; private set; }
    [field: SerializeField] public float RangeUpgrade { get; private set; }
    [field: SerializeField] public float SpeedUpgrade { get; private set; }
    [field: SerializeField] public float DamageUpgrade { get; private set; }
    [field: SerializeField] public int GoldPerKillUpgrade { get; private set; }
}