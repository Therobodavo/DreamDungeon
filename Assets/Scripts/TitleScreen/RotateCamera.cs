using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotateSpeed;
    public float rotateDistance;
    public float height;
    public Vector3 rotatePoint;
    
    private float timer;
    private float t_height = 30;

    void Update()
    {
        t_height = Mathf.Lerp(t_height, height, 1.0f * Time.deltaTime);
        float x = rotatePoint.x + Mathf.Sin(timer * rotateSpeed) * rotateDistance;
        float z = rotatePoint.z + Mathf.Cos(timer * rotateSpeed) * rotateDistance;
        this.transform.position = new Vector3(x, t_height, z);
        timer += Time.deltaTime;
        this.transform.LookAt(rotatePoint);
    }
}
