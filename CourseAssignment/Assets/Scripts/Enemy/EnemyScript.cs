using System;
using System.Collections;
using Level;
using UnityEngine;

namespace Enemy
{
    public class EnemyScript : MonoBehaviour
    {
        [SerializeField] public float enemyHealth = 2.0f;
        public float enemyDamage = 1.0f;
        [SerializeField] public bool immortal = false;
        private EnemyAudioScript audioScript;
        [SerializeField] private LevelLogicScript levelLogic;

        private void Awake()
        {
            audioScript = GetComponent<EnemyAudioScript>();
        }

        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Projectile"))
                DecrementHealth();
        }

        private void DecrementHealth()
        {
            // Return if the enemy is Immortal
            if (immortal) return;

            // Check if Enemy should be killed
            if (enemyHealth - 1 <= 0)
            {
                levelLogic.IncrementScore(enemyDamage);
                StartCoroutine(OnDeath());
                return;
            }
            
            // Reduce Health & play damage SFX
            enemyHealth--;
            StartCoroutine(OnDamage());
        }

        private IEnumerator OnDeath()
        {
            // For additional behaviour on death
            // Andreas: EXPLOSIONS!
            yield return new WaitForSeconds(audioScript.PlayDeathRattle());
            Destroy(gameObject);
        }
        
        private IEnumerator OnDamage()
        {
            yield return new WaitForSeconds(audioScript.PlayDamageSound());
        }
    }
}