using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollectible : MonoBehaviour
{
    [HideInInspector] public AudioSource collectionSound;
 
    void Start()
    {
        collectionSound = GetComponent<AudioSource>();
    }

}
