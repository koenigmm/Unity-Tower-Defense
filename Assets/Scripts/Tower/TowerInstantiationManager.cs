using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInstantiationManager : MonoBehaviour
{
    // public Tower SelectedTower {get => selectedTower;}
    [SerializeField] private List<Tower> towers = new();
    public Tower SelectedTower { get; private set; }

    void Start()
    {
        //TODO testing | Select tower with UI;
        SelectedTower = towers[1];
    }
}
