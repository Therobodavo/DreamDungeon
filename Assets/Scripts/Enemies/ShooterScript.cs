using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : BasicEnnemy
{
    //ennemy script for the "stamper" ennemy
    //2 attacks. 
    //attack 1- He runs at the player



    float speed;
 
    public GameObject bullet;
    public float bulletTime = 300;


    public void Start()
    {
        init();


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
        speed = Random.Range(speedMin, speedMax);
        bullet.transform.position = transform.position;
        bullet.GetComponent<BulletMove>().isPlayer = false;
        bullet.GetComponent<BulletMove>().foward = (player.transform.position - transform.position).normalized;
        bullet.GetComponent<BulletMove>().speed = speed;
        bullet.GetComponent<BulletMove>().time = bulletTime;
        bullet.GetComponent<BulletMove>().push = push;
        bullet.GetComponent<BulletMove>().damage = damage;
        bullet.GetComponent<C_LookAt>().target = Camera.main.gameObject;
        Instantiate(bullet);
        bullet.SetActive(true);
        atTimer = 0;
        atCheck = Random.Range(50f, 160f);
      
    }

    public void hideFromPlayer()
    {
        if ((player.transform.position - transform.position).magnitude < circleDistance)
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
 
   

 


