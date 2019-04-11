using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0.01f, 20.0f)]
    public float speed = 10.0f;
    [Range(0.01f, 20.0f)]
    public float jumpForce = 10.0f;

    /// <summary>
    /// Updates the player position based on input.
    /// </summary>
    void Update()
    {
        Move();
        Jump();
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

        this.transform.position += movement * .001f;
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
                    this.GetComponent<Rigidbody>().AddForce(this.transform.up * jumpForce * 50.0f);
                }
            }
        }
    }
}
