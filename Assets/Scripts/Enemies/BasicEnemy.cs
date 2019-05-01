using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float noticeDistance = 5.0f;
    public float keepDistance = 1.0f;
    public float speed = .5f;
    public float maxSpeed = 5.0f;

    void Update()
    {
        GameObject player = GameObject.Find("Player");

        if(player != null)
        {
            Vector3 playerPosition = player.transform.position;
            float distanceToPlayer = Vector3.Distance(this.transform.position, playerPosition);
            Vector3 direction = playerPosition - this.transform.position;

            if (distanceToPlayer < noticeDistance && distanceToPlayer > keepDistance)
            {
                if(this.GetComponent<Rigidbody>().velocity.magnitude < maxSpeed)
                    this.GetComponent<Rigidbody>().AddForce(direction * speed);
            }
            else
            {
                this.GetComponent<Rigidbody>().velocity *= .90f;
            }

        }
    }
}
