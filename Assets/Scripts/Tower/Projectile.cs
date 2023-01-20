using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private MoverForSimplePaths enemyMoverForSimplePaths;
    [SerializeField] private float speed;
    [SerializeField] private float maxFlightDuration = 4f;
    [SerializeField] private bool _bIsHoming = true;

    private void Start()
    {
        Destroy(gameObject, maxFlightDuration);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        if (!_bIsHoming) return;
        transform.LookAt(enemyMoverForSimplePaths.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        float collisionDestroyTime = 1.5f;
        Destroy(gameObject, collisionDestroyTime);

        Debug.Log("Collision" + collision.transform.name);
    }
}
