using System;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile_Scripts
{
    public class DetectionScript : MonoBehaviour
    {
        private readonly List<GameObject> enemiesInRange = new ();

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Enemy")) return;
            enemiesInRange.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Enemy")) return;
            enemiesInRange.Remove(other.gameObject);
        }

        public List<GameObject> GetEnemiesInRange()
        {
            return enemiesInRange;
        }
    }
}
