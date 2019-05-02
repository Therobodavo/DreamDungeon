using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public GameObject destination;
    

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = destination.transform.position;
            other.GetComponent<PlayerMovement>().Push(new Vector3(0, 0, 0), 0, 1);

        }
        else if (other.tag == "Enemy")
        {
            Destroy(other);

        }



    }
}
