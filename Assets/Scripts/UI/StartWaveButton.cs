using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartWaveButton : MonoBehaviour
{

    [Header("Wave Phase")]
    [SerializeField] private Color wavePhaseColor;
    [SerializeField] private string wavePhaseText = "Wave Phase";

    [Header("Build Phase")]
    [SerializeField] private Color buildPhaseColor;
    [SerializeField] private string buildPhaseText = "Release Wave";

    private TextMeshProUGUI _buttonTextElement;
    private EnemyWavePool _enemyObjectPoolHandler;
    private Image _buttonImage;

    private void Awake()
    {
        _enemyObjectPoolHandler = FindObjectOfType<EnemyWavePool>();
        _buttonImage = GetComponent<Image>();
        _buttonTextElement = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _enemyObjectPoolHandler.OnWavePhase += HandleWavePhase;
        _enemyObjectPoolHandler.OnBuildPhase += HandleBuildPhase;
        HandleBuildPhase();
    }

    private void OnDisable()
    {
        _enemyObjectPoolHandler.OnWavePhase -= HandleWavePhase;
        _enemyObjectPoolHandler.OnBuildPhase -= HandleBuildPhase;
    }

    public void HandleButtonClick()
    {
        if (!_enemyObjectPoolHandler.TryReleaseNewWave()) return;

        _buttonImage.color = Color.red;
        _buttonTextElement.text = wavePhaseText;
    }

    private void HandleWavePhase()
    {
        _buttonImage.color = wavePhaseColor;
        _buttonTextElement.text = wavePhaseText;

    }

    private void HandleBuildPhase()
    {
        _buttonTextElement.text = buildPhaseText;
        _buttonImage.color = buildPhaseColor;

    }
}
