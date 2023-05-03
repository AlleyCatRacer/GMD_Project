using UnityEngine;

namespace Enemy
{
    public class EnemyScript : MonoBehaviour
    {
        [SerializeField] public float enemyHealth = 2.0f;
        [SerializeField] public float enemyDamage = 1.0f;
        [SerializeField] public bool immortal = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Projectile"))
                DecrementHealth();
        }

        private void DecrementHealth()
        {
            // Return if the enemy is Immortal
            if (immortal) return;
            
            // Reduce Health
            enemyHealth--;
            
            // Check if Enemy should be killed
            if (enemyHealth <= 0)
                OnDeath();
        }

        private void OnDeath()
        {
            // For additional behaviour on death
            // Andreas: EXPLOSIONS!
            Destroy(gameObject);
        }
    }
}