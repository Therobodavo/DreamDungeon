using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public GameObject Slash;
    public GameObject Shoot;
    public int SelectedMeleeAttack;
    public int SelectedRangeAttack;
    public List<ItemBase> Items = new List<ItemBase>();
    List<Action> ConsumableActions = new List<Action>();
    
    // Start is called before the first frame update
    void Start()
    {
        ConsumableActions.Add(() => Heal());

        Items.Add(new ItemConsumable("Health Potion", 0));
        //Items.Add(new Weapon1("Blue Flame",Slash));
        Items.Add(gameObject.GetComponent<Weapon1>());
        Items.Add(new Weapon2("Energy Blast", Shoot));

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
        if(Input.GetMouseButtonDown(0) == true)
        {
            Items[SelectedMeleeAttack].UseItem();
        }
        if (Input.GetMouseButtonDown(1) == true)
        {
            Items[SelectedRangeAttack].UseItem();
        }

        Items[SelectedMeleeAttack].Update();
        Items[SelectedRangeAttack].Update();
    }

    void Heal()
    {
        Debug.Log("Used");
    }
}
