﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlash : MonoBehaviour
{
    bool attacking;
    float pushForce;

    Hotbar hotbar;
    public void Start()
    {
        hotbar = GameObject.Find("Player").GetComponent<Hotbar>();
        if (hotbar.Items.Count > 0)
        {
            attacking = ((Weapon1)hotbar.Items[0]).attacking;
            pushForce = ((Weapon1)hotbar.Items[0]).pushForce;
        }
    }
    public void Update()
    {
        if (hotbar.Items.Count > 0)
        {
            attacking = ((Weapon1)hotbar.Items[0]).attacking;
            pushForce = ((Weapon1)hotbar.Items[0]).pushForce;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(attacking)
        {
            if(other.tag == "Enenmy")
            {
               
                Vector3 force = transform.forward.normalized;
           
                other.GetComponent<BasicEnnemy>().knockBack(force,350, 2);

         

            }
           

        }

    }
}
