using UnityEngine;

public class Gold : MonoBehaviour
{
    // public int AmountOfGold { get => _amountOfGold; }
    [SerializeField] private int initialAmountOfGold = 50;
    private int _amountOfGold;

    private void Awake()
    {
        _amountOfGold = initialAmountOfGold;
    }

    public void IncreaseAmountOfGold(int amountOfGold)
    {
        _amountOfGold += Mathf.Abs(amountOfGold);
    }

    public void DecreaseAmountOfGold(int amountOfGold)
    {
        _amountOfGold -= Mathf.Abs(amountOfGold);
    }

    public bool CanBuildWithCurrentMoney(int buildingCost) => buildingCost <= _amountOfGold;
}
