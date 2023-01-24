using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Vector2Int gridDimensions;
    private static int _gridSnappingValue;
    private Dictionary<Vector2Int, CellType> _grid = new();

    public static Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int(Mathf.RoundToInt(position.x / _gridSnappingValue), Mathf.RoundToInt(position.z / _gridSnappingValue));
        return coordinates;
    }

    public void ChangeCellType(Vector2Int coordinates, CellType cellType) => _grid[coordinates] = cellType;

    public CellType GetCellTypeForGivenCoordinates(Vector2Int coordinates) => _grid[coordinates];


    private void Awake()
    {
        _gridSnappingValue = Mathf.RoundToInt(UnityEditor.EditorSnapSettings.move.x);
        if (_gridSnappingValue % 2 != 0) throw new System.Exception("Grid snapping value should be an even number");
        FillGridDictionaryWithInitlalValues();
    }

    // TODO Remove testing code
    private void Start()
    {
        DebugPrintDictionary();
    }

    private void FillGridDictionaryWithInitlalValues()
    {
        _grid.Clear();
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
        foreach (var cell in _grid)
        {
            Debug.Log(cell.Key + " " + cell.Value);
        }
    }
}
