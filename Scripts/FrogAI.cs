using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAI : Enemy
{
    // Inspector variables
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float jumpDistance;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask groundLayer;

    // Other variables
    bool facingLeft = true;

    // Start() variables
    //private BoxCollider2D coll;
    //private Rigidbody2D rb;
    //private Animator anim;

    // FSM
    public enum State { idle, jumping, falling, dying }
    public State state = State.idle;

    protected override void Start()
    {
        base.Start();    
    }

    void Update()
    {
        AnimationState();
    }

    private void FrogMove()
    {
        if (facingLeft)
        {
            if (transform.localScale.x != 1)
            {
                transform.localScale = new Vector3(1, 1, 1); // Turn sprite left

            }
            if (transform.position.x > leftCap)
            {
                // Test to see if we are on the ground, and if yes, jump.
                if (coll.IsTouchingLayers(groundLayer))
                {
                    state = State.jumping;
                    rb.velocity = new Vector2(-jumpDistance, jumpHeight);
                    // -jumpDistance because we're jumping left
                }
            }
            else
                facingLeft = false;
        }
        else // facing right
        {
            if (transform.localScale.x != -1)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Turn sprite right

            }
            if (transform.position.x < rightCap)
            {
                if (coll.IsTouchingLayers(groundLayer))
                {
                    state = State.jumping;
                    rb.velocity = new Vector2(jumpDistance, jumpHeight);
                }
            }
            else
                facingLeft = true;
        }
    }

    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(groundLayer))
            {
                state = State.idle;
            }
        }
        anim.SetInteger("state", (int)state);
    }
}
