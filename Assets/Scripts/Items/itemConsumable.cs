using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConsumable : ItemBase
{
    #region Private Variables
    private int effect;
    private int Max_Uses;
    private int uses;


    /* Quality of item
     private int quality;
     private string qualityName;
    */
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="itemName">Name</param>
    /// <param name="effect">Effect Code</param>
    /// <param name="maxuses">Max Uses</param>
    /// <param name="type">Item Type</param>
    public ItemConsumable(string itemName, int effect, int maxuses = 1) : base(itemName, Type.Consumable)
    {
        this.effect = effect;
        this.Max_Uses = maxuses;
        uses = Max_Uses;
    }

    /// <summary>
    /// Use item: update uses, return effect and uses remaining
    /// </summary>
    /// <returns> int array: (effect, uses)</returns>
    public int[] Use()
    {
        uses--;
        int[] Return = new int[2] { effect, uses };
        return Return;
    }
    /// <summary>
    /// Return item data
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return base.ToString();
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
        throw new System.NotImplementedException();
    }
}
