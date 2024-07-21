using UnityEngine;

public class Way : MonoBehaviour
{
    [SerializeField] private Transform[] _corners;

    public bool TryGetTarget(int targetIndex, out Vector3 target)
    {
        target = Vector3.zero;

        if (targetIndex < 0)
            throw new System.ArgumentOutOfRangeException(nameof(targetIndex));

        if (targetIndex >= _corners.Length)
            return false;

        target = _corners[targetIndex].position;

        return true;
    }
}