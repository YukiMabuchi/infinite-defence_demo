using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] GameObject _towerUpgradePanel;
    [SerializeField] GameObject _towerSelectPanel;

    [Tooltip("These UIs will be hidden when the game is over")]
    [SerializeField] GameObject[] UIsToHide;

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

    public void CloseTowerUpgradePanel(bool openUpgradePanel = true)
    {
        _towerUpgradePanel.SetActive(false);
        if (TowerManager.instance.SelectedTower)
        {
            TowerManager.instance.UnSelectTower();
        }
        if (openUpgradePanel) OpenTowerSelectPanel();
    }

    public void OpenTowerSelectPanel()
    {
        _towerSelectPanel.SetActive(true);
    }

    public void CloseTowerSelectPanel()
    {
        _towerSelectPanel.SetActive(false);
    }

    public void HideUIs()
    {
        foreach (GameObject UI in UIsToHide)
        {
            UI.SetActive(false);
        }
    }
}
