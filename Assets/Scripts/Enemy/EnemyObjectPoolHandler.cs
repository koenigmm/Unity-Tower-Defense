using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPoolHandler : MonoBehaviour
{
    public Action OnWaveCleared;
    public int Wave { get => wave; }
    public bool _bShouldStartWave = true;
    [SerializeField] private int defeatedEnemies;
    [SerializeField] private float timeBetwwenEnemySpawn = 2f;
    private int _amountOfEnemiesInPool;
    private int wave = 1;
    private List<Health> enemies = new();
    private bool _bWaveCleard;

    void Awake()
    {
        var enemiesInChildren = GetComponentsInChildren<Health>();

        foreach (var enemy in enemiesInChildren)
        {
            enemies.Add(enemy);
            enemy.gameObject.SetActive(false);
        }

        _amountOfEnemiesInPool = enemies.Count;
    }

    private void Update()
    {
        if (!_bShouldStartWave)
            return;

        StartCoroutine(StartWave());
    }

    void OnDisable() => RemoveAllEventListeners();

    IEnumerator StartWave()
    {
        _bWaveCleard = false;
        _bShouldStartWave = false;
        var counter = 0;
        
        while (counter < wave && wave < _amountOfEnemiesInPool)
        {
            if (_bWaveCleard)
                break;

            var currentEnemy = enemies[counter];
            currentEnemy.gameObject.SetActive(true);
            currentEnemy.OnDie += HandleDeath;
            yield return new WaitForSeconds(timeBetwwenEnemySpawn);
            counter++;
        }

    }

    void HandleDeath(Health health)
    {
        health.OnDie -= HandleDeath;
        defeatedEnemies++;

        if (defeatedEnemies == wave)
            _bWaveCleard = true;

        Debug.Log("handle death");

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
        print("wave cleared | testing");
        defeatedEnemies = 0;
        wave++;
        OnWaveCleared?.Invoke();
    }
}