using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionPickUp : MonoBehaviour
{
    Hotbar bar;
    Color filled;
    // Start is called before the first frame update
    void Start()
    {
        bar = GameObject.Find("Player").GetComponent<Hotbar>();
        filled = new Color(255, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(!bar.consumableFilled)
        {
            bar.slots[3].GetComponent<Image>().color = filled;
            bar.consumableFilled = true;
            Destroy(this.gameObject);
        }
    }
}
