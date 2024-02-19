using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Contributors: Taylor, Aidan
    public static bool GamePaused = false;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject UICanvas;
    [SerializeField] private GameObject optionsMenu;

    private void Start()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        UICanvas.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GamePaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }
    }
    private void Pause()
    {
        pauseMenu.SetActive(true);
        UICanvas.SetActive(false);
        Time.timeScale = 0;
        GamePaused = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void MainMenu()
    {
        GamePaused = false;
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        UICanvas.SetActive(true);
        Time.timeScale = 1;
        GamePaused = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
