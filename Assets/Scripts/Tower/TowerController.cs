using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour
{
    [SerializeField] private Transform towerTop;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private GameObject projectilePrefab;
    // [SerializeField] private float range = 20f;

    // Target
    [SerializeField] private Health enemyHealth;
    // Testing
    private int maxShots = 4;

    private void Awake()
    {
        enemyHealth = FindObjectOfType<Health>();
    }

    void Start()
    {
        if (enemyHealth == null) return;
        StartCoroutine(FireWeapon());
    }

    // Update is called once per frame
    void Update()
    {
        AimWeapon();
    }

    private void AimWeapon()
    {
        //TODO restrict to y rotation?
        if (enemyHealth == null) return;
        towerTop.LookAt(enemyHealth.transform);
    }

    private IEnumerator FireWeapon()
    {
        int counter = 0;

        while (counter < maxShots)
        {
            if (enemyHealth == null) break;
            GameObject projectile = Instantiate(projectilePrefab, towerTop.transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().SetTarget(enemyHealth);
            yield return new WaitForSeconds(timeBetweenAttacks);
            counter++;
        }

    }
}
