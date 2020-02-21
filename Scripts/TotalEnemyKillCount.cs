using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalEnemyKillCount : MonoBehaviour
{
    [SerializeField] private Text frogKillCount;
    [SerializeField] private Text eagleKillCount;
    [SerializeField] private Text batKillCount;

    void Start()
    {
        frogKillCount.text = "0";
        eagleKillCount.text = "0";
        batKillCount.text = "0";
    }

    void Update()
    {
        frogKillCount.text = PlayerController.frogKillCount.ToString();
        eagleKillCount.text = PlayerController.eagleKillCount.ToString();
        batKillCount.text = PlayerController.batKillCount.ToString();

        // All are divided by 2 because for some reason when jumping on an enemy, the code in
        // the onCollisionEnter2D is executed twice. So, the counters get incremented by 2 for each kill instead of one. Dividing by 2 neutralizes that. 
    }
}
