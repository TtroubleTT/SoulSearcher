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


    public void NewGameDialogeYes() //maybe have to change this to NewGameYesButton since thats the name of the actual button
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGameYesDialouge()
    {
        if(PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("LevelToLoad");
            SceneManager.LoadScene(levelToLoad);
        } else
        {
            NoSaveGameDialouge.SetActive(true);
        }
    }
    

    public void LoadGameNoDialouge()
    {
            SceneManager.LoadScene(1);
            NoSaveGameDialouge.SetActive(true);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
