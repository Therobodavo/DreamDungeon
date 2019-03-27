using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public GameObject Slash;
    public GameObject Shoot;
    public int SelectedAttack = 0;
    public List<ItemBase> Items = new List<ItemBase>();
    public List<Action> ConsumableActions = new List<Action>();
    
    // Start is called before the first frame update
    void Start()
    {
        ConsumableActions.Add(() => Heal());

        //Items.Add(new Weapon1("Blue Flame",Slash));
        Items.Add(gameObject.GetComponent<Weapon1>());
        Items.Add(new Weapon2("Energy Blast", Shoot));
        Items.Add(new Weapon2("Energy Blast", Shoot));
        Items.Add(new Weapon2("Energy Blast", Shoot));
        Items.Add(new Weapon2("Energy Blast", Shoot));
        Items.Add(new Weapon2("Energy Blast", Shoot));
        Items.Add(new ItemConsumable("Health Potion", 0));

        //Selected = 1;
    }

    // Update is called once per frame
    /// <summary>
    /// Left click = Call Melee Attack's UseItem
    /// Right click = Call Ranged Attack's UseItem
    /// 
    /// Update Selected Attacks.
    /// </summary>
    void Update()
    {
        //if (Input.GetAxis("click") > 0 )
        //{
        //    Items[Selected].UseItem();
        //}
       // Debug.Log("FIRE FIRE: " + SelectedAttack);
        if(Input.GetMouseButtonDown(0) == true)
        {
            Items[SelectedAttack].UseItem();
        }

        Items[0].Update();
        Items[1].Update();
    }

    void Heal()
    {
        Debug.Log("Used");
    }
}
