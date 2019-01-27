using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_Smoke : MonoBehaviour
{
    #region Public Variables
    [Range(1.1f, 10.0f)]
    public float growthRate = 10f;      //The maximum amount the particle can grow
    [Range(1.1f, 5.0f)]
    public float accelerationRate = 3f; //How fast the particle can accelerate upwards
    #endregion

    #region Private Variables
    private SpriteRenderer spriteRenderer; //The sprite renderer reference
    #endregion

    /// <summary>
    /// Sets the particle's random ranges and sets its scale accordingly.
    /// </summary>
    void Start ()
    {
        growthRate = Random.Range(1.1f, growthRate);
        accelerationRate = Random.Range(1.1f, accelerationRate);
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        float randomScale = Random.Range(0.1f, 5.0f);
        this.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }
	
    /// <summary>
    /// Increases the scale, rotation, and transparency of the particle over time.
    /// </summary>
	void Update ()
    {
        //Increasing scale
        this.transform.localScale = 
            new Vector3(
                this.transform.localScale.x + (growthRate * Time.deltaTime), 
                this.transform.localScale.y + (growthRate * Time.deltaTime), 
                this.transform.localScale.z + (growthRate * Time.deltaTime)
            );

        //Increasing upwards movement
        Vector3 movement = Random.insideUnitSphere * accelerationRate;

        if (movement.y <= 0)
        {
            movement = new Vector3(movement.x, -movement.y, movement.z);
        }  

        this.transform.position += (movement * Time.deltaTime * .1f);

        //Rotating (Not working? Might be locked in billboarding script)
        this.transform.Rotate(new Vector3(0, 0, 20.0f * Time.deltaTime));

        //Decreasing opacity
        spriteRenderer.color = 
            new Color(
                spriteRenderer.color.r, 
                spriteRenderer.color.g, 
                spriteRenderer.color.b, 
                spriteRenderer.color.a - Time.deltaTime
            );

        //If the particle is invisible, destroy itself
        if(spriteRenderer.color.a <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
