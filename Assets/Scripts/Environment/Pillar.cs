using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    Hotbar bar;
    public int lockVal = 0;
    public Material mat;
    bool unlocked = false;
    // Start is called before the first frame update
    void Start()
    {
        bar = GameObject.Find("Player").GetComponent<Hotbar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bar.keys[lockVal] && !unlocked)
        {
            this.gameObject.GetComponent<Renderer>().material = mat;
            unlocked = true;
        }
    }
}
