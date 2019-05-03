using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireHazard2 : MonoBehaviour
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
        Vector3 force = (player.transform.position - transform.position).normalized * 50 * Time.deltaTime;
        force.y = 0;
        GetComponent<Rigidbody>().AddForce(force);
       jumpControl();
    }
    public void jumpControl()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.down);
        Vector3 force = new Vector3(0, 20, 0);
        if (Physics.Raycast(transform.position, fwd,0.4f))
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {

            Vector3 force = (player.transform.position - transform.position).normalized;
            force.y = 0;
            player.GetComponent<PlayerMovement>().Push(force, 1000, 10);
            Destroy(gameObject);


        }
    }
}
