using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] GameObject towerPanel;

    private void Awake()
    {
        instance = this;
        CloseTowerPanel();
    }

    public void OpenTowerPanel()
    {
        towerPanel.SetActive(true);
    }
    public void CloseTowerPanel()
    {
        towerPanel.SetActive(false);
    }
}
