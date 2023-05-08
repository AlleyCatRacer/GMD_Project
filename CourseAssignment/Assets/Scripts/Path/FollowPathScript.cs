using System;
using SplineMesh.Scripts.Bezier;
using UnityEngine;

namespace Path
{
    public class FollowPathScript : MonoBehaviour
    {
        [SerializeField] private float speed = 3f;


        private float _progress = 0f;

        public float Progress => _progress;
        public Spline SplineToFollow { get; set; }

        private void Update()
        {
            if (SplineToFollow == null) return;
            
            // Check if the enemy has exited the spline
            if (_progress > SplineToFollow.Length)
            {
                Destroy(gameObject);
            }

            try
            {
                // Figure out next position on Spline
                CurveSample nextPos = SplineToFollow.GetSampleAtDistance(_progress);

                // Move to next Position
                transform.position = nextPos.location;
                // Rotate to match Spline
                transform.rotation = nextPos.Rotation;

                // Update Progress
                _progress += speed * Time.deltaTime;
            }
            catch (ArgumentException e)
            {
                Destroy(gameObject);
            }
        }
    }
}