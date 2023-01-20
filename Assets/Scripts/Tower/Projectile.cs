using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField][Range(0, 200)] private int damage = 50;
    [SerializeField] private float maxFlightDuration = 4f;
    [SerializeField] private bool _bIsHoming = true;
    [SerializeField]private Health _enemyHealth;

    private void Start()
    {
        Destroy(gameObject, maxFlightDuration);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        if (!_bIsHoming) return;
        transform.LookAt(_enemyHealth.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        float collisionDestroyTime = 1.5f;
        Destroy(gameObject, collisionDestroyTime);

        if (collision.gameObject.TryGetComponent(out Health enemyHealth))
        {
            enemyHealth.GetDamage(damage);
            Destroy(gameObject);
        }
    }
}
