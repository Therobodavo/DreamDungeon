using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShoot : MonoBehaviour
{
    public GameObject bullet;
  public  float timer;
    public float timeStop;
    public float speed;
    public float force;
    GameObject playerCam;
    // Start is called before the first frame update
    void Start()
    {
        timer = 200;  
        playerCam = GameObject.Find("Player Camera");
    }

    // Update is called once per frame
    void Update()
    {
  
        timer += 1 * Time.deltaTime;
        if (Input.GetAxis("click") > 0 && timer * Time.deltaTime > timeStop * Time.deltaTime)
        {
            bullet.transform.position = transform.position;
            bullet.GetComponent<BulletMove>().isPlayer = true;
            bullet.GetComponent<BulletMove>().foward = (Camera.main.gameObject.transform.forward).normalized;
            bullet.GetComponent<BulletMove>().speed = speed;
            bullet.GetComponent<C_LookAt>().target = Camera.main.gameObject;
            Instantiate(bullet);
            //Make sure bullet is active after Instantiation ~Schrupp 3/4/19
            bullet.SetActive(true);

            timer = 0;
           

        }
    }
}
