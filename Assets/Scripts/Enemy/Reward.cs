using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    private Health _healthComponent;
    private Gold _gold;
    
    private void Awake()
    {
        _healthComponent = GetComponent<Health>();
        _gold = GameObject.FindObjectOfType<Gold>();    
    }

    private void OnEnable()
    {
        _healthComponent.OnDie += RewardPlayer;
    }

    private void OnDisable()
    {
        _healthComponent.OnDie -= RewardPlayer;
    }

    private void RewardPlayer(Health health)
    {
        _gold.IncreaseAmountOfGold(10);
    }
}
