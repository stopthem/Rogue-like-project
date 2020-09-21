using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string levelToLoad;
    public bool isLevel1,isLevel2;
    public bool isLevel1Completed, isLevel2Completed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(LevelManager.instance.LevelEnd());
            if (isLevel1)
            {
                isLevel1Completed = true;
                PlayerPrefs.SetInt("ninja",1);
            }
            if (isLevel2)
            {
                isLevel2Completed = true;
                PlayerPrefs.SetInt("devil",1);
            }
            
        }
    }
}
