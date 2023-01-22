using UnityEngine;
using Unity.Collections;

[System.Serializable]
public class Tower
{
    [field: SerializeField] public TowerType TowerClass { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float Range { get; private set; }
    [field: SerializeField] public int BuildCost { get; private set; }
    [field: SerializeField] public GameObject prefab { get; private set; }
}

[System.Serializable]
public enum TowerType
{
    SmallTower,
    BigTower,
    Frost
}
