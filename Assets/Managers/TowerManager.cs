using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;

    Tower selectedTower;
    public Tower SelectedTower { get { return selectedTower; } }

    private void Awake()
    {
        instance = this;
    }

    public void SelectTower(Tower tower)
    {
        selectedTower = tower;
    }
}
