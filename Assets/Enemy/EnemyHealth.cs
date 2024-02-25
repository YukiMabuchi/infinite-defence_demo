using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Enemy))] // このスクリプトをつけた時にEnemyの一緒についてくるようになる
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 3;

    [Tooltip("Adds amount to maxHitPoints when enemy dies.")]
    [SerializeField] int difficultyRamp = 4;

    [SerializeField] Slider healthBar;

    int currentHitPoints = 0;

    Enemy enemy;

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
        healthBar.maxValue = currentHitPoints;
        healthBar.value = currentHitPoints;
        healthBar.gameObject.SetActive(false);
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>(); // requireする 
    }

    private void Update()
    {
        healthBar.transform.rotation = Camera.main.transform.rotation;
    }

    void OnParticleCollision(GameObject other)
    {
        // Particle systemのcollision moduleのSend collision messageをonにしないといけない
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHitPoints--;
        healthBar.value = currentHitPoints;
        healthBar.gameObject.SetActive(true);

        if (currentHitPoints <= 0)
        {
            enemy.RewardGold();
            maxHitPoints += difficultyRamp;
            gameObject.SetActive(false); // reuse in pool
        }
    }
}
