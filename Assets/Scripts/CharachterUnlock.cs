using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterUnlock : MonoBehaviour
{
    private bool canUnlock;
    public bool completedLevel1, completedLevel2;
    public static CharachterUnlock instance;
    public bool isNinja;
    public bool isDevil;
    public GameObject message;
    public GameObject charachterToUnlock;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("ninja"))
        {
            completedLevel1 = true;
        }
        if (PlayerPrefs.HasKey("devil"))
        {
            completedLevel2 = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canUnlock)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isNinja && completedLevel1)
                {
                    gameObject.SetActive(false);
                    charachterToUnlock.SetActive(true);
                }
                else
                {
                    AudioManager.instance.PlaySFX(19);
                }
                if (isDevil && completedLevel2)
                {
                    gameObject.SetActive(false);
                    charachterToUnlock.SetActive(true);
                }
                else
                {
                    AudioManager.instance.PlaySFX(19);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canUnlock = true;
            message.SetActive(true);
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canUnlock = false;
            message.SetActive(false);
        }
    }
}
