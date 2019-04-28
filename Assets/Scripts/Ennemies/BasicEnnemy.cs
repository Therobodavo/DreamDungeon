using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnnemy : MonoBehaviour
{
    Vector3 pushForce; //force of ennemy getting pushed back by player
    Rigidbody rigidbody; // ridgid body
    [HideInInspector]
    public  GameObject player; // player gameobject

    [HideInInspector]
    float sTimer;//stunn timer
    [HideInInspector]
    float sTimeStop = 400;//stunn timer stop time
    [HideInInspector]
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
    [HideInInspector]
    Vector3 position;
    [HideInInspector]
    float maxSpeed;

    
   
    float mass = 3;

    public float health;

    public float push;
    public int damage;

    public bool behindPlayer = false; //bool for when ennemy is behind player


    public float speedMax = 4.5f;
    public float speedMin = 2f;
    public float circleDistance = 4.5f;

    public enum stateType
    {
        active,
        stunned,
        recoil,
        wounder,
        dead
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


        if (state == stateType.active)
        {
            chooseAttack(); //if enemy is active he will attack the player
            attackTimer(); //controls the attack timer and attack stats   
            detectPlayer(); //detecting where player is

        }     
        else if(state == stateType.wounder)
        {
            wonder(); //if enemy isn't active he will wonder around
            detectPlayer(); //detecting where player is
        }      
     
       
        behindPlayer = false;

        

    }

   public void stunned(float sT)
    {
        sTimeStop = sT;
        state = stateType.stunned;

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





    //method for making the ennemy knock backword
    public void knockBack(Vector3 force,float weight, float dmg, bool isBeam)
    {
        if (state == stateType.stunned)
            weight = weight / 2;
   
        if (inState == inStateType.none)
        {
            force *= weight;
            this.GetComponent<Rigidbody>().AddForce(force);
            health -= dmg;
          
        }
        if (!isBeam)
        {
            invTimer = 0;
        }

        //if health drops to 0 baddie dies
        if (health <= 0)
        {
            state = stateType.dead;
        }
        

    }

    //method for checking invisnbility frames
    void invCheck()
    {
        if (state != stateType.stunned && state != stateType.dead)
        {
            sTimer = 0;
            if (invTimer < 150 * Time.deltaTime)
            {
                invTimer += 1 * Time.deltaTime;
                inState = inStateType.damage;
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.8f); // displaying invcibility
                state = stateType.recoil;

            }
            else
            {
                inState = inStateType.none;
                state = stateType.wounder;
                //restart velociy and accl

                if (!behindPlayer) //fill opacity if ennemy is not invicle or behind player
                    GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
        }
        else
        {
            sTimer += 1 * Time.deltaTime;
            if(state == stateType.stunned)
            {
                inState = inStateType.none;
                if (sTimer >= sTimeStop * Time.deltaTime)
                {
                    
                    state = stateType.wounder;
                }
            }
            else if(state == stateType.dead)
            {
                inState = inStateType.damage;
                if (sTimer >= 200 * Time.deltaTime)
                {
                    Destroy(gameObject);
                }
            }
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
            if (hit.transform.gameObject.tag == "Wall")
                state = stateType.wounder;
            else
                state = stateType.active;
           
        }
        //even if the play can be seen, if hes too far the ennmy won't attack
        if((player.transform.position.normalized - transform.position.normalized).magnitude * 1000 > 4)
        {
            state = stateType.wounder;
        }
      
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Player" && inState != inStateType.damage && state == stateType.active)
        {

            Vector3 force = (player.transform.position - transform.position).normalized;
            force.y = 0;
            atTimer = 0;
            player.GetComponent<PlayerMovement>().Push(force, push * 100, damage);
          
         

        }


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
