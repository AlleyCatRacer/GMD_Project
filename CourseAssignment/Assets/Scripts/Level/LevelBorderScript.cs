using UnityEngine;

namespace Level
{
    public class LevelBorderScript : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Projectile"))
                Destroy(other.gameObject);
        }
    }
}