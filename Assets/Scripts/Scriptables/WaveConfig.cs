using UnityEngine;

[CreateAssetMenu(fileName = nameof(WaveConfig), menuName = nameof(WaveConfig))]
public class WaveConfig : ScriptableObject
{
    [field: SerializeField] public float SpawningDelay { get; private set; }
    [field: SerializeField] public float MinimumDelay { get; private set; }
    [field: SerializeField] public float DelayPerWave { get; private set; }
    [field: SerializeField] public int EnemiesPerWave { get; private set; }
    [field: SerializeField] public int Enemies { get; private set; }
}