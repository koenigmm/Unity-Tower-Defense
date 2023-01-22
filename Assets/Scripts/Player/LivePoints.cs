using System;
using UnityEngine;

public class LivePoints : MonoBehaviour
{
    public Action OnDie, OnDemage;
    public int CurrentLivePoints { get => _currentLivePoints; }
    [SerializeField] private int maxLivePoints = 5;
    private int _currentLivePoints;

    private void Awake()
    {
        _currentLivePoints = maxLivePoints;
    }

    public void DecreaseLivePoints()
    {
        _currentLivePoints = Mathf.Max(0, _currentLivePoints - 1);
        if (_currentLivePoints == 0)
        {
            //TODO Enable game over screen
            Debug.Log("Game Over");
            OnDie?.Invoke();
        }
        OnDemage?.Invoke();
    }
}
