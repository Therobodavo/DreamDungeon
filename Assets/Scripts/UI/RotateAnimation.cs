using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    public float offset;         //The offset of the animation
    public float movementFreq; //The movement frequency
    public float movementAmp;  //The movmeent amplitude
    public float rotationFreq; //The rotation frequency
    public float rotationAmp;  //The rotation amplitude

    private RectTransform t;   //The transform of the UI element
    private float timer;       //The timer keeping track of time since instantiation
     
    /// <summary>
    /// Creates the transform reference.
    /// </summary>
    void Start()
    {
        t = this.GetComponent<RectTransform>();
    }

    /// <summary>
    /// Updates the transform based on time.
    /// </summary>
    void Update()
    {
        timer += Time.deltaTime;
        if(t != null) //If the transform isn't null...
        {
            float positionChange = Mathf.Sin(timer * movementFreq + offset) * movementAmp; //Move accordingly
            float rotationChange = Mathf.Sin(timer * rotationFreq + offset) * rotationAmp;
            t.position += new Vector3(0, positionChange, 0);
            t.Rotate(new Vector3(0, 0, rotationChange));
        }
    }
}
