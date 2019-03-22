using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : ItemBase
{
    public GameObject Slash;
    public BasicSlash Slashscript;
    float timer = 60;
    //Timer for slashscript
    float slashTimer;
    public Weapon1(string itemName,GameObject weapon) : base(itemName, Type.Weapon)
    {
        Slash = weapon;
        Slashscript = Slash.GetComponent<BasicSlash>();
    }

    public override void UseItem()
    {
        Slash.SetActive(true);
            Slashscript.InactiveTimer = slashTimer;
            slashTimer = 0;
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
        slashTimer += Time.deltaTime;

        if (timer <= 0)
        {
            Slash.SetActive(false);
            
        }

    }
}
