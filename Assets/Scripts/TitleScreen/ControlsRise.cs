using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsRise : MonoBehaviour
{
    public float appearSpeed;
    public float alphaFull;

    private Image imageRef;
    private float alpha;

    void Start()
    {
        imageRef = this.GetComponent<Image>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            alpha = Mathf.Lerp(alpha, alphaFull, appearSpeed);
        }
        else
        {
            alpha = Mathf.Lerp(alpha, 0.0f, appearSpeed);
        }
        imageRef.color = new Color(255, 255, 255, alpha);
    }
}
