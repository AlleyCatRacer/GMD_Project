using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        if (other.CompareTag("Enemy"))
            _targetLocations.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        _targetLocations.Remove(other.transform);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for a Target
        if (_currentTarget == null) return;

        // Look at the Target
        transform.LookAt(_currentTarget, Vector3.up);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
    }

    private void FixedUpdate()
    {
        // Reduce Cooldown
        _currentCooldown -= Time.deltaTime;

        if (!_targetLocations.Any()) return;
        _currentTarget = _targetLocations[0];

        // Check if Shooting is available
        if (_currentCooldown < 0f)
        {
            // Ensure the Target is being looked at, even if Framerate is dying
            transform.LookAt(_currentTarget, Vector3.up);
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            // Replace with a LERP to figure out angle, use angle later to ensure target is close enough in sight for

            // Shoot
            var bullet = Instantiate(bulletPrefab, bulletSpawnLocation.position, Quaternion.identity);
            bullet.transform.LookAt(_currentTarget, Vector3.up);

            // Set Timer on Bullet
            Destroy(bullet, _bulletLifeDuration);

            // Reset Cooldown
            _currentCooldown = fireDelay;
        }
    }
}