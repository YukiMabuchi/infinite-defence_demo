using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] GameObject _towerUpgradePanel;
    [SerializeField] GameObject[] towerUpgradeButtons;
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

    public void OpenTowerUpgradePanel(bool upgradable = true)
    {
        _towerUpgradePanel.SetActive(true);
        foreach (GameObject upgradeButton in towerUpgradeButtons)
        {
            upgradeButton.SetActive(upgradable);
        }

        TowerUpgradePanel towerUpgradePanel = _towerUpgradePanel.GetComponent<TowerUpgradePanel>();
        if (towerUpgradePanel)
        {
            towerUpgradePanel.UpdateDisplay();
        }
        CloseTowerSelectPanel();
    }

    public void CloseTowerUpgradePanel(bool openUpgradePanel = true)
    {
        if (BattleManager.instance.IsGamePaused) return;

        _towerUpgradePanel.SetActive(false);
        if (TowerManager.instance.SelectedTower)
        {
            TowerManager.instance.UnSelectTower();
        }
        if (openUpgradePanel) OpenTowerSelectPanel();
    }

    public void OpenTowerSelectPanel()
    {
        if (BattleManager.instance.IsGamePaused) return;
        UpdateAllTowerSelectButtons();
        _towerSelectPanel.SetActive(true);
    }

    public void CloseTowerSelectPanel()
    {
        _towerSelectPanel.SetActive(false);
    }

    public void UpdateAllTowerSelectButtons()
    {
        for (int i = 0; i < _towerSelectPanel.transform.childCount; i++)
        {
            Transform child = _towerSelectPanel.transform.GetChild(i);
            TowerButton towerButton = child.GetComponent<TowerButton>();
            if (towerButton != null) towerButton.UpdateDisplayTowerAvailableNumber();
        }
    }

    public void HideUIs()
    {
        foreach (GameObject UI in UIsToHide)
        {
            UI.SetActive(false);
        }
    }
}
