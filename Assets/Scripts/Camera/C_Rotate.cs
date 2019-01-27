using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Rotate : MonoBehaviour
{
    #region Public Variables
    [Range(1.0f, 10.0f)]
    public float speed = 5.0f;             //The camera's speed - basically the mouse sensitivity
    [Range(0.0f, 5.0f)]
    public float cameraHeight = 1.5f;      //The camera's hight from the player's perspective
    [Range(0.0f, 15.0f)]
    public float cameraDistance = 7.5f;    //The distance from the player to the camera (on an arc)
    [Range(0.0f, 1.5f)]
    public float distanceThreshold = 1.0f; //The extra distance added to the camera's position so it dosen't clip through walls
    #endregion

    #region Private Variables
    private GameObject player;     //Player reference 
    private Vector3 offset;        //The camera offset vector
    #endregion

    /// <summary>
    /// Locks the cursor and sets up the camera for third-person rotation.
    /// </summary>
    void Start()
    {
        player = this.transform.parent.gameObject;
        offset = new Vector3(player.transform.position.x, player.transform.position.y + cameraHeight, player.transform.position.z + cameraDistance);
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Rotates the camera around the player based on mouse input difference.
    /// </summary>
	void LateUpdate()
    {
        //Create an angle axis from mouse input and the original offset vector
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * speed, Vector3.up) * offset;

        //Set where the camera *should* be after movement
        Vector3 goalPosition = player.transform.position + offset;

        //If the position of the camera is where it started, don't smooth it and just move it
        this.transform.position = goalPosition;

        //Initial camera "distance"
        float newCameraDistance = cameraDistance - distanceThreshold;

        //Raycast from the player to the camera
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, -(player.transform.position - this.transform.position), out hit))
        {
            if (hit.transform.gameObject.name != "Player")
            {
                float distanceCheck = Vector3.Distance(player.transform.position, hit.point);
                //If the raycast hits an object inbetween the camera and the player...
                if (distanceCheck < cameraDistance)
                {
                    //Make the cameras distance the distance to that object
                    newCameraDistance = Mathf.Clamp(distanceCheck - distanceThreshold, 0.1f, cameraDistance);
                }
            }
        }

        //Normalize the offset and set its magnitude
        offset.Normalize();
        offset *= newCameraDistance;

        //Orient the camera towards the player
        this.transform.LookAt(player.transform.position);
    }
}