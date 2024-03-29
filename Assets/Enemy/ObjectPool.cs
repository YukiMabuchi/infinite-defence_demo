using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ObjectPoolは、あらかじめ決められた数のObjectを生成しておいて、Deactivateして必要な時に取り出してactivateする
// 終わったらまたdeactivateして再利用する
public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField][Range(0, 50)] int poolSize = 3; // imp to have range (only works in Unity Editor)
    [Tooltip("Adds amount to poolSize when a wave ends.")]
    [SerializeField] int difficultyRamp = 2;

    [SerializeField][Range(0.1f, 30f)] float spawnTimer = 2.5f; // imp to have range

    List<GameObject> pool = new List<GameObject>();
    int maxSpawnSize; // poolSize + dramp
    int spawnCount; // to cound the number of already spawned ones

    bool isPoolInitialized;
    public bool IsPoolInitialized { get { return isPoolInitialized; } }
    bool isPoolCleared;
    public bool IsPoolCleared { get { return isPoolCleared; } }

    private void Update()
    {
        if (!pool.Find((GameObject obj) => obj.activeInHierarchy)) // TODO: This may be expensive to use Find in Update
        {
            isPoolCleared = true;
        }
    }

    /// <summary>
    /// First function to start populating and spawning
    /// </summary>
    public void PopulateAndSpawn()
    {
        maxSpawnSize = poolSize;
        isPoolCleared = false;
        PopulatePool();
        isPoolInitialized = true;
        StartCoroutine(SpawnEnemy());
    }

    void PopulatePool()
    {
        for (int i = 0; i < maxSpawnSize; i++)
        {
            pool.Add(Instantiate(enemyPrefab, transform)); // pos and rotate is handled by EnemyMover
            pool[i].SetActive(false);
        }
    }

    void IncreasePool(int ramp)
    {
        for (int i = 0; i < ramp; i++)
        {
            pool.Add(Instantiate(enemyPrefab, transform));
            pool[^1].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < maxSpawnSize; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                spawnCount++;
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        isPoolCleared = false;
        while (spawnCount < maxSpawnSize)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    /// <summary>
    /// Once the pool gets initialised by PopulateAndSpawn, this funciton will continue repopulating and respawning
    /// </summary>
    public void Respawn()
    {
        IncreasePool(difficultyRamp);
        maxSpawnSize += difficultyRamp;
        spawnCount = 0;
        StartCoroutine(SpawnEnemy());
    }
}
