using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerController : MonoBehaviour
{
    [SerializeField] private Transform towerTop;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float range = 30f;
    [SerializeField] private List<Enemy> enemies = new();
    private SphereCollider _sphereCollider;

    // Target
    [SerializeField] private Enemy closestEnemy;

    private void Awake()
    {
        // enemyHealth = FindObjectOfType<Health>();
        _sphereCollider = GetComponent<SphereCollider>();
    }

    void Start()
    {
        _sphereCollider.radius = range;
        if (closestEnemy == null) return;
        // StartCoroutine(FireWeapon());

    }

    // Update is called once per frame
    void Update()
    {
        AimWeapon();
        FindClosestTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemies.Add(enemy);
            enemy.OnDestroyed += RemoveEnemyFromList;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemies.Remove(enemy);
        }
    }

    private void RemoveEnemyFromList(Enemy enemy)
    {
        if (closestEnemy == enemy)
        {
            closestEnemy = null;
        }

        enemy.OnDestroyed -= RemoveEnemyFromList;
        enemies.Remove(enemy);
        FindClosestTarget();
    }


    private void CleanEnemyHealthList()
    {
        foreach (var enemy in enemies)
        {
            if (enemy == null)
            {
                enemies.Remove(enemy);
                FindClosestTarget();
            }
        }
    }

    private void FindClosestTarget()
    {
        if (enemies.Count == 0) return;
        
        float closestDistance = Mathf.Infinity;
        Enemy target = null;

        foreach (var enemy in enemies)
        {
            float enemyDistance = Vector3.Distance(enemy.transform.position, transform.position);
            if (enemyDistance < closestDistance)
            {
                closestDistance = enemyDistance;
                target = enemy;
            }
        }
        closestEnemy = target; ;
    }

    private void AimWeapon()
    {
        //TODO restrict to y rotation?
        if (closestEnemy == null) return;
        towerTop.LookAt(closestEnemy.transform);
    }

    private IEnumerator FireWeapon()
    {
        int counter = 0;

        while (counter < 4)
        {
            if (closestEnemy == null) break;
            GameObject projectile = Instantiate(projectilePrefab, towerTop.transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().SetTarget(closestEnemy);
            yield return new WaitForSeconds(timeBetweenAttacks);
            counter++;
        }

    }
}
