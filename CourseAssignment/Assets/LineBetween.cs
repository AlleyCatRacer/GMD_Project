using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBetween : MonoBehaviour
{
    [SerializeField] private Transform PosA, PosB;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(PosA.position, PosB.position);
    }
}
