using System;
using System.Collections.Generic;
using UnityEngine;

public class RuneSystem : MonoBehaviour
{
    [SerializeField] private RuneSelector _runeSelector;
    [SerializeField] private RuneHolder _runeHolder;

    private Action<IReadOnlyList<Rune>> _currentCallback;

    private void Awake()
    {
        TurnOff();
    }

    public void SendRunesToSelection(List<Rune> runes, Action<IReadOnlyList<Rune>> callback)
    {
        _currentCallback = callback;
        TurnOn(runes);
    }

    private void TurnOn(List<Rune> runes)
    {
        _runeHolder.TurnOn();
        _runeSelector.TurnOn();
        _runeSelector.SelectionEnded += OnSelectionEnded;
        _runeHolder.SetRunes(runes);
    }

    private void TurnOff()
    {
        _runeHolder.TurnOff();
        _runeSelector.TurnOff();
    }

    private void OnSelectionEnded(IReadOnlyList<Rune> runesList)
    {
        _currentCallback.Invoke(runesList);
        _runeSelector.SelectionEnded -= OnSelectionEnded;
        _currentCallback = null;
        TurnOff();
    }
}