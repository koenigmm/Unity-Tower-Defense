using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField] private int reward = 10;
    private Health _healthComponent;
    private Gold _gold;
    
    private void Awake()
    {
        _healthComponent = GetComponent<Health>();
        _gold = GameObject.FindObjectOfType<Gold>();    
    }

    private void OnEnable() => _healthComponent.OnDefeat += RewardPlayer;

    private void OnDisable() => _healthComponent.OnDefeat -= RewardPlayer;

    private void RewardPlayer() => _gold.IncreaseAmountOfGold(reward);
}
