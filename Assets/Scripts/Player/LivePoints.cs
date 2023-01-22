using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivePoints : MonoBehaviour
{
    //TODO OnDie obsolet?
    public Action OnDie, OnDemage;
    public int CurrentLivePoints { get => _currentLivePoints; }
    [SerializeField] private int maxLivePoints = 5;
    [SerializeField] private int gameOverSceneIndex = 1;
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
            Debug.Log("Game Over");
            OnDie?.Invoke();
            SceneManager.LoadScene(gameOverSceneIndex);
        }
        OnDemage?.Invoke();
    }
}
