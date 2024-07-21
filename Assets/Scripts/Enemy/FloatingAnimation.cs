using DG.Tweening;
using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _duration;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _transform.DOLocalMoveY(_distance, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }
}
