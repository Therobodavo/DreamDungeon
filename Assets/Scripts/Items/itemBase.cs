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

    public ItemBase(string itemName, Type type)
    {
        this.itemName = itemName;
        this.itemType = type;
    }

    public abstract void UseItem();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public abstract void Update();
    
}
