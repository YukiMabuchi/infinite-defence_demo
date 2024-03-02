using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int damage = 1;
    public int Damage { get { return damage; } }

    public void UpgradeDamage(int amount)
    {
        // this damage is used in EnemyHealth
        damage = amount;
    }
}
