using Level;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class SceneNavigator : MonoBehaviour
    {
        // Serialized Fields
        [SerializeField] private AudioControllerScript audioScript;
        
        // Private Stuff
        private bool gameIsPaused = false;
        private bool settingsOpen = false;
        
        // Public
        public int HighScore => PlayerPrefs.GetInt(nameof(HighScore));

        private void Start()
        {
            // This makes my ears smile :)
            // https://youtu.be/Z-WdGYi9Pv4
            audioScript.PlayMenuMusic();
        }

        void Update()
        {
            // This could probably have been made in a better way (possibly with the new Input System)
            // But it is the night before deadline, so whatever works will have to do
            if (gameIsPaused)
            {
                if (Input.GetKeyDown(KeyCode.R))
                        Resume();

                if (Input.GetKeyDown(KeyCode.M))
                        QuitGameMainMenu();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.P))
                    Pause();
            }

            if (settingsOpen)
            {
                if (Input.GetKeyDown(KeyCode.X))
                    CloseSettings();
            }
            // TODO: add this reference for when the game over is displaying 
            //if (Input.GetKeyDown(KeyCode.T))
            //    StartGame();
        }

        public void StartGame()
        {
            SceneManager.LoadScene(2);
            audioScript.PlayGameMusic();
        }

        public void Resume()
        {
            // Lock the Mouse to the Center of the screen
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("Resuming Game");

            // Set the Time Scale to start the game
            Time.timeScale = 1f;

            // Unload the Scene
            SceneManager.UnloadSceneAsync(3);

            // Play the Correct Music
            audioScript.PlayGameMusic();

            // Set the Variable for Keeping Track
            gameIsPaused = false;
        }

        private void Pause()
        {
            // Unlock the Cursor
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("Pausing Game");

            // Freeze the Game
            Time.timeScale = 0f;

            // Load the Scene on top
            SceneManager.LoadScene(3, LoadSceneMode.Additive);

            // Play the correct music (this should maybe not happen here, but it's 23:32, the day before deadline)
            audioScript.PlayMenuMusic();

            // Track Keeping
            gameIsPaused = true;
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
            settingsOpen = true;
        }

        private void CloseSettings()
        {
            SendMessage("RefreshSettings");
            SceneManager.UnloadSceneAsync(1);
            settingsOpen = false;
        }
    }
}