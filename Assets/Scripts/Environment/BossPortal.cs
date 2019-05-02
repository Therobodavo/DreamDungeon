using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossPortal : MonoBehaviour
{
    public GameObject particle;
    Hotbar bar;
    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        bar = GameObject.Find("Player").GetComponent<Hotbar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bar.keys[0] && bar.keys[1] && bar.keys[2])
        {
            particle.SetActive(true);
            isActive = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Go to boss scene
        if (isActive && other.tag == "Player")
        {
            SceneManager.LoadScene("BossRoom");
        }
    }
}
