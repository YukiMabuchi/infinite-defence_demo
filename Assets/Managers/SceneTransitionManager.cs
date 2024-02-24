using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public void LoadTo(string sceneName)
    {
        Time.timeScale = 1; // Game over makes Time.timeScale 0, so need to reset to 1
        SceneManager.LoadScene(sceneName);
    }
}
