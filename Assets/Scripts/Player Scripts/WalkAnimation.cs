using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnimation : MonoBehaviour
{
    #region Public Variables
    [Header("Animation Sprites")]
    public Sprite idleSprite;
    public Sprite walkLeftSprite;
    public Sprite walkRightSprite;
    public Sprite jumpUpSprite;
    public Sprite jumpDownSprite;
    public Sprite damageSprite;
    [Header("Animation Variables")]
    [Range(0.01f, 0.5f)]
    public float walkSpeed;
    #endregion

    #region Private Variables
    private float walkTimer;
    private int walkIndex;
    private Sprite[] walkSprites;
    private WalkingState walkState;
    private enum WalkingState
    {
        Idle  = 0,
        Right = 1,
        Left  = 2,
        Hurt  = 3
    }
    #endregion

    /// <summary>
    /// Sets up variables for walk animation.
    /// </summary>
    void Start()
    {
        walkTimer = 0.0f;
        walkIndex = 0;

        walkSprites = new Sprite[4];
        if(idleSprite != null)
        {
            walkSprites[0] = walkLeftSprite; //Frame 0 ---->
            walkSprites[1] = idleSprite;
            walkSprites[2] = walkRightSprite;
            walkSprites[3] = idleSprite;     //Frame 3 (Frames 1-4)
        }
    }
	
    /// <summary>
    /// Updates the players sprites based on its current state.
    /// </summary>
	void Update()
    {
        //Sprite renderer component reference
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();

        //If the player is NOT in the air...
        if(!this.transform.parent.GetComponent<PlayerMovement>().IsJumping)
        {
            //If the player is moving...
            if (this.transform.parent.GetComponent<PlayerMovement>().IsMoving)
            {
                //If the player just started walking, set its sprite to the first walk animation sprite
                if (walkTimer == 0.0f && walkIndex == 0)
                {
                    renderer.sprite = walkLeftSprite;
                }

                //If the timer goes over the walk speed time
                if (IncrementTimer(ref walkTimer) >= walkSpeed)
                {
                    walkTimer = 0.0f;
                    //Increment the sprite in the animation index
                    walkIndex++;
                    if (idleSprite != null && walkSprites != null)
                        renderer.sprite = walkSprites[walkIndex % walkSprites.Length]; //Mod % 4 to only get 4 frames regardless of walkIndex value
                }
            }
            //If the player is NOT walking...
            else
            {
                walkTimer = 0.0f;
                walkIndex = 0;
                //Set the sprite to idle
                if (idleSprite != null)
                    renderer.sprite = idleSprite;
            }
        }
        else
        {
            //Else make the player look like they're jumping
            if(this.GetComponentInParent<Rigidbody>().velocity.y > 0)
                renderer.sprite = jumpUpSprite;
            else
                renderer.sprite = jumpDownSprite;
        }
	}

    /// <summary>
    /// Increments a timer with the current change of time since the last frame.
    /// </summary>
    /// <param name="timer">The timer to be incremented.</param>
    /// <returns>The timer's current time.</returns>
    float IncrementTimer(ref float timer)
    {
        timer = (timer < 0.0f) ? 0.0f : timer; //If the timer is less than 0.0f, set the timer to 0.0f
        timer += Time.deltaTime;
        return timer;
    }
}
