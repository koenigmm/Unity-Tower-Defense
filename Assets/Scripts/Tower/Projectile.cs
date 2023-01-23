using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Flying Behavior")]
    [SerializeField] private float speed;
    [SerializeField] private float maxFlightDuration = 4f;
    [SerializeField] private bool _bIsHoming = true;

    [Header("Frost Values")]
    [SerializeField] private float slowingPercentage;
    [SerializeField] private float slowingDuration;
    public int Damage
    {
        get { return _damage; }
        set { _damage = Mathf.Abs(value); }
    }

    private Health enemyHealth;
    private int _damage;
    private bool _bCausesDamage;

    private void Start()
    {
        Destroy(gameObject, maxFlightDuration);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        if (!_bIsHoming) return;
        if (enemyHealth == null) return;
        transform.LookAt(enemyHealth.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out Health collidedEnemy)) return;
        if (collision.transform.CompareTag("Tower")) return;

        if (_bCausesDamage)
        {
            collidedEnemy.GetDamage(_damage);
        }
        else
        {
            var moverComponent = collision.gameObject.GetComponent<MoverAbstract>();
            moverComponent.ReduceSpeedTemporarily(slowingPercentage, slowingDuration);
        }
  
        Destroy(gameObject);

    }

    public void SetProjectileValues(Health enemy, bool canCauseDamage = true)
    {
        this.enemyHealth = enemy;
        _bCausesDamage = canCauseDamage;
    }
}
