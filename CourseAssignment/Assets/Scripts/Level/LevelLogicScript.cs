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
        private SceneNavigator navigator;
        private int highScore;
        public int score;
        // TODO: Figure out what type the UI text is to able to link it to this variable - Aldís
        public TextMeshProUGUI highScoreText;

        private void Start()
        {
            navigator = GetComponent<SceneNavigator>();
            highScore = PlayerPrefs.GetInt(nameof(highScore), 0);
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

        public void IncreaseScore(int addition)
        {
            // Increase the current score
            score += addition;
            // Update HighScore if the new Score is higher
            if (score > highScore)
                UpdateHighScoreDisplay();
        }

        private void UpdateHighScoreDisplay()
        {
            // Save the Score
            PlayerPrefs.SetInt(nameof(highScore), highScore);
            
            // Update the Visuals
            highScoreText.text = $"{score}";
            highScore = score;
        }
    }
}