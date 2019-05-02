using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPickup : MonoBehaviour
{
    Hotbar bar;
    public int keyVal = 0;

    // Start is called before the first frame update
    void Start()
    {
        bar = GameObject.Find("Player").GetComponent<Hotbar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(keyVal >= 0 && keyVal <= 2)
        {
            if(!bar.keys[keyVal])
            {
                bar.keySlots[keyVal].GetComponent<Image>().enabled = true;
                bar.keys[keyVal] = true;
                Destroy(this.gameObject);
            }
        }
    }
}
