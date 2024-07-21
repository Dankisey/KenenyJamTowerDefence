using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(SpriteRenderer))]
public class TowerSpot : MonoBehaviour
{
    [SerializeField] private List<Rune> _avaiableRunes;

    private SpriteRenderer _spriteRenderer;
    private TowerBuilder _towerBuilder;
    private RuneSystem _runeSystem;
    private bool _isOccupied = false;

    [Inject]
    private void Construct(RuneSystem runeSystem, TowerBuilder towerBuilder)
    {
        _towerBuilder = towerBuilder;
        _runeSystem = runeSystem;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Activate()
    {
        if (_isOccupied)
            return;

        _runeSystem.SendRunesToSelection(_avaiableRunes, OnRunesSelected);
    }

    private void OnRunesSelected(IReadOnlyList<Rune> list)
    {
        _spriteRenderer.color = new Color(0, 0, 0, 0);
        Tower tower = _towerBuilder.GetTower(list);
        _isOccupied = true;
        tower.SetSpot(this);
    }
}