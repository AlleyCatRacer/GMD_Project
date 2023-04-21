using System.Linq;
using UnityEngine;

namespace Projectile_Scripts
{
    public class TargetingScript : MonoBehaviour
    {
        [SerializeField] private DetectionScript detectionScript;

        private GameObject targetEnemy;

        [SerializeField] private float rotationSpeed = 3.0f;

        [SerializeField] private AutoFiringScript autoFiringScript;

        private Vector3 lastKnownPosition;

        private Quaternion towerRotation;

        private void Start()
        {
            CheckTargets();
        }

        private void FixedUpdate()
        {
            CheckTargets();
        
            // If there is no target within range, return
            if (targetEnemy == null)
            {
                StopCoroutine(autoFiringScript.FiringLoop());
                return;
            }
        
            Debug.DrawLine(transform.position, targetEnemy.transform.position, Color.red);
        
            // If target moved since last time update the position
            if (lastKnownPosition != targetEnemy.transform.position)
            {
                lastKnownPosition = targetEnemy.transform.position;
                towerRotation = Quaternion.LookRotation(lastKnownPosition - transform.position);
            }

            // If tower is not facing towards the updated position rotate it
            if (transform.rotation != towerRotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, towerRotation, rotationSpeed * Time.deltaTime);
            }

            StartCoroutine(autoFiringScript.FiringLoop());
        }

        private void CheckTargets()
        {
            if (detectionScript.GetEnemiesInRange().Any())
                targetEnemy = detectionScript.GetEnemiesInRange()[0];
        }
    }
}
