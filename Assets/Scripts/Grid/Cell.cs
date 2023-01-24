using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Cell : MonoBehaviour
{
    
    [SerializeField][FormerlySerializedAs("cellType")] CellType initialCellType;
    [SerializeField] string towerParentTag;
    [SerializeField] Material rangeDisplayMaterial;

    private bool _bIsPlaceable = true;
    public bool _bIsStart { get => initialCellType == CellType.Start; }

    private GameObject _towerParent;
    private GameObject _sphere;
    private float _currentTowerPrefabRange;
    private EnemyWavePool enemyObjectPoolHandler;
    private TowerInstantiationManager _towerInstantiationManager;
    private Gold _gold;
    private Grid _grid;
    private Vector2Int _coordinatesFromGrid = Vector2Int.zero;

    private void Awake()
    {
        SetReferences();
        _coordinatesFromGrid = _grid.GetCoordinatesFromPosition(transform.position);
        _grid.ChangeCellType(_coordinatesFromGrid, initialCellType);
        _bIsPlaceable = _grid.CanBuildOnCell(_coordinatesFromGrid);

        if (_towerParent != null) return;
        CreateParentGameObject();
    }

    private void Start() => CreateRangeDisplay();

    private void OnEnable() => _towerInstantiationManager.OnSelectNewTowerType += SetRangeDisplayScale;

    private void OnDisable() => _towerInstantiationManager.OnSelectNewTowerType -= SetRangeDisplayScale;

    private void OnMouseDown()
    {
        //TODO clean up |CanBuild?
        if (!_bIsPlaceable || !enemyObjectPoolHandler.B_WaveCleared) return;
        if (!_towerInstantiationManager.IsInSelectionMode) return;
        if (!_gold.CanBuildWithCurrentMoney(_towerInstantiationManager.SelectedTower.BuildCost)) return;
        BuildTower();
    }

    private void BuildTower()
    {
        var tower = Instantiate(_towerInstantiationManager.SelectedTower.Prefab, transform.position, Quaternion.identity);
        tower.transform.parent = _towerParent.transform;
        tower.GetComponent<TowerController>().InitializeTowerValues(_towerInstantiationManager.SelectedTower.TowerClass);
        _gold.DecreaseAmountOfGold(_towerInstantiationManager.SelectedTower.BuildCost);
        _towerInstantiationManager.IsInSelectionMode = false;
        SetCellTypeToTower();
        _sphere.SetActive(false);
    }

    private void OnMouseOver()
    {
        //TODO clean up | CanBuild?
        if (!_bIsPlaceable || !enemyObjectPoolHandler.B_WaveCleared) return;
        if (!_towerInstantiationManager.IsInSelectionMode) return;
        _sphere.SetActive(_towerInstantiationManager.IsInSelectionMode);
    }

    private void OnMouseExit() => _sphere.SetActive(false);

    private void SetCellTypeToTower()
    {
        // if (cellType != CellType.Grass)
        // {
        //     Debug.LogWarning("Only grass cells can be changed");
        //     return;
        // }

        // cellType = CellType.Tower;

        // TODO Testing
        _grid.ChangeCellType(_coordinatesFromGrid, CellType.Tower);
        _bIsPlaceable = false;
    }

    private void SetReferences()
    {
        _towerParent = GameObject.FindGameObjectWithTag(towerParentTag);
        enemyObjectPoolHandler = GameObject.FindObjectOfType<EnemyWavePool>();
        _towerInstantiationManager = GameObject.FindObjectOfType<TowerInstantiationManager>();
        _gold = GameObject.FindObjectOfType<Gold>();
        _grid = GameObject.FindObjectOfType<Grid>();
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