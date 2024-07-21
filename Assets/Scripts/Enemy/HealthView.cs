using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float _maxChangingDelta;
    [SerializeField] private Image _image;

    private Health _health;

    private void OnDestroy()
    {
        Unsubscribe();
    }
    
    public void Initialize(Health health)
    {
        _health = health;
        Subscribe();
    }

    private void OnHealthChanged()
    {
        float normalizedValue = _health.Value / _health.MaxValue;
        _image.fillAmount = normalizedValue;
    }

    private void Subscribe()
    {
        _health.Changed += OnHealthChanged;
        _health.Reseted += OnReseted;     
    }

    private void Unsubscribe()
    {
        _health.Changed -= OnHealthChanged;
        _health.Reseted -= OnReseted;
    }

    private void OnReseted()
    {
        _image.fillAmount = _health.Value / _health.MaxValue;
    }
}