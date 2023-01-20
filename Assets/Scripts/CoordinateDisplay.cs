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
    private Vector2Int _coordinates = new();

    private void Awake()
    {
        if (Application.isPlaying) _label.enabled = _bCanShowCoordinatesInPlayMode;
        if (!_bCanShowCoordinatesInPlayMode) return;
        
        DisplayCoordinates();
    }

    private void Update()
    {
        if (_bCanShowCoordinatesInPlayMode) DisplayCoordinates();
        if (Application.isPlaying) return;

        UpdateObjectName();

    }

    private void DisplayCoordinates()
    {
        _coordinates.x = Mathf.RoundToInt(transform.parent.parent.position.x);
        _coordinates.y = Mathf.RoundToInt(transform.parent.parent.position.z);

        if (_bShouldDivideByGridSize)
        {
            _coordinates.x /= Mathf.RoundToInt(UnityEditor.EditorSnapSettings.move.x);
            _coordinates.y /= Mathf.RoundToInt(UnityEditor.EditorSnapSettings.move.z);
        }
        _label.text = $"{_coordinates.x},{_coordinates.y}";
    }

    private void UpdateObjectName()
    {
        transform.parent.parent.name = _coordinates.ToString();
    }
}
