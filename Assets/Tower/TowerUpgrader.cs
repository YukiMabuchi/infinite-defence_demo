using UnityEngine;

// this has to be attached to Tower
[RequireComponent(typeof(Tower))]
[RequireComponent(typeof(TargetLocator))]
public class TowerUpgrader : MonoBehaviour
{
    [SerializeField] bool allowUpgrade = true;
    public bool AllowUpgrade { get { return allowUpgrade; } }

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

    // check if the tower still has things to upgrades
    bool hasMoreUpgrade = true;
    public bool HasMoreUpgrade { get { return hasMoreUpgrade; } }

    int currentLevel = 1; // NOTE: for uses, it stats with 1 
    public int CurrentLevel { get { return currentLevel; } } // the level to manage all the upgrades

    int totalCost;
    public int TotalCost { get { return totalCost; } }

    private void Awake()
    {
        targetLocator = GetComponent<TargetLocator>();
    }

    public void UpgradeFireRate()
    {
        if (!AllowUpgrade || !hasFireRateUpgrade) return;

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
        if (!AllowUpgrade || !hasRangeUpgrade) return;

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
        if (!AllowUpgrade || !hasDamageUpgrade) return;

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

    // TODO: better level management
    public void CheckCurrentLevel()
    {
        if (!allowUpgrade)
        {
            hasMoreUpgrade = false;
            return;
        }

        int allLevelsSum = currentDamageUpgrade + currentFireRateUpgrade + currentRangeUpgrade;
        if (allLevelsSum == 0) return;

        // NOTE: this is assuming that the current tower max level is 3
        if (!hasDamageUpgrade && !hasFireRateUpgrade && !hasRangeUpgrade) hasMoreUpgrade = false;
        else if (allLevelsSum % 6 == 0) currentLevel = 3;
        else if (allLevelsSum % 3 == 0) currentLevel = 2;

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
