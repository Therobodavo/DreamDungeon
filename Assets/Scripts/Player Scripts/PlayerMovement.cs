﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Public Variables
    [Header("Movement")]
    [Range(1.0f, 50.0f)]
    public float speed = 20.0f;
    [Range(1.0f, 100.0f)]
    public float maxSpeed = 10.0f;
    [Range(1.0f, 100.0f)]
    public float slowdown = 20.0f;
    [Header("Jumping")]
    [Range(0.1f, 2.0f)]
    public float jumpCheck = 2.0f;
    [Range(1.0f, 250.0f)]
    public float jumpForce = 100.0f;
    [Range(1.0f, 50.0f)]
    public float gravity = 9.81f;
    #endregion

    #region Private Variables
    private GameObject mainCamera;
    private Rigidbody body;
    private bool isMoving;
    private bool isJumping;
    #endregion

    /// <summary>
    /// Sets the camera reference to the main camera.
    /// </summary>
    void Start ()
    {
        mainCamera = GameObject.Find("Player Camera"); //Camera needs to be named "Player Camera"
        body = this.GetComponent<Rigidbody>();
        isMoving = false;
        isJumping = false;
	}
	
    /// <summary>
    /// Updates the player's movement code.
    /// </summary>
	void Update ()
    {
        Move();
        Jump();
	}

    /// <summary>
    /// Checks to see if the player is in the air or on the ground, and jumps on input.
    /// </summary>
    void Jump()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, -this.transform.up, out hit, this.GetComponent<Collider>().bounds.extents.y + jumpCheck))
        {
            float floorDistance = Vector3.Distance(hit.point, this.transform.position);
            isJumping = !(floorDistance < jumpCheck);
        }

        if(!isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                body.AddForce(this.transform.up * jumpForce * 10.0f);
            }
        }
        else
        {
            body.AddForce(-this.transform.up * gravity * body.mass * 10.0f);
        }
    }

    /// <summary>
    /// Makes the player move based on player input.
    /// </summary>
    void Move()
    {
        isMoving = false;
        if(mainCamera != null && body != null)
        {
            Vector3 forwardVector = mainCamera.transform.forward;  //Get and normalize forward vector from camera
            forwardVector.Set(forwardVector.x, 0, forwardVector.z);
            forwardVector.Normalize();

            Vector3 rightVector = mainCamera.transform.right;      //Get and normalize right vector from camera
            rightVector.Set(rightVector.x, 0, rightVector.z);
            rightVector.Normalize();

            Vector3 force = Vector3.zero;

            if (Input.GetKey(KeyCode.W))      //Forward
            {
                force += forwardVector * speed;
            }
            else if (Input.GetKey(KeyCode.S)) //Down
            {
                force += -forwardVector * speed;
            }

            if (Input.GetKey(KeyCode.A))      //Left
            {
                force += -rightVector * speed;
            }
            else if (Input.GetKey(KeyCode.D)) //Right
            {
                force += rightVector * speed;
            }

            if(force != Vector3.zero) //If the added force ISN'T zero
            {
                //The player is moving
                isMoving = true; 
            }
            else
            {
                //If no keys are being pressed, multiply is velocity by the slowdown
                if (body.velocity.magnitude != 0.0f)
                    body.velocity = body.velocity * (100 - slowdown) / 100.0f;
            }

            if (body.velocity.magnitude < maxSpeed)
                body.AddForce(force * body.mass);

        }
    }

    #region Properties
    public bool IsMoving { get { return isMoving; } }   //Returns whether the player is moving or not
    public bool IsJumping { get { return isJumping; } } //Returns whether the player is jumping (in the air) or not
    #endregion
}
