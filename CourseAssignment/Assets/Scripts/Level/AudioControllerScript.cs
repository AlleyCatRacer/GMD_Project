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
        [SerializeField] private Slider slider;

        private void Awake()
        {
            // TODO: Somehow link the slider
            // slider.onValueChanged.AddListener(OnVolumeChanged);
        }

        private void OnVolumeChanged(float value)
        {
            menuMusic.volume = value;
            gameMusic.volume = value;
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