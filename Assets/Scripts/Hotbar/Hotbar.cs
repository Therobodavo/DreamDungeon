using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Hotbar Class
 * Coded by David Knolls
 * 
 * Sets up code for input to change hotbar slots and use selected slots
 */
public class Hotbar : MonoBehaviour
{
   //4 Slot references
   private GameObject[] slots;

   //Selector object reference
   private GameObject selector;
   
   //Current slot player has selected
   private int currentSelected = 0;

   //All slots the player has avaliable
   private List<int> unlockedSlots;

   //Mouse timing to limit speed of switching between items
   private float lastMovedMouse = 0;
   private float delay = .2f;

    //Controls for hotkeys
    KeyCode[] numKeyControls;

    //Prefabs used for weapons
    public GameObject Slash;
    public GameObject Shoot;

    //List to hold items
    public List<ItemBase> Items = new List<ItemBase>();

    public bool switchable = true;

    /*
     * Start Method
     * - Initializes variables/references
     */
    void Start()
    {
        //Sets up references to teach hotbar slot
        slots = new GameObject[7];

        slots[0] = GameObject.Find("Weapon1");
        slots[1] = GameObject.Find("Weapon2");
        slots[2] = GameObject.Find("Weapon3");
        slots[3] = GameObject.Find("Consumable");

        //Sets up reference to the selector object
        selector = GameObject.Find("Selector");

        //Create items in hotbar inventory
        Items.Add(new Weapon1(Slash));
        Items.Add(new Weapon2(Shoot));
        Items.Add(new Weapon3(Shoot));
        Items.Add(new ItemConsumable("Health Potion", 0));

        //Sets default unlocked slots
        unlockedSlots = new List<int>();
        unlockedSlots.Add(0);
        unlockedSlots.Add(1);
        unlockedSlots.Add(2);
        unlockedSlots.Add(3);

        //Sets default controls for hotkeys
        numKeyControls = new KeyCode[7];

        numKeyControls[0] = KeyCode.Alpha1;
        numKeyControls[1] = KeyCode.Alpha2;
        numKeyControls[2] = KeyCode.Alpha3;
        numKeyControls[3] = KeyCode.Alpha4;

    }

    /*
     * Update Method
     * - Checks for input to change/trigger slots
     */
    void Update()
    {
        
        //Checks input from the mouse scroll wheel
        SwitchHotBar_Mouse();

        //Checks input from the # hotkeys
        SwitchHotBar_Key();

        if (Input.GetMouseButtonDown(0) == true)
        {
            Items[currentSelected].UseItem();
        }

        //Consumables
        if(Input.GetKeyDown(numKeyControls[3]))
        {
            Items[3].UseItem();
        }
        
    }


    /*
     * isLocked Function
     * - Checks if the given value is an unlocked slot value
     * - Returns true if no value is found, otherwise returns false
     */
    private bool isLocked(int inputVal)
    {
        bool locked = true;

        //Gets first index of given value, is -1 when no value in list is found
        int index = unlockedSlots.IndexOf(inputVal);

        //If no value was found, return false
        if(index != -1)
        {
            locked = false;
        }
        return locked;
    }
    
    /*
     * UpdateSelector Method
     * - Sets position of selector object to current slot being selected
     * - Used only after current slot is changed
     */
    public void UpdateSelector()
    {
        selector.transform.position = slots[currentSelected].transform.position;
    }

    /*
     * SwitchHotBar_Key Method
     * - Checks for Key Input
     * - Used for switching slots by #
     */
    private void SwitchHotBar_Key()
    {
        if(switchable)
        {
            //Checks input for the 1 Key
            if(Input.GetKeyDown(numKeyControls[0]) && !isLocked(0) && currentSelected != 0)
            {
                ((WeaponBase)Items[currentSelected]).OffSelect();
                currentSelected = 0;
                ((WeaponBase)Items[currentSelected]).OnSelect();
                UpdateSelector();
            }
            //Checks input for the 2 Key
            else if(Input.GetKeyDown(numKeyControls[1]) && !isLocked(1) && currentSelected != 1)
            {
                ((WeaponBase)Items[currentSelected]).OffSelect();
                currentSelected = 1;
                ((WeaponBase)Items[currentSelected]).OnSelect();
                UpdateSelector();
            }
            //Checks input for the 3 Key
            else if(Input.GetKeyDown(numKeyControls[2]) && !isLocked(2) && currentSelected != 2)
            {
                ((WeaponBase)Items[currentSelected]).OffSelect();
                currentSelected = 2;
                ((WeaponBase)Items[currentSelected]).OnSelect();
                UpdateSelector();
            }
        }
    }

    /*
     * SwitchHotBar_Mouse Method
     * - Checks for Mouse Scroll Wheel Input
     * - Used for switching slots by scrool wheel
     * - Scroll forwards = move to right
     * - Scroll backwards = move to left
     * - Slot values Wrap
     */
    private void SwitchHotBar_Mouse()
    {
        if(switchable)
        {
            //If there's valid mouse scroll wheel input - based on last input by the mouse
            if(Input.mouseScrollDelta.y != 0 && Time.timeSinceLevelLoad - lastMovedMouse > delay)
            {
                //Sets last input to current time
                lastMovedMouse = Time.timeSinceLevelLoad;

                //Gets value to change index by (-1 to 1)
                int increment = Mathf.Clamp(Mathf.CeilToInt(Input.mouseScrollDelta.y),-1,1);

                //If between 0 and 2
                if((unlockedSlots.IndexOf(currentSelected) + increment) < unlockedSlots.Count - 1 && (unlockedSlots.IndexOf(currentSelected) + increment) >= 0)
                {
                    currentSelected = unlockedSlots[(unlockedSlots.IndexOf(currentSelected) + increment)];
                }
                //If above 2
                else if((unlockedSlots.IndexOf(currentSelected) + increment) >= unlockedSlots.Count - 1)
                {
                    currentSelected = 0;
                }
                //If below 0
                else
                {
                    currentSelected = 2;
                }

                //Update selector position
                UpdateSelector();
            }
        }
    }

    /*
     * GetSelectedSlot Method
     * - Returns current slot being selected, used for other scripts
     * - Return value is 0 - 6
     */
    public int GetSelectedSlot()
    {
        return currentSelected;
    }
}
