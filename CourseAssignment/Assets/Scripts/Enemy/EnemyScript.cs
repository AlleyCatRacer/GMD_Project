using System;
using Projectile_Scripts;
using Tower;
using UnityEngine;

namespace Enemy
{
    public class EnemyScript : MonoBehaviour
    {
        [SerializeField] public float enemyHealth = 2.0f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Projectile"))
                DecrementHealth();
        }

        private void DecrementHealth()
        {
            enemyHealth--;
            if (enemyHealth <= 0)
                OnDeath();
        }

        private void OnDeath()
        {
            // For additional behaviour on death
            Destroy(gameObject);
        }
    }
}