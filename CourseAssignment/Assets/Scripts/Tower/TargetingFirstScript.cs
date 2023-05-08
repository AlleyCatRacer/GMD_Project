using System;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using JetBrains.Annotations;
using UnityEngine;

namespace Tower
{
    public class TargetingFirstScript : MonoBehaviour
    {
        // Target Info
        private List<Transform> targetLocations = new();
        private Transform currentTarget;

        private void FixedUpdate()
        {
            Retarget();
        }

        [CanBeNull]
        public Transform GetNextEnemy()
        {
            try
            {
                // Returns the First Enemy if any is found. Null otherwise
                return targetLocations.Any() ? targetLocations[0] : null;
            }
            catch (ArgumentNullException)
            {
                targetLocations.RemoveAt(0);
                return null;
            }
        }

        public void AddEnemy(Transform enemy)
        {
            // Check if the enemy is already added
            if (targetLocations.Contains(enemy.transform)) return;

            // Check if the Enemy is further ahead on the Track than other enemies
            if (InFrontOfRest(enemy.transform))
                targetLocations.Insert(0, enemy.transform);
            else
                targetLocations.Add(enemy.transform);
        }

        private bool InFrontOfRest(Component enemy)
        {
            // True if no other enemies are present
            if (targetLocations.Count < 1) return true;

            // Get the Front Runner
            Transform frontRunner = GetNextEnemy();
            
            // False if the Front Runner is not alive
            if (frontRunner == null)
                return false;
            
            // Get Progress for Front Runner and 
            var frontRunnerProgress = frontRunner.gameObject.GetComponent<FollowPathScript>().Progress;
            var newEnemyProgress = enemy.GetComponent<FollowPathScript>().Progress;

            // True if New Enemy is further ahead than the previous Front Runner
            return frontRunnerProgress < newEnemyProgress;

            /*
            var otherEnemyProgress = 99999999.0f;
            var currentEnemyProgress = 0.0f;
            try
            {
                otherEnemyProgress = currentTarget.GetComponent<FollowPathScript>().Progress;
                currentEnemyProgress = enemy.GetComponent<FollowPathScript>().Progress;
            }
            catch (Exception e)
            {
                Debug.Log($"Enemy died during comparison: {e.GetType()}");
            }

            return currentEnemyProgress < otherEnemyProgress;
            */
        }

        public void RemoveEnemy(Transform enemy)
        {
            try
            {
                targetLocations.Remove(enemy);
            }
            catch (NullReferenceException e)
            {
                Debug.Log($"----- Enemy not found in target list:\n{e.Message}");
            }

//            Retarget();
        }

        private void Retarget()
        {
            // Check if any targets are in range and if so target the first one on the list
            currentTarget = GetNextEnemy();
            if (currentTarget == null)
                return;

            // Ensure the Target is being looked at, even if Framerate is dying
            transform.LookAt(currentTarget, Vector3.up);
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            // Replace with a LERP to figure out angle, use angle later to ensure target is close enough in sight for
        }
    }
}