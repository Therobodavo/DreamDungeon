using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    //bool to determine if its a hazard to the player
    public bool hazard;
    public float push = 10;

    // Transforms to act as start and end markers for the journey.
    GameObject endMarker;
    public GameObject destination1;
    public GameObject destination2;

    // Movement speed in units/sec.
    public float speed = 0.005F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
        recalulate();
    }

    // Update is called once per frame
    void Update()
    {
        // Distance moved = time * speed.
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(transform.position, endMarker.transform.position, fracJourney);
        if ((transform.position - endMarker.transform.position).magnitude < 0.05f)
            recalulate();
    }
    public void recalulate()
    {
        if (endMarker == destination1)
            endMarker = destination2;
        else
            endMarker = destination1;

        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(transform.position, endMarker.transform.position);

    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player" && hazard)
        {
            Vector3 force = (other.transform.position - transform.position).normalized;
            force.y = 0;
           other.gameObject.GetComponent<PlayerMovement>().Push(force, push * 100, 3);


        }



    }
}
