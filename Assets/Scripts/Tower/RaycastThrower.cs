using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class RaycastThrower : MonoBehaviour
{
    private Camera _mainCamera;
    private PlayerInputActions _input;

    [Inject]
    private void Construct(PlayerInputActions inputActions)
    {
        _mainCamera = Camera.main;
        _input = inputActions;
    }

    private void OnEnable()
    {
        _input.Player.Tap.performed += OnTapPerformed;
    }

    private void OnDisable()
    {
        _input.Player.Tap.performed -= OnTapPerformed;
    }

    private void OnTapPerformed(InputAction.CallbackContext context)
    {
        Vector2 cursorPosition = _input.Player.TapPosition.ReadValue<Vector2>();
        RaycastHit2D hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(cursorPosition), Vector2.zero);

        if (hit.collider.TryGetComponent(out TowerSpot towerSpot))
            towerSpot.Activate();
    }

    private void OnDestroy()
    {
        _input.Disable();
    }
}