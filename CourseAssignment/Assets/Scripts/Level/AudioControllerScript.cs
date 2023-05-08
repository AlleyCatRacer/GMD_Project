using UnityEngine;

namespace Level
{
    public class AudioControllerScript : MonoBehaviour
    {
        [SerializeField] private AudioClip menuMusic;
        [SerializeField] private AudioClip gameMusic;
        //TODO: Put script on Player - Aldís
        private AudioSource src;
        
        private void Awake()
        {
            src = GetComponent<AudioSource>();
            src.loop = true;
        }
        
        // TODO: Call from UI methods for switching menu & game music - Aldís
        private void PlayMenuMusic()
        {
            src.PlayOneShot(menuMusic);
        }
        
        private void PlayGameMusic()
        {
            src.PlayOneShot(gameMusic);
        }
    }
}