using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _time;

    private Transform _transform;
    private Transform _target;
    private Vector3 _startPosition;
    private float _elapsedTime;

    public event Action<Bullet> Reached;

    public void SetTarget(Transform target)
    {
        _transform = transform;
        _startPosition = transform.position;
        _target = target;
    }

    private void Update()
    {
        if (_target == null)
            return;

        _elapsedTime += Time.deltaTime;
        float normalizedTime = Mathf.Clamp01(_elapsedTime / _time);
        _transform.position = Vector3.Lerp(_startPosition, _target.position, normalizedTime);

        if (_elapsedTime >= _time)
            Reached?.Invoke(this);
    }
}