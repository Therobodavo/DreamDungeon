using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlash : MonoBehaviour {
  public Animator animator;
public  int index;
  float timer;
    public float timeStop;
    public float timeStop2;
    public float pushForce;

    // Use this for initialization
    void Start () {
        timer = 200;
        index = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
        timer += 1 * Time.deltaTime;
        if(timer * Time.deltaTime > timeStop2 * Time.deltaTime)
        {
            index = 0;
        }

        ///Changed Input check to OR to eliminate it without deleting - Schrupp
      if(Input.GetAxis("click") > 0 || timer * Time.deltaTime > timeStop * Time.deltaTime)
        {
            index++;
            timer = 0;
            if (index > 3)
            {
                index = 0;
                timer = timeStop;
            }
             
        }

        animator.SetInteger("SlashNum", index);

    }

    private void OnTriggerStay(Collider other)
    {
        if(index > 0 && timer * Time.deltaTime < timeStop * Time.deltaTime)
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
