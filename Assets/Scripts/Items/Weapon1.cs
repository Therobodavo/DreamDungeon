using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : ItemBase
{
    GameObject Slash;
    float timer = 60;
    public Weapon1(string itemName,GameObject weapon) : base(itemName, Type.Consumable)
    {
        Slash = weapon;
    }

    public override void UseItem()
    {
        Slash.SetActive(true);
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
            Slash.SetActive(false);
        }
    }
}
