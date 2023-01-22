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
    private EnemyObjectPoolHandler _enemyObjectPoolHandler;
    private Image _buttonImage;

    private void Awake()
    {
        _enemyObjectPoolHandler = FindObjectOfType<EnemyObjectPoolHandler>();
        _buttonImage = GetComponent<Image>();
        _buttonTextElement = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _enemyObjectPoolHandler.OnWavePhase += HandleWavePhase;
        _enemyObjectPoolHandler.OnBuildPhase += HandleBuildPhase;
        // HandleWavePhase();
    }

    private void OnDisable()
    {
        _enemyObjectPoolHandler.OnWavePhase -= HandleWavePhase;
        _enemyObjectPoolHandler.OnBuildPhase -= HandleBuildPhase;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleButtonClick()
    {
        if (_enemyObjectPoolHandler.TryReleaseNewWave())
        {
            print("pressed");
            _buttonImage.color = Color.red;
            _buttonTextElement.text = wavePhaseText;
        }
    }

    private void HandleWavePhase()
    {

        print("Wave color");
        _buttonImage.color = wavePhaseColor;
        _buttonTextElement.text = wavePhaseText;

    }

    private void HandleBuildPhase()
    {

        print("build color");
        _buttonTextElement.text = buildPhaseText;
        _buttonImage.color = buildPhaseColor;

    }
}
