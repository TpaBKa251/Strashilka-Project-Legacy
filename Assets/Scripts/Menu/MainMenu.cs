using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        Pause.pause = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
