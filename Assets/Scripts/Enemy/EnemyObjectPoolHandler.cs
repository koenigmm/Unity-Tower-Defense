using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPoolHandler : MonoBehaviour
{
    private int _amountOfEnemies = 0;
    [SerializeField]private int defeatedEnemies;
    private int wave = 5;
    private List<Health> enemies = new();

    void Awake()
    {
        var enemiesInChildren = GetComponentsInChildren<Health>();

        foreach (var enemy in enemiesInChildren)
        {
            enemies.Add(enemy);   
            enemy.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        StartCoroutine(StartWave());
    }

    void OnDisable()
    {
        RemoveAllEventListeners();
    }

    IEnumerator StartWave()
    {
        var counter = 0;
        while (counter < wave)
        {
            var currentEnemy = enemies[counter];
            currentEnemy.gameObject.SetActive(true);
            currentEnemy.OnDie += HandleDeath;
            yield return new WaitForSeconds(2f);
            counter++;
        }
    }

    void HandleDeath(Health health)
    {
        defeatedEnemies++;
        Debug.Log("handle death");
        health.OnDie -= HandleDeath;
    }

    void RemoveAllEventListeners()
    {
        foreach (var enemy in enemies)
        {
            enemy.OnDie -= HandleDeath; 
        }
    }
}
