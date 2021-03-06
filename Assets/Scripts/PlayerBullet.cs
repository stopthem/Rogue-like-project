﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 7.5f;
    public Rigidbody2D rigidBody;
    public GameObject bulletImpactVFX;
    public int damageToGive = 50;

    void Update()
    {
        rigidBody.velocity = transform.right * bulletSpeed;

    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(bulletImpactVFX, transform.position, transform.rotation);
        AudioManager.instance.PlaySFX(4);
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().TakeDamage(damageToGive);
        }
        if (other.tag == "Boss")
        {
            BossController.instance.TakeDamage(damageToGive);
            Instantiate(BossController.instance.hitEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
