using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : ItemBase
{
    public override void Update()
    {
        throw new System.NotImplementedException();
    }

    public override void UseItem()
    {
        throw new System.NotImplementedException();
    }
    public virtual void OnSelect()
    {

    }
    public virtual void OffSelect()
    {
        
    }
    public void Use()
    {
        //p e t t y
        UseItem();
    }

}
