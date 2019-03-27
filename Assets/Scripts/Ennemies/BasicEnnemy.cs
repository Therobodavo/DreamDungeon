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
    [HideInInspector]
    public Vector3 attackForce;
    [HideInInspector]
    public float atTimer = 0;//attack timer
    [HideInInspector]
    public bool hide = false;

    Vector3 position;
  Vector3 acceleration;
public float maxSpeed;
    Vector3 velocity;
    public float mass;

    public float health;
 float pushEm = 3000;

    public bool behindPlayer = false; //bool for when ennemy is behind player



    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        if(behindPlayer)
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        else
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

        invCheck();// checking to see if enemy is inviciable
        detectPlayer(); //detecting where player is
        UpdatePosition(); // updating velocity (for knockback)

        if (isActive)
            chooseAttack(); //if enemy is active he will attack the player
        else
            wonder(); //if enemy isn't active he will wonder around

       

        behindPlayer = false;

        if (!attackOver)
        {
            GetComponent<SpriteRenderer>().color = new Color(10f, 1f, 1f, 1f); // displaying invcibility
        }
        

    }
    //method for making the ennemy knock backwords
    public void knockBack(Vector3 force, float weight, float damge)
    {
        if (!invicable && !hide)
        {
            invTimer = 0; //invcibility timer
            force.y = 0.2f; // setting force of y, so enemy dosn't fly away
            force *= weight; // multiplying wieghts
            health -= damge; // taking damage
            acceleration += force/mass; //creating accl
            
        }
        //if health drops to 0 baddie dies
        if (health <= 0)
            Destroy(gameObject);

    }

    //method for checking invisnbility frames
    void invCheck()
    {
        if (invTimer < 100 * Time.deltaTime)
        {
            invTimer += 1 * Time.deltaTime;
            invicable = true;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.8f); // displaying invcibility
        }
        else
        {
           
            invicable = false;
            //restart velociy and accl
            acceleration = new Vector3(0, 0, 0);
            velocity = new Vector3(0, 0, 0);

            if(!behindPlayer) //fill opacity if ennemy is not invicle or behind player
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

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
    private void OnCollisionStay(Collision collision)
    {
            if(collision.transform.tag == "Player")
            {
                Vector3 force = (player.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<PlayerMovement>().knockBack(force, pushEm, 1);
            atTimer = 0;
        }

       
    }


    public void addForce(Vector3 force, float weight)
    {
        force.y = 0.2f; // setting force of y, so enemy dosn't fly away
        force *= weight; // multiplying wieghts
        acceleration += force / mass; //creating accl
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
   public void init()
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
