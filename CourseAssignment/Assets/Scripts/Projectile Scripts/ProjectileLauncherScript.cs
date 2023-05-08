using UnityEngine;

namespace Projectile_Scripts
{
    public class ProjectileLauncherScript : MonoBehaviour
    {
        //Inspired by Jakob Knop Rasmussen
        [SerializeField]
        private Rigidbody projectilePrefab;

        [SerializeField]
        private float firingForce = 2000f;

        [SerializeField]
        private Transform firingPoint;

        void Awake()
        {
            if (GetComponent<AutoFiringScript>() != null)
            {
                GetComponent<AutoFiringScript>().OnFire += Shoot;
            }
        }

        private void Shoot()
        {
            var projectile = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
            projectile.AddForce(projectile.transform.forward * firingForce);
        }
    }
}
