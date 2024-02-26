using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPopup;
    [SerializeField] GameObject menuPopup;

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
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
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
}
