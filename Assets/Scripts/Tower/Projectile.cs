using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField][Range(0, 200)] private int damage = 50;
    [SerializeField] private float maxFlightDuration = 4f;
    [SerializeField] private bool _bIsHoming = true;
    private Health enemy;

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
            enemyHealth.GetDamage(damage);
        }

        Destroy(gameObject);
    }

    public void SetTarget(Health enemy) => this.enemy = enemy;
}
