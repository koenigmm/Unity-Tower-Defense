using UnityEngine;

enum ProjectileType
{
    Projectile,
    Rocket,
    FrostProjectile
}

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileType projectileType;
    [SerializeField] private float speed;
    public int Damage
    {
        get { return _damage; }
        set { _damage = Mathf.Abs(value); }
    }
    [SerializeField] private float maxFlightDuration = 4f;
    [SerializeField] private bool _bIsHoming = true;
    private Health enemy;
    private int _damage;
    private bool _bCausesDamage;

    private void Awake() =>
         _bCausesDamage = projectileType != ProjectileType.FrostProjectile;

    private void Start()
    {
        Destroy(gameObject, maxFlightDuration);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        if (!_bIsHoming) return;
        if (enemy == null) return;
        transform.LookAt(enemy.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Health enemyHealth))
        {
            // if (_bCausesDamage) enemyHealth.GetDamage(_damage);
            collision.gameObject.GetComponent<MoverAbstract>().ReduceSpeedTemporarily(50f, 5);
        }

        Destroy(gameObject);
    }

    public void SetTarget(Health enemy) => this.enemy = enemy;
}
