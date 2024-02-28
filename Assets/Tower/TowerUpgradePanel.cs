using UnityEngine;
using TMPro;

// this has to be attached to the UI panel
public class TowerUpgradePanel : MonoBehaviour
{
    // [SerializeField] TextMeshProUGUI displayCost;

    // private void Start()
    // {
    //     UpdateDisplay();
    // }

    public void RemoveTower()
    {
        Destroy(TowerManager.instance.SelectedTower.gameObject);
        Bank.instance.Deposit(TowerManager.instance.SelectedTower.Cost);
        gameObject.SetActive(false);
    }

    public void Upgrade()
    {
        TowerUpgrader upgrader = TowerManager.instance.SelectedTower.Upgrader;
        if (upgrader.HasRangeUpgrade && upgrader.HasFireRateUpgrade && Bank.instance.IsAffordable(upgrader.RangeUpgrades[upgrader.CurrentRangeUpgrade].Cost + upgrader.FireRateUpgrades[upgrader.CurrentFireRateUpgrade].Cost))
        {
            upgrader.UpgradeRange();
            upgrader.UpgradeFireRate();
        }
        // UpdateDisplay();
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

    // TODO: cost display
    // public void UpdateDisplay()
    // {
    //     TowerUpgrader upgrader = TowerManager.instance.SelectedTower.Upgrader;
    //     int cost = upgrader.RangeUpgrades[upgrader.CurrentRangeUpgrade].Cost + upgrader.FireRateUpgrades[upgrader.CurrentFireRateUpgrade].Cost;
    //     displayCost.text = cost.ToString() + "G";
    // }
}
