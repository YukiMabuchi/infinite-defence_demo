using UnityEngine;
using TMPro;

// this has to be attached to the UI panel
public class TowerUpgradePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayTowerLevel;
    [SerializeField] TextMeshProUGUI displayCost;

    GridManager gridManager;
    Pathfinder pathfinder;
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    public void RemoveTower()
    {
        Tower tower = TowerManager.instance.SelectedTower;
        Vector2Int coordinates = gridManager.GetCoordinatesFromPosition(tower.transform.position);
        Destroy(tower.gameObject); // tower.OnDestroy is dependent on this
        if (gridManager) gridManager.UnblockNode(coordinates);
        if (pathfinder) pathfinder.NotifyReceivers();
        Bank.instance.Deposit(tower.Cost + tower.Upgrader.TotalCost);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// NOTE: If you fix this function, don't forget to check UpdateTowerLevelDisplay and UpdateCostDisplay functions as well
    /// </summary>
    public void Upgrade()
    {
        TowerUpgrader upgrader = TowerManager.instance.SelectedTower.Upgrader;
        if (Bank.instance.IsAffordable(upgrader.RangeUpgrades[upgrader.CurrentRangeUpgrade].Cost + upgrader.FireRateUpgrades[upgrader.CurrentFireRateUpgrade].Cost))
        {
            upgrader.UpgradeRange();
            upgrader.UpgradeFireRate();
            upgrader.UpgradeDamage(); // TODO: free now
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
