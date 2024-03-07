using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] GameObject _towerUpgradePanel;
    [SerializeField] GameObject _towerSelectPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        CloseTowerUpgradePanel();
    }

    public void OpenTowerUpgradePanel()
    {
        _towerUpgradePanel.SetActive(true);
        TowerUpgradePanel towerUpgradePanel = _towerUpgradePanel.GetComponent<TowerUpgradePanel>();
        if (towerUpgradePanel)
        {
            towerUpgradePanel.UpdateDisplay();
        }
        CloseTowerSelectPanel();
    }

    public void CloseTowerUpgradePanel()
    {
        _towerUpgradePanel.SetActive(false);
        if (TowerManager.instance.SelectedTower)
        {
            TowerManager.instance.UnSelectTower();
        }
        OpenTowerSelectPanel();
    }

    public void OpenTowerSelectPanel()
    {
        _towerSelectPanel.SetActive(true);
    }

    public void CloseTowerSelectPanel()
    {
        _towerSelectPanel.SetActive(false);
    }
}
