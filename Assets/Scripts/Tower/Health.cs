using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void GetDamage(int damage)
    {
        int lowestPossibleHealthPoints = 0;
        _currentHealth = Mathf.Max(lowestPossibleHealthPoints, _currentHealth - damage);

        if (_currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }
}
