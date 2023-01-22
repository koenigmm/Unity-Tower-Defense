using UnityEngine;
using TMPro;

public class LivePointsDisplay : MonoBehaviour
{
    [SerializeField] private string baseMessage = "Live Points: ";
    private LivePoints _livePoints;
    private TextMeshProUGUI _liveTextField;

    private void Awake()
    {
        _livePoints = GameObject.FindObjectOfType<LivePoints>();
        _liveTextField = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        _livePoints.OnDemage += HandleDamage;
    }

    private void Start()
    {
        HandleDamage();
    }

    private void OnDisable()
    {
        _livePoints.OnDemage -= HandleDamage;
    }

    private void HandleDamage()
    {
        _liveTextField.text = $"{baseMessage} {_livePoints.CurrentLivePoints}";
    }
}
