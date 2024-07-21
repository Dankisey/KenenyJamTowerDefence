using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rune))]
public class RuneView : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _animationDuration;
    [SerializeField] private float _unselectedScale = 1f;
    [SerializeField] private float _selectedScale = 1.1f;

    private Transform _transform;
    private Tween _currentTween;
    private Rune _rune;

    private void Awake()
    {
        _rune = GetComponent<Rune>();
        _transform = transform;
        _transform.localScale = Vector3.one * _unselectedScale;
    }

    private void OnEnable()
    {
        _rune.Selected += OnSelected;
        _rune.Unselected += OnUnselected;
    }

    private void OnDisable()
    {
        _rune.Selected -= OnSelected;
        _rune.Unselected -= OnUnselected;
    }

    private void OnDestroy()
    {
        _currentTween?.Kill();
    }

    private void OnSelected()
    {
        DoAnimation(_selectedScale);
    }

    private void OnUnselected()
    {
        DoAnimation(_unselectedScale);
    }

    private void DoAnimation(float targetScale)
    {
        _currentTween?.Kill();
        _currentTween = _transform.DOScale(targetScale, _animationDuration).SetEase(Ease.InOutQuad);
    }
}