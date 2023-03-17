using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraPositionScript : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private Transform[] positions;
    
    private string[] keys = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

    // Start is called before the first frame update
    void Start()
    {
        // Get positions of all Children
        positions = GetComponentsInChildren<Transform>()[1..];

        // Change Keys to only contain Valid Numbers
        keys = keys[0..positions.Length];
    }

    // Update is called once per frame
    void Update()
    {
        // Get the User Input
        string input = Input.inputString;
        
        // Check if it is a valid input
        if (keys.Contains(input))
        {
            // Convert input to an Integer [Starting from 0]
            int idx = int.Parse(input) - 1;

            // Get the new Transform
            Transform newTransform = positions[idx];
            
            // Update Camera Position / Rotation
            camera.position = newTransform.position;
            camera.rotation = newTransform.rotation;
        }
    }
}