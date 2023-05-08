using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;

namespace Tower
{
    public class AutoShootingScript : MonoBehaviour
    {
        // Complex Serialized
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawnLocation;

        // Simple Serialized
        [SerializeField] private float fireDelay = .1f;
        [SerializeField] private float spinSpeed = .1f;

        // Bullet info
        private float _bulletSpeed;
        private float _bulletLifeDuration;

        // Target Info
        private List<Transform> _targetLocations = new();
        private Transform _currentTarget;
        private Transform _deadTarget;

        // Other things
        private float _currentCooldown;

        private void Awake()
        {
            var bulletInfo = bulletPrefab.GetComponent<BulletScript>();

            if (bulletInfo != null)
            {
                _bulletSpeed = bulletInfo.Speed;
                _bulletLifeDuration = bulletInfo.Lifetime;
            }
        }

        private void Start()
        {
            _currentCooldown = fireDelay;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if the incoming Collider belongs to an Enemy
            if (other.transform.parent.CompareTag("Enemy"))
            {
                // Check if the enemy is already added
                if (_targetLocations.Contains(other.transform.parent)) return;
                
                // Check if the Enemy is further ahead on the Track than other enemies
                if (_targetLocations.Count < 1 ||
                    !(other.GetComponentInParent<FollowPathScript>().Progress >
                      _targetLocations[0].GetComponentInParent<FollowPathScript>().Progress))
                {
                    _targetLocations.Add(other.transform);
                }
                else
                {
                    _targetLocations.Insert(0, other.transform);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _targetLocations.Remove(other.transform);
        }

        // Update is called once per frame
        void Update()
        {
            // Check for a Target
            if (_currentTarget == null)
            {
                TargetKilled();
            }
        }

        private void FixedUpdate()
        {
            // Reduce Cooldown
            _currentCooldown -= Time.deltaTime;

            // Find target
            Retarget();

            // Check if Shooting is available and there is a target in range
            if (_currentCooldown <= 0f && _currentTarget != null)
            {
                Shoot();

                // Check if bullet killed enemy and retarget if necessary
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
            _deadTarget = _currentTarget;
            var bullet = Instantiate(bulletPrefab, bulletSpawnLocation.position, Quaternion.identity);
            bullet.transform.LookAt(_currentTarget, Vector3.up);

            // Set Timer on Bullet
            Destroy(bullet, _bulletLifeDuration);
        }

        private void TargetKilled()
        {
            try
            {
                _targetLocations.Remove(_deadTarget);
            }
            catch (NullReferenceException e)
            {
                UnityEngine.Debug.Log($"----- Dead enemy not found in target list:\n{e.Message}");
            }

            _deadTarget = null;
            Retarget();
        }

        private void Retarget()
        {
            // Check if any targets are in range and if so target the first one on the list
            if (!_targetLocations.Any())
            {
                _currentTarget = null;
                return;
            }

            // Set Current Target as the First
            _currentTarget = _targetLocations[0];

            // Ensure the Target is being looked at, even if Framerate is dying
            transform.LookAt(_currentTarget, Vector3.up);
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            // Replace with a LERP to figure out angle, use angle later to ensure target is close enough in sight for
        }
    }
}