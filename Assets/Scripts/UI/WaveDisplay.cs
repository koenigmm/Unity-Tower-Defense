using UnityEngine;
using TMPro;

public class WaveDisplay : MonoBehaviour
{
    [SerializeField] private EnemyObjectPoolHandler enemyObjectPoolHandler;
    [SerializeField] private TextMeshProUGUI waveDisplayCounterText;
    [SerializeField] private TextMeshProUGUI waveStatusText;

    private void Awake()
    {
        if (enemyObjectPoolHandler != null) return;
        enemyObjectPoolHandler = FindObjectOfType<EnemyObjectPoolHandler>();
    }

    private void Start() => UpdateWaveDisplay();

    private void Update()
    {

    }

    private void OnEnable() => enemyObjectPoolHandler.OnWavePhase += UpdateWaveDisplay;

    private void OnDisable() => enemyObjectPoolHandler.OnWavePhase -= UpdateWaveDisplay;

    private void UpdateWaveDisplay()
    {
        waveDisplayCounterText.text = enemyObjectPoolHandler.Wave.ToString();

        if (enemyObjectPoolHandler.B_WaveCleared)
        {
            waveStatusText.text = "Build Phase";
        }
        else
        {
            waveStatusText.text = "Wave Phase";
        }
    }
}
