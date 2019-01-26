using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Rotate : MonoBehaviour
{

    #region Public Variables
    [Range(1.0f, 10.0f)]
    public float speed = 5.0f;
    [Range(0.0f, 5.0f)]
    public float cameraHeight = 1.5f;
    [Range(0.0f, 15.0f)]
    public float cameraDistance = 7.5f;
    #endregion

    #region Private Variables
    private GameObject player;
    private Vector3 offset;
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
	void Update()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * speed, Vector3.up) * offset;
        transform.position = player.transform.position + offset;

        //float newCameraDistance = cameraDistance;

        //RaycastHit hit;
        //if(Physics.Raycast(player.transform.position, this.transform.position, out hit))
        //{
        //    float distanceCheck = Vector3.Distance(hit.point, player.transform.position);
        //    if (distanceCheck < cameraDistance)
        //    {
        //        newCameraDistance = distanceCheck;
        //    }
        //}

        //offset.Normalize();
        //offset *= newCameraDistance;

        this.transform.LookAt(player.transform.position);
    }
}
