using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float gravity = -10f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = .4f;
    [SerializeField] private LayerMask canJumpFrom;

    private CharacterController _controller;
    private Vector3 _velocity;
    private bool _isGrounded;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        // Check if the player is grounded
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, canJumpFrom);

        // Reset Velocity if grounded
        if (_isGrounded && _velocity.y < 0)
            _velocity.y = -.2f;

        // Get Raw Movement
        float right = Input.GetAxis("Horizontal");
        float fwd = Input.GetAxis("Vertical");

        // Apply Movement
        Vector3 move = transform.right * right + transform.forward * fwd;
        _controller.Move(move * (speed * Time.deltaTime));
        
        // Jump
        if (Input.GetButtonDown("Jump") && _isGrounded)
            _velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        
        // Gravity
        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}