using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonMusicPlayer : MonoBehaviour

{
    static bool AudioBegin = false;
    [SerializeField] private AudioSource bgMusic;

    void Awake()
    {
        if (!AudioBegin)
        {
            bgMusic.Play();
            DontDestroyOnLoad(this.gameObject);
            AudioBegin = true;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 2)
            // If level is not level 2...
        {
            bgMusic.Stop();
            AudioBegin = false;
        }
    }
}
