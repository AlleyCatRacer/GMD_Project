using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}