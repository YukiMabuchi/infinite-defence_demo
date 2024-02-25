using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenalty = 25;
    [SerializeField] float damage = 15; // TODO: separate file like EnemyHealth later


    Bank bank;
    Castle castle;

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
}
