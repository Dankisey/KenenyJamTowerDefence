using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class TriggerCursor : MonoBehaviour
{
    private Transform _transform;
    private Camera _mainCamera;

    public event Action<Collider2D> Entered;
    public event Action<Collider2D> Exited;
    public event Action<Collider2D> Staying;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _transform = transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entered?.Invoke(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Staying?.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Exited?.Invoke(collision);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }

    public void ChangePosition(Vector2 screenPoint) => _transform.position = _mainCamera.ScreenToWorldPoint(screenPoint);
}