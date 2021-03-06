﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float waitToLoad = 4f;
    public string levelToLoad;
    public bool isPaused;
    public int currentCoins;
    public Transform spawnPoint;
    
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        PlayerController.instance.transform.position = spawnPoint.position;
        PlayerController.instance.canMove = true;
        currentCoins = CharachterTracker.instance.currentCoin;
        Time.timeScale = 1f;
        UIController.instance.coinText.text = currentCoins.ToString();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnPause();
        }
    }
    public IEnumerator LevelEnd()
    {
        AudioManager.instance.PlayWinMusic();
        PlayerController.instance.canMove = false;
        UIController.instance.StartFadeToBlack();
        yield return new WaitForSeconds(waitToLoad);
        SceneManager.LoadScene(levelToLoad);
        CharachterTracker.instance.currentCoin = currentCoins;
        CharachterTracker.instance.currentHealth = PlayerHealth.instance.currentHealth;
        CharachterTracker.instance.maxHealth = PlayerHealth.instance.maxHealth;

    }
    public void PauseUnPause()
    {
        if (!isPaused)
        {
            UIController.instance.pauseScreen.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            UIController.instance.pauseScreen.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }
    public void GetCoins(int coinAmount)
    {
        currentCoins += coinAmount;
        UIController.instance.coinText.text = currentCoins.ToString();
    }
    public void SpendCoins(int coinAmount)
    {
        currentCoins -= coinAmount;
        UIController.instance.coinText.text = currentCoins.ToString();
        if (currentCoins < 0)
        {
            currentCoins = 0;
            UIController.instance.coinText.text = currentCoins.ToString();
        }
    }
}
