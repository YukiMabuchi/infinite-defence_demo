using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [SerializeField] TextMeshProUGUI displayWave;

    [SerializeField] int waveDifficultyRamp = 3; // to determine when to activate ObjectPool. eg) Every 3 waves.

    [SerializeField] GameObject enemyPools;
    List<ObjectPool> objectPools = new List<ObjectPool>();

    int wave = 1;
    public int Wave { get { return wave; } }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        wave = 1;
        UpdateDisplay();

        for (int i = 0; i < enemyPools.transform.childCount; i++)
        {
            Transform child = enemyPools.transform.GetChild(i);
            ObjectPool pool = child.GetComponent<ObjectPool>();
            if (pool != null)
            {
                objectPools.Add(pool);
                if (i == 0) pool.PopulateAndSpawn();
            }
        }
    }

    private void Update()
    {
        if (AreAllPoolsCleared())
        {
            WaveClear();
        }
    }

    public void WaveClear()
    {
        wave++;
        UpdateDisplay();
        bool ramped = false;
        for (int i = 0; i < objectPools.Count; i++)
        {
            ObjectPool pool = objectPools[i];
            if (pool.IsPoolInitialized)
            {
                pool.Respawn();
            }
            else if (!ramped && wave % waveDifficultyRamp == 0)
            {
                pool.PopulateAndSpawn();
                ramped = true;
            }
        }
    }

    void UpdateDisplay()
    {
        displayWave.text = "Wave: " + wave;
    }

    bool AreAllPoolsCleared()
    {
        foreach (ObjectPool pool in objectPools)
        {
            if (!pool.IsPoolCleared)
            {
                return false;
            }
        }
        return true;
    }
}