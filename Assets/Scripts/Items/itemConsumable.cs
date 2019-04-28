using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemConsumable : ItemBase
{
    int potionHealth = 0;
    public GameObject player;
    Color invis;

    public ItemConsumable(int health)
    {
        potionHealth = health;
        player = GameObject.Find("Player");
        invis = new Color(255, 255, 255, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void UseItem()
    {
        if (player.GetComponent<PlayerMovement>().health < 100)
        {
            player.GetComponent<Hotbar>().slots[3].GetComponent<Image>().color = invis;
            player.GetComponent<Hotbar>().consumableFilled = false;
            if (player.GetComponent<PlayerMovement>().health + potionHealth > 100)
            {
                player.GetComponent<PlayerMovement>().health = 100;
            }
            else
            {
                player.GetComponent<PlayerMovement>().health += potionHealth;
            }
        }   
    }
}
