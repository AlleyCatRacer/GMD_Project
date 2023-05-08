using System;
using Enemy;
using TMPro;
using UnityEngine;
using Utility;

namespace Level
{
    public class LevelLogicScript : MonoBehaviour
    {
        [SerializeField] private float lives = 100.0f;
        public float score;
        private SceneNavigator navigator;
        private float highScore;
        // TODO: Figure out what type the UI text is to able to link it to this variable - Aldís
        public TextMeshPro highScoreText;

        private void Start()
        {
            navigator = GetComponent<SceneNavigator>();
            highScore = navigator.highScore;
            UpdateHighScoreDisplay();
        }

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
                Debug.Log($"Damage taken: {damage}\nLives left: {lives}");
                return;
            }
            lives = 0;
            Debug.Log("You are dead.");
            // TODO: Call navigator to display game over UI - Aldís
        }

        public void IncrementScore(float addition)
        {
            score += addition;
            if (score > highScore)
                UpdateHighScoreDisplay();
        }

        private void UpdateHighScoreDisplay()
        {
            highScoreText.text = score.ToString();
            highScore = score;
            navigator.highScore = highScore;
        }
    }
}