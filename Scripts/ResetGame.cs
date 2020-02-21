using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            // Resetting all static variables to 0 if game reset. 
            PlayerController.fishCollectedLevel1 = 0;
            PlayerController.fishCollectedLevel2 = 0;

            PlayerController.deathCount = 0;

            PlayerController.frogKillCount = 0;
            PlayerController.eagleKillCount = 0;
            PlayerController.batKillCount = 0;

            SceneManager.LoadScene(0); // Load the main menu
        }

    }
}
