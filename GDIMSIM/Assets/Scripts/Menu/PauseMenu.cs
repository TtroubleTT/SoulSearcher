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
    [SerializeField] private GameObject optionsMenu;

    private List<GameObject> _deactivatedUI = new();

    private void Start()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        foreach (GameObject playerUI in GameObject.FindGameObjectsWithTag("PlayerUI"))
        {
            playerUI.SetActive(true);
        }
        _deactivatedUI.Clear();
    }
    
    public void Pause()
    {
        pauseMenu.SetActive(true);
        
        foreach (GameObject playerUI in GameObject.FindGameObjectsWithTag("PlayerUI"))
        {
            _deactivatedUI.Add(playerUI);
            playerUI.SetActive(false);
        }
        
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
        
        foreach (GameObject playerUI in _deactivatedUI)
        {
            playerUI.SetActive(true);
        }
        
        _deactivatedUI.Clear();
        
        Time.timeScale = 1;
        GamePaused = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public bool IsGamePaused()
    {
        return GamePaused;
    }
}
