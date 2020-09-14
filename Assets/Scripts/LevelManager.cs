using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float waitToLoad = 4f;
    public string levelToLoad;
    public bool isPaused;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
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
}
