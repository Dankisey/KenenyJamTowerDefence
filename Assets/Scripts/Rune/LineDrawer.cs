using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(LineRenderer))]
public class LineDrawer : MonoBehaviour
{
    private IReadOnlyList<Transform> _points;
    private RuneSelector _runeSelector;
    private LineRenderer _lineRenderer;

    [Inject]
    private void Construct(RuneSelector runeSelector)
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _runeSelector= runeSelector;
    }

    private void OnEnable()
    {
        _runeSelector.PointsChanged += OnPointsChanged;
        _runeSelector.SelectionEnded += OnSelectionEnded;
    }

    private void OnSelectionEnded(IReadOnlyList<Rune> list)
    {
        _lineRenderer.positionCount = 0;
        _points = null;
    }

    private void OnDisable()
    {
        _runeSelector.PointsChanged -= OnPointsChanged;
        _runeSelector.SelectionEnded -= OnSelectionEnded;
    }

    private void Update()
    {
        if (_points == null || _points.Count == 0)
            return;

        for (int i = 0; i <_points.Count; i++)
            _lineRenderer.SetPosition(i, _points[i].position);
    }

    private void OnPointsChanged(IReadOnlyList<Transform> points)
    {
        _points = points;
        _lineRenderer.positionCount = _points.Count;
    }
}