using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopperScript : BasicEnnemy
{//ennemy script for the "stamper" ennemy
 //2 attacks. 
 //attack 1- He runs at the player


    
    float circleTimer = 0; // circle timer


    float wTimer = 500; //timer for wondering
    float wX = 0;
    float wZ = 0;

    float circDir; // current direction of circle
    float speed;
    float wSpeed = 150;



  float bulletSpeed = 0.01f;
    public GameObject bullet;

    public float bulletPush;
    public float speedJump;
    float currentJump;
    public float speedDash = 2f;
    public float height = 0.1f;

    public void Start()
    {
        init();
        speed = Random.Range(speedMin,speedMax);
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


        if (atkState == atkStateType.atk1)
            bulletAttack();
        else if (atkState != atkStateType.atkOver)       
             diveAttack(); 
        else
            circlePlayer();

        jumpControl();



    }

    public void bulletAttack()
    {
   
        bullet.transform.position = transform.position;
        bullet.GetComponent<BulletMove>().isPlayer = false;
        bullet.GetComponent<BulletMove>().foward = (player.transform.position - transform.position).normalized;
        bullet.GetComponent<BulletMove>().speed = bulletSpeed;
        bullet.GetComponent<BulletMove>().damage = damage;
        bullet.GetComponent<BulletMove>().push = bulletPush;
        bullet.GetComponent<BulletMove>().timeAlive = 7.5f;

    bullet.GetComponent<C_LookAt>().target = Camera.main.gameObject;
        
        Instantiate(bullet);
        bullet.SetActive(true);

        atTimer = 0;
        circDir = Random.Range(1, 2);
        if (circDir != 1)
            circDir = -1;
        speed = Random.Range(speedMin, speedMax);
        atCheck = Random.Range(540f, 460f);
        circleTimer += Random.Range(0, 360) * Time.deltaTime;
        atkChoice = Random.Range(0, 3);
 

    }

    public void jumpControl()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.down);
        Vector3 force = new Vector3(0, speedJump, 0);
        if (Physics.Raycast(transform.position, fwd, height))
            finalForce += force;

    }

    //enemy will jump into player
    public void diveAttack()
    {
        
        Vector3 force = (player.transform.position - transform.position).normalized * speed * speedDash;

        force.y = 0;
        // transform.position += force;
        finalForce += force;

        if (atTimer > (atCheck + 1000) * Time.deltaTime)
        {
            circDir = Random.Range(1, 2);
            if (circDir != 1)
                circDir = -1;
            speed = Random.Range(speedMin, speedMax);
            atCheck = Random.Range(240f, 460f);
            circleTimer += Random.Range(0, 360) * Time.deltaTime;
            atTimer = 0;
           atkChoice = Random.Range(0, 3);

        }
 
    }

    //enemy will ciricle around player
    public void circlePlayer()
    {
        circleTimer += Time.deltaTime * 0.5f;

        float x = Mathf.Cos(circleTimer) * circDir;
        float z = Mathf.Sin(circleTimer) * circDir;

        Vector3 targetPos = player.transform.position + (new Vector3(x, 0, z) * Random.Range(circleDistance - 0.5f, circleDistance));

        targetPos = (targetPos - transform.position).normalized * speed;

        targetPos.y = 0;

        // transform.position += targetPos;

        finalForce += targetPos;
    }

    public override void wonder()
    {

        Vector3 targetPos = new Vector3(0, 0, 0);

        if ((startPos.normalized - transform.position.normalized).magnitude * 1000 < 3.5f)
        {
            wTimer += Time.deltaTime;
            if (wTimer > 250 * Time.deltaTime)
            {
                wX = Mathf.Cos(Random.Range(0, 360));
                wZ = Mathf.Sin(Random.Range(0, 360));
                wTimer = 0;
            }

            targetPos = transform.position + (new Vector3(wX, 0, wZ) * 1.5f);

        }
        else
        {
            targetPos = startPos;

        }

        targetPos = (targetPos - transform.position).normalized * wSpeed ;

        targetPos.y = 0.0f;

        //   transform.position += targetPos;
        finalForce += targetPos;
        jumpControl();
    }

}


