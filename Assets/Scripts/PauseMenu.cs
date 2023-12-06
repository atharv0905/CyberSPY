using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseScreenUI;

    public static bool isGamePaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        pauseScreenUI.SetActive(true);
        isGamePaused = true;

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        pauseScreenUI.SetActive(false);
        isGamePaused = false;

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        SceneManager.LoadScene(0);
    }
}
