﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rigidBody;
    private Vector2 moveInput;
    public Transform gunArm;
    public Animator anim;
    // public GameObject bulletToFire;
    // public Transform firePoint;
    // public float timeBetweenShots;
    // public float shotCounter;
    public static PlayerController instance;
    public SpriteRenderer bodySprite;
    private float activeMoveSpeed;
    public float dashSpeed = 8f, dashLength = .5f, dashCooldown = 1f, dashInvincibleAbility = .5f;
    private float dashCdCounter;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public float dashCounter;
    public List<Gun> availableGuns = new List<Gun>();
    [HideInInspector] public int currentGun;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        activeMoveSpeed = movementSpeed;
        UIController.instance.currentGun.sprite = availableGuns[currentGun].weaponUI;
        UIController.instance.gunText.text = availableGuns[currentGun].weaponName;
    }
    void Update()
    {
        if (canMove && LevelManager.instance.isPaused == false)
        {
            MovePlayer();
            GunAndBodyFacing();
        }
        else
        {
            rigidBody.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (availableGuns.Count > 0)
            {
                currentGun++;
                if (currentGun >= availableGuns.Count)
                {
                    currentGun = 0;
                }
                SwitchGun();
            }
            else
            {
                Debug.LogError("Player has no guns!");
            }
        }

    }

    private void GunAndBodyFacing()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = CameraController.instance.mainCamera.WorldToScreenPoint(transform.localPosition);

        if (mousePosition.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            gunArm.localScale = Vector3.one;
        }

        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void MovePlayer()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        rigidBody.velocity = moveInput * activeMoveSpeed;
        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCdCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                anim.SetTrigger("dash");
                AudioManager.instance.PlaySFX(8);
                PlayerHealth.instance.MakePlayerInvincible(dashInvincibleAbility);
            }

        }
        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                activeMoveSpeed = movementSpeed;
                dashCdCounter = dashCooldown;
            }
        }
        if (dashCdCounter > 0)
        {
            dashCdCounter -= Time.deltaTime;
        }
    }
    public void SwitchGun()
    {
        foreach (Gun theGun in availableGuns)
        {
            theGun.gameObject.SetActive(false);
        }
        availableGuns[currentGun].gameObject.SetActive(true);
        UIController.instance.currentGun.sprite = availableGuns[currentGun].weaponUI;
        UIController.instance.gunText.text = availableGuns[currentGun].weaponName;
    }
}
