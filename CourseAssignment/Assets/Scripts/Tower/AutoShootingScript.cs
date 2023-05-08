using System;
using UnityEngine;

namespace Tower
{
    public class AutoShootingScript : MonoBehaviour
    {
        // Complex Serialized
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawnLocation;

        // Simple Serialized
        [SerializeField] private float fireDelay = .1f;

        // Bullet info
        private float _bulletLifeDuration;

        // Target Info
        private TargetingFirstScript targetingScript;
        private Transform _currentTarget;

        // Other things
        private float _currentCooldown;

        private void Awake()
        {
            var bulletInfo = bulletPrefab.GetComponent<BulletScript>();

            if (bulletInfo != null)
            {
                _bulletLifeDuration = bulletInfo.Lifetime;
            }

            targetingScript = GetComponent<TargetingFirstScript>();
        }

        private void Start()
        {
            _currentCooldown = fireDelay;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if the incoming Collider belongs to an Enemy
            if (other.gameObject.CompareTag("Enemy"))
                targetingScript.AddEnemy(other.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
                targetingScript.RemoveEnemy(other.transform);
        }

        private void FixedUpdate()
        {
            // Reduce Cooldown
            _currentCooldown -= Time.deltaTime;

            // Update
            var possibleTarget = targetingScript.GetNextEnemy();

            // Check if Shooting is available and there is a target in range
            if (_currentCooldown > 0f || possibleTarget == null)
                return;
            
            // Shoot at the enemy
            Shoot();

            // Reset Cooldown
            _currentCooldown = fireDelay;
        }

        private void Shoot()
        {
            // Store target to remove from target list in case it is killed
            var bullet = Instantiate(bulletPrefab, bulletSpawnLocation.position, Quaternion.identity);
            bullet.transform.LookAt(targetingScript.GetNextEnemy(), Vector3.up);

            // Set Timer on Bullet
            Destroy(bullet, _bulletLifeDuration);
        }
    }
}