using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public GameObject Slash;
    public int Selected;
    List<ItemBase> Items = new List<ItemBase>();
    List<Action> ConsumableActions = new List<Action>();
    
    // Start is called before the first frame update
    void Start()
    {
        ConsumableActions.Add(() => Heal());

        Items.Add(new ItemConsumable("Health Potion", 0));
        Items.Add(new Weapon1("Blue Flame",Slash));

        Selected = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("click") > 0)
        {
            Items[Selected].UseItem();
        }
        Items[Selected].Update();
    }

    void UseSelected()
    {

    }

    void Heal()
    {
        Debug.Log("Used");
    }
}
