using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

    public float smooth = 5.0f;
    public float tiltAngle = 60.0f;
    public float incX;
    public float incY;
    public float incZ;
    float X;
    float Y;
    float Z;


    // Use this for initialization
    void Start () {
        X = transform.eulerAngles.x;
        Y = transform.eulerAngles.y;
        Z = transform.eulerAngles.z;
    }
	
	// Update is called once per frame
	void Update () {

 
        // Smoothly tilts a transform towards a target rotation.
        float tiltAroundX =  X * tiltAngle;
        float tiltAroundY = Y * tiltAngle;
        float tiltAroundZ =  Z * tiltAngle;


        //create a target for rotation, that increments
        Quaternion target = Quaternion.Euler(tiltAroundX, tiltAroundY, tiltAroundZ);

        //rotate transform
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        //increment rotations by custom values
        X += incX* Time.deltaTime;
        Y += incY * Time.deltaTime;
        Z += incZ * Time.deltaTime;
        if(X > 180 )
        {
            X = 0;
        }
        if (Y > 180)
        {
            Y = 0;
        }
        if (Z > 180)
        {
            Z = 0;
        }
    }
}
