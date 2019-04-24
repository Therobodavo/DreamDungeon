﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float pushForce;
    public Vector3 foward;
    public float speed;
    public bool isPlayer; //checks if bullet is made by player

    public int damage;
    public float push;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (foward.normalized * speed);
        timer += 1 * Time.deltaTime;
        if(timer > 300 * Time.deltaTime)
            Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (isPlayer)
        {
            if (other.tag == "Enenmy")
            {

                Vector3 force = transform.forward.normalized;

                other.GetComponent<BasicEnnemy>().knockBack(-force, 4, 1);



            }
            else if (other.tag == "Pushable")
            {
                Rigidbody rigidbody = other.GetComponent<Rigidbody>();
                Vector3 force = transform.forward.normalized;
                force.y = 0.5f;
                rigidbody.velocity += force * pushForce;

            }
        }
        else
        {
            ///need to make it so enemies give the info needed for this
            if (other.tag == "Player")
            {
                Vector3 force = (other.gameObject.transform.position - transform.position).normalized;
               other.GetComponent<PlayerMovement>().Push(force, push * 100, damage);
            }
        }
            

        }

    
}
