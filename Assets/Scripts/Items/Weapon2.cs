using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2 : WeaponBase
{
    GameObject Shoot;
    bool active = false;
    float timer = 60;
    public Weapon2(GameObject weapon)
    {
        Shoot = weapon;
    }

    public override void UseItem()
    {
        Shoot.SetActive(true);
        active = true;
        timer = 0.7f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && active)
        {
            Shoot.SetActive(false);
        }
    }
    public override void OnSelect()
    {
        active = true;
       timer = 0.7f;
    }

    public override void OffSelect()
    {
        active = false;
        Shoot.SetActive(false);
    }
}
