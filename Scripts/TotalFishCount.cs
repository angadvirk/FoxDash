using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TotalFishCount : MonoBehaviour
{
    [SerializeField] private Text level1Collected;
    [SerializeField] private Text level2Collected;

    private void Start()
    {
        level1Collected.text = "0";
        level2Collected.text = "0";
    }
    // Update is called once per frame
    void Update()
    {
        level1Collected.text = PlayerController.fishCollectedLevel1.ToString();
        level2Collected.text = PlayerController.fishCollectedLevel2.ToString();
    }
}
