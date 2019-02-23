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
  public  bool isActive; //bool determing if ennemy notces the player
    [HideInInspector]
    public   bool attackOver; //bool for checking if the attack has ended
    [HideInInspector]
    public bool invicable; //bool that checks if ennemy is invicable

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
        invCheck();
        UpdatePosition();
        chooseAttack();
  
    }

    public void knockBack(Vector3 force, float weight)
    {
        if (!invicable)
        {
            invTimer = 0;
            force.y = 0.2f;
            force *= weight;
            acceleration += force/mass;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        }
    
    }

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
            acceleration = new Vector3(0, 0, 0);
            velocity = new Vector3(0, 0, 0);

        }
    }
    
    public virtual void chooseAttack()
    {
     
    }

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
    }
}
