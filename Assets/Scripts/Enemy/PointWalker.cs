using System;
using UnityEngine;

public class PointWalker
{
    private Transform _transform;
    private Way _way;
    private Vector3 _currentTarget;
    private int _currentTargetIndex;
    private float _speed;

    public event Action FinishReached;

    public PointWalker(Transform transform, float speed, Way way)
    {
        _transform = transform;
        _speed = speed;
        _way = way;
        _currentTargetIndex = 0;
        SetUp();
    }

    public void Update()
    {
        if (_way == null)
            return;

        Move();

        if (Approximately(_transform.position, _currentTarget))
        {
            _currentTargetIndex++;

            if (_way.TryGetTarget(_currentTargetIndex, out _currentTarget) == false)
            {
                FinishReached?.Invoke();
                _way = null;
            }
        }
    }

    public void Reset(Way newWay = null)
    {
        if (newWay != null)
            _way = newWay;

        _currentTargetIndex = 0;
        SetUp();
    }

    private void Move()
    {
        Vector3 next = Vector3.MoveTowards(_transform.position, _currentTarget, _speed * Time.deltaTime);
        _transform.position = next;
    }

    private void SetUp()
    {
        _way.TryGetTarget(_currentTargetIndex, out _currentTarget);
        _transform.position = _currentTarget;
    }

    private bool Approximately(Vector3 a, Vector3 b)
    {
        bool isSimilarX = Mathf.Approximately(a.x, b.x);
        bool isSimilarY = Mathf.Approximately(a.y, b.y);
        bool isSimilarZ = Mathf.Approximately(a.z, b.z);

        return isSimilarX && isSimilarY && isSimilarZ;
    }
}
