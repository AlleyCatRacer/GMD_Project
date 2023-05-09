using System;
using Enemy;
using TMPro;
using UnityEngine;
using Utility;

namespace Level
{
    public class LevelLogicScript : MonoBehaviour
    {
        [SerializeField] private int lives = 100;

        private int highScore;
        private int currentScore;
        public TextMeshProUGUI currentScoreText;
        public TextMeshProUGUI highScoreText;
        public TextMeshProUGUI livesText;

        private void Start()
        {
            highScore = PlayerPrefs.GetInt(nameof(highScore), 0);
            UpdateHighScoreDisplay();
        }

        private void FixedUpdate()
        {
            UpdateHighScoreDisplay();
            UpdateLivesDisplay();
            UpdateCurrentScoreDisplay();
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
            int temp = (int) damage;
            var updatedLives = lives - temp;
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
            currentScore += addition;
            // Update HighScore if the new Score is higher
            if (currentScore > highScore)
                UpdateHighScoreDisplay();
        }

        private void UpdateHighScoreDisplay()
        {
            // Save the Score
            PlayerPrefs.SetInt(nameof(highScore), highScore);
            
            if (highScoreText == null) return;
            
            // Update the Visuals
            highScoreText.text = $"{currentScore}";
            highScore = currentScore;
        }
        
        private void UpdateCurrentScoreDisplay()
        {
            // Save the Score
            PlayerPrefs.SetInt(nameof(currentScore), currentScore);
            
            if (currentScoreText == null) return;
            
            // Update the Visuals
            currentScoreText.text = $"{currentScore}";
        }
        
        private void UpdateLivesDisplay()
        {
            // Save the Score
            PlayerPrefs.SetInt(nameof(lives), lives);
            
            if (livesText == null) return;
            
            // Update the Visuals
            livesText.text = $"Lives: {currentScore}";
        }
    }
}