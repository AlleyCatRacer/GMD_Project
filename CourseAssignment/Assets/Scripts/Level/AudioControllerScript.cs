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
            PlayMenuMusic();
        }

        private void RefreshSettings()
        {
            Debug.Log("Refreshing Audio Settings");
            menuMusic.volume = PlayerPrefs.GetFloat("MenuMusicVolume");
            gameMusic.volume = PlayerPrefs.GetFloat("GameMusicVolume");
        }

        public void PlayMenuMusic()
        {

   
            // Pause Music
            if (gameMusic.isPlaying)
                gameMusic.Pause();

            // Check if the Music is paused mid-way
            if (menuMusic.time > 0)
                menuMusic.UnPause();
            else
                menuMusic.Play();
            
            // Refresh to ensure settings are correct
            RefreshSettings();
        }

        public void PlayGameMusic()
        {
            // Pause Music
            if (menuMusic.isPlaying)
                menuMusic.Pause();

            // Check if the Music is paused mid-way
            if (gameMusic.time > 0)
                gameMusic.UnPause();
            else
                gameMusic.Play();
            
            // Refresh to ensure settings are correct
            RefreshSettings();
        }
    }
}