using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : BasicEnnemy
{


    float circleTimer = 0; // circle timer

    float wTimer = 500; //timer for wondering
    float wX = 0;
    float wZ = 0;

    float circDir; // current direction of circle
    float speed;
    float wSpeed = 3;
    public GameObject bullet;
    public GameObject fire;
    public GameObject fire2;
    public GameObject bat;

    public float bulletSpeed = 0.06f;
    public float speedDash = 2f;
    public float speedFly = 3;

    public float height1 = 1f;
    public float height2 = 0.4f;
    public float distance;
    float clampSpeed = 4;

    public void Start()
    {
        init();
        speed = Random.Range(speedMin, speedMax);
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
            finalForce += force;

    }



    //enemy decides what attack he will do 
    public override void chooseAttack()
    {
        atTimer += 1 * Time.deltaTime;
        if (atkState == atkStateType.atk1)
            dashAttack();
        else if (atkState == atkStateType.atk2)     
            shootAttack();
              
        else if (atkState == atkStateType.atk3)
            spawnBat();
               
        else if (atkState == atkStateType.atk4)
            dashAttack();         
        else if (atkState == atkStateType.atk5)
        {
            shootAttack();
            shootAttack();
        }           
        else if (atkState == atkStateType.atk6)
        {
            spawnFire1();
            spawnFire2();
            spawnFire1();
        }      
              
        else if (atkState == atkStateType.atk7)
        {

            spawnFire2();
            shootAttack();
        }
            
        else if (atkState == atkStateType.atk8)
        {
            spawnFire1();
            spawnFire2();
            spawnFire1();
        }
        else
        {
            circlePlayer();
        }
        flying();

    }

    public void spawnFire1()
    {
        fire.transform.position = transform.position;
        Instantiate(fire);
        fire.SetActive(true);
        atTimer = 0;
        atCheck = Random.Range(100f, 200f);
        atkChoice = Random.Range(0, 9);
    }

    public void spawnFire2()
    {
        fire2.transform.position = transform.position;
        Instantiate(fire2);
        fire2.SetActive(true);
        atTimer = 0;
        atCheck = Random.Range(100f, 200f);
        atkChoice = Random.Range(0, 9);
    }



    public void spawnBat()
    {
       bat.transform.position = transform.position;
        Instantiate(bat);
        bat.SetActive(true);
        atTimer = 0;
        atCheck = Random.Range(100f, 350f);
        atkChoice = Random.Range(0, 9);
    }
    //enemy will charge at player
    public void dashAttack()
    {
        Vector3 force = (player.transform.position - transform.position).normalized * speed * Time.deltaTime * speedDash;


        force.y = 0;
        finalForce += force;

        if (atTimer > (atCheck + 300) * Time.deltaTime)
        {
            speed = Random.Range(2f, 4.5f);
            circDir = Random.Range(1, 2);
            if (circDir != 1)
                circDir = -1;
            speed = Random.Range(speedMin, speedMax);
            atCheck = Random.Range(50f, 100f);
            circleTimer += Random.Range(0, 360) * Time.deltaTime;
            atTimer = 0;
            atkChoice = Random.Range(0, 9);

        }

    }

    public void shootAttack()
    {

      
        bullet.GetComponent<BulletMove>().isPlayer = false;
        bullet.GetComponent<BulletMove>().foward = (player.transform.position - transform.position).normalized;
        bullet.GetComponent<BulletMove>().speed = bulletSpeed;
        bullet.GetComponent<BulletMove>().push = push;
        bullet.GetComponent<BulletMove>().damage = damage;
        bullet.transform.position = transform.position;
        Instantiate(bullet);
        bullet.transform.position = transform.position + new Vector3(0.5f,0.5f,0.5f);
        Instantiate(bullet);
        bullet.transform.position = transform.position - new Vector3(0.5f, 0.5f, 0.5f);
        Instantiate(bullet);
        bullet.SetActive(true);
        atTimer = 0;
        atCheck = Random.Range(50f, 160f);
        atkChoice = Random.Range(0, 9);

    }

    //enemy will ciricle around player
    public void circlePlayer()
    {

        circleTimer += Time.deltaTime * 1.5f;

        float x = Mathf.Cos(circleTimer) * circDir;
        float z = Mathf.Sin(circleTimer) * circDir;

        Vector3 targetPos = player.transform.position + (new Vector3(x, 0, z) * circleDistance);

        targetPos = (targetPos - transform.position).normalized * speed * Time.deltaTime;



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

        targetPos = (targetPos - transform.position).normalized * wSpeed * Time.deltaTime;

        targetPos.y = 0;
        finalForce += targetPos;
        // 
        flying();
    }
}
