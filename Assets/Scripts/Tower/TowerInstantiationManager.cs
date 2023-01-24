using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TowerInstantiationManager : MonoBehaviour
{
    public Action OnSelectNewTowerType;
    public Tower SelectedTower { get; private set; }
    [HideInInspector] public bool IsInSelectionMode;

    [SerializeField] private List<Tower> towers = new();
    [SerializeField] private string towerParentTag = "towerparent";
    private GameObject _towerParent;
    private Gold _gold;

    public void ChangeSelectetTower(int index)
    {
        IsInSelectionMode = true;
        SelectedTower = towers[index];
        OnSelectNewTowerType?.Invoke();
    }

    public int GetBuildCost(TowerType towerType)
    {
        foreach (var tower in towers)
        {
            if (tower.TowerClass == towerType) return tower.BuildCost;
        }

        return 0;
    }

    public GameObject GetProjectile(TowerType towerType)
    {
        foreach (var tower in towers)
        {
            if (tower.TowerClass == towerType) return tower.Projectile;
        }

        return null;
    }

    public void BuildTower(Vector3 position)
    {
        var tower = Instantiate(SelectedTower.Prefab, position, Quaternion.identity);
        tower.transform.parent = _towerParent.transform;
        tower.GetComponent<TowerController>().InitializeTowerValues(SelectedTower.TowerClass);
        _gold.DecreaseAmountOfGold(SelectedTower.BuildCost);
        IsInSelectionMode = false;
    }

    private void Awake()
    {
        _gold = GameObject.FindObjectOfType<Gold>();
        if (_towerParent != null) return;
        CreateParentGameObject();
    }

    private void CreateParentGameObject()
    {
        _towerParent = GameObject.FindGameObjectWithTag(towerParentTag);
        GameObject newParent = new GameObject();
        newParent.name = towerParentTag;
        newParent.tag = towerParentTag;
        _towerParent = newParent;
    }
}
