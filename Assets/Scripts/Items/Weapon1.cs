using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : WeaponBase
{
    public GameObject Slash;
    float timer = 60;
    //Timer for slashscript
    float slashTimer;
    public Animator animator;
    public  int index;
    //float timer;
    public float timeStop;
    public float timeStop2;
    public float pushForce;

    //Time since enabled
    public float inactiveTimer;

    public bool attacking = false;
    public Weapon1(GameObject weapon)
    {
        Slash = weapon;
        timer = 200;
        index = 0;
        inactiveTimer = 0;
        animator = Slash.GetComponent<Animator>();
    }

    public override void UseItem()
    {
            Slash.SetActive(true);
            inactiveTimer = slashTimer;
            slashTimer = 0;
            timer = 0.7f;
    }

    // Update is called once per frame
    public override void Update()
    {
        timer -= Time.deltaTime;
        slashTimer += Time.deltaTime;
        
        if (timer <= 0)
        {
            //Slash.SetActive(false);
            
        }

         timer += 1 * Time.deltaTime;

        //If InactiveTimer is over 0,  (Just set to active) update timer and reset combo if needed. ~Schrupp
        if (inactiveTimer > 0)
        {
            timer += inactiveTimer;

            //If InactiveTimer (Time since last active) is over timeStop2, reset animation chain. ~Schrupp
            if (inactiveTimer > timeStop2)
            {
                index = 0;
                timer = timeStop;
            }

            inactiveTimer = 0;
        }
        //
        
        if(timer  > timeStop2 )
        {
            index = 0;
        }
        
        


        ///Changed Input check to OR to eliminate it without deleting - Schrupp
        if (timer * Time.deltaTime >= timeStop * Time.deltaTime)
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

        if(index > 0 && timer * Time.deltaTime < timeStop * Time.deltaTime)
        {
            attacking = true;
        }
        else
        {
            attacking = false;
        }
        //Debug.Log(index);
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
