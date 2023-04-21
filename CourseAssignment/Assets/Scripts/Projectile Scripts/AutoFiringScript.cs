using System;
using System.Collections;
using UnityEngine;

namespace Projectile_Scripts
{
    public class AutoFiringScript : MonoBehaviour
    {
        //Inspired by Jakob Knop Rasmussen
        public event Action OnFire = delegate { };

        [SerializeField]
        private float waitTime = 0.5f;

        private bool isFiring;

        private void OnCollisionEnter(Collision other)
        {
            if (isFiring) return;
            isFiring = true;
            StartCoroutine(FiringLoop());
        }

        void Awake()
        {
            StartCoroutine(FiringLoop());
        }

        public IEnumerator FiringLoop()
        {
            Debug.Log($"Engaged ice cream launcher at {Time.time}");
            //Should we make the fire rate dynamic by creating the var inside the loop for upgrading towers and such? - Aldís 30.03.23 
            var wait = new WaitForSeconds(waitTime);
            //TODO replace while statement arg with targeting script - Aldís 30.03.23 
            while (true)
            {
                OnFire();
                yield return wait;
            }

            isFiring = false;
            Debug.Log($"Ceased fire at {Time.time}");
        }
    }
}
