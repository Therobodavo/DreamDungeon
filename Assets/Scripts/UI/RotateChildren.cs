using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateChildren : MonoBehaviour
{
    public string search;      //The string the object name must include
    public float offsetAmt;    //The amount of offset per letter
    public float movementFreq; //The movement frequency
    public float movementAmp;  //The movmeent amplitude
    public float rotationFreq; //The rotation frequency
    public float rotationAmp;  //The rotation amplitude

    //Applies the script to each letter object
    void Start()
    {
        float i = 0;
        foreach(Transform child in transform)
        {
            if(child.name.Contains(search))
            {
                i += offsetAmt;
                child.gameObject.AddComponent<RotateAnimation>();
                RotateAnimation scr = child.gameObject.GetComponent<RotateAnimation>();
                scr.offset       = i;
                scr.movementFreq = movementFreq;
                scr.movementAmp  = movementAmp;
                scr.rotationFreq = rotationFreq;
                scr.rotationAmp  = rotationAmp;
            }
        }
    }
}
