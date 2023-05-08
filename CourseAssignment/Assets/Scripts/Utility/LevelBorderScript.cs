using UnityEngine;

namespace Utility
{
    public class LevelBorderScript : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Projectile"))
                Destroy(other.gameObject);
        }
    }
}
