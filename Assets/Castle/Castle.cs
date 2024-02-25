using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [SerializeField] float maxHitPoints = 100f;

    [SerializeField] Slider healthBar;

    // public Transform[] attackPoints;
    float currentHitPoints = 0;

    BattleManager battleManager;

    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
    }

    private void Start()
    {
        currentHitPoints = maxHitPoints;

        healthBar.maxValue = maxHitPoints;
        healthBar.value = currentHitPoints;
    }

    public void TakeDamage(float damageToTake)
    {
        currentHitPoints -= damageToTake;

        if (currentHitPoints <= 0)
        {
            currentHitPoints = 0;
            gameObject.SetActive(false);
            battleManager.GameOver();
        }

        healthBar.value = currentHitPoints;
    }
}