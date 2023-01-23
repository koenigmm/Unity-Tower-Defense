using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingCostDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textElement;
    [SerializeField] private TowerType towerType;
    private TowerInstantiationManager _towerInstantiationManager;

    private void Awake()
    {
        _towerInstantiationManager = GameObject.FindObjectOfType<TowerInstantiationManager>();
        if (_textElement != null) return;
        _textElement = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _textElement.text = _towerInstantiationManager.GetBuildCost(towerType).ToString();
    }
}
