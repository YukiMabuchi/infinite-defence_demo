using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;

    [SerializeField] Tower[] towers;

    // the tower for placing
    Tower placingTower;
    public Tower PlacingTower { get { return placingTower; } }

    // the tower for upgrade
    Tower selectedTower;
    public Tower SelectedTower { get { return selectedTower; } }

    /// <summary>
    /// {towerName: [initialAvailableNumber, currentPlacedNumber, currentAvailableNumber]}
    /// </summary>
    Dictionary<string, int[]> allTowerPlacementStatus = new Dictionary<string, int[]>();
    /// <summary>
    /// {towerName: [initialAvailableNumber, currentPlacedNumber, currentAvailableNumber]}
    /// </summary>
    public Dictionary<string, int[]> AllTowerPlacementStatus { get { return allTowerPlacementStatus; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        foreach (Tower tower in towers)
        {
            if (!allTowerPlacementStatus.ContainsKey(tower.TowerName)) allTowerPlacementStatus[tower.TowerName] = new int[3] { tower.InitialAvailableNumbers, 0, tower.InitialAvailableNumbers };
        }
    }

    public void SelectTower(Tower tower)
    {
        selectedTower = tower;
        if (selectedTower) selectedTower.ManageRangeIndicator(true); // Unselectでnullが来る可能性もある
    }

    public void UnSelectTower()
    {
        if (selectedTower)
        {
            selectedTower.ManageRangeIndicator(false);
            SelectTower(null);
        }
        UIManager.instance.CloseTowerUpgradePanel();
    }

    public void SetPlacingTower(Tower tower)
    {
        placingTower = tower;
    }

    public void UnsetPlacingTower()
    {
        placingTower = null;
    }

    /// <summary>
    /// Manage the total count of all the tower status (initial, placed, available) by their names baesd on states of being placed or removed.
    /// </summary>
    /// <param name="tower"></param>
    /// <param name="placed"></param>
    public void ManageTowerPlacementStatus(Tower tower, bool placed = true)
    {
        if (tower.IsInfinite) return;

        string towerName = tower.TowerName;
        if (allTowerPlacementStatus.ContainsKey(towerName))
        {
            // placed or removed
            if (placed)
            {
                allTowerPlacementStatus[towerName][1]++;
                allTowerPlacementStatus[towerName][2] = allTowerPlacementStatus[towerName][0] - allTowerPlacementStatus[towerName][1];
            }
            else
            {
                allTowerPlacementStatus[towerName][1]--;
                allTowerPlacementStatus[towerName][2] = allTowerPlacementStatus[towerName][1] == 0 ? allTowerPlacementStatus[towerName][0] : allTowerPlacementStatus[towerName][0] - allTowerPlacementStatus[towerName][1];
            }
        }
    }
}
