using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) // Level 1
        {
            PlayerController.fishCollectedLevel1 = player.fishCollected;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2) // Level 2
        {
            PlayerController.fishCollectedLevel2 = player.fishCollected;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load next scene
    }
}
