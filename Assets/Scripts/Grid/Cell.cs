using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Cell : MonoBehaviour
{

    public bool _bIsStart { get => initialCellType == CellType.Start; }
    [SerializeField][FormerlySerializedAs("cellType")] private CellType initialCellType;
    [SerializeField] private Material rangeDisplayMaterial;
    [SerializeField] private Material blockedGrassMaterial;

    // private bool _bIsPlaceable = true;
    private GameObject _sphere;
    private float _currentTowerPrefabRange;
    private EnemyWavePool enemyObjectPoolHandler;
    private TowerInstantiationManager _towerInstantiationManager;
    private Gold _gold;
    private Grid _grid;
    private Vector2Int _coordinatesFromGrid = Vector2Int.zero;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        SetReferences();
    }

    private void Start()
    {
        _coordinatesFromGrid = _grid.GetCoordinatesFromPosition(transform.position);
        _grid.ChangeCellType(_coordinatesFromGrid, initialCellType);
        CreateRangeDisplay();
    }

    private void OnEnable()
    {
        _grid.OnCellTypeChange += HandleCellTypeChange;
        _towerInstantiationManager.OnSelectNewTowerType += SetRangeDisplayScale;
    }

    private void OnDisable()
    {
        _grid.OnCellTypeChange -= HandleCellTypeChange;
        _towerInstantiationManager.OnSelectNewTowerType -= SetRangeDisplayScale;
    }

    private void OnMouseDown()
    {
        //TODO clean up |CanBuild?
        if (!_grid.CanBuildOnCell(_coordinatesFromGrid) || !enemyObjectPoolHandler.B_WaveCleared) return;
        if (!_towerInstantiationManager.IsInSelectionMode) return;
        if (!_gold.CanBuildWithCurrentMoney(_towerInstantiationManager.SelectedTower.BuildCost)) return;

        var requiredCells = _towerInstantiationManager.SelectedTower.RequiredCellsForBuilding;
        if (requiredCells.x > 1 || requiredCells.y > 1)
        {
            bool isAreaBlocked = _grid.IsAreaBlocked(transform.position, requiredCells.x, requiredCells.y);
            
            if (isAreaBlocked)
                return;
        }

        HandleTowerBuilding();
    }

    private void HandleTowerBuilding()
    {
        _towerInstantiationManager.BuildTower(transform.position);
        _grid.ChangeCellType(_coordinatesFromGrid, CellType.Tower);
        // _bIsPlaceable = false;
        _meshRenderer.material = blockedGrassMaterial;
    }

    private void HandleCellTypeChange(Vector2Int coordinates)
    {
        var changedPositionFromGrid = _grid.GetPositionFromCoordinates(coordinates);
        if (transform.position == changedPositionFromGrid )
        {
            _meshRenderer.material = blockedGrassMaterial;
        }
    }

    private void OnMouseOver()
    {
        //TODO clean up | CanBuild?
        if ((!_grid.CanBuildOnCell(_coordinatesFromGrid)) || !enemyObjectPoolHandler.B_WaveCleared) return;
        if (!_towerInstantiationManager.IsInSelectionMode) return;
        _sphere.SetActive(_towerInstantiationManager.IsInSelectionMode);
    }

    private void OnMouseExit() => _sphere.SetActive(false);

    private void SetReferences()
    {

        enemyObjectPoolHandler = GameObject.FindObjectOfType<EnemyWavePool>();
        _towerInstantiationManager = GameObject.FindObjectOfType<TowerInstantiationManager>();
        _gold = GameObject.FindObjectOfType<Gold>();
        _grid = GameObject.FindObjectOfType<Grid>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
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
}