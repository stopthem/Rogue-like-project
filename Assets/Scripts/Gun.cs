﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletToFire;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;
    public string weaponName;
    public Sprite weaponUI;
    public int itemCost;
    public Sprite gunShopSprite;

    void Update()
    {
        if (PlayerController.instance.canMove && !LevelManager.instance.isPaused)
        {
            Shoot();
        }

    }

    private void Shoot()
    {
        if (shotCounter > 0)
        {
            shotCounter -= Time.deltaTime;
        }
        else
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                AudioManager.instance.PlaySFX(12);
                shotCounter = timeBetweenShots;
            }
        }
    }
}
