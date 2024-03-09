using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int initialGoldReward = 25;
    [SerializeField] int initialGoldPenalty = 25;
    [SerializeField] float initialRamp = 1f;

    [SerializeField] float damage = 15; // TODO: separate file like EnemyHealth later

    EnemyHealth enemyHeath;

    EnemySkill enemySkill;

    float ramp;
    int goldReward;
    int goldPenalty;

    Bank bank;
    Castle castle;

    private void Awake()
    {
        enemyHeath = GetComponent<EnemyHealth>();
        enemySkill = GetComponent<EnemySkill>();
    }
    private void OnEnable()
    {
        ManageGold();
        UpgradeHealth();

        // TODO
        // if (enemySkill != null)
        // {
        //     StartCoroutine(enemySkill.AttackFromLongDistance());
        // }
    }

    private void Start()
    {
        bank = FindObjectOfType<Bank>();
        castle = FindObjectOfType<Castle>();
    }

    public void RewardGold()
    {
        if (!bank) return;
        bank.Deposit(goldReward);
    }

    public void StealGold()
    {
        if (!bank) return;
        bank.Withdraw(goldPenalty);
    }
    public void GiveDamage()
    {
        castle.TakeDamage(damage);
    }

    public void ManageGold()
    {
        float wave = WaveManager.instance.Wave; // changing type to float will hold the fractional part of the res of / operator

        ramp = initialRamp + (wave / 10);
        goldReward = (int)Math.Floor(initialGoldReward * ramp);
        goldPenalty = (int)Math.Floor(initialGoldPenalty * ramp);
    }

    void UpgradeHealth()
    {
        enemyHeath.UpgradeHealth(WaveManager.instance.Wave);
    }
}
