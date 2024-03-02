using UnityEngine;

// this has to be attached to Tower
[RequireComponent(typeof(Tower))]
[RequireComponent(typeof(TargetLocator))]
public class TowerUpgrader : MonoBehaviour
{
    TargetLocator targetLocator;

    [SerializeField] UpgradeStage[] rangeUpgrades;
    public UpgradeStage[] RangeUpgrades { get { return rangeUpgrades; } }
    int currentRangeUpgrade;
    public int CurrentRangeUpgrade { get { return currentRangeUpgrade; } }
    bool hasRangeUpgrade = true;
    public bool HasRangeUpgrade { get { return hasRangeUpgrade; } }

    [SerializeField] UpgradeStage[] fireRateUpgrades;
    public UpgradeStage[] FireRateUpgrades { get { return fireRateUpgrades; } }
    int currentFireRateUpgrade;
    public int CurrentFireRateUpgrade { get { return currentFireRateUpgrade; } }
    bool hasFireRateUpgrade = true;
    public bool HasFireRateUpgrade { get { return hasFireRateUpgrade; } }

    [SerializeField] UpgradeStage[] damageUpgrades;
    public UpgradeStage[] DamageUpgrades { get { return damageUpgrades; } }
    int currentDamageUpgrade;
    public int CurrentDamageUpgrade { get { return currentDamageUpgrade; } }
    bool hasDamageUpgrade = true;
    public bool HasDamageUpgrade { get { return hasDamageUpgrade; } }

    int totalCost;
    public int TotalCost { get { return totalCost; } }

    private void Awake()
    {
        targetLocator = GetComponent<TargetLocator>();
    }

    public void UpgradeFireRate()
    {
        if (!hasFireRateUpgrade) return;

        targetLocator.UpgradeFireRate(fireRateUpgrades[CurrentFireRateUpgrade].Amount);
        Bank.instance.Withdraw(fireRateUpgrades[CurrentFireRateUpgrade].Cost);
        totalCost += fireRateUpgrades[CurrentFireRateUpgrade].Cost;

        if (currentFireRateUpgrade >= fireRateUpgrades.Length - 1)
        {
            hasFireRateUpgrade = false;
        }
        else
        {
            currentFireRateUpgrade++;
        }
    }

    public void UpgradeRange()
    {
        if (!hasRangeUpgrade) return;

        targetLocator.UpgradeRange(rangeUpgrades[currentRangeUpgrade].Amount);
        Bank.instance.Withdraw(rangeUpgrades[currentRangeUpgrade].Cost);
        totalCost += rangeUpgrades[currentRangeUpgrade].Cost;

        if (currentRangeUpgrade >= rangeUpgrades.Length - 1)
        {
            hasRangeUpgrade = false;
        }
        else
        {
            currentRangeUpgrade++;
        }
    }

    public void UpgradeDamage()
    {
        if (!hasDamageUpgrade) return;

        targetLocator.UpgradeDamage((int)damageUpgrades[currentDamageUpgrade].Amount);
        Bank.instance.Withdraw(damageUpgrades[currentDamageUpgrade].Cost);
        totalCost += damageUpgrades[currentDamageUpgrade].Cost;

        if (currentDamageUpgrade >= damageUpgrades.Length - 1)
        {
            hasDamageUpgrade = false;
        }
        else
        {
            currentDamageUpgrade++;
        }
    }
}


[System.Serializable]
public class UpgradeStage
{
    [SerializeField] float amount;
    public float Amount { get { return amount; } }

    [SerializeField] int cost;
    public int Cost { get { return cost; } }
}
