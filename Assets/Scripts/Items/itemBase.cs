using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    //Item Variables
    public string itemName = "";
    public enum Type
    {
        Weapon,
        Utility,
        Consumable,
    }
    public Type itemType;

    public abstract void UseItem();

    // Update is called once per frame
    public abstract void Update();
    
}
