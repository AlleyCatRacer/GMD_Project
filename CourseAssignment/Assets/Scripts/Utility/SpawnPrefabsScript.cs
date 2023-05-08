using System;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using Path;
using SplineMesh.Scripts.Bezier;
using UnityEngine;

namespace Utility
{
    public class SpawnPrefabsScript : MonoBehaviour
    {
        [SerializeField] private float delayBetweenWaves = 5f;
        [SerializeField] private float delayBetweenSpawns = .3f;
        private float _cooldown;

        // Enemy and Wave details
        private EnemyPrefabReference ref_enemies;
        private WaveReference ref_waves;

        // Path to spawn things on
        private Spline path;

        // Spawn Queue
        private List<GameObject> _spawnQueue;

        private void Awake()
        {
            path = GetComponent<Spline>();
            ref_enemies = GetComponent<EnemyPrefabReference>();
            ref_waves = GetComponent<WaveReference>();

            _spawnQueue = new();
            _cooldown = delayBetweenSpawns;
        }

        private void Start()
        {
            InvokeRepeating(nameof(SetNextWave), 0f, delayBetweenWaves);
        }

        private void SetNextWave()
        {
            // Debug
            UnityEngine.Debug.Log($"Sending Wave {ref_waves.Current}");

            try
            {
                // Get the Next Wave's information
                var waveQueue = ref_waves.Next();

                // Go through info and add as needed
                foreach (var refAndAmount in waveQueue)
                {
                    for (int i = 0; i < refAndAmount.Value; i++)
                    {
                        var next = ref_enemies.Get(refAndAmount.Key);
                        _spawnQueue.Add(next);
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
                // No more rounds
                CancelInvoke(nameof(SetNextWave));
            }
        }

        private void FixedUpdate()
        {
            // Return if still on Cooldown
            if (_cooldown > 0)
            {
                _cooldown -= Time.deltaTime;
                return;
            }

            // Check if anything is available in the Queue
            if (!_spawnQueue.Any()) return;

            // Get the next thing to spawn
            var next = _spawnQueue.First();

            // Remove it from the list
            _spawnQueue.Remove(next);

            // Spawn the thing
            var newEnemy = Instantiate(next, path.nodes[0].Position, Quaternion.identity);

            // Set the Path for the enemy to follow
            newEnemy.GetComponent<FollowPathScript>().SplineToFollow = path;

            _cooldown = delayBetweenSpawns;
        }
    }
}