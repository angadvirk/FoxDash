using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBat : Enemy
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

}
