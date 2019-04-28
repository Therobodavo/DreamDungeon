using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireHazard : MonoBehaviour
{
    GameObject player;
    float timerAlive = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 force = (player.transform.position - transform.position).normalized * 300 * Time.deltaTime;
        force.y = 0;
        GetComponent<Rigidbody>().AddForce(force);
    }

     void Update()
    {
        timerAlive += 1 * Time.deltaTime;
        if (timerAlive > 6000 * Time.deltaTime)
            Destroy(gameObject);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {

            Vector3 force = (player.transform.position - transform.position).normalized;
            force.y = 0;
            player.GetComponent<PlayerMovement>().Push(force, 1000, 2);
            Destroy(gameObject);


        }


    }
}
