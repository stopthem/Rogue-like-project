using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rigidBody;
    private Vector2 moveInput;
    public Transform gunArm;
    public Camera mainCamera;
    public Animator anim;
    public GameObject bulletToFire;
    public Transform firePoint;
    public float timeBetweenShots;
    public float shotCounter;
    public static PlayerController instance;
    public SpriteRenderer bodySprite;
    private float activeMoveSpeed;
    public float dashSpeed = 8f , dashLength= .5f , dashCooldown= 1f , dashInvincibleAbility = .5f;
    private float dashCdCounter;
    [HideInInspector]public bool canMove = true;
    [HideInInspector]public float dashCounter;
    private void Awake() {
        instance=this;
    }
    void Start()
    {
        mainCamera=Camera.main;
        activeMoveSpeed=movementSpeed;
    }
    void Update()
    {
        if (canMove)
        {
            MovePlayer();
            GunAndBodyFacing();
            Shoot();
        }
        else
        {
            rigidBody.velocity = Vector2.zero;
            anim.SetBool("isMoving",false);
        }
        
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletToFire,firePoint.position,firePoint.rotation);
            AudioManager.instance.PlaySFX(12);
            shotCounter=timeBetweenShots;
        }
        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter<=0)
            {
                Instantiate(bulletToFire,firePoint.position,firePoint.rotation);
                AudioManager.instance.PlaySFX(12);
                shotCounter=timeBetweenShots;
            }
        }
    }

    private void GunAndBodyFacing()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition);

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
}
