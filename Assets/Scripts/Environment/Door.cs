using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
   public bool trap = false;
    bool activeTrap = false;
    public GameObject door;
    public GameObject[] guards;
    bool open = false;
    Vector3 startPos;
    Vector3 trapPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = door.transform.position;

        if (trap)
        {
           
            trapPos = startPos + new Vector3(0, -250 * Time.deltaTime, 0);
            door.GetComponent<BoxCollider>().isTrigger = true;
            foreach (GameObject eni in guards)
            {
                if (eni.activeSelf)
                    eni.SetActive(false);
            }
           
        }
           
    }

    // Update is called once per frame
    void Update()
    {
        if (trap)
            closeDoor();
        else
        {
            if (!open)
            {
                bool allDead = true;
                foreach (GameObject eni in guards)
                {
                    if (eni != null)
                        allDead = false;
                }
                if (allDead)
                    open = true;

            }
            else
                openDoor();
        }
       

    }

    public void openDoor()
    {
        door.GetComponent<BoxCollider>().isTrigger = true;
        door.transform.position = door.transform.position + new Vector3(0, -5 * Time.deltaTime, 0);
        if (door.transform.position.y < startPos.y - (1050 * Time.deltaTime))
        {
            Destroy(door);
            Destroy(gameObject);

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.tag == "Player")
        {
            activeTrap = true;
            foreach (GameObject eni in guards)
            {
                if (!eni.activeSelf)
                    eni.SetActive(true);
            }
        }
    }

    public void closeDoor()
    {
        if (!activeTrap)
            door.transform.position = trapPos;
        else
        {
            door.transform.position = door.transform.position + new Vector3(0, 5 * Time.deltaTime, 0);
            if (door.transform.position.y >= startPos.y)
            {
                trap = false;
                door.GetComponent<BoxCollider>().isTrigger = false;
            }
           
        }
    }
}
