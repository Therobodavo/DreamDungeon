using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon3 : WeaponBase
{
    RaycastHit hit;
    Ray attackLine;
    GameObject player;
    GameObject cam;

    public Weapon3(GameObject weapon)
    {
        player = GameObject.Find("Player");
        cam = GameObject.Find("Player Camera");
    }

    public override void UseItem()
    {
        attackLine = new Ray(player.transform.position, cam.transform.forward);
        if (Physics.Raycast(attackLine,out hit,10f))
        {
            if (hit.transform.tag == "Enenmy")
            {
                //hit.transform.gameObject.GetComponent<BasicEnemy>
                Debug.Log("WE HIT THOSE");
            }
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void OnSelect()
    {
        
    }

    public override void OffSelect()
    {
        
    }
}
