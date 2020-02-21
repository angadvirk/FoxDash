using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalDeathCount : MonoBehaviour
{
    [SerializeField] private Text deathCounter;

    private void Start()
    {
        deathCounter.text = "0";    
    }

    void Update()
    {
        deathCounter.text = PlayerController.deathCount.ToString();
    }
}
