using System;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private float sensitivity = 100f;

    private float _verticalRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        // Get the Raw Rotation
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        
        // Clamp vertical rotation
        _verticalRotation -= mouseY;
        _verticalRotation = Math.Clamp(_verticalRotation, -90f, 90f);
        
        // Apply rotations
        transform.Rotate(Vector3.up * mouseX);
        camera.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
    }
}
