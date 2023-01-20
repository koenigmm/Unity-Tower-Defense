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
    [SerializeField] GameObject towerPrefab;
    [SerializeField] string towerParentTag;
    public bool b_isPlaceable { get => cellType == CellType.Grass; }
    private GameObject towerParent;

    private void Awake()
    {
        towerParent = GameObject.FindGameObjectWithTag(towerParentTag);

        if (towerParent != null) return;
        CreateParentGameObject();
    }

    private void OnMouseDown()
    {
        if (!b_isPlaceable) return;
        // print(transform.name);
        Instantiate(towerPrefab, transform.position, Quaternion.identity, towerParent.transform);
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

     private void CreateParentGameObject()
    {
        GameObject newParent = new GameObject();
        newParent.name = towerParentTag;
        newParent.tag = towerParentTag;
        towerParent = newParent;
    }
}




