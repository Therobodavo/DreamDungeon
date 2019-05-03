using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiderweb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "bullet" || other.gameObject.name == "bullet(Clone)")
        {
        Destroy(transform.parent.gameObject);
        }
    }

}
 
