using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerController : MonoBehaviour
{
    [SerializeField] private Transform towerTop;
    [SerializeField] private GameObject launchpoint;
    private TowerInstantiationManager _towerInstantiationManager;
    private List<Health> _enemiesInRange = new();
    private SphereCollider _sphereCollider;
    private Health _closestEnemy;
    private float _timer;
    private bool _bCanShoot = true;

    // From instantiation manager
    private TowerType _towerType = TowerType.SmallTower;
    private bool _bCanCauseDamage;
    private GameObject _projectile;
    private float _timeBetweenAtacks;
    private int _damage;


    // Public functions
    public void InitializeTowerValues(TowerType towerType)
    {
        _towerType = towerType;
        _projectile = _towerInstantiationManager.GetProjectile(_towerType);
        _bCanCauseDamage = _towerInstantiationManager.SelectedTower.B_canCauseDamage;
        _timeBetweenAtacks = _towerInstantiationManager.SelectedTower.TimeBetweenAttacks;
        _damage = _towerInstantiationManager.SelectedTower.Damage;
        _sphereCollider.radius = _towerInstantiationManager.SelectedTower.Range;
    }

    // Unity event functions
    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _towerInstantiationManager = FindObjectOfType<TowerInstantiationManager>();
    }

    private void Start()
    {
        // InitializeTowerValues(_towerType);
    }

    private void Update()
    {
        LookAtTarget();
        FindAndSetClosestTarget();
    }

    void OnEnable()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health enemy))
        {
            _enemiesInRange.Add(enemy);
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
        _enemiesInRange.Remove(enemy);
        FindAndSetClosestTarget();
    }

    private void FindAndSetClosestTarget()
    {
        if (_enemiesInRange.Count == 0) return;

        float closestDistance = Mathf.Infinity;
        Health target = null;

        foreach (var enemy in _enemiesInRange)
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
             _projectile,
             launchpoint.transform.position,
             Quaternion.identity
        );

        var projectileComponent = projectile.GetComponent<Projectile>();

        projectileComponent.SetProjectileValues(_closestEnemy, _bCanCauseDamage);
        projectileComponent.Damage = _towerInstantiationManager.SelectedTower.Damage;

        _bCanShoot = false;
    }

}

