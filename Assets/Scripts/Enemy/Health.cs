using System;
using UnityEngine;

public class Health
{
    public float Value { get; private set; }
    public float MaxValue {  get; private set; }
    public bool IsAlive { get; private set; }

    public event Action Changed;
    public event Action Reseted;

    public Health(float maxValue) 
    {
        MaxValue = maxValue;
        Value = maxValue;
    }

    public void TakeDamage(float amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        Value -= amount;
        IsAlive = Value > 0;
        Value = Mathf.Clamp(Value, 0, MaxValue);
        Changed?.Invoke();
    }

    public void Reset(float newMaxValue)
    {
        MaxValue = newMaxValue;
        Value = newMaxValue;
        Reseted?.Invoke();
    }
}