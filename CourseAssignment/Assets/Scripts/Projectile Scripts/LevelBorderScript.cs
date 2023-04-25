using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile_Scripts
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