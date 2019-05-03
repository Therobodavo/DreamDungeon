using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlash : MonoBehaviour
{
    bool attacking;

    Hotbar hotbar;
    public void Start()
    {
        hotbar = GameObject.Find("Player").GetComponent<Hotbar>();
        if (hotbar.Items.Count > 0)
        {
            attacking = ((Weapon1)hotbar.Items[0]).attacking;
        }
    }
    public void Update()
    {
        if (hotbar.Items.Count > 0)
        {
            attacking = ((Weapon1)hotbar.Items[0]).attacking;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(attacking)
        {
            if(other.tag == "Enenmy")
            {
               
                Vector3 force = transform.forward.normalized;
           
                other.GetComponent<BasicEnnemy>().knockBack(force,250, 2, false);

         

            }
           

        }

    }
}
