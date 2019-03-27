using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float pushForce;
    public Vector3 foward;
    public float speed;
    public bool isPlayer; //checks if bullet is made by player

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
            if (other.tag == "Player")
            {
                Vector3 force = (other.gameObject.transform.position - transform.position).normalized;
                other.gameObject.GetComponent<PlayerMovement>().knockBack(force, 1000, 1);
            }
        }
            

        }

    
}
