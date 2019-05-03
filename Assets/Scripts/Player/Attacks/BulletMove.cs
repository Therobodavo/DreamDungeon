using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public Vector3 foward;
    public float speed;
    public bool isPlayer; //checks if bullet is made by player

    //this script can be made by player or ennemies

    public int damage;
    public float push;

    public float time = 300;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (foward * speed);
        timer += 1 * Time.deltaTime;
        if(timer > time * Time.deltaTime)
            Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.tag);
        if (isPlayer)
        {
            Debug.Log(other.tag);
            if (other.tag == "Enenmy")
            {

                Vector3 force = (other.gameObject.transform.position - transform.position).normalized;

                other.GetComponent<BasicEnnemy>().knockBack(force, 200, 1, false);

            }
            else if (other.tag == "Web")
            {
                Debug.Log(other.tag);
                Destroy(other.gameObject);
            }
        }
        
        else
        {
            ///need to make it so enemies give the info needed for this
            if (other.tag == "Player")
            {
                Vector3 force = (other.gameObject.transform.position - transform.position).normalized;
                other.GetComponent<PlayerMovement>().Push(force, push * 100, damage);
            } 
        }

        if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }


}
