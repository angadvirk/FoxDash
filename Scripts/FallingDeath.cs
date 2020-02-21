using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallingDeath : MonoBehaviour
{
    public PlayerController player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player jumps off the scene, restart the level
        if (collision.gameObject.tag == "Player")
        {
            player.hurtSound.Play();
            player.state = PlayerController.State.hurt;
        }

        // If enemy jumps off the scene, destroy it. 
        else if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject); 
        }
    }
}
