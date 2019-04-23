using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlash : MonoBehaviour
{
    bool attacking;
    float pushForce;
    public void Start()
    {
        attacking = GameObject.Find("Player").GetComponent<Weapon1>().attacking;
        pushForce = GameObject.Find("Player").GetComponent<Weapon1>().pushForce;
    }
    public void Update()
    {
        attacking = GameObject.Find("Player").GetComponent<Weapon1>().attacking;
        pushForce = GameObject.Find("Player").GetComponent<Weapon1>().pushForce;
    }
    private void OnTriggerStay(Collider other)
    {
        if(attacking)
        {
            if(other.tag == "Enenmy")
            {
               
                Vector3 force = transform.forward.normalized;
           
                other.GetComponent<BasicEnnemy>().knockBack(force, 8, 1);



            }
            else if (other.tag == "Pushable")
            {
                Rigidbody rigidbody = other.GetComponent<Rigidbody>();
                Vector3 force = transform.forward.normalized;
                force.y = 0.5f;
                rigidbody.velocity += force * pushForce;

            }

        }

    }
}
