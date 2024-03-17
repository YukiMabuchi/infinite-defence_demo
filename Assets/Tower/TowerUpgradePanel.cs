using UnityEngine;
using TMPro;

// this has to be attached to the UI panel
public class TowerUpgradePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayTowerLevel;
    [SerializeField] TextMeshProUGUI displayCost;

    GridManager gridManager;
    Pathfinder[] pathfinders;
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinders = FindObjectsOfType<Pathfinder>();
    }

    public void RemoveTower()
    {
        if (BattleManager.instance.IsGamePaused) return;

        Tower tower = TowerManager.instance.SelectedTower;
        Vector2Int coordinates = gridManager.GetCoordinatesFromPosition(tower.transform.position);
        Bank.instance.Deposit((tower.Cost + tower.Upgrader.TotalCost) / 2); // give back the half of the total cost
        TowerManager.instance.ManageTowerPlacementStatus(tower, false);
        Destroy(tower.gameObject); // tower.OnDestroy is dependent on this
        if (gridManager) gridManager.UnblockNode(coordinates);
        foreach (Pathfinder pathfinder in pathfinders)
        {
            pathfinder.NotifyReceivers();
        }
        gameObject.SetActive(false);
        UIManager.instance.OpenTowerSelectPanel();
    }

    /// <summary>
    /// NOTE: If you fix this function, don't forget to check UpdateTowerLevelDisplay and UpdateCostDisplay functions as well
    /// </summary>
    public void Upgrade()
    {
        if (BattleManager.instance.IsGamePaused) return;

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
        TowerUpgrader upgrader = TowerManager.instance.SelectedTower.Upgrader;
        upgrader.CheckCurrentLevel();
        UpdateTowerLevelDisplay(upgrader);
        UpdateCostDisplay(upgrader);
    }

    public void UpdateTowerLevelDisplay(TowerUpgrader upgrader)
    {
        // TODO: since now both range and firerate are upgraded at the same time, I use range value here.
        // if they get separated, need to fix here
        displayTowerLevel.text = upgrader.HasMoreUpgrade ? "Level" + upgrader.CurrentLevel : "Level Max";
    }

    public void UpdateCostDisplay(TowerUpgrader upgrader)
    {
        // TODO: since now both range and firerate are upgraded at the same time, I use range value here.
        // if they get separated, need to fix here
        int cost = upgrader.RangeUpgrades[upgrader.CurrentRangeUpgrade].Cost + upgrader.FireRateUpgrades[upgrader.CurrentFireRateUpgrade].Cost;
        string displayText = upgrader.HasMoreUpgrade ? cost.ToString() + "G" : "Max";
        displayCost.text = displayText;
    }
}
