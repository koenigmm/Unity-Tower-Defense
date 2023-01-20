using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellType
{
    Tower,
    Grass,
    Way
}

public class Cell : MonoBehaviour
{
    [SerializeField] CellType cellType;
    public bool b_isPlaceable { get => cellType == CellType.Grass; }

    private void OnMouseDown()
    {
        if (!b_isPlaceable) return;
        print(transform.name);
    }

    public void SetCellTypeToTower()
    {
        if (cellType != CellType.Grass) 
        {
            Debug.LogWarning("Only grass cells can be changed");
            return;
        }

        cellType = CellType.Tower;
    }
}




