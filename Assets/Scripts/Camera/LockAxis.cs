using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAxis : MonoBehaviour
{
    #region Public Variables
    [Header("Positional Locks")]
    public bool lockXPosition = false;
    public bool lockYPosition = false;
    public bool lockZPosition = false;
    [Header("Rotational Locks")]
    public bool lockXRotation = false;
    public bool lockYRotation = false;
    public bool lockZRotation = false;
    #endregion

    #region Private Variables
    private float lockedXPosition = 0.0f;
    private float lockedYPosition = 0.0f;
    private float lockedZPosition = 0.0f;
    private float lockedXRotation = 0.0f;
    private float lockedYRotation = 0.0f;
    private float lockedZRotation = 0.0f;
    private bool locked = false;
    #endregion

    /// <summary>
    /// Sets the positions of locked axii to their locked positions.
    /// </summary>
	void Update ()
    {
        if (lockXPosition) this.transform.position = SetVectorAxis(this.transform.position, "x", lockedXPosition);
        if (lockYPosition) this.transform.position = SetVectorAxis(this.transform.position, "y", lockedYPosition);
        if (lockZPosition) this.transform.position = SetVectorAxis(this.transform.position, "z", lockedZPosition);
        if (lockXRotation) this.transform.rotation = Quaternion.Euler(SetVectorAxis(this.transform.rotation.eulerAngles, "x", lockedXRotation));
        if (lockYRotation) this.transform.rotation = Quaternion.Euler(SetVectorAxis(this.transform.rotation.eulerAngles, "y", lockedYRotation));
        if (lockZRotation) this.transform.rotation = Quaternion.Euler(SetVectorAxis(this.transform.rotation.eulerAngles, "z", lockedZRotation));
    }

    /// <summary>
    /// Initializes the lock positions after update has been called.
    /// </summary>
    void LateUpdate()
    {
        if(!locked)
        {
            if (lockXPosition) lockedXPosition = this.transform.position.x;
            if (lockYPosition) lockedYPosition = this.transform.position.y;
            if (lockZPosition) lockedZPosition = this.transform.position.z;
            if (lockXRotation) lockedXRotation = this.transform.rotation.eulerAngles.x;
            if (lockYRotation) lockedYRotation = this.transform.rotation.eulerAngles.y;
            if (lockZRotation) lockedZRotation = this.transform.rotation.eulerAngles.y;
            locked = !locked;
        }
    }

    /// <summary>
    /// Returns a vector with an axis value changed.
    /// </summary>
    /// <param name="v">The vector to be changed.</param>
    /// <param name="axis">The axis that will be changed (x,y,z).</param>
    /// <param name="value">The value to change it to.</param>
    /// <returns>The new, changed vector.</returns>
    Vector3 SetVectorAxis(Vector3 v, string axis, float value)
    {
        switch(axis)
        {
            case ("x"):
            case ("X"): return new Vector3(value, v.y, v.z); //Change the X value
            case ("y"):
            case ("Y"): return new Vector3(v.x, value, v.z); //Change the Y value
            case ("z"):
            case ("Z"): return new Vector3(v.x, v.y, value); //Change the Z value
            default:    return v;
        }
    }
}
