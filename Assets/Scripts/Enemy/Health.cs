using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<Health> OnDie;
    public Action OnDefeat;
    [SerializeField] int maxHealth = 100;
    private int _currentHealth;

    private void Awake() => _currentHealth = maxHealth;

    private void OnEnable() => _currentHealth = maxHealth;

    private void OnDisable() => OnDie?.Invoke(this);

    public void GetDamage(int damage)
    {
        int lowestPossibleHealthPoints = 0;
        _currentHealth = Mathf.Max(lowestPossibleHealthPoints, _currentHealth - damage);

        if (_currentHealth == 0)
            HandleDefeat();
    }

    private void HandleDefeat()
    {
        OnDefeat?.Invoke();
        gameObject.SetActive(false);
    }
}
