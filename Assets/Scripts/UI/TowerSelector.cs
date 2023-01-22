using UnityEngine;

// For Unity Inspector
public class TowerSelector : MonoBehaviour
{
    private TowerInstantiationManager _towerInstantiationManager;

    private void Awake() => _towerInstantiationManager = GameObject.FindObjectOfType<TowerInstantiationManager>();

    public void SelectTower(int index)
    {
        _towerInstantiationManager.ChangeSelectetTower(index);
    }
}
