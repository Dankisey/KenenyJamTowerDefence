using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class RuneSelector : MonoBehaviour, ISwitchable
{
    [SerializeField] private TriggerCursor _triggerCursor;

    private TowerUpgradeConfig _config;
    private PlayerInputActions _input;
    private List<Transform> _points;
    private List<Rune> _selectedRunes;
    private bool _isHolding;
    private bool _isEnabled;

    public event Action<IReadOnlyList<Transform>> PointsChanged;
    public event Action<IReadOnlyList<Rune>> SelectionEnded;

    [Inject]
    private void Construct(PlayerInputActions inputActions, TowerUpgradeConfig config)
    {
        _selectedRunes = new List<Rune>();
        _points = new List<Transform>();
        _input = inputActions;
        _isHolding = false;
        _isEnabled = false;
        _config = config;
    }

    public void TurnOn()
    {
        _triggerCursor.Entered += OnColliderEntered;
        _triggerCursor.Staying += OnColliderStaying;
        _triggerCursor.Exited += OnColliderExited;
        _input.Player.Hold.performed += OnHoldStarted;
        _input.Player.Hold.canceled += OnHoldEnd;
        _isEnabled = true;
    }

    public void TurnOff()
    {
        _triggerCursor.Entered -= OnColliderEntered;
        _triggerCursor.Staying -= OnColliderStaying;
        _triggerCursor.Exited -= OnColliderExited;
        _input.Player.Hold.performed -= OnHoldStarted;
        _input.Player.Hold.canceled -= OnHoldEnd;
        _isEnabled = false;

        Clear();
    }

    private void Update()
    {
        if (_isEnabled)
            _triggerCursor.ChangePosition(_input.Player.TapPosition.ReadValue<Vector2>());
    }

    private void OnHoldStarted(InputAction.CallbackContext context)
    {
        _isHolding = true;
    }

    private void OnHoldEnd(InputAction.CallbackContext context)
    {
        if(_selectedRunes.Count == 0 || _isHolding == false)
            return;

        SelectionEnded?.Invoke(_selectedRunes);
        Clear();
    }

    private void OnColliderEntered(Collider2D collider)
    {
        if (collider.TryGetComponent(out Rune rune) == false)
            return;

        rune.Select();

        if (_isHolding == false)
            return;

        if (_selectedRunes.Contains(rune))
            return;

        AddRune(rune);
    }

    private void OnColliderStaying(Collider2D collider)
    {
        if (_isHolding == false)
            return;

        if (collider.TryGetComponent(out Rune rune) == false)
            return;

        if (_selectedRunes.Contains(rune))
            return;

        AddRune(rune);
    }

    private void OnColliderExited(Collider2D collider)
    {
        if (collider.TryGetComponent(out Rune rune) == false)
            return;

        if (_selectedRunes.Contains(rune) == false)
            rune.Unselect();
    }

    private void AddRune(Rune rune)
    {
        if (_selectedRunes.Count >= _config.MaxUpgrades)
        {
            rune.Unselect();
            return;
        }

        _points.Add(rune.transform);
        List<Transform> visiblePoints = new(_points);

        if (visiblePoints.Count < _config.MaxUpgrades)
            visiblePoints.Add(_triggerCursor.transform);

        PointsChanged?.Invoke(visiblePoints);
        _selectedRunes.Add(rune);
    }

    private void Clear()
    {
        foreach (Rune rune in _selectedRunes)
            rune.Unselect();

        _selectedRunes.Clear();
        _points.Clear();
        _isHolding = false;
    }
}
