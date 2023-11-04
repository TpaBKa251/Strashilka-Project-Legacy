using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour

    
{

    public GameObject pausePanel;
    public static bool pause;

    void Update()
    {
        if (!Inventory.isOpen)
        {
            if (Input.GetButtonDown("Pause") && !pause && !EnemyStatesManager.isDead)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0;
                pause = true;
            }

            else if (Input.GetButtonDown("Pause") && pause)
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1;
                pause = false;
            }
        }
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        pause = false;
    }
}
