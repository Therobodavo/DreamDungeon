using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2 : ItemBase
{
    GameObject Shoot;
    float timer = 60;
    public Weapon2(string itemName, GameObject weapon) : base(itemName, Type.Weapon)
    {
        Shoot = weapon;
    }

    public override void UseItem()
    {
        Shoot.SetActive(true);
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
        if (timer <= 0)
        {
            Shoot.SetActive(false);
        }
    }
}
