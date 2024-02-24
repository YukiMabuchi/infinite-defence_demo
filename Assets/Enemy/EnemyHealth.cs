using UnityEngine;

[RequireComponent(typeof(Enemy))] // このスクリプトをつけた時にEnemyの一緒についてくるようになる
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 3;

    [Tooltip("Adds amount to maxHitPoints when enemy dies.")]
    [SerializeField] int difficultyRamp = 2;

    int currentHitPoints = 0;

    Enemy enemy;

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>(); // requireする 
    }

    void OnParticleCollision(GameObject other)
    {
        // Particle systemのcollision moduleのSend collision messageをonにしないといけない
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHitPoints--;
        if (currentHitPoints <= 0)
        {
            enemy.RewardGold();
            maxHitPoints += difficultyRamp;
            gameObject.SetActive(false); // reuse in pool
        }
    }
}
