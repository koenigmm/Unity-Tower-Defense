using UnityEngine;
using System;

public class Gold : MonoBehaviour
{
    public int AmountOfGold { get => _amountOfGold; }
    public Action OnValueChange;
    [SerializeField] private int initialAmountOfGold = 50;
    private int _amountOfGold;

    private void Awake()
    {
        _amountOfGold = initialAmountOfGold;
    }

    public void IncreaseAmountOfGold(int amountOfGold)
    {
        _amountOfGold += Mathf.Abs(amountOfGold);
        OnValueChange?.Invoke();
    }

    public void DecreaseAmountOfGold(int amountOfGold)
    {
        _amountOfGold -= Mathf.Abs(amountOfGold);
        OnValueChange?.Invoke();
    }

    public bool CanBuildWithCurrentMoney(int buildingCost) => buildingCost <= _amountOfGold;
}
