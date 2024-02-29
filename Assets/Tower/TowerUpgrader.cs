using UnityEngine;

// this has to be attached to Tower
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


    private void Awake()
    {
        targetLocator = GetComponent<TargetLocator>();
    }

    public void UpgradeFireRate()
    {
        if (!hasFireRateUpgrade) return;

        targetLocator.UpgradeFireRate(fireRateUpgrades[CurrentFireRateUpgrade].Amount);
        Bank.instance.Withdraw(fireRateUpgrades[CurrentFireRateUpgrade].Cost);
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
        if (currentRangeUpgrade >= rangeUpgrades.Length - 1)
        {
            hasRangeUpgrade = false;
        }
        else
        {
            currentRangeUpgrade++;
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
