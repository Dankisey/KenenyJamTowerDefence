using System;
using UnityEngine;

public class Rune : MonoBehaviour
{
    [field: SerializeField] public RuneIDs RuneID { get; private set; }

    public event Action Selected;
    public event Action Unselected;

    public Rune Select()
    {
        Selected?.Invoke();

        return this;
    }

    public void Unselect()
    {
        Unselected?.Invoke();
    }
}

public enum RuneIDs
{
    Attack,
    Range,
    Speed,
    Gold
}