using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnnemy : MonoBehaviour
{
    Vector3 pushForce; //force of ennemy getting pushed back by player
    Rigidbody rigidbody; // ridgid body
    [HideInInspector]
    public  GameObject player; // player gameobject

    float invTimer; //timer for invicbility frames
  public  int attackRgen; // int value that determines what attack the ennmy will use
    [HideInInspector]
    public  bool isActive; //bool determing if ennemy notces the player
    [HideInInspector]
    public   bool attackOver; //bool for checking if the attack has ended
    [HideInInspector]
    public bool invicable; //bool that checks if ennemy is invicable
    [HideInInspector]
    public Vector3 startPos; //starting position
    [HideInInspector]
    public bool returnHome; //bool checking to see if ennemy needs to return back to its orginal spot

    Vector3 position;
  Vector3 acceleration;
public float maxSpeed;
    Vector3 velocity;
    public float mass;

    public float health;



    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        invCheck();// checking to see if enemy is inviciable
        detectPlayer(); //detecting where player is
        UpdatePosition(); // updating velocity (for knockback)

        if (isActive)
            chooseAttack(); //if enemy is active he will attack the player
        else
            wonder(); //if enemy isn't active he will wonder around
        

    }
    //method for making the ennemy knock backwords
    public void knockBack(Vector3 force, float weight, float damge)
    {
        if (!invicable)
        {
            invTimer = 0; //invcibility timer
            force.y = 0.2f; // setting force of y, so enemy dosn't fly away
            force *= weight; // multiplying wieghts
            health -= damge; // taking damage
            acceleration += force/mass; //creating accl
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f); // displaying invcibility
        }
        //if health drops to 0 baddie dies
        if (health <= 0)
            Destroy(gameObject);

    }

    //method for checking invisnbility frames
    void invCheck()
    {
        if (invTimer < 40 * Time.deltaTime)
        {
            invTimer += 1 * Time.deltaTime;
            invicable = true;
  
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            invicable = false;
            //restart velociy and accl
            acceleration = new Vector3(0, 0, 0);
            velocity = new Vector3(0, 0, 0);

        }
    }
    
    //script for choosing which attack the enemy will do
    public virtual void chooseAttack()
    {
     
    }

    //method for making ennmy wonder around
    public virtual void wonder()
    {

    }

    //check if player is close enough to attack
    public virtual void detectPlayer()
    {
        RaycastHit hit;
        //if player is in sight it will attack
        if(Physics.Raycast(transform.position, (player.transform.position - this.transform.position).normalized, out hit))
        {
            if (hit.transform.gameObject.name == "Player")
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }
           
        }
        //even if the play can be seen, if hes too far the ennmy won't attack
        if((player.transform.position.normalized - transform.position.normalized).magnitude * 1000 > 7)
        {
            isActive = false;
        }
      
    }

    //method to add velociy to ennemy when needed
    void UpdatePosition()
    {
        // Grab the transform's position so the character
        //   is updated every frame

        position = transform.position;

        // Add accel to velocity
        velocity += acceleration;

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Add velocity to position
         position += velocity;

        // Start "fresh" with accel
        acceleration = Vector3.zero;


        // Set the transform
        transform.position = position;


    }

    //method for filling core varables. All ennemies will do this the same way
   public  void init()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        invTimer = 51;
        invicable = false;
        isActive = false;
        startPos = transform.position;
        startPos.y = 0;
    }
}
