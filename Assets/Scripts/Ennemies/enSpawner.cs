using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
  public GameObject currentEnemy;
    GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        createBaddie();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, player.transform.position - transform.position);
    

        if (Physics.Raycast(ray, out hit))
        if (currentEnemy == null && (player.transform.position - transform.position).magnitude < 70 && hit.transform.gameObject.tag != "Player"  && hit.transform.gameObject.tag != "Trigger")
        createBaddie();

    }

    public void createBaddie()
    {
        enemyPrefab.SetActive(true);
        enemyPrefab.transform.position = transform.position;
        currentEnemy = Instantiate(enemyPrefab);
       
    }
}
