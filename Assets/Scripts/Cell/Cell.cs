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

    //TODO repair _towerParent
    private GameObject _towerParent;
    private GameObject _sphere;
    private float _currentTowerPrefabRange;
    private EnemyObjectPoolHandler enemyObjectPoolHandler;
    private TowerInstantiationManager _towerInstantiationManager;

    private void Awake()
    {
        _towerParent = GameObject.FindGameObjectWithTag(towerParentTag);
        enemyObjectPoolHandler = GameObject.FindObjectOfType<EnemyObjectPoolHandler>();
        _towerInstantiationManager = GameObject.FindObjectOfType<TowerInstantiationManager>();

        if (_towerParent != null) return;
        CreateParentGameObject();
    }

    private void Start() => CreateRangeDisplay();

    private void OnEnable() => _towerInstantiationManager.OnSelectNewTowerType += SetRangeDisplayScale;

    private void OnDisable() => _towerInstantiationManager.OnSelectNewTowerType -= SetRangeDisplayScale;

    private void OnMouseDown()
    {
        //TODO clean up |CanBuild?
        if (!b_isPlaceable || !enemyObjectPoolHandler.B_WaveCleared) return;
        if (!_towerInstantiationManager.IsInSelectionMode) return;

        Instantiate(_towerInstantiationManager.SelectedTower.Prefab, transform.position, Quaternion.identity);
        _towerInstantiationManager.IsInSelectionMode = false;
        cellType = CellType.Tower;
        _sphere.SetActive(false);
    }

    private void OnMouseOver()
    {
        //TODO clean up | CanBuild?
        if (!b_isPlaceable || !enemyObjectPoolHandler.B_WaveCleared) return;
        if (!_towerInstantiationManager.IsInSelectionMode) return;
        _sphere.SetActive(_towerInstantiationManager.IsInSelectionMode);
    }

    private void OnMouseExit() => _sphere.SetActive(false);

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
        const float sphereYTranlateFactor = 1.5f;
        _sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _sphere.SetActive(false);
        _sphere.transform.position = transform.position;
        _sphere.transform.Translate(Vector3.up * sphereYTranlateFactor);
        _sphere.GetComponent<Collider>().enabled = false;
        var renderer = _sphere.GetComponent<Renderer>();
        renderer.material = rangeDisplayMaterial;
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        _sphere.transform.SetParent(transform);

        SetRangeDisplayScale();
    }

    private void SetRangeDisplayScale()
    {
        // diameter = 2 x radius (sphere collider from tower prefab)
        _currentTowerPrefabRange = _towerInstantiationManager.SelectedTower.Range * 2f;

        const float yScaleDivisor = 5f;
        float sphereYScale = _currentTowerPrefabRange / yScaleDivisor;
        _sphere.transform.localScale = new Vector3(_currentTowerPrefabRange, sphereYScale, _currentTowerPrefabRange);
    }

    private void CreateParentGameObject()
    {
        GameObject newParent = new GameObject();
        newParent.name = towerParentTag;
        newParent.tag = towerParentTag;
        _towerParent = newParent;
    }
}




