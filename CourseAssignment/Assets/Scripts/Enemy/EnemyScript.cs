using System;
using System.Collections;
using Level;
using UnityEngine;

namespace Enemy
{
    public class EnemyScript : MonoBehaviour
    {
        [SerializeField] public float enemyHealth = 2.0f;
        [SerializeField] public bool immortal = false;
        [SerializeField] private LevelLogicScript levelLogic;
        public float enemyDamage = 1.0f;
        private EnemyAudioScript audioScript;

        // private GameManager _manager;

        private void Awake()
        {
            audioScript = GetComponent<EnemyAudioScript>();
            if (levelLogic == null)
            {
                levelLogic = FindObjectOfType<LevelLogicScript>();
            }
            // _manager = FindObjectOfType<GameManager>();
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
                // _manager.SendMessage("IncreaseScore", 10);
                levelLogic.IncreaseScore(10);
                StartCoroutine(OnDeath());
                return;
            }

            // Reduce Health & play damage SFX
            levelLogic.IncreaseScore(5);
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