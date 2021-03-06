﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon3 : WeaponBase
{
    RaycastHit hit;
    Ray attackLine;
    GameObject player;
    GameObject cam;
    GameObject beam;
    bool active = false;
    float lastUsed = 0;

    public Weapon3(GameObject weapon)
    {
        player = GameObject.Find("Player");
        cam = GameObject.Find("Player Camera");
        beam = weapon;
    }

    public override void UseItem()
    {
        if (!active)
        {
            attackLine = new Ray(player.transform.position, cam.transform.forward);
            if (Physics.Raycast(attackLine, out hit, 30f))
            {
                if (hit.transform.tag == "Enenmy")
                {
                    Vector3 force = cam.transform.forward * -1;
                    active = true;
                    lastUsed = Time.timeSinceLevelLoad;
                    hit.transform.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    hit.transform.gameObject.GetComponent<BasicEnnemy>().knockBack(force, (hit.transform.position - player.transform.position).magnitude * 100, 0.1f, true);
                    hit.transform.gameObject.GetComponent<BasicEnnemy>().stunned(200);
                    beam.GetComponent<LineRenderer>().SetPosition(0, player.transform.position);
                    beam.GetComponent<LineRenderer>().SetPosition(1, hit.transform.position);
                    beam.SetActive(true);
                    player.GetComponent<Hotbar>().switchable = false;
                }
            }
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        if (active && Time.timeSinceLevelLoad - lastUsed > 1)
        {
            active = false;
            beam.SetActive(false);
            player.GetComponent<Hotbar>().switchable = true;
        }

        if (active && hit.transform.gameObject != null)
        {
            beam.GetComponent<LineRenderer>().SetPosition(0, player.transform.position);
            beam.GetComponent<LineRenderer>().SetPosition(1, hit.transform.position);
            player.GetComponent<Hotbar>().switchable = false;
        }
        else
        {
            beam.SetActive(false);
            player.GetComponent<Hotbar>().switchable = true;
        }
    }

    public override void OnSelect()
    {
        
    }

    public override void OffSelect()
    {
        
    }
}
