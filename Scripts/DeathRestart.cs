using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathRestart : MonoBehaviour
{
    [SerializeField] private Text deathText;
    [SerializeField] private Image deathTextContainer;

    private void Start()
    {
        deathText.enabled = false;
        deathTextContainer.enabled = false;
    }
    void Update()
    {
        // If the player is dead, restart game if any key is pressed. 
        if (GameObject.Find("Player") == null)
        {
            deathText.enabled = true;
            deathTextContainer.enabled = true;

            // Reset enemy kill counts:
            if (SceneManager.GetActiveScene().buildIndex == 1) // if level 1, reset frog and eagle kill counts
            {
                PlayerController.frogKillCount = 0;
                PlayerController.eagleKillCount = 0;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2) // if level 2, reset bat kill count
            {
                PlayerController.batKillCount = 0;
            }

            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            // TODO: Make some text appear when the player dies telling the user that they can press any key to restart the level.
            // Bonus Points if you can make it fade in.
        }

    }
}
