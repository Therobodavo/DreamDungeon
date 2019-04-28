using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : WeaponBase
{
    public GameObject Slash;
    float timer = 60;
    public Animator animator;
    public  int index;
    public float pushForce;
    float timeInterval = 0.25f;
    float currentTime = 0f;
    float initialDelay = 0.1f;
    //Time since enabled
    public float inactiveTimer;
    public bool attacking = false;
    Hotbar bar;
    public Weapon1(GameObject weapon)
    {
        Slash = weapon;
        index = 0;
        currentTime = Time.timeSinceLevelLoad;
        animator = Slash.GetComponent<Animator>();
        bar = GameObject.Find("Player").GetComponent<Hotbar>();
    }

    public override void UseItem()
    {
            Slash.SetActive(true);
            if (Time.timeSinceLevelLoad - currentTime >= timeInterval + initialDelay)
            {
                currentTime = Time.timeSinceLevelLoad;
                index++;
                if (index > 3)
                {
                    index = 0;
                    initialDelay = 0.1f;
                }
                else
                {
                    initialDelay = 0;
                }
                animator.SetInteger("SlashNum", index);
                attacking = true;
                bar.switchable = false;
            }
    }

    // Update is called once per frame
    public override void Update()
    {
        
        if (Time.timeSinceLevelLoad - currentTime >= timeInterval + initialDelay + .1f && attacking)
        {
            attacking = false;
            bar.switchable = true;
            initialDelay = 0.1f;
            Slash.SetActive(false);
        }
        if (Time.timeSinceLevelLoad - currentTime >= timeInterval + initialDelay + 1f)
        {
            index = 0;
            attacking = false;
            bar.switchable = true;
        }
    }

    public override void OnSelect()
    {
       index = 0;
    }

    public override void OffSelect()
    {
        index = 0;
        attacking = false;
        Slash.SetActive(false);
    }
}
