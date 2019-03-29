﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : BasicEnnemy
{
    //ennemy script for the "stamper" ennemy
    //2 attacks. 
    //attack 1- He runs at the player



    float speed = 4;
    public float bulletSpeed = 0.1f;
    public GameObject bullet;


    public void Start()
    {
        init();
        speed = Random.Range(2f, 4.5f);

        atCheck = Random.Range(240f, 860f);


    }

    //enemy decides what attack he will do 
    public override void chooseAttack()
    {
        hideFromPlayer();
        atTimer += 1 * Time.deltaTime;
        if (atkState != atkStateType.atkOver && inState == inStateType.none)
        {
            shootAttack();
        }
       

    }

    //enemy will charge at player
    public void shootAttack()
    {
        bullet.transform.position = transform.position;
        bullet.GetComponent<BulletMove>().isPlayer = false;
        bullet.GetComponent<BulletMove>().foward = (player.transform.position - transform.position).normalized;
        bullet.GetComponent<BulletMove>().speed = bulletSpeed;
        bullet.GetComponent<C_LookAt>().target = Camera.main.gameObject;
        Instantiate(bullet);
        bullet.SetActive(true);
        atTimer = 0;
        atCheck = Random.Range(40f, 460f);
        atkState = atkStateType.atkOver;
    }

    public void hideFromPlayer()
    {
        if ((player.transform.position - transform.position).magnitude < 5.5f)
        {
            inState = inStateType.hide;
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 1f, 0.5f, 0.9f); // displaying invcibility
        }
        else
        {
            inState = inStateType.none;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f); // displaying invcibility
        }
           
    }

}
 
   

 


