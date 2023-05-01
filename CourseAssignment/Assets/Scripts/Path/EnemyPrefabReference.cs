using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyPrefabReference : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    
    public GameObject Get(int enemyIndex)
    {
        if (enemyIndex < 0)
            throw new IndexOutOfRangeException("enemyIndex must be a non-negative integer");
        if (enemyIndex >= enemyPrefabs.Count)
            throw new IndexOutOfRangeException($"enemyIndex must be smaller than {enemyPrefabs.Count}");

        return enemyPrefabs[enemyIndex];
    }
}