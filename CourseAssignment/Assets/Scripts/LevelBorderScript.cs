using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBorderScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
            Destroy(other.gameObject);
    }
}
