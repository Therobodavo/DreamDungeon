using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : BasicEnnemy
{
    //ennemy script for the "stamper" ennemy
    //2 attacks. 
    //attack 1- He runs at the player


    float atCheck;
    float circleTimer = 0; // circle timer

    float wTimer = 500; //timer for wondering
    float wX = 0;
    float wZ = 0;

    float circDir; // current direction of circle
    float speed = 4;
    float wSpeed = 2;
    public float bulletSpeed = 0.1f;
    public GameObject bullet;


    public void Start()
    {
        init();
        speed = Random.Range(2f, 4.5f);
        circDir = Random.Range(1, 2);
        atCheck = Random.Range(240f, 860f);

        if (circDir != 1)
            circDir = -1;
        circleTimer += Random.Range(0, 360) * Time.deltaTime;
    }

    //enemy decides what attack he will do 
    public override void chooseAttack()
    {
        atTimer += 1 * Time.deltaTime;
        if (!invicable)
        {
            if (atTimer > atCheck * Time.deltaTime && !hide)
            {
                shootAttack();
                attackOver = false;
            }
            else
            {
               
                attackOver = true;
            }

        }

        hideFromPlayer();
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
    }

    public void hideFromPlayer()
    {
        Debug.Log((player.transform.position - transform.position).magnitude);
        if((player.transform.position - transform.position).magnitude < 5.5f)
        {
            hide = true;
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 1f, 0.5f, 0.9f); // displaying invcibility
        }
        else
        {
            hide = false;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f); // displaying invcibility
        }
       

    }
   

 

}
