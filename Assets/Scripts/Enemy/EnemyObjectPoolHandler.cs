using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPoolHandler : MonoBehaviour
{
    public Action OnWavePhase, OnBuildPhase;
    public int Wave { get => wave; }
    public bool B_WaveCleared { get => _bWaveCleard; }
    [SerializeField] private int defeatedEnemies;
    [SerializeField] private float timeBetwwenEnemySpawn = 2f;
    [SerializeField] private List<Enemy> enemyTypeList = new();
    private int _amountOfEnemiesInPool;
    private int wave = 1;
    private List<Health> enemies = new();
    private bool _bWaveCleard = true;
    private bool _bShouldStartWave;

    void Awake()
    {
        FillPool();
    }

    private void FillPool()
    {
        foreach (var enemy in enemyTypeList)
        {
            for (int i = 0; i < enemy.InitialAmountInObjectPool; i++)
            {
                var currentEnemyGameObject = Instantiate(enemy.Prefab);
                currentEnemyGameObject.SetActive(false);
                currentEnemyGameObject.transform.parent = transform;
                var currentEnemyHealthComponent = currentEnemyGameObject.GetComponent<Health>();
                enemies.Add(currentEnemyHealthComponent);
            }
        }
        _amountOfEnemiesInPool = enemies.Count;
    }

    private void Update()
    {
        if (!_bShouldStartWave)
            return;

        StartCoroutine(StartWave());
        print("phase change");
    }

    void OnDisable() => RemoveAllEventListeners();

    public bool TryReleaseNewWave()
    {
        if (!B_WaveCleared) return false;
        _bShouldStartWave = true;
        return true;
    }

    IEnumerator StartWave()
    {
        _bWaveCleard = false;
        _bShouldStartWave = false;
        var counter = 0;
        OnWavePhase?.Invoke();

        while (counter < wave)
        {
            if (_bWaveCleard)
                break;

            var currentEnemy = enemies[counter];
            currentEnemy.gameObject.SetActive(true);
            currentEnemy.OnDie += HandleDeath;
            yield return new WaitForSeconds(timeBetwwenEnemySpawn);
            counter++;
        }

        if (counter == _amountOfEnemiesInPool) 
            FillPool();

    }

    void HandleDeath(Health health)
    {
        health.OnDie -= HandleDeath;
        defeatedEnemies++;

        if (defeatedEnemies == wave)
            _bWaveCleard = true;

        if (_bWaveCleard)
            HandleClearedWave();
    }

    void RemoveAllEventListeners()
    {
        foreach (var enemy in enemies)
        {
            enemy.OnDie -= HandleDeath;
        }
    }

    private void HandleClearedWave()
    {
        defeatedEnemies = 0;
        wave++;
        OnBuildPhase?.Invoke();
    }
}
