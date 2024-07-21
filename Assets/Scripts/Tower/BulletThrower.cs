using UnityEngine;

public class BulletThrower : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private Bullet _prefab;

    private void OnEnable()
    {
        _tower.Shooted += OnShooted;
    }

    private void OnDisable()
    {
        _tower.Shooted -= OnShooted;
    }

    private void OnShooted(Transform from, Transform to)
    {
        Bullet bullet = Instantiate(_prefab, from.position, Quaternion.identity);
        bullet.SetTarget(to);
        bullet.Reached += OnBulletReached;
    }

    private void OnBulletReached(Bullet bullet)
    {
        bullet.Reached -= OnBulletReached;
        Destroy(bullet.gameObject);
    }
}