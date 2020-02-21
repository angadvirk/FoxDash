using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private enum LadderPart { complete, bottom, top }
    [SerializeField] private LadderPart part = LadderPart.complete;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            switch (part)
            {
                case LadderPart.complete:
                    player.canClimb = true;
                    player.ladder = this;
                    break;
                case LadderPart.top:
                    player.ladderTop = true;
                    break;
                case LadderPart.bottom:
                    player.ladderBottom = true;
                    break;
                default:
                    Debug.Log("default switch case triggered");
                    break;
            }
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            switch (part)
            {
                case LadderPart.complete:
                    player.canClimb = false;
                    break;
                case LadderPart.top:
                    player.ladderTop = false;
                    break;
                case LadderPart.bottom:
                    player.ladderBottom = false;
                    break;
                default:
                    Debug.Log("default switch case triggered");
                    break;
            }
        }
    }
}
