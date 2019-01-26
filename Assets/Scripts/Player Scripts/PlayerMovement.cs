using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Public Variables
    [Range(0.001f, 1.0f)]
    public float playerSpeed = 2.0f;
    #endregion

    #region Private Variables
    private GameObject mainCamera;
    private bool isMoving;
    #endregion

    /// <summary>
    /// Sets the camera reference to the main camera.
    /// </summary>
    void Start ()
    {
        mainCamera = GameObject.Find("Player Camera"); //Camera needs to be named "Player Camera"
	}
	
    /// <summary>
    /// Updates the player's movement code.
    /// </summary>
	void Update ()
    {
        Move();
	}

    /// <summary>
    /// Makes the player move based on player input.
    /// </summary>
    void Move()
    {
        isMoving = false;
        if (mainCamera != null)
        {
            //The vector from the camera -> player
            Vector3 movementVector = this.transform.position - mainCamera.transform.position;
            //Chop off the Y so they're always moving relative to the ground
            movementVector.y = 0;
            movementVector.Normalize();
            movementVector *= playerSpeed;

            Vector3 posCheck = this.transform.position;

            //Forward/Backward
            if (Input.GetKey(KeyCode.W))
            {
                this.transform.position += movementVector;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                this.transform.position += -movementVector;
            }

            //Right/Left
            if (Input.GetKey(KeyCode.D))
            {
                Quaternion rotation = Quaternion.Euler(0, 90, 0);
                movementVector = rotation * movementVector;
                this.transform.position += movementVector;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Quaternion rotation = Quaternion.Euler(0, -90, 0);
                movementVector = rotation * movementVector;
                this.transform.position += movementVector;
            }

            if (posCheck != this.transform.position)
                isMoving = true;
        }
    }

    #region Properties
    public bool IsMoving { get { return isMoving; } } //Returns whether the player is moving or not
    #endregion
}
