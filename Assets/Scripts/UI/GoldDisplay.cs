using UnityEngine;
using TMPro;

public class GoldDisplay : MonoBehaviour
{
    [SerializeField] string baseMessageForDisplay = "Gold: ";
    private Gold _gold;
    private TextMeshProUGUI _textField;

    private void Awake()
    {
        _gold = FindObjectOfType<Gold>();
        _textField = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        HandleValueChange();
    }

    private void OnEnable()
    {
        _gold.OnValueChange += HandleValueChange;
    }

    private void OnDisable()
    {
        _gold.OnValueChange -= HandleValueChange;
    }

    private void HandleValueChange()
    {
        _textField.text = $"{baseMessageForDisplay} {_gold.AmountOfGold}";
    }
}
