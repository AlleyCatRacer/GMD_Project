using SplineMesh;
using UnityEngine;

public class SpawnPrefabsScript : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnablePrefabs;
    [SerializeField] private float delayBetweenSpawns = 5f;

    private Spline path;

    private void Awake()
    {
        path = GetComponent<Spline>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, delayBetweenSpawns);
    }

    private void SpawnEnemy()
    {
        // Spawn an Enemy
        GameObject newEnemy = Instantiate(spawnablePrefabs[0], path.nodes[0].Position , Quaternion.identity, transform);

        // Set Path for new Enemy
        newEnemy.GetComponent<FollowPathScript>().SplineToFollow = path;
    }
}