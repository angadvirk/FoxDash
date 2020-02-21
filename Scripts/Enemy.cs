using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected BoxCollider2D coll;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected AudioSource deathSound;

    protected virtual void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        deathSound = GetComponent<AudioSource>();
    }

    public void JumpedOn()
    {
        deathSound.Play();
        Destroy(rb);
        Destroy(coll);
        anim.SetTrigger("death");
        //Debug.Log("JumpedOn!");
    }

    private void DestroyEnemy()
    {
        // Increment appropriate kill count on defeating an enemy. 
        if (gameObject.GetComponent<FrogAI>())
        {
            PlayerController.frogKillCount += 1;
        }
        else if (gameObject.GetComponent<EagleAI>())
        {
            PlayerController.eagleKillCount += 1;
        }
        else if (gameObject.GetComponent<BatAI>())
        {
            PlayerController.batKillCount += 1;
        }
        Destroy(gameObject);
    }
}
