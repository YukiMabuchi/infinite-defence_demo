using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPopup;
    [SerializeField] GameObject menuPopup;

    [SerializeField] List<GameObject> speedAdjusterButtons;
    int currentGameSpeed = 1;

    private void Awake()
    {
        gameOverPopup.SetActive(false);
        menuPopup.SetActive(false);
    }

    public void GameOver()
    {
        gameOverPopup.SetActive(true);
        PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
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
        Time.timeScale = 0;
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
