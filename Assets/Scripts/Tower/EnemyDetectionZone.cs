using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class EnemyDetectionZone : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private int _hitsToCheck = 5;

    public void SetRadius(float radius)
    {
        GetComponent<CircleCollider2D>().radius = radius;
    }

    public bool TryGetEnemy(out Enemy enemy)
    {
        enemy = null;

        RaycastHit2D[] hits = new RaycastHit2D[_hitsToCheck];
        _rigidBody.Cast(Vector2.zero, hits);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null)
                break;

            if (hit.collider.TryGetComponent(out enemy))       
                return true;          
        }

        return false;
    }
}