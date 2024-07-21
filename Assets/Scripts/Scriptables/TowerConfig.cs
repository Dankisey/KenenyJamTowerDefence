using UnityEngine;

[CreateAssetMenu(fileName = nameof(TowerConfig), menuName = nameof(TowerConfig))]
public class TowerConfig : ScriptableObject
{
    [field: SerializeField] public float BasicRange { get; private set; }
    [field: SerializeField] public float BasicSpeed { get; private set; }
    [field: SerializeField] public float BasicDamage { get; private set; }
    [field: SerializeField] public int BasicGoldPerKill { get; private set; }
}