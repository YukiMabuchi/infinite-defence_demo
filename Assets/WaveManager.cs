using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayWave;

    // [SerializeField] public int maxEnemyCount = 15; // how many enemies should be deactivated for one wave

    // TODO: manage total spawn count in ObjectPool for a wave
    // [SerializeField] int difficultyRamp = 5;
    // public int maxEnemyCountPerWave;

    int wave;


    private void Awake()
    {
        wave = 1;
        // maxEnemyCountPerWave = maxEnemyCount;
        UpdateDisplay();
    }

    public void WaveClear()
    {
        wave++;
        // maxEnemyCountPerWave += difficultyRamp;
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        displayWave.text = "Wave: " + wave;
    }
}
