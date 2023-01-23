using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerController : MonoBehaviour
{
    [SerializeField] private Transform towerTop;
    // [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject launchpoint;
    // [SerializeField] private float range = 30f;

    // public float Range {get => range;}
    private TowerInstantiationManager _towerInstantiationManager;
    private List<Health> _enemies = new();
    private SphereCollider _sphereCollider;
    private Health _closestEnemy;
    private float _timer;
    private bool _bCanShoot = true;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _towerInstantiationManager = FindObjectOfType<TowerInstantiationManager>();
    }

    void Start()
    {
        _sphereCollider.radius = _towerInstantiationManager.SelectedTower.Range;
        if (_closestEnemy == null) return;
    }

    private void Update()
    {
        LookAtTarget();
        FindAndSetClosestTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health enemy))
        {
            _enemies.Add(enemy);
            enemy.OnDie += RemoveEnemyFromList;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Health enemy))
        {
            RemoveEnemyFromList(enemy);
        }
    }

    private void RemoveEnemyFromList(Health enemy)
    {
        if (_closestEnemy == enemy)
        {
            _closestEnemy = null;
        }

        enemy.OnDie -= RemoveEnemyFromList;
        _enemies.Remove(enemy);
        FindAndSetClosestTarget();
    }

    private void FindAndSetClosestTarget()
    {
        if (_enemies.Count == 0) return;

        float closestDistance = Mathf.Infinity;
        Health target = null;

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

    private void LookAtTarget()
    {
        HandleTimer();

        //TODO restrict to y rotation?
        if (_closestEnemy == null) return;
        towerTop.LookAt(_closestEnemy.transform);
        // transform.LookAt(_closestEnemy.transform);

        if (_bCanShoot) FireWeapon();
    }

    private void HandleTimer()
    {
        _timer += Time.deltaTime;

        if (_timer < _towerInstantiationManager.SelectedTower.TimeBetweenAttacks) return;

        _timer = 0f;
        _bCanShoot = true;
    }

    private void FireWeapon()
    {
        GameObject projectile = Instantiate
        (
             _towerInstantiationManager.SelectedTower.Projectile,
             launchpoint.transform.position,
             Quaternion.identity
        );

        var projectileComponent = projectile.GetComponent<Projectile>();

        projectileComponent.SetTarget(_closestEnemy);
        projectileComponent.Damage = _towerInstantiationManager.SelectedTower.Damage;

        _bCanShoot = false;
    }

}

