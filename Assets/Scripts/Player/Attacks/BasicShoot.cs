﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShoot : MonoBehaviour
{
    public GameObject bullet;
  public  float timer;
    public float timeStop;
    public float speed;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        timer = 200;
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;
        if (Input.GetAxis("click2") > 0 && timer * Time.deltaTime > timeStop * Time.deltaTime)
        {
            bullet.transform.position = transform.position;
            bullet.GetComponent<BulletMove>().foward = transform.forward;
            bullet.GetComponent<BulletMove>().foward.y = 0;
            bullet.GetComponent<BulletMove>().speed = speed;
            bullet.GetComponent<BulletMove>().pushForce = force;
            bullet.GetComponent<C_LookAt>().target = Camera.main.gameObject;
            Instantiate(bullet);
            timer = 0;
           

        }
    }
}