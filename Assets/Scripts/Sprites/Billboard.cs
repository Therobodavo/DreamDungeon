using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    #region Public Variables
    [Header("Billboarding Speed")]
    [Tooltip("Speed")]
    [Range(0.0f, 1.0f)]
    public float interpolation = 1.0f;
    #endregion

    #region Private Variables
    private GameObject target;
    private Vector3 forward;
    #endregion

    /// <summary>
    /// Sets the target "lookat" destination to the main camera.
    /// </summary>
    void Start()
    {
        target = GameObject.Find("Player Camera"); //Camera needs to be named "Player Camera"
    }

    /// <summary>
    /// Updates the sprite's forward vector after the camera's rotation has been updated.
    /// </summary>
	void LateUpdate()
    {
        Vector3 newTarget = target.transform.forward;
        newTarget = new Vector3(newTarget.x, 0, newTarget.z);
        this.transform.forward = Vector3.Lerp(forward, newTarget, interpolation);
        this.forward = this.transform.forward;
    }
}
