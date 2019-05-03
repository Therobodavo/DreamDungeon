using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Range(0.01f, 20.0f)]
    public float speed = 10.0f;
    [Range(0.01f, 30.0f)]
    public float jumpForce = 10.0f;
    public int health = 100;
    public float invcTimer = 10;
    Vector3 jump;
    bool test;

    GameObject fill;
    public enum invState //determines if player is invicible
    {
       isTrue,
       isFalse
    }

    public invState inState;

    private void Start()
    {
        fill = GameObject.Find("HealthFill");
        jump = new Vector3(0, 0, 0);
        test = false;
    }
    /// <summary>
    /// Updates the player position based on input.
    /// </summary>
    void Update()
    {
        Move();
        Jump();
        invincability();
        if (health < 0)
        {
            health = 0;
        }
        fill.GetComponent<Image>().fillAmount = ((float)health) / 100;
       // Debug.Log(health);
    }
    private void FixedUpdate()
    {
      
        this.GetComponent<Rigidbody>().AddForce(jump);
            jump = new Vector3(0, 0, 0);


    }

    /// <summary>
    /// Moves the player.
    /// </summary>
    void Move()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movement += -this.transform.right * speed;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movement += this.transform.right * speed;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movement += this.transform.forward * speed;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movement += -this.transform.forward * speed;
        }

        this.transform.position += movement * Time.deltaTime; 
    }

    /// <summary>
    /// Makes the player Jump.
    /// </summary>
    void Jump()
    {
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hit;
            if(Physics.Raycast(this.transform.position, -this.transform.up, out hit))
            {
                if (Vector3.Distance(this.transform.position, hit.point) < .5f)
                {

                    jump = this.transform.up * jumpForce * 50.0f;
                }
                
                
            }
        }



    }

    public void Push(Vector3 Force, float weight, int damage)
    {
        if(inState == invState.isFalse)
        {
            Force *= weight;
            this.GetComponent<Rigidbody>().AddForce(Force);
            invcTimer = 0;
            inState = invState.isTrue;
            health -= damage;
            
        }

    }

    void invincability()
    {
        if(inState == invState.isTrue)
        {
            invcTimer += 1 * Time.deltaTime;
            GetComponent<SpriteRenderer>().color = new Color(1f, 0.8f, 0.8f, 0.5f); // displaying invcibility
        }
        else
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f); // displaying invcibility

        //might wanna add layers later to make it so ennemies go through player when invicble

        if (invcTimer > 100 * Time.deltaTime)
            inState = invState.isFalse;
    }
}
