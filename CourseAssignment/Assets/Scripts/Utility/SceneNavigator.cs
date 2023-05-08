using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneNavigator : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool SettingsOpen = false;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && !GameIsPaused)
        {
            Debug.Log("Attempting to Pause/Resume game");
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.R) && GameIsPaused && !SettingsOpen)
        {
            Resume();
        }
        
        if (Input.GetKeyDown(KeyCode.S) && GameIsPaused && !SettingsOpen)
        {
            OpenSettings();
        }
        
        if (Input.GetKeyDown(KeyCode.Q) && GameIsPaused && !SettingsOpen)
        {
            QuitGameMainMenu();
        }
        
        if (Input.GetKeyDown(KeyCode.X) && SettingsOpen)
        {
            CloseSettings();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }
    public void Resume()
    {
        Debug.Log("Resuming Game");
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync(3);
        GameIsPaused = false;
    }
    public void Pause()
    {
        Debug.Log("Pausing Game");
        Time.timeScale = 0f;
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
        GameIsPaused = true;
    }

    public void QuitGameMainMenu()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game...");
    }
    public void OpenSettings()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        SettingsOpen = true;
    }

    public void CloseSettings()
    {
        SceneManager.UnloadSceneAsync(1);
        SettingsOpen = false;
    }

}
