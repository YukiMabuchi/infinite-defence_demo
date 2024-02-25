using System.Collections;
using UnityEngine;

// ObjectPoolは、あらかじめ決められた数のObjectを生成しておいて、Deactivateして必要な時に取り出してactivateする
// 終わったらまたdeactivateして再利用する
public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField][Range(0, 50)] int poolSize = 5; // imp to have range
    [SerializeField][Range(0.1f, 30f)] float spawnTimer = 2.5f; // imp to have range

    GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform); // pos and rotate is handled by EnemyMover
            pool[i].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true) // need to have some sort of way of getting out
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
