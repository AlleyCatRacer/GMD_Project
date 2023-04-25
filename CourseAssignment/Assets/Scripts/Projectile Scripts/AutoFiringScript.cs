using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Projectile_Scripts
{
    public class AutoFiringScript : MonoBehaviour
    {
        //Inspired by Jakob Knop Rasmussen
        
        [SerializeField] private DetectionScript detectionScript;
        
        private GameObject targetEnemy;
        private List<GameObject> enemiesInRange = new ();
        
        public event Action OnFire = delegate { };

        [SerializeField] private float waitTime = 0.5f;

        private bool isFiring;
        
        private void FixedUpdate()
        {
            CheckTargets();
        }

        public IEnumerator FiringLoop()
        {
            Debug.Log($"Engaged ice cream launcher at {Time.time}");
            //Should we make the fire rate dynamic by creating the var inside the loop for upgrading towers and such? - Aldís 30.03.23 
            var wait = new WaitForSeconds(waitTime);
            isFiring = true;
            while (enemiesInRange.Any())
            {
                OnFire();
                yield return wait;
            }

            isFiring = false;
            Debug.Log($"Ceased fire at {Time.time}");
        }
        
        private void CheckTargets()
        {
            if (isFiring) return;
            enemiesInRange = detectionScript.GetEnemiesInRange();
            if (enemiesInRange.Any())
            {
                StartCoroutine(FiringLoop());
            }
        }
    }
}
