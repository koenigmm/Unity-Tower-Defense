using UnityEngine;
using Unity.Collections;

[System.Serializable]
public class Tower
{
    [field: SerializeField] public TowerType TowerClass { get; private set; }
    [field: SerializeField] public bool B_canCauseDamage { get; private set; } = true;
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public int BuildCost { get; private set; }
    [field: SerializeField] public float Range { get; private set; }
    [field: SerializeField] public float TimeBetweenAttacks { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public GameObject Projectile { get; private set; }
}

[System.Serializable]
public enum TowerType
{
    SmallTower,
    BigTower,
    Frost
}
