using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HappyMusicPlayer : MonoBehaviour
{
    static bool AudioBegin = false;
    [SerializeField] private AudioSource happyMusic;

    void Awake()
    {
        if (!AudioBegin)
        {
            happyMusic.Play();
            DontDestroyOnLoad(this.gameObject);
            AudioBegin = true;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1) // If current level != level 1
        {
            happyMusic.Stop();
            AudioBegin = false;
        }
    }
}
