using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    //TODO remove | obsolet?
    public Action <Vector2Int> OnCellTypeChange;
    [SerializeField] private Vector2Int gridDimensions;
    [SerializeField] private int _gridSnappingValue = 10;
    private Dictionary<Vector2Int, CellType> _grid = new();

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int(Mathf.RoundToInt(position.x / _gridSnappingValue), Mathf.RoundToInt(position.z / _gridSnappingValue));
        return coordinates;
    }

    public void ChangeCellType(Vector2Int coordinates, CellType cellType)
    {
        OnCellTypeChange?.Invoke(coordinates);
        _grid[coordinates] = cellType;
    }

     public void ChangeCellsToTower(Vector3 position, int xRange, int yRange)
    {
        var coordinates = GetCoordinatesFromPosition(position);
        print(coordinates);
        var neighborCoordinates = GetNeighborCoordinates(coordinates, xRange, yRange);

        foreach (var neighbor in neighborCoordinates)
        {
            ChangeCellType(neighbor, CellType.Tower);
        }
        

        DebugPrintDictionary();
    }

    public bool CanBuildOnCell(Vector2Int coordinates) => GetCellTypeForGivenCoordinates(coordinates) == CellType.Grass;

    private void Awake()
    {
        // _gridSnappingValue = Mathf.RoundToInt(UnityEditor.EditorSnapSettings.move.x);
        if (_gridSnappingValue % 2 != 0) throw new System.Exception("Grid snapping value should be an even number");
        FillGridDictionaryWithInitlalValues();
    }

    // TODO remove?
    public Vector3 GetPositionFromCoordinates(Vector2 coordinates)
    {
        Vector3 position = Vector3.zero;
        position.x = coordinates.x * _gridSnappingValue;
        position.y = 0;
        position.z = coordinates.y * _gridSnappingValue;

        return position;
    }

    private List<Vector2Int> GetNeighborCoordinates(Vector2Int coordinates, int xRange, int yRange)
    {
        List<Vector2Int> coordinatesInRange = new();

        for (int x = 0; x < xRange; x++)
        {
            for (int y = 0; y < yRange; y++)
            {
                if (x == 0 && y == 0) continue;
                Vector2Int nextPosition = new Vector2Int(coordinates.x + x, coordinates.y + y);
                coordinatesInRange.Add(nextPosition);
                // ChangeCellType(nextPosition, CellType.Tower);
            }
        }
        return coordinatesInRange;
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

    //TODO Remove testing
    public void DebugPrintDictionary()
    {
        foreach (var cell in _grid)
        {
            Debug.Log(cell.Key + " " + cell.Value);
        }
    }

    private CellType GetCellTypeForGivenCoordinates(Vector2Int coordinates) => _grid[coordinates];
}
