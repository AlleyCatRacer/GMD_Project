using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyAudioScript : MonoBehaviour
    {
        [SerializeField] private AudioClip enemyDamageClip;
        [SerializeField] private AudioClip enemyDeathClip;
        private AudioSource src;

        private void Awake()
        {
            src = GetComponent<AudioSource>();
        }
        
        public float PlayDeathRattle()
        {
            src.PlayOneShot(enemyDeathClip);
            return enemyDeathClip.length;
        }

        public float PlayDamageSound()
        {
            src.PlayOneShot(enemyDamageClip);
            return enemyDamageClip.length;
        }
    }
}