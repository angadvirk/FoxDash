using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start() variables
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;

    // Sounds:
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource collectionSound;
    [SerializeField] public AudioSource hurtSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource climbSound;

    // Finite State Machine
    public enum State { idle, running, jumping, falling, hurt, climbing }
    public State state = State.idle;

    // Ladder Variables
    [HideInInspector] public bool canClimb = false;
    //public bool ladderComplete = false;
    [HideInInspector] public bool ladderBottom = false;
    [HideInInspector] public bool ladderTop = false;
    [HideInInspector] public Ladder ladder;

    // Inspector variables
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5f; // Running speed of player
    [SerializeField] private float jumpForce = 6f; // How high the player jumps
    [SerializeField] private float hurtForceX = 3f; // How far horizontally the player moves when hurt
    [SerializeField] private float hurtForceY = 3f; // How far vertically the player moves when hurt
    [SerializeField] public int fishCollected = 0; // How many fish the player has collected
    [SerializeField] private Text fishCounter;

    // Static Variables: 
    public static int fishCollectedLevel1 = 0;
    public static int fishCollectedLevel2 = 0;

    public static int deathCount = 0;

    public static int frogKillCount = 0;
    public static int eagleKillCount = 0;
    public static int batKillCount = 0;

    // Other
    private Transform childCamera;
    private bool playerDestroyed = false;
    private bool tooSlopey = false;
    private bool climbSoundPlayed = false;

    // Fish collection Trigger Variables

    //private Time timeAtTriggerStart;
    //private Time timeAtTriggerEnd;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        fishCounter.text = "0";
    }

    private void Update()
    {
        if (state != State.hurt)
        {
            PlayerMovement();
        }
        LinearDragChecker(); // Function that increases Linear Drag when on ground and removes it when jumping/falling
        AnimationState();
        if (Mathf.Abs(rb.velocity.y) < Mathf.Epsilon && (state == State.idle || state == State.falling))
        { 
            tooSlopey = false;
        }
        
        if (state != State.climbing)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        // If cannot climb, or not climbing
        if (!canClimb || state != State.climbing)
        {
            rb.gravityScale = 1;
        }
    }
    private void LinearDragChecker()
    {
        if (coll.IsTouchingLayers(groundLayer) && state != State.hurt && !tooSlopey && state != State.jumping)
        {
            rb.drag = 3;
        }
        else
            rb.drag = 0;
    }

    private void PlayerMovement() // Changes player velocity & direction based on buttons being pressed
    {
        float hDirection = Input.GetAxis("Horizontal"); // variable to store left/right movement input
        float vDirection = Input.GetAxis("Vertical");

        if (canClimb && Mathf.Abs(vDirection) > 0.1f && (rb.velocity.y < 4))
        {
            if (vDirection > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, vDirection*speed);
            }
            else if (vDirection < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, vDirection*speed);
            }
            if (!climbSoundPlayed)
            {
                climbSound.Play();
                climbSoundPlayed = true;
            }
            state = State.climbing;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            transform.position = new Vector2(ladder.transform.position.x, rb.position.y);
            rb.gravityScale = 0f;
        }

        // Moving LEFT
        else if (hDirection < 0)
        {
            rb.velocity = new Vector2((speed * hDirection) + 0.5f, rb.velocity.y); // Multiplying player speed w/ hDirection so it gradually increases
            TurnPlayer("left");
        }
        // Moving RIGHT
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2((speed * hDirection) + 0.5f, rb.velocity.y);
            TurnPlayer("right");
        }
        // JUMPING
        if (Input.GetButtonDown("Jump") && (coll.IsTouchingLayers(groundLayer) || canClimb) && !tooSlopey)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jumping;
            jumpSound.Play();
            rb.gravityScale = 1;
            if (rb.velocity.y > 3f)
            // velocity check so that this doesn't just happen when you press jump, but when you actually physically jump up
            {
                tooSlopey = true; // Done so that you can't infinitely jump off a wall by sticking to it and then spamming jump button
            }
            climbSoundPlayed = false;
        }
        // Slow down if directional button not held in air
        if (Mathf.Abs(hDirection) < 0.8f)
        {
            rb.velocity = new Vector2(rb.velocity.x / 1.05f, rb.velocity.y);
        }
        /*
        if (Input.GetKeyDown("down"))
        {
            player.rb.velocity = new Vector2(0, player.rb.velocity.y);
        }
        */
    }

    private void AnimationState() // Handles player state-related stuff
    {
        if (state == State.climbing)
        {
            if (!canClimb)
            {
                state = State.falling;
                rb.gravityScale = 1;
                climbSoundPlayed = false;
            }
            else if (coll.IsTouchingLayers(groundLayer))
            {
                state = State.idle;
                //climbSoundPlayed = false;
            }
            else if (canClimb && Mathf.Abs(Input.GetAxisRaw("Vertical")) < 0.5f)
            {
                tooSlopey = false; // done so player can jump off the ladder.
                rb.velocity = new Vector2(0, 0);
            }
        }
        else if (state == State.jumping)
        {
            if (rb.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if (!coll.IsTouchingLayers(groundLayer) && state != State.falling && state != State.hurt && state != State.climbing)
        {
            state = State.falling;
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(groundLayer) && !tooSlopey)
                state = State.idle;
        }
        else if (state == State.hurt)
        {
            // Comment out the following 'if' block to remove one-hit player death...
            if (state == State.hurt)
            {
                anim.SetTrigger("death");
            }

            /* And uncomment this one:
            if (feetColl.IsTouchingLayers(groundLayer) && Mathf.Abs(player.rb.velocity.x) < 0.1f)
            {
                player.state = PlayerController.State.idle;
            }
            */
        }
        else if (Mathf.Abs(rb.velocity.x) > 0.5f && state != State.climbing) // ie: Player is running
        {
            state = State.running;
        }
        else // ie: Player is idle
        {
            state = State.idle;
        }
        anim.SetInteger("state", (int)state);
    }

    public void TurnPlayer(string direction) // Turns the player to face "direction"
    {
        if (direction == "left")
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if (direction == "right")
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else // Print error to console if anything else is supplied as a parameter
        {
            Debug.Log("Invalid Parameter to TurnPlayer function: " + direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Collectible")
        {
            collectionSound.Play();
            Destroy(collision.gameObject);
            fishCollected += 1;
            fishCounter.text = fishCollected.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        
        if (collision.gameObject.tag == "Enemy")
        {
            foreach (ContactPoint2D hitPos in collision.contacts)
            {
                if (hitPos.normal.y < 0.6) // Enemy hit from the sides
                {
                    // Enemy is to the right of the player
                    if (collision.gameObject.transform.position.x > transform.position.x)
                    {
                        state = State.hurt;
                        rb.velocity = new Vector2(-hurtForceX, rb.velocity.y); // Player moves to left, hence -hurtForceX
                    }
                    // Enemy is to the left of the player
                    else if (collision.gameObject.transform.position.x < transform.position.x)
                    {
                        state = State.hurt;
                        rb.velocity = new Vector2(hurtForceX, rb.velocity.y); // Player moves to right
                    }
                    hurtSound.Play();
                }
                else // No condition for state.falling here because you should be able to kill enemies if you drop down on them, no matter your state
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce / 1.15f); // Cause player to gain upward velocity
                    state = State.jumping; // Activate player's jumping animation
                    enemy.JumpedOn(); // Execute function in Enemy that destroys it.
                }
            } 
        }
        else if (coll.IsTouchingLayers(groundLayer))
        {
            foreach (ContactPoint2D hitPos in collision.contacts)
            {
                if (hitPos.normal.y < 0.6)
                {
                    tooSlopey = true; // If this is true then character can't jump
                    rb.drag = 0;
                }
                else
                {
                    tooSlopey = false;
                }
            }
        }
    }

    public void DestroyPlayer()
    {
        // Increment deathCount:
        deathCount += 1;

        // Detach camera before destroying player. 
        childCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
        childCamera.parent = null;

        Destroy(gameObject);
    }

    private void PlayDeathSound()
    {
        deathSound.Play();
    }
}
