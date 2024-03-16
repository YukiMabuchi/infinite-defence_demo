using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [SerializeField] GameObject gameOverPopup;
    [SerializeField] GameObject menuPopup;

    [SerializeField] List<GameObject> speedAdjusterButtons;
    int currentGameSpeed = 1;

    bool isGamePaused = false;
    public bool IsGamePaused { get { return isGamePaused; } }

    bool isGameOver = false;
    public bool IsGameOver { get { return isGameOver; } }

    private void Awake()
    {
        if (instance == null) instance = this;

        gameOverPopup.SetActive(false);
        menuPopup.SetActive(false);
    }

    public void GameOver()
    {
        isGameOver = true;
        UIManager.instance.CloseTowerSelectPanel();
        UIManager.instance.CloseTowerUpgradePanel(false);
        UIManager.instance.HideUIs();
        gameOverPopup.SetActive(true);
        PauseGame();
    }

    public void PauseGame()
    {
        // NOTE: PauseGame doesn't edit currentGameSpeed
        isGamePaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = currentGameSpeed;
    }

    public void RestartGame()
    {
        currentGameSpeed = 1;
        ResumeGame();
        ReloadScene();
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void OpenMenu()
    {
        if (isGameOver) return;

        PauseGame();
        menuPopup.SetActive(true);
    }

    public void CloseMenu()
    {
        ResumeGame();
        menuPopup.SetActive(false);
    }

    // TODO: find better way
    public void AdjustGameSpeed(int times)
    {
        if (isGameOver || isGamePaused) return;

        Time.timeScale = 1 * times;

        Dictionary<int, int> buttonToShow = new Dictionary<int, int> {
            { 1, 1 },
            { 2, 2 },
            { 4, 0 },
        };
        Dictionary<int, int> buttonToHide = new Dictionary<int, int> {
            { 1, 0 },
            { 2, 1 },
            { 4, 2 },
        };

        speedAdjusterButtons[buttonToHide[times]].SetActive(false);
        speedAdjusterButtons[buttonToShow[times]].SetActive(true);

        currentGameSpeed = times;
    }
}
