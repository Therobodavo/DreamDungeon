using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_Smoke : MonoBehaviour
{
    #region Public Variables
    [Range(1.1f, 10.0f)]
    public float growthRate = 10f;
    [Range(1.1f, 5.0f)]
    public float accelerationRate = 3f;
    #endregion

    #region Private Variables
    private SpriteRenderer spriteRenderer;
    #endregion

    void Start ()
    {
        growthRate = Random.Range(1.1f, growthRate);
        accelerationRate = Random.Range(1.1f, accelerationRate);
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        float randomScale = Random.Range(0.1f, 5.0f);
        this.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }
	
	void Update ()
    {
        this.transform.localScale = 
            new Vector3(
                this.transform.localScale.x + (growthRate * Time.deltaTime), 
                this.transform.localScale.y + (growthRate * Time.deltaTime), 
                this.transform.localScale.z + (growthRate * Time.deltaTime)
            );

        Vector3 movement = Random.insideUnitSphere * accelerationRate;

        if (movement.y <= 0)
        {
            movement = new Vector3(movement.x, -movement.y, movement.z);
        }  

        this.transform.position += (movement * Time.deltaTime * .1f);

        this.transform.Rotate(new Vector3(0, 0, 20.0f * Time.deltaTime));

        spriteRenderer.color = 
            new Color(
                spriteRenderer.color.r, 
                spriteRenderer.color.g, 
                spriteRenderer.color.b, 
                spriteRenderer.color.a - Time.deltaTime
            );

        if(spriteRenderer.color.a <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
