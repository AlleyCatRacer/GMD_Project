using System;
using UnityEngine;

namespace Tower
{
    public class BulletScript : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] public float lifetime;

        public float Speed => speed;
        public float Lifetime => lifetime;

        private void Update()
        {
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
            // TODO: Kill enemy as well - Aldís 29.04.23
        }
    }
}