using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;

    Tower selectedTower;
    public Tower SelectedTower { get { return selectedTower; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        UIManager.instance.CloseTowerPanel();
    }
}
