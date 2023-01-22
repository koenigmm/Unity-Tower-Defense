using System;
using UnityEngine;

public class LivePoints : MonoBehaviour
{
    public Action OnDie, OnDemage;
    public int CurrentLivePoints {get => _currentLivePoints;}
    [SerializeField] private int maxLivePoints = 5;
    private int _currentLivePoints;

    private void Awake()
    {
        _currentLivePoints = maxLivePoints;
    }

    public void DecreaseLivePoints()
    {
        _currentLivePoints--;
        if (_currentLivePoints == 0) OnDie?.Invoke();
        OnDemage?.Invoke();
    }
}
