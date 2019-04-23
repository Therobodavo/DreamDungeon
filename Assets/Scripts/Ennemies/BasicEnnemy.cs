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
    [HideInInspector]
    public  int attackRgen; // int value that determines what attack the ennmy will use
    [HideInInspector]
    public Vector3 startPos; //starting position
    [HideInInspector]
    public Vector3 attackForce;
    [HideInInspector]
    public float atTimer = 0;//attack timer
    [HideInInspector]
    public float atCheck; //when atTimer passes this number, ennemy starts atacking
    [HideInInspector]
    public int atkChoice = 0;
 
    Vector3 position;
  Vector3 acceleration;
 float maxSpeed;
    [HideInInspector]
    public Vector3 velocity;
    float mass = 3;

    public float health;
    float pushEm = 3000;

    public bool behindPlayer = false; //bool for when ennemy is behind player


    public float speedMax = 4.5f;
    public float speedMin = 2f;
    public float circleDistance = 4.5f;

    public enum stateType
    {
        active,
        stunned,
        returnHome,
        wounder
    }

    public enum inStateType //determines if the ennemy is invicible
    {
        damage, //means the invicbility frames the enemy gets after being hit
        hide, //means the ennemy is hiding or sheilding and can't be damaed
        none
    }
    public enum atkStateType
    {
        atk1,
        atk2,
        atk3,
        atk4,
        atk5,
        atkMove, //circling around player or general movement
        atkOver

      
    }

    public stateType state;
    public inStateType inState;
    public atkStateType atkState;

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
        else if (inState == inStateType.none)
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);


        invCheck();// checking to see if enemy is inviciable
        detectPlayer(); //detecting where player is
        UpdateKnockback(); // updating velocity (for knockback)
        attackTimer(); //controls the attack timer and attack stats
      
        if (state == stateType.active)
            chooseAttack(); //if enemy is active he will attack the player
        else if(state == stateType.wounder || state == stateType.returnHome)
            wonder(); //if enemy isn't active he will wonder around

       
        behindPlayer = false;

        

    }

    void attackTimer()
    {
        atTimer += 1 * Time.deltaTime;

        if (atTimer > atCheck * Time.deltaTime)
        {
            if (atkChoice == 2)
                atkState = atkStateType.atk2;
            else if (atkChoice == 3)
                atkState = atkStateType.atk3;
            else if (atkChoice == 4)
                atkState = atkStateType.atk4;
            else if (atkChoice == 5)
                atkState = atkStateType.atk5;
            else
                atkState = atkStateType.atk1;
        }
        else
            atkState = atkStateType.atkOver;
    }





    //method for making the ennemy knock backwords
    public void knockBack(Vector3 force, float weight, float damge)
    {
        if (inState == inStateType.none)
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
        if (invTimer < 150 * Time.deltaTime)
        {
            invTimer += 1 * Time.deltaTime;
            inState = inStateType.damage;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.8f); // displaying invcibility
            state = stateType.stunned;
        }
        else
        {
            inState = inStateType.none;
            state = stateType.wounder;
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
                state = stateType.active;
            else
                state = stateType.wounder;
           
        }
        //even if the play can be seen, if hes too far the ennmy won't attack
        if((player.transform.position.normalized - transform.position.normalized).magnitude * 1000 > 7)
        {
            state = stateType.wounder;
        }
      
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            Vector3 force = (player.transform.position - transform.position).normalized;
            atTimer = 0;
        }


    }


  

    //method to add velociy to ennemy when needed
    void UpdateKnockback()
    {
        // Grab the transform's position so the character
        //   is updated every frame

        position = transform.position;

        // Add accel to velocity
        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, 0.07f);

      
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
        inState = inStateType.none;
        state = stateType.wounder;
        startPos = transform.position;
        startPos.y = 0;
    }
}
