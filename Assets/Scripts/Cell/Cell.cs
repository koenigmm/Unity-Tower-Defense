using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellType
{
    Tower,
    Grass,
    Way,
    Start,
    End,
    Blocked
}

public class Cell : MonoBehaviour
{
    [SerializeField] CellType cellType;
    [SerializeField] GameObject towerPrefab;
    [SerializeField] string towerParentTag;
    [SerializeField] Material rangeDisplayMaterial;
    public bool b_isPlaceable { get => cellType == CellType.Grass; }
    public bool b_isStart { get => cellType == CellType.Start; }
    private GameObject _towerParent;
    private GameObject _sphere;
    private bool _bIsInBuildMode = true; //testing
    private float _currentTowerPrefabRange;
    private EnemyObjectPoolHandler enemyObjectPoolHandler;

    private void Awake()
    {
        _towerParent = GameObject.FindGameObjectWithTag(towerParentTag);
        enemyObjectPoolHandler = GameObject.FindObjectOfType<EnemyObjectPoolHandler>();

        if (_towerParent != null) return;
        CreateParentGameObject();
    }

    private void Start()
    {
        CreateRangeDisplay();
    }

    private void OnMouseDown()
    {
        if (!b_isPlaceable || !enemyObjectPoolHandler.B_WaveCleared) return;
        Instantiate(towerPrefab, transform.position, Quaternion.identity, _towerParent.transform);
        cellType = CellType.Tower;
        _sphere.SetActive(false);
    }

    private void OnMouseOver()
    {
        if (!b_isPlaceable  || !enemyObjectPoolHandler.B_WaveCleared) return;
        _sphere.SetActive(_bIsInBuildMode);
    }

    private void OnMouseExit()
    {
        _sphere.SetActive(false);
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

    private void CreateRangeDisplay()
    {
        // TODO Range and damage in sperate data class or scriptable object?
        // TODO Remove the following testing line
        // diameter = 2 x radius (sphere collider from tower prefab)
        _currentTowerPrefabRange = towerPrefab.GetComponent<TowerController>().Range * 2f;

        float sphereYScale = _currentTowerPrefabRange / 10f;
        const float sphereYTranlateFactor = 1f;

        _sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _sphere.transform.localScale = new Vector3(_currentTowerPrefabRange, sphereYScale, _currentTowerPrefabRange);
        _sphere.SetActive(false);
        _sphere.transform.position = transform.position;
        _sphere.transform.Translate(Vector3.up * sphereYTranlateFactor);
        _sphere.GetComponent<Collider>().enabled = false;
        var renderer = _sphere.GetComponent<Renderer>();
        renderer.material = rangeDisplayMaterial;
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        _sphere.transform.SetParent(transform);
    }

    private void CreateParentGameObject()
    {
        GameObject newParent = new GameObject();
        newParent.name = towerParentTag;
        newParent.tag = towerParentTag;
        _towerParent = newParent;
    }
}




