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
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();

        if(this.transform.parent.GetComponent<PlayerMovement>().IsMoving)
        {
            if(walkTimer == 0.0f && walkIndex == 0)
            {
                renderer.sprite = walkLeftSprite;
            }

            if (IncrementTimer(ref walkTimer) >= walkSpeed) 
            {
                walkTimer = 0.0f;
                walkIndex++;
                if (idleSprite != null && walkSprites != null)
                    renderer.sprite = walkSprites[walkIndex % walkSprites.Length]; //Mod % 4 to only get 4 frames regardless of walkIndex value
            }
        }
        else
        {
            walkTimer = 0.0f;
            walkIndex = 0;
            if (idleSprite != null)
                renderer.sprite = idleSprite;
        }
	}

    /// <summary>
    /// Increments a timer with the current change of time since the last frame.
    /// </summary>
    /// <param name="timer">The timer to be incremented.</param>
    /// <returns>The timer's current time.</returns>
    float IncrementTimer(ref float timer)
    {
        timer = (timer < 0.0f) ? 0.0f : timer;
        timer += Time.deltaTime;
        return timer;
    }
}
