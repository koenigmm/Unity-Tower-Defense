using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingCostDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textElement;
    [SerializeField] private TowerType towerType;
    [SerializeField] private string baseMessage = "Gold: ";
    private TowerInstantiationManager _towerInstantiationManager;

    private void Awake()
    {
        _towerInstantiationManager = GameObject.FindObjectOfType<TowerInstantiationManager>();
        if (_textElement != null) return;
        _textElement = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _textElement.text = $"{baseMessage} {_towerInstantiationManager.GetBuildCost(towerType).ToString()}";
    }
}
