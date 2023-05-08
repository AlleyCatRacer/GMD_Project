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
            {
                Debug.Log("I see it");
                targetingScript.AddEnemy(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("I don't see it anymore");
            targetingScript.RemoveEnemy(other.transform);
        }

        // Update is called once per frame
        private void Update()
        {
            // Update current target
            _currentTarget = targetingScript.GetNextEnemy();
            
            if (_currentTarget == null)
            {
                TargetKilled();
            }
        }

        private void FixedUpdate()
        {
            // Reduce Cooldown
            _currentCooldown -= Time.deltaTime;

            // Update
            _currentTarget = targetingScript.GetNextEnemy();

            // Check if Shooting is available and there is a target in range
            if (_currentCooldown <= 0f && _currentTarget != null)
            {
                Shoot();

                // Check if bullet killed enemy
                if (_currentTarget == null)
                {
                    TargetKilled();
                }

                // Reset Cooldown
                _currentCooldown = fireDelay;
            }
        }

        private void Shoot()
        {
            // Store target to remove from target list in case it is killed
            targetingScript.deadTarget = _currentTarget;
            var bullet = Instantiate(bulletPrefab, bulletSpawnLocation.position, Quaternion.identity);
            bullet.transform.LookAt(_currentTarget, Vector3.up);

            // Set Timer on Bullet
            Destroy(bullet, _bulletLifeDuration);
        }

        private void TargetKilled()
        {
            Debug.Log("Death");
            targetingScript.KillEnemy();
        }
    }
}