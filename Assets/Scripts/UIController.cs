using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    public GameObject deathScreen;
    public Image fadeScreen;
    public float fadeTime;
    private bool fadeToBlack, fadeOutBlack;
    public string newGameScene, mainMenuScene;
    public GameObject pauseScreen;
    public TextMeshProUGUI coinText;
    // Start is called before the first frame update
    private void Awake() {
        instance=this;
    }
    void Start()
    {
        fadeOutBlack = true;
        fadeToBlack = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOutBlack == true)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeTime * Time.deltaTime));
            if(fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }
        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeTime * Time.deltaTime));
            if(fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }
    }
    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }
    public void NewGameButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(newGameScene);
    }
    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
    public void Resume()
    {
        LevelManager.instance.PauseUnPause();
    }
}
