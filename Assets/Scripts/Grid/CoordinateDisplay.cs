using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private bool _bCanShowCoordinatesInPlayMode = true;
    [SerializeField] private bool _bShouldDivideByGridSize = true;
    [SerializeField] private float _gridSize = 10;
    private Vector2Int _coordinates = new();

    private void Awake()
    {
        if (Application.isPlaying) _label.enabled = _bCanShowCoordinatesInPlayMode;
        if (!_bCanShowCoordinatesInPlayMode) return;
        DisplayCoordinates();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (_bCanShowCoordinatesInPlayMode) DisplayCoordinates();
        if (Application.isPlaying) return;

        UpdateObjectName();
    }

#endif


    private void DisplayCoordinates()
    {
        _coordinates.x = Mathf.RoundToInt(transform.parent.parent.position.x);
        _coordinates.y = Mathf.RoundToInt(transform.parent.parent.position.z);

        if (_bShouldDivideByGridSize)
        {
            _coordinates.x /= Mathf.RoundToInt(_gridSize);
            _coordinates.y /= Mathf.RoundToInt(_gridSize);
        }
        _label.text = $"{_coordinates.x},{_coordinates.y}";
    }

    private void UpdateObjectName() => transform.parent.parent.name = _coordinates.ToString();

}
