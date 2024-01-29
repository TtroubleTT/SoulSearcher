using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Levels To Load")]
    public string newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject NoSaveGameDialouge = null;


    public void NewGameDialogeYes()
    {
        SceneManager.LoadScene(newGameLevel);
    }

    /*public void LoadGameDialogYes()
    {
        if(PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("LevelToLoad);
            SceneManager.LoadScene(levelToLoad);
        } else
        {
            NoSaveGameDialouge.SetActive(true);
        }
    }
    */

    public void LoadGameDialogYes()
    {
            SceneManager.LoadScene("AidanTesting");
            NoSaveGameDialouge.SetActive(true);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
