using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryJingle : MonoBehaviour
{
    [SerializeField] private AudioSource victoryJingle;

    void Awake()
    {
        victoryJingle.Play(); // Just play once on awake
    }
}
