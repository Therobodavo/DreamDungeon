using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StamperScript : BasicEnnemy
{
    //ennemy script for the "stamper" ennemy
    //2 attacks. 
    //attack 1- He runs at the player

  public  float atTimer = 0;//attack timer
    float atCheck;
    float circleTimer = 0;

    float circDir;
    float speed = 3;

    public void Start()
    {
        init();
        speed = Random.Range(2f, 3.5f);
        circDir = Random.Range(1, 2);
         atCheck = Random.Range(340f, 2460f);
 
        if (circDir != 1)
            circDir = -1;
        circleTimer += Random.Range(0, 360) * Time.deltaTime;
    }


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

    public void dashAttack()
    {
        transform.position += (player.transform.position - transform.position).normalized * speed * Time.deltaTime * 2.5f;

        if(atTimer > (atCheck + 200) * Time.deltaTime)
        {
            speed = Random.Range(2f, 3.5f);
            circDir = Random.Range(1, 2);
            if (circDir != 1)
                circDir = -1;
            speed = Random.Range(2f, 3.5f);
            atCheck = Random.Range(340f, 1460f);
            circleTimer += Random.Range(0, 360) * Time.deltaTime;
            atTimer = 0;

        }
    }

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

}
