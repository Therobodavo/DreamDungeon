using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInLight : MonoBehaviour
{
    public float speed;
    private float intensity;

    void Start()
    {
        intensity = this.GetComponent<Light>().intensity;
        this.GetComponent<Light>().intensity = 0;
    }

    void Update()
    {
        this.GetComponent<Light>().intensity = Mathf.Lerp(this.GetComponent<Light>().intensity, intensity, speed * Time.deltaTime);
    }

    
}
