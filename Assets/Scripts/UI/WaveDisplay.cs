using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveDisplay : MonoBehaviour
{
    [SerializeField] private EnemyObjectPoolHandler enemyObjectPoolHandler;
    [SerializeField] private TextMeshProUGUI waveDisplayCounterText;

    private void Awake()
    {
        if (enemyObjectPoolHandler != null) return;
        enemyObjectPoolHandler = FindObjectOfType<EnemyObjectPoolHandler>();
    }

    private void Start()
    {
        UpdateWaveDisplay();
    }

    private void OnEnable()
    {
        enemyObjectPoolHandler.OnWaveCleared += UpdateWaveDisplay;
    }

    private void OnDisable()
    {
        enemyObjectPoolHandler.OnWaveCleared -= UpdateWaveDisplay;
    }

    private void UpdateWaveDisplay()
    {
        waveDisplayCounterText.text = enemyObjectPoolHandler.Wave.ToString();
    }
}
