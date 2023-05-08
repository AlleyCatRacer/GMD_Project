using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject instance;

    public GameObject Instance
    {
        get => instance;
        private set => instance = value;
    }

    private void Start()
    {
        // Ensure this stays alive through Scene Changes
        if (instance == null)
        {
            instance = gameObject;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}