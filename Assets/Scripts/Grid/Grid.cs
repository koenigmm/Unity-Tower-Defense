using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Vector2Int gridDimensions;
    private int _gridSnappingValue;
    private Dictionary<Vector2Int, CellType> _grid = new();


    private void Awake()
    {
        _gridSnappingValue = Mathf.RoundToInt(UnityEditor.EditorSnapSettings.move.x);
        FillGridDictionaryWithInitlalValues();
        // DebugPrintDictionary();
    }

    private void FillGridDictionaryWithInitlalValues()
    {
        for (int i = 0; i < gridDimensions.x; i++)
        {
            for (int j = 0; j < gridDimensions.y; j++)
            {
                _grid.Add(new Vector2Int(i, j), CellType.Grass);
            }
        }
    }

    private void DebugPrintDictionary()
    {
        
        foreach(var cell in _grid)
        {
            Debug.Log(cell.Key + " " + cell.Value);
        }
    }
}
