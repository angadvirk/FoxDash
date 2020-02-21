using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAI : Enemy
{
    // FSM:
    private enum State { idle, movingLeft, movingRight }
    private State state = State.idle;

    // Left/Right Waypoints for Movement:
    [SerializeField] private float leftCap = 55f;
    [SerializeField] private float rightCap = 65f;

    // Speed:
    [SerializeField] private float xSpeed = 5f; // The speed at which the bat should move along X axis
    [SerializeField] private float ySpeed = 0f; // The speed at which the bat should move along X axis

    // idleEntered Boolean:
    private bool idleEntered = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        StateHandler();
        BatMove();
    }

    private void BatMove()
    { 
        if (state == State.movingRight)
        {
            rb.velocity = new Vector2(xSpeed, 0);
            if (transform.position.x >= rightCap)
            {
                state = State.idle;
            }
            
        }
        else if (state == State.movingLeft)
        {
            rb.velocity = new Vector2(-xSpeed, 0);
            if (transform.position.x <= leftCap) 
            {
                state = State.idle;
            }
        }
    }
    private void StateHandler()
    {
        if (state == State.idle && Mathf.Abs(rb.velocity.x) < 0.15f)
        {
            if (transform.position.x <= leftCap || transform.position.x < rightCap)
            {
                state = State.movingRight;
            }
            else if (transform.position.x >= rightCap || transform.position.x > leftCap)
            {
                state = State.movingLeft;
            }
            anim.SetInteger("state", (int)state);
        }
        // If idle state, stop moving, but not instantly:
        if (state == State.idle)
        {
            anim.SetInteger("state", (int)state);
            if (idleEntered) // We have been in idle state for some time now, stop instantly:
            {
                rb.velocity = new Vector2(0, 0);
                idleEntered = false;
            }
            else // idle state JUST entered, slow down but not stop instantly
            {
                if (rb.velocity.x < 0) // was moving left...
                {
                    rb.velocity = new Vector2((-rb.velocity.x / 1.15f), 0);
                }
                else if (rb.velocity.x > 0)
                {
                    rb.velocity = new Vector2((rb.velocity.x / 1.15f), 0);
                }
                if (Mathf.Abs(rb.velocity.x) <= 0.2f)
                {
                    idleEntered = true;
                }
            }
        }
    }
}
