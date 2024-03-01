using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] GameObject towerPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        CloseTowerPanel();
    }

    public void OpenTowerPanel()
    {
        towerPanel.SetActive(true);
        TowerUpgradePanel towerUpgradePanel = towerPanel.GetComponent<TowerUpgradePanel>();
        if (towerUpgradePanel)
        {
            towerUpgradePanel.UpdateDisplay();
        }
    }

    public void CloseTowerPanel()
    {
        towerPanel.SetActive(false);
        if (TowerManager.instance.SelectedTower)
        {
            TowerManager.instance.UnSelectTower();
        }
    }
}
