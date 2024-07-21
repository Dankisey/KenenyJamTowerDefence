using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RuneHolder : MonoBehaviour, ISwitchable
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _parent;
    [SerializeField] private float _radius = 1f;

    private List<Rune> _runes = new();

    public void TurnOn()
    {
        _spriteRenderer.color = Color.white;
    }

    public void TurnOff()
    {
        _spriteRenderer.color = new Color(0, 0, 0, 0);

        foreach (Rune rune in _runes)
            Destroy(rune.gameObject);

        _runes.Clear();
    }

    public void SetRunes(List<Rune> runes)
    {
        InstantiateRunes(runes);
        DoCircle();
    }

    private void InstantiateRunes(List<Rune> runes)
    {
        foreach (Rune rune in runes)
            _runes.Add(Instantiate(rune, _parent));
    }

    private void DoCircle()
    {
        if (_runes.Count == 0)
            return;

        float angle = (360f / _runes.Count) * Mathf.Deg2Rad;

        for (int i = 0; i < _runes.Count; i++)
        {
            float currentAngle = angle * i;

            float x = Mathf.Cos(currentAngle);
            float y = Mathf.Sin(currentAngle);

            Vector2 position = new Vector3(x, y) * _radius;
            _runes[i].transform.localPosition = position;
        }
    }
}