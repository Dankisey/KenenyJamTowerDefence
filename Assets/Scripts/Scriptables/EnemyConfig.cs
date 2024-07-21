using UnityEngine;

[CreateAssetMenu(fileName = nameof(EnemyConfig), menuName = nameof(EnemyConfig))]
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField] public float BasicHP { get; private set; }
    [field: SerializeField] public float HPPerWave { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
}