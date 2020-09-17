﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public float moveSpeed;
    public Transform target;
    public Camera bigMapCamera, mainCamera;

    private bool bigMapActive;
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
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y,transform.position.z), moveSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!bigMapActive)
            {
                ActivateBigMap();
            }
            else
            {
                DeActivateBigMap();
            }
        }
        
    }
    public void ChangeTraget(Transform newTarget)
    {
        target = newTarget;
    }
    public void ActivateBigMap()
    {
        if (!LevelManager.instance.isPaused)
        {
            bigMapActive = true;
            bigMapCamera.enabled = true;
            mainCamera.enabled = false;
            PlayerController.instance.canMove = false;
            Time.timeScale = 0f;
            UIController.instance.mapDisplay.SetActive(false);
            UIController.instance.bigMapText.SetActive(true);
        }
        
    }
    public void DeActivateBigMap()
    {
        if(!LevelManager.instance.isPaused)
        {
            bigMapActive = false;
            bigMapCamera.enabled = false;
            mainCamera.enabled = true;
            PlayerController.instance.canMove = true;
            Time.timeScale = 1f;
            UIController.instance.mapDisplay.SetActive(true);
            UIController.instance.bigMapText.SetActive(false);
        }
    }
}
