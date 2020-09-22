﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource gameOverMusic, levelMusic, winMusic,bossMusic;
    public AudioSource[] sfx;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGameOver()
    {
        levelMusic.Stop();
        gameOverMusic.Play();
    }
    public void PlayWinMusic()
    {
        levelMusic.Stop();
        winMusic.Play();
    }
    public void PlayBossMusic()
    {
        levelMusic.Stop();
        bossMusic.Play();
        bossMusic.volume = 0.1f;
    }
    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();

    }
}
