using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TowerInstantiationManager : MonoBehaviour
{
    public Action OnSelectNewTowerType;
    public Tower SelectedTower { get; private set; }
    [SerializeField] private List<Tower> towers = new();

    void Start()
    {
        //TODO testing | Select tower with UI;
        SelectedTower = towers[1];
        // StartCoroutine(Testing());
    }

    public void ChangeSelectetTower(int index)
    {
        SelectedTower = towers[index];
        OnSelectNewTowerType?.Invoke();
    }

    // TODO Remove
    IEnumerator Testing()
    {
        yield return new WaitForSeconds(10f);
        print("TowerChange");
        ChangeSelectetTower(0);
    }
}
