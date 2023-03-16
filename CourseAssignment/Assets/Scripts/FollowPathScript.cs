using SplineMesh;
using UnityEngine;

public class FollowPathScript : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    

    private float progress =0f;
    
    public Spline SplineToFollow { get; set; }
    
    private void Update()
    {
        // Check if the enemy has exited the spline
        if (progress > SplineToFollow.Length)
        {
            Destroy(gameObject);
        }

        // Figure out next position on Spline
        CurveSample nextPos = SplineToFollow.GetSampleAtDistance(progress);

        // Move to next Position
        transform.position = nextPos.location;
        // Rotate to match Spline
        transform.rotation = nextPos.Rotation;

        // Update Progress
        progress += speed * Time.deltaTime;
    }
}
