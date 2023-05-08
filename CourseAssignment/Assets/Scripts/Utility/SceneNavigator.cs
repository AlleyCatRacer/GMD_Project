using System;
using Level;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class SceneNavigator : MonoBehaviour
    {
        [SerializeField] private AudioControllerScript audioScript;
        public static bool GameIsPaused = true;
        public static bool SettingsOpen = false;

        private void Awake()
        {
            audioScript = GetComponent<AudioControllerScript>();
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            audioScript.PlayMenuMusic();
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
                QuitGame();
            }

            if (Input.GetKeyDown(KeyCode.M) && GameIsPaused && !SettingsOpen)
            {
                QuitGameMainMenu();
            }
        
            if (Input.GetKeyDown(KeyCode.X) && SettingsOpen)
            {
                CloseSettings();
            }

            if (Input.GetKeyDown(KeyCode.N) && GameIsPaused && !SettingsOpen)
            {
                StartGame();
            }
        }

        public void StartGame()
        {
            GameIsPaused = false;
            SceneManager.LoadScene(2);
            audioScript.PlayGameMusic();
        }
        public void Resume()
        {
            Debug.Log("Resuming Game");
            Time.timeScale = 1f;
            SceneManager.UnloadSceneAsync(3);
            audioScript.PlayGameMusic();
            GameIsPaused = false;
        }
        public void Pause()
        {
            Debug.Log("Pausing Game");
            Time.timeScale = 0f;
            SceneManager.LoadScene(3, LoadSceneMode.Additive);
            audioScript.PlayMenuMusic();
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
}
