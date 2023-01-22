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
        //TODO testing | Select tower with UI elements;
        SelectedTower = towers[1];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
