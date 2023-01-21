using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerController : MonoBehaviour
{
    [SerializeField] private Transform towerTop;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float range = 30f;
    // Testing
    public float Range {get => range;}
    private List<Enemy> _enemies = new();
    private SphereCollider _sphereCollider;
    private Enemy _closestEnemy;
    private float _timer;
    private bool _bCanShoot = true;

    private void Awake()
    {
        // enemyHealth = FindObjectOfType<Health>();
        _sphereCollider = GetComponent<SphereCollider>();
    }

    void Start()
    {
        _sphereCollider.radius = range;
        if (_closestEnemy == null) return;
    }

    // Update is called once per frame
    void Update()
    {
        AimWeapon();
        FindAndSetClosestTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _enemies.Add(enemy);
            enemy.OnDestroyed += RemoveEnemyFromList;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            RemoveEnemyFromList(enemy);
        }
    }

    private void RemoveEnemyFromList(Enemy enemy)
    {
        if (_closestEnemy == enemy)
        {
            _closestEnemy = null;
        }

        enemy.OnDestroyed -= RemoveEnemyFromList;
        _enemies.Remove(enemy);
        FindAndSetClosestTarget();
    }

    private void FindAndSetClosestTarget()
    {
        if (_enemies.Count == 0) return;

        float closestDistance = Mathf.Infinity;
        Enemy target = null;

        foreach (var enemy in _enemies)
        {
            float enemyDistance = Vector3.Distance(enemy.transform.position, transform.position);
            if (enemyDistance < closestDistance)
            {
                closestDistance = enemyDistance;
                target = enemy;
            }
        }
        _closestEnemy = target;
    }

    private void AimWeapon()
    {
        HandleTimer();
        //TODO restrict to y rotation?
        if (_closestEnemy == null) return;
        towerTop.LookAt(_closestEnemy.transform);

        if (_bCanShoot) FireWeapon();
    }

    private void HandleTimer()
    {
        _timer += Time.deltaTime;

        if (_timer < timeBetweenAttacks) return;

        _timer = 0f;
        _bCanShoot = true;
    }

    private void FireWeapon()
    {
        GameObject projectile = Instantiate(projectilePrefab, towerTop.transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetTarget(_closestEnemy);
        _bCanShoot = false;
    }

}

