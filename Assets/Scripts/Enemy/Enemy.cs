using UnityEngine;
public enum EnemyType
{
    BasicEnemy,
    FlyingEnemy,
    BigEnemy
}
[System.Serializable]
public class Enemy
{
    [field: SerializeField] public EnemyType EnemyType { get; private set; }
    [field: SerializeField] public int MaxHealth { get; private set; }
    [field: SerializeField] public int Reward { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float Altitude { get; private set; }
    [field: SerializeField] public float InitialAmountInObjectPool { get; private set; } = 5;
    [field: SerializeField] public GameObject Prefab { get; private set; }

}
