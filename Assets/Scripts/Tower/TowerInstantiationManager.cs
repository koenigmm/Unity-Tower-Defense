using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TowerInstantiationManager : MonoBehaviour
{
    public Action OnSelectNewTowerType;
    public Tower SelectedTower { get; private set; }
    [SerializeField] private List<Tower> towers = new();
    [HideInInspector] public bool IsInSelectionMode;

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
}
