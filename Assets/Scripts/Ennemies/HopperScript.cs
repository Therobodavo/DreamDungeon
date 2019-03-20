using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopperScript : BasicEnnemy
{//ennemy script for the "stamper" ennemy
 //2 attacks. 
 //attack 1- He runs at the player

    public float atTimer = 0;//attack timer
    float atCheck;
    float circleTimer = 0; // circle timer

    float wTimer = 500; //timer for wondering
    float wX = 0;
    float wZ = 0;

    float circDir; // current direction of circle
    float speed = 4;
    float wSpeed = 2;



    public void Start()
    {
        init();
        health = 7;
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
            if (atTimer > atCheck * Time.deltaTime)
            {
                dashAttack();
                attackOver = false;
            }
            else
            {
                circlePlayer();
                attackOver = true;
            }

        }


    }

    //enemy will charge at player
    public void dashAttack()
    {
        transform.position += (player.transform.position - transform.position).normalized * speed * Time.deltaTime * 3.5f;

        if (atTimer > (atCheck + 400) * Time.deltaTime)
        {
            speed = Random.Range(2f, 4.5f);
            circDir = Random.Range(1, 2);
            if (circDir != 1)
                circDir = -1;
            speed = Random.Range(2f, 3.5f);
            atCheck = Random.Range(240f, 860f);
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

        Vector3 targetPos = player.transform.position + (new Vector3(x, 0, z) * 5.5f);

        targetPos = (targetPos - transform.position).normalized * speed * Time.deltaTime;

        targetPos.y = 0;

        transform.position += targetPos;
    }

    public override void wonder()
    {
        Vector3 targetPos = new Vector3(0, 0, 0);
        if (!returnHome)
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
                returnHome = true;
            }
        }

        if (returnHome)
        {
            targetPos = startPos;
            if ((startPos.normalized - transform.position.normalized).magnitude * 1000 < 3.5f)
            {
                returnHome = false;
            }
        }

        targetPos = (targetPos - transform.position).normalized * wSpeed * Time.deltaTime;

        targetPos.y = 0;

        transform.position += targetPos;

    }

}
