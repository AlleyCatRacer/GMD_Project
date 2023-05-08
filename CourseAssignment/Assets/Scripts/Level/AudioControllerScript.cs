using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class AudioControllerScript : MonoBehaviour
    {
        [SerializeField] private AudioSource menuMusic;
        [SerializeField] private AudioSource gameMusic;

        private void Awake()
        {
            RefreshSettings();
            // TODO: Somehow link the slider, maybe put the value float in the scene navigation or game manager? - Aldís
        }

        private void RefreshSettings()
        {
            Debug.Log("Refreshing Audio Settings");
            menuMusic.volume = PlayerPrefs.GetFloat("MenuMusicVolume");
            gameMusic.volume = PlayerPrefs.GetFloat("GameMusicVolume");
        }
        
        public void PlayMenuMusic()
        {
            gameMusic.Pause();

            if (menuMusic.time > 0)
            {
                menuMusic.UnPause();
                return;
            }

            menuMusic.Play();
        }

        public void PlayGameMusic()
        {
            menuMusic.Pause();

            if (gameMusic.time > 0)
            {
                gameMusic.UnPause();
                return;
            }

            gameMusic.Play();
        }
    }
}