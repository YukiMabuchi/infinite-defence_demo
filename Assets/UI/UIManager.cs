using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] GameObject towerPanel;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CloseTowerPanel();
    }

    public void OpenTowerPanel(Tower tower)
    {
        towerPanel.SetActive(true);
        TowerManager.instance.SelectTower(tower);
        TowerUpgradePanel towerUpgradePanel = towerPanel.GetComponent<TowerUpgradePanel>();
        if (towerUpgradePanel)
        {
            towerUpgradePanel.UpdateDisplay();
        }
    }
    public void CloseTowerPanel()
    {
        towerPanel.SetActive(false);
        TowerManager.instance.SelectTower(null);
    }
}
