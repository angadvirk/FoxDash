using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject pToPause;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameIsPaused)
            {
                Pause();
                
            }
        }
        if (gameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Resume();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Quit();
            }
        }
    }

    public void Pause()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        pToPause.SetActive(false);
    }

    public void Resume()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        pToPause.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit(); // Quit Game
    }
}
