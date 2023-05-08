using System;
using Enemy;
using UnityEngine;

namespace Level
{
    public class LevelLogicScript : MonoBehaviour
    {
        [SerializeField] private float lives = 100.0f;
        
        private void OnTriggerEnter(Collider other)
        {
            if(!other.gameObject.CompareTag("Enemy")) return;
            var enemyScript = other.gameObject.GetComponent<EnemyScript>();
            DecrementLives(enemyScript.enemyDamage);
        }

        /// <summary>
        /// Decrements player lives by the damage amount of the enemy.
        /// </summary>
        /// <param name="damage">Float of how many lives an enemy takes if they make it to the exit portal.</param>
        private void DecrementLives(float damage)
        {
            var updatedLives = lives - damage;
            if (updatedLives > 0)
            {
                lives = updatedLives;
               UnityEngine. Debug.Log($"Damage taken: {damage}\nLives left: {lives}");
                return;
            }
            lives = 0;
            UnityEngine.            Debug.Log("You are dead.");
            // TODO: Trigger game over UI - Aldís 03.05.23
        }
    }
}