﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : BasicEnnemy
{
    //ennemy script for the "stamper" ennemy
    //2 attacks. 
    //attack 1- He runs at the player



    float circleTimer = 0; // circle timer

    float wTimer = 500; //timer for wondering
    float wX = 0;
    float wZ = 0;

    float circDir; // current direction of circle
    float speed = 4;
    float wSpeed = 2;



    public float speedDash = 2f;
    public float speedFly = 3;
  
    public float height1 = 1f;
    public float height2 = 0.4f;
    public float distance;
    float clampSpeed = 4;

    public void Start()
    {
        init();
        health = 7;
        speed = Random.Range(speedMin,speedMax);
        circDir = Random.Range(1, 2);
        atCheck = Random.Range(440f, 1200f);

        if (circDir != 1)
            circDir = -1;
        circleTimer += Random.Range(0, 360) * Time.deltaTime;
    }



    public void flying()
    {
        float height = height1;
        if (atkState != atkStateType.atkOver && (player.transform.position - transform.position).magnitude < distance)
              height = height2;



        Vector3 fwd = transform.TransformDirection(Vector3.down);
        Vector3 force = new Vector3(0, speedFly, 0);
        if (Physics.Raycast(transform.position, fwd, height))
            GetComponent<Rigidbody>().AddForce(force);

    }



    //enemy decides what attack he will do 
    public override void chooseAttack()
    {
        atTimer += 1 * Time.deltaTime;
        if (atkState != atkStateType.atkOver)
        {
            dashAttack();
        }
        else
        {
            circlePlayer();
        }
        flying();

    }

    //enemy will charge at player
    public void dashAttack()
    {
     Vector3 force = (player.transform.position - transform.position).normalized * speed * Time.deltaTime * speedDash;

        
        force.y = 0;
        GetComponent<Rigidbody>().AddForce(force);

        if (atTimer > (atCheck + 900) * Time.deltaTime)
        {
            speed = Random.Range(2f, 4.5f);
            circDir = Random.Range(1, 2);
            if (circDir != 1)
                circDir = -1;
            speed = Random.Range(speedMin, speedMax);
            atCheck = Random.Range(440f, 1200f);
            circleTimer += Random.Range(0, 360) * Time.deltaTime;
            atTimer = 0;

        }
   
    }

    //enemy will ciricle around player
    public void circlePlayer()
    {
   
        circleTimer += Time.deltaTime * 0.5f;

        float x = Mathf.Cos(circleTimer) * circDir;
        float z = Mathf.Sin(circleTimer) * circDir;

        Vector3 targetPos = player.transform.position + (new Vector3(x, 0, z) * circleDistance);

        targetPos = (targetPos - transform.position).normalized * speed * Time.deltaTime;

        targetPos.y = 0f;

        GetComponent<Rigidbody>().AddForce(targetPos);
        Vector3 vel = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, clampSpeed);
        GetComponent<Rigidbody>().velocity = vel;

    }

    public override void wonder()
    {
        Vector3 targetPos = new Vector3(0, 0, 0);
        if (state == stateType.wounder)
        {
            wTimer += Time.deltaTime;
            if (wTimer > 1000 * Time.deltaTime)
            {
                wX = Mathf.Cos(Random.Range(0, 360));
                wZ = Mathf.Sin(Random.Range(0, 360));
                wTimer = 0;
            }


            targetPos = transform.position + (new Vector3(wX, 0, wZ) * 1.5f);


            if ((startPos.normalized - transform.position.normalized).magnitude * 1000 > 6)
            {
                state = stateType.returnHome;
            }
        }

        if (state == stateType.returnHome)
        {
            targetPos = startPos;
            if ((startPos.normalized - transform.position.normalized).magnitude * 1000 < 3.5f)
            {
                state = stateType.wounder;
            }
        }

        targetPos = (targetPos - transform.position).normalized * wSpeed * Time.deltaTime;

        targetPos.y = 0;

        GetComponent<Rigidbody>().AddForce(targetPos);
        // 
        flying();
    }

}
