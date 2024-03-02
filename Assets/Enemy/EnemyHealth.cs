using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Enemy))] // このスクリプトをつけた時にEnemyの一緒についてくるようになる
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int initialMaxHitPoints = 3;

    [Tooltip("Adds amount to maxHitPoints when enemy dies.")]
    [SerializeField] int difficultyRamp = 4;

    [SerializeField] Slider healthBar;

    int maxHitPoints;
    int currentHitPoints = 0;

    Enemy enemy;

    private void Awake()
    {
        maxHitPoints = initialMaxHitPoints;
    }

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

    // Particle systemのcollision moduleのSend collision messageをonにしないといけない
    void OnParticleCollision(GameObject other)
    {
        Projectile projectile = other.GetComponent<Projectile>();
        if (!projectile) return;

        ProcessHit(projectile.Damage);
    }

    void ProcessHit(int damage)
    {
        currentHitPoints -= damage;
        healthBar.value = currentHitPoints;
        healthBar.gameObject.SetActive(true);

        if (currentHitPoints <= 0)
        {
            enemy.RewardGold();
            gameObject.SetActive(false); // reuse in pool
        }
    }

    public void UpgradeHealth(int wave)
    {
        if (wave == 1) return;
        maxHitPoints = initialMaxHitPoints + (difficultyRamp * wave);
    }
}
