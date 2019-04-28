using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon3 : WeaponBase
{
    RaycastHit hit;
    Ray attackLine;
    GameObject player;

    public Weapon3(GameObject weapon)
    {
        
    }

    public override void UseItem()
    {
        attackLine = new Ray();
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void OnSelect()
    {
        
    }

    public override void OffSelect()
    {
        
    }
}
