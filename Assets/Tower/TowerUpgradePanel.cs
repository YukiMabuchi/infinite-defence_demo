using UnityEngine;
using TMPro;

// this has to be attached to the UI panel
public class TowerUpgradePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayTowerLevel;
    [SerializeField] TextMeshProUGUI displayCost;


    public void RemoveTower()
    {
        Destroy(TowerManager.instance.SelectedTower.gameObject);
        Bank.instance.Deposit(TowerManager.instance.SelectedTower.Cost);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// NOTE: If you fix this function, don't forget to check UpdateTowerLevelDisplay and UpdateCostDisplay functions as well
    /// </summary>
    public void Upgrade()
    {
        TowerUpgrader upgrader = TowerManager.instance.SelectedTower.Upgrader;
        if (upgrader.HasRangeUpgrade && upgrader.HasFireRateUpgrade && Bank.instance.IsAffordable(upgrader.RangeUpgrades[upgrader.CurrentRangeUpgrade].Cost + upgrader.FireRateUpgrades[upgrader.CurrentFireRateUpgrade].Cost))
        {
            upgrader.UpgradeRange();
            upgrader.UpgradeFireRate();
        }
        UpdateDisplay();
    }

    // TODO: separate later
    // public void UpgradeFireRate()
    // {

    // }
    // public void UpgradeRange()
    // {
    //     TowerUpgrader upgrader = TowerManager.instance.SelectedTower.Upgrader;
    //     if (upgrader.HasRangeUpgrade && Bank.instance.IsAffordable(upgrader.RangeUpgrades[upgrader.CurrentRangeUpgrade].Cost))
    //     {
    //         upgrader.UpgradeRange();
    //     }
    // }

    public void UpdateDisplay()
    {
        UpdateTowerLevelDisplay();
        UpdateCostDisplay();
    }

    public void UpdateTowerLevelDisplay()
    {
        // NOTE: since now both range and firerate are upgraded at the same time, I use range value here.
        // if they get separated, need to fix here
        TowerUpgrader upgrader = TowerManager.instance.SelectedTower.Upgrader;
        displayTowerLevel.text = upgrader.HasRangeUpgrade ? "Level" + upgrader.CurrentRangeUpgrade : "Level Max";
    }

    public void UpdateCostDisplay()
    {
        // NOTE: since now both range and firerate are upgraded at the same time, I use range value here.
        // if they get separated, need to fix here
        TowerUpgrader upgrader = TowerManager.instance.SelectedTower.Upgrader;
        int cost = upgrader.RangeUpgrades[upgrader.CurrentRangeUpgrade].Cost + upgrader.FireRateUpgrades[upgrader.CurrentFireRateUpgrade].Cost;
        displayCost.text = cost.ToString() + "G";
    }
}
