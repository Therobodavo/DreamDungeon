using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    [Range(0.1f, 10.0f)]
    public float rotationSpeed = 1.0f;

    /// <summary>
    /// Rotate the skybox over time.
    /// </summary>
	void LateUpdate ()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
